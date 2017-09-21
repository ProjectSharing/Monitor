using JQCore.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace JQCore.Emits
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：EmitUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/7/12 18:35:07
    /// </summary>
    public static class EmitUtil
    {
        #region 根据类型获取将object转为ParamList的方法

        /// <summary>
        /// 根据类型获取将object转为ParamList的方法
        /// </summary>
        /// <typeparam name="TParam">DbParameter类型</typeparam>
        /// <param name="objType">object的类型</param>
        /// <returns>将object转为ParamList的方法</returns>
        public static DynamicMethod CreateObjectToParamListMethod<TParam>(Type objType)
        {
            var listType = typeof(List<TParam>);
            var instanceType = typeof(TParam);
            //三个参数，第一个参数是object，第二个参数是参数符号（@,:,?），参数前缀
            DynamicMethod convertMethod = new DynamicMethod("ConvertObjectToParamList" + objType.Name, listType, new Type[] { TypeUtil._ObjectType, TypeUtil._StringType, TypeUtil._StringType }, true);

            ILGenerator il = convertMethod.GetILGenerator();
            LocalBuilder listBuilder = il.DeclareLocal(listType);//List<T> 存储对象
            il.Emit(OpCodes.Newobj, listType.GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Stloc_S, listBuilder);

            LocalBuilder objBuilder = il.DeclareLocal(objType);
            il.Emit(OpCodes.Ldarg_0);
            if (objType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, objType);
            }
            else
            {
                il.Emit(OpCodes.Castclass, objType);
            }
            il.Emit(OpCodes.Stloc, objBuilder);

            LocalBuilder dbNullBuilder = il.DeclareLocal(TypeUtil._ObjectType);
            il.Emit(OpCodes.Ldsfld, typeof(DBNull).GetField("Value"));
            il.Emit(OpCodes.Stloc, dbNullBuilder);

            List<PropertyInfo> properties = PropertyUtil.GetTypeProperties(objType);
            foreach (var item in properties)
            {
                Label setDbNullLable = il.DefineLabel();//设置dbnull
                Label setParamLable = il.DefineLabel();//给param赋值
                Label exitLable = il.DefineLabel();//退出

                LocalBuilder paramNameBuilder = il.DeclareLocal(TypeUtil._StringType);//参数变量名字
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldarg_2);
                il.Emit(OpCodes.Ldstr, item.Name);
                il.Emit(OpCodes.Call, TypeUtil._StringType.GetMethod("Concat", new Type[] { TypeUtil._StringType, TypeUtil._StringType, TypeUtil._StringType }));
                il.Emit(OpCodes.Stloc, paramNameBuilder);

                LocalBuilder instanceBuilder = il.DeclareLocal(instanceType);// T 存储对象
                il.Emit(OpCodes.Newobj, instanceType.GetConstructor(Type.EmptyTypes));
                il.Emit(OpCodes.Stloc_S, instanceBuilder);

                LocalBuilder dbTypeBuilder = il.DeclareLocal(TypeUtil._DbTypeType);
                Ldc(il, (int)TypeUtil.Type2DbType(item.PropertyType));//数据库类型
                il.Emit(OpCodes.Stloc, dbTypeBuilder);

                //设置dbType
                il.Emit(OpCodes.Ldloc, instanceBuilder);
                il.Emit(OpCodes.Ldloc, dbTypeBuilder);
                il.Emit(OpCodes.Callvirt, instanceType.GetMethod("set_DbType", new Type[] { TypeUtil._DbTypeType }));

                //设置ParameterName
                il.Emit(OpCodes.Ldloc, instanceBuilder);
                il.Emit(OpCodes.Ldloc, paramNameBuilder);
                il.Emit(OpCodes.Callvirt, instanceType.GetMethod("set_ParameterName", new Type[] { TypeUtil._StringType }));
                il.Emit(OpCodes.Nop);

                LocalBuilder dbValueBuilder = il.DeclareLocal(TypeUtil._ObjectType);
                if (item.GetGetMethod() == null)
                {
                    il.Emit(OpCodes.Br_S, setDbNullLable);
                }
                else
                {
                    il.Emit(OpCodes.Ldloc, objBuilder);
                    if (item.DeclaringType.IsValueType && Nullable.GetUnderlyingType(item.DeclaringType) == null)
                    {
                        var t = il.DeclareLocal(item.DeclaringType);
                        il.Emit(OpCodes.Stloc, t);
                        il.Emit(OpCodes.Ldloca_S, t);
                    }
                    if (item.DeclaringType.IsValueType)
                    {
                        il.Emit(OpCodes.Call, item.GetGetMethod());
                    }
                    else
                    {
                        il.Emit(OpCodes.Callvirt, item.GetGetMethod());
                    }

                    if (item.PropertyType.IsValueType)
                    {
                        il.Emit(OpCodes.Box, item.PropertyType);
                        if (item.PropertyType.IsEnum)
                        {
                            il.Emit(OpCodes.Ldstr, "d");
                            il.Emit(OpCodes.Call, typeof(Enum).GetMethod("ToString", new Type[] { TypeUtil._StringType }));
                        }
                    }
                    il.Emit(OpCodes.Stloc, dbValueBuilder);
                    il.Emit(OpCodes.Ldloc, dbValueBuilder);
                    il.Emit(OpCodes.Ldnull);
                    il.Emit(OpCodes.Beq_S, setDbNullLable);
                    il.Emit(OpCodes.Br_S, setParamLable);
                }
                il.MarkLabel(setDbNullLable);
                il.Emit(OpCodes.Ldloc, dbNullBuilder);
                il.Emit(OpCodes.Stloc, dbValueBuilder);
                il.Emit(OpCodes.Br_S, setParamLable);

                il.MarkLabel(setParamLable);
                il.Emit(OpCodes.Ldloc, instanceBuilder);
                il.Emit(OpCodes.Ldloc, dbValueBuilder);
                il.Emit(OpCodes.Callvirt, instanceType.GetMethod("set_Value", new Type[] { TypeUtil._ObjectType }));
                il.Emit(OpCodes.Ldloc, listBuilder);
                il.Emit(OpCodes.Ldloc, instanceBuilder);
                il.Emit(OpCodes.Call, listType.GetMethod("Add"));
            }
            il.Emit(OpCodes.Ldloc, listBuilder);
            il.Emit(OpCodes.Ret);
            return convertMethod;
        }

        #endregion 根据类型获取将object转为ParamList的方法

        /// <summary>
        /// 设置整数值的emit代码
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        private static void Ldc(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;

                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;

                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;

                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;

                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;

                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;

                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;

                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;

                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;

                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            if (value > -129 && value < 128)
                il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
            else
                il.Emit(OpCodes.Ldc_I4, value);
        }
    }
}