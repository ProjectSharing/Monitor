namespace JQCore.Result
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：AjaxUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：ajax工具类
    /// 创建标识：yjq 2017/9/11 21:07:02
    /// </summary>
    public static class AjaxUtil
    {
        /// <summary>
        /// 将操作结果转为AJAX结果
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="operateResult">操作结果</param>
        /// <returns>AJAX结果</returns>
        public static AjaxResult ToAjaxResult<T>(this OperateResult<T> operateResult)
        {
            return new AjaxResult(ToAjaxState(operateResult), operateResult.Message, operateResult.Value);
        }

        /// <summary>
        /// 将操作结果转为AJAX结果
        /// </summary>
        /// <param name="operateResult">操作结果</param>
        /// <returns>AJAX结果</returns>
        public static AjaxResult ToAjaxResult(this OperateResult operateResult)
        {
            return new AjaxResult(ToAjaxState(operateResult), operateResult.Message);
        }

        /// <summary>
        /// 将操作结果状态转为Ajax状态
        /// </summary>
        /// <param name="operateResult">操作结果状态</param>
        /// <returns>Ajax状态</returns>
        public static AjaxState ToAjaxState(this OperateResult operateResult)
        {
            if (operateResult == null) return AjaxState.Failed;
            switch (operateResult.State)
            {
                case OperateState.Success:
                    return AjaxState.Success;

                case OperateState.ParamError:
                    return AjaxState.Failed;

                case OperateState.Failed:
                    return AjaxState.Failed;

                default:
                    return AjaxState.Failed;
            }
        }
    }
}