using JQCore.Expressions;
using JQCore.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JQCore.DataAccess.Expressions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BaseParamExpressionVisitor.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/12 15:56:34
    /// </summary>
    internal class BaseParamExpressionVisitor<T> : DataExpressionVistor where T : class
    {
        private List<T> _paramList = new List<T>();
        private readonly string _prefix = null;

        public BaseParamExpressionVisitor(string prefix = "")
        {
            _prefix = prefix;
        }

        public ValueTuple<string, List<T>> GetSqlWhere(Expression exp)
        {
            DataMember member = Resolve(exp);
            return ValueTuple.Create(member.Name, _paramList);
        }

        /// <summary>
        /// 拼接左边和右边参数的方法
        /// </summary>
        /// <param name="left"></param>
        /// <param name="expType"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        protected virtual DataMember AppendMember(DataMember left, ExpressionType expType, DataMember right)
        {
            if (left == null && right == null)
            {
                return null;
            }
            if (expType == ExpressionType.Coalesce)
            {
                return AppendMemberByCoalesce(left, expType, right);
            }
            else
            {
                var dataMembers = DataMemberUtil.GetKeyMember(left, right);
                var memberKey = ToParamMember(dataMembers.Item1);
                var memberValue = ToParamMember(dataMembers.Item2);
                AddParam(memberKey, memberValue);
                StringBuilder nameBuilder = new StringBuilder();
                nameBuilder.AppendFormat("({0} {1} {2})", memberKey?.Name, ResolveExpressionType(expType), memberValue?.Name);
                return new DataMember(nameBuilder.ToString(), null, DataMemberType.None);
            }
        }

        protected virtual DataMember AppendMemberByCoalesce(DataMember left, ExpressionType expType, DataMember right)
        {
            var dataMembers = DataMemberUtil.GetKeyMember(left, right);
            var memberKey = ToParamMember(dataMembers.Item1);
            var memberValue = ToParamMember(dataMembers.Item2);
            AddParam(memberKey, memberValue);
            StringBuilder nameBuilder = new StringBuilder();
            nameBuilder.AppendFormat("ISNULL({0},{1})", memberKey.Name, memberValue.Name);
            return new DataMember(nameBuilder.ToString(), null, DataMemberType.None);
        }

        protected virtual void AddParam(params DataMember[] member)
        {
            if (member != null)
            {
                foreach (var item in member)
                {
                    if (item.IsValue())
                    {
                        if (item.Value is T)
                        {
                            _paramList.Add(item.Value as T);
                        }
                    }
                }
            }
        }

        protected virtual DataMember ToParamMember(DataMember member)
        {
            if (member == null || member.MemberType != DataMemberType.Value)
            {
                return member;
            }
            var sqlParameter = GetParameter(member.Value);
            member.Name = sqlParameter.Item1;
            member.Value = sqlParameter.Item2;
            return member;
        }

        #region ExpressionVisitor
        protected override DataMember VisitBinary(BinaryExpression exp)
        {
            if (exp.NodeType == ExpressionType.ArrayIndex)
            {
                return Resolve(exp.ToConstantExpression());
            }
            var expressionLeft = exp.Left;
            var expressionRight = exp.Right;
            if (exp.NodeType == ExpressionType.Coalesce)//??的方法解析
            {
                bool leftIsConstatnt = expressionLeft.IsConstantExpression();
                bool rightIsConstatnt = expressionRight.IsConstantExpression();
                if (leftIsConstatnt && rightIsConstatnt)//如果都是常量则直接返回
                {
                    return Resolve(exp.ToConstantExpression());
                }
            }
            var leftSqlMember = Resolve(expressionLeft);
            var rightSqlMember = Resolve(expressionRight);
            return AppendMember(leftSqlMember, exp.NodeType, rightSqlMember);
        }

        protected override DataMember VisitConditional(ConditionalExpression exp)
        {
            try
            {
                var value = Expression.Lambda(exp.Test).Compile().DynamicInvoke();
                if (value != null && (bool)value == true)
                {
                    return Resolve(exp.IfTrue);
                }
                return Resolve(exp.IfFalse);
            }
            catch
            {
                var conditionSqlMember = Resolve(exp.Test);
                var ifTrueSqlMember = ToParamMember(Resolve(exp.IfTrue));
                var ifFalseSqlMember = ToParamMember(Resolve(exp.IfFalse));
                return ResolveConditionalMethod(conditionSqlMember, ifTrueSqlMember, ifFalseSqlMember);
            }
        }

        protected override DataMember VisitConstant(ConstantExpression exp)
        {
            object value = exp.Value;
            if (exp.Type == typeof(bool))
            {
                if (exp.Value != null)
                {
                    if ((bool)exp.Value)
                    {
                        value = "1";
                    }
                    else
                    {
                        value = "0";
                    }
                }
            }
            return new DataMember(string.Empty, value, DataMemberType.Value);
        }

        protected override DataMember VisitInvocation(InvocationExpression exp)
        {
            throw new NotImplementedException();
        }

        protected override DataMember VisitLambda(LambdaExpression exp)
        {
            throw new NotImplementedException();
        }

        protected override DataMember VisitListInit(ListInitExpression exp)
        {
            throw new NotImplementedException();
        }

        protected override DataMember VisitMemberAccess(MemberExpression exp)
        {
            if (exp.IsNotParameterExpression())
            {
                var memberValue = Expression.Lambda(exp).Compile().DynamicInvoke();
                var memberValueType = memberValue.GetType();
                if (memberValueType.IsArrayOrCollection())
                {
                    var tupleList = new List<ValueTuple<string, T>>();
                    foreach (var item in (IEnumerable)memberValue)
                    {
                        tupleList.Add(GetParameter(item));
                    }
                    var memberName = string.Concat("(", string.Join(",", tupleList.Select(m => m.Item1)), ")");
                    _paramList.AddRange(tupleList.Select(m => m.Item2));
                    return new DataMember(memberName, memberValue, DataMemberType.None);
                }
                else
                {
                    return Resolve(Expression.Constant(memberValue));
                }
            }
            else
            {
                if (exp.Member.Name == "Length")
                {
                    DataMember dataMember = Resolve(exp.Expression);
                    var memberName = $"LEN({dataMember.Name})";
                    return new DataMember(memberName, null, DataMemberType.Key);
                }
                if (exp.Member.Name == "Count")
                {
                    throw new NotSupportedException("Count");
                }
                else
                {
                    var memberName = string.Concat(_prefix, exp.Member.Name);
                    return new DataMember(memberName, null, DataMemberType.Key);
                }
            }
        }

        protected override DataMember VisitMemberInit(MemberInitExpression exp)
        {
            throw new NotImplementedException();
        }

        protected override DataMember VisitMethodCall(MethodCallExpression exp)
        {
            try
            {
                return Resolve(exp.ToConstantExpression());
            }
            catch
            {
                string methodName = exp.Method.Name;
                switch (methodName)
                {
                    case "StartsWith":
                        return ResolveMethodStartsWith(exp);

                    case "Contains":
                        return ResolveMethodContains(exp);

                    case "EndsWith":
                        return ResolveMethodEndsWith(exp);

                    case "TrimEnd":
                        return ResolveMethodTrimEnd(exp);

                    case "TrimStart":
                        return ResolveMethodTrimStart(exp);

                    case "Trim":
                        return ResolveMethodTrim(exp);

                    default:
                        throw new NotSupportedException(methodName);
                }
            }
        }

        protected override DataMember VisitNew(NewExpression exp)
        {
            throw new NotImplementedException();
        }

        protected override DataMember VisitNewArray(NewArrayExpression exp)
        {
            throw new NotImplementedException();
        }

        protected override DataMember VisitParameter(ParameterExpression exp)
        {
            throw new NotImplementedException();
        }

        protected override DataMember VisitTypeIs(TypeBinaryExpression exp)
        {
            throw new NotImplementedException();
        }

        protected override DataMember VisitUnary(UnaryExpression exp)
        {
            var operandExp = exp.Operand;
            if (operandExp.NodeType == ExpressionType.MemberAccess)
            {
                return Resolve(operandExp);
            }
            var memberValue = Expression.Lambda(exp).Compile().DynamicInvoke();
            return Resolve(Expression.Constant(memberValue));
        } 
        #endregion

        #region 需要重写的一些方法

        protected virtual DataMember ResolveMethodStartsWith(MethodCallExpression exp)
        {
            StringBuilder nameBuilder = new StringBuilder();
            if (exp.Object != null)
            {
                DataMember dataMember = ToParamMember(Resolve(exp.Object));
                nameBuilder.Append(dataMember.Name);
                AddParam(dataMember);
            }
            nameBuilder.Append(" LIKE '%'+ ");
            if (exp.Arguments != null)
            {
                foreach (var item in exp.Arguments)
                {
                    DataMember dataMember = ToParamMember(Resolve(item));
                    nameBuilder.Append(dataMember.Name);
                    AddParam(dataMember);
                }
            }
            return new DataMember(nameBuilder.ToString(), null, DataMemberType.None);
        }

        protected virtual DataMember ResolveMethodEndsWith(MethodCallExpression exp)
        {
            StringBuilder nameBuilder = new StringBuilder();
            if (exp.Object != null)
            {
                DataMember dataMember = ToParamMember(Resolve(exp.Object));
                nameBuilder.Append(dataMember.Name);
                AddParam(dataMember);
            }
            nameBuilder.Append(" LIKE  ");
            if (exp.Arguments != null)
            {
                foreach (var item in exp.Arguments)
                {
                    DataMember dataMember = ToParamMember(Resolve(item));
                    nameBuilder.Append(dataMember.Name);
                    AddParam(dataMember);
                }
            }
            nameBuilder.Append(" +'%' ");
            return new DataMember(nameBuilder.ToString(), null, DataMemberType.None);
        }

        protected virtual DataMember ResolveMethodContains(MethodCallExpression exp)
        {
            var rightExpType = exp.Arguments[0].NodeType;
            Expression leftExp = exp.Object ?? exp.Arguments[1];
            Expression rightExp = null;
            if (new ExpressionType[] { ExpressionType.MemberAccess, ExpressionType.Constant }.Contains(rightExpType))
            {
                rightExp = exp.Arguments[0];
            }
            else
            {
                rightExp = Expression.Lambda(exp.Arguments[0]);
            }
            StringBuilder nameBuilder = new StringBuilder();
            DataMember leftSqlMember = ToParamMember(Resolve(leftExp));
            DataMember rightSqlMEember = ToParamMember(Resolve(rightExp));
            if (leftSqlMember.IsArrayOrCollection())//是个数组或者集合
            {
                nameBuilder.AppendFormat("{0} IN {1}", rightSqlMEember.Name, leftSqlMember.Name);
            }
            else if (rightSqlMEember.IsArrayOrCollection())
            {
                nameBuilder.AppendFormat("{0} IN {1}", leftSqlMember.Name, rightSqlMEember.Name);
            }
            else
            {
                nameBuilder.AppendFormat(" {0} LIKE '%'+{1}+'%' ", leftSqlMember.Name, rightSqlMEember.Name);
            }
            AddParam(leftSqlMember, rightSqlMEember);
            return new DataMember(nameBuilder.ToString(), null, DataMemberType.None);
        }

        protected virtual DataMember ResolveMethodTrimEnd(MethodCallExpression exp)
        {
            StringBuilder nameBuilder = new StringBuilder("RTRIM(");
            if (exp.Object != null)
            {
                DataMember dataMember = ToParamMember(Resolve(exp.Object));
                nameBuilder.Append(dataMember.Name);
                AddParam(dataMember);
            }
            nameBuilder.Append(")");
            return new DataMember(nameBuilder.ToString(), null, DataMemberType.Key);
        }

        protected virtual DataMember ResolveMethodTrimStart(MethodCallExpression exp)
        {
            StringBuilder nameBuilder = new StringBuilder("LTRIM(");
            if (exp.Object != null)
            {
                DataMember dataMember = ToParamMember(Resolve(exp.Object));
                nameBuilder.Append(dataMember.Name);
                AddParam(dataMember);
            }
            nameBuilder.Append("))");
            return new DataMember(nameBuilder.ToString(), null, DataMemberType.Key);
        }

        protected virtual DataMember ResolveMethodTrim(MethodCallExpression exp)
        {
            StringBuilder nameBuilder = new StringBuilder("RTRIM(LTRIM(");
            List<T> paramList = new List<T>();
            if (exp.Object != null)
            {
                DataMember dataMember = ToParamMember(Resolve(exp.Object));
                nameBuilder.Append(dataMember.Name);
                AddParam(dataMember);
            }
            nameBuilder.Append(")");
            return new DataMember(nameBuilder.ToString(), null, DataMemberType.Key);
        }

        /// <summary>
        /// 处理条件方法
        /// </summary>
        /// <param name="conditionMember"></param>
        /// <param name="ifTrueMember"></param>
        /// <param name="ifFalseMember"></param>
        /// <returns></returns>
        protected virtual DataMember ResolveConditionalMethod(DataMember conditionMember, DataMember ifTrueMember, DataMember ifFalseMember)
        {
            if (conditionMember != null)
            {
                StringBuilder nameBuilder = new StringBuilder();
                nameBuilder.Append("(CASE WHEN ");
                if (conditionMember.MemberType == DataMemberType.None)
                {
                    nameBuilder.Append(conditionMember?.Name);
                }
                else
                {
                    nameBuilder.AppendFormat("{0}=1", conditionMember?.Name);
                }
                nameBuilder.AppendFormat(" THEN {0} ELSE {1})", ifTrueMember?.Name, ifFalseMember?.Name);
                AddParam(conditionMember, ifTrueMember, ifFalseMember);
                return new DataMember(nameBuilder.ToString(), null, DataMemberType.Key);
            }
            return null;
        }

        /// <summary>
        /// 根据值获取参数的方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual ValueTuple<string, T> GetParameter(object value)
        {
            throw new NotImplementedException();
        }

        protected virtual string ResolveExpressionType(ExpressionType expressionType)
        {
            string operateSign = string.Empty;
            switch (expressionType)
            {
                case ExpressionType.Add:
                case ExpressionType.AddAssign:
                case ExpressionType.AddAssignChecked:
                case ExpressionType.AddChecked:
                    operateSign = "+";
                    break;

                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.AndAssign:
                    operateSign = "AND";
                    break;

                case ExpressionType.LessThan:
                    operateSign = "<";
                    break;

                case ExpressionType.LessThanOrEqual:
                    operateSign = "<=";
                    break;

                case ExpressionType.Equal:
                    operateSign = "=";
                    break;

                case ExpressionType.NotEqual:
                    operateSign = "!=";
                    break;

                case ExpressionType.Not:
                    operateSign = "NOT";
                    break;

                case ExpressionType.GreaterThan:
                    operateSign = ">";
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    operateSign = ">=";
                    break;

                case ExpressionType.OrElse:
                    operateSign = "OR";
                    break;
            }
            return operateSign;
        }

        #endregion 需要重写的一些方法
    }
}