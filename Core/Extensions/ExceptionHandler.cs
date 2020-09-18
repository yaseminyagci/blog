using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ExceptionHandler:Attribute, IInterceptor
    {
        public int Priority { get; set; }

        public void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                //OnException(invocation, e);
                throw;
            }
        }

        //protected void OnException(IInvocation invocation, System.Exception e)
        //{
        //    LogDetailWithException logDetailWithException = GetLogDetail(invocation);
        //    logDetailWithException.ExceptionMessage = e.Message;
            
        //}
        //private LogDetailWithException GetLogDetail(IInvocation invocation)
        //{
        //    var logParameters = new List<LogParameter>();
        //    for (int i = 0; i < invocation.Arguments.Length; i++)
        //    {
        //        logParameters.Add(new LogParameter
        //        {
        //            Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
        //            Value = invocation.Arguments[i],
        //            Type = invocation.Arguments[i].GetType().Name
        //        });
        //    }

        //    var logDetailsWithException = new LogDetailWithException
        //    {
        //        MethodName = invocation.Method.Name,
        //        LogParameters = logParameters
        //    };
        //    return logDetailsWithException;
        //}
    }
}
