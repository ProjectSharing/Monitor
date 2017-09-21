using System;
using System.Linq.Expressions;

namespace JQCore.DataAccess.Expressions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DataExpressionVistor.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/12 15:59:17
    /// </summary>
    internal abstract class DataExpressionVistor
    {
        protected DataExpressionVistor()
        {
        }

        protected abstract DataMember VisitUnary(UnaryExpression exp);

        protected abstract DataMember VisitBinary(BinaryExpression exp);

        protected abstract DataMember VisitTypeIs(TypeBinaryExpression exp);

        protected abstract DataMember VisitConditional(ConditionalExpression exp);

        protected abstract DataMember VisitConstant(ConstantExpression exp);

        protected abstract DataMember VisitParameter(ParameterExpression exp);

        protected abstract DataMember VisitMemberAccess(MemberExpression exp);

        protected abstract DataMember VisitMethodCall(MethodCallExpression exp);

        protected abstract DataMember VisitLambda(LambdaExpression exp);

        protected abstract DataMember VisitNew(NewExpression exp);

        protected abstract DataMember VisitNewArray(NewArrayExpression exp);

        protected abstract DataMember VisitInvocation(InvocationExpression exp);

        protected abstract DataMember VisitMemberInit(MemberInitExpression exp);

        protected abstract DataMember VisitListInit(ListInitExpression exp);

        protected DataMember Resolve(Expression exp)
        {
            DataMember dataMember = null;
            if (exp == null)
                return dataMember;

            switch (exp.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    dataMember = this.VisitUnary((UnaryExpression)exp);
                    break;

                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    dataMember = this.VisitBinary((BinaryExpression)exp);
                    break;

                case ExpressionType.TypeIs:
                    dataMember = this.VisitTypeIs((TypeBinaryExpression)exp);
                    break;

                case ExpressionType.Conditional:
                    dataMember = this.VisitConditional((ConditionalExpression)exp);
                    break;

                case ExpressionType.Constant:
                    dataMember = this.VisitConstant((ConstantExpression)exp);
                    break;

                case ExpressionType.Parameter:
                    dataMember = this.VisitParameter((ParameterExpression)exp);
                    break;

                case ExpressionType.MemberAccess:
                    dataMember = VisitMemberAccess((MemberExpression)exp);
                    break;

                case ExpressionType.Call:
                    dataMember = VisitMethodCall((MethodCallExpression)exp);
                    break;

                case ExpressionType.Lambda:
                    dataMember = this.VisitLambda((LambdaExpression)exp);
                    break;

                case ExpressionType.New:
                    dataMember = this.VisitNew((NewExpression)exp);
                    break;

                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    dataMember = this.VisitNewArray((NewArrayExpression)exp);
                    break;

                case ExpressionType.Invoke:
                    dataMember = this.VisitInvocation((InvocationExpression)exp);
                    break;

                case ExpressionType.MemberInit:
                    dataMember = this.VisitMemberInit((MemberInitExpression)exp);
                    break;

                case ExpressionType.ListInit:
                    dataMember = this.VisitListInit((ListInitExpression)exp);
                    break;

                default:
                    throw new NotSupportedException(exp.NodeType.ToString());
            }
            return dataMember;
        }
    }
}