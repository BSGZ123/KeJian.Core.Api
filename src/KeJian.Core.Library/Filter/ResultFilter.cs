using System.Diagnostics;
using KeJian.Core.Library.Attributes;
using KeJian.Core.Library.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KeJian.Core.Library.Filter
{
    public class ResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var notWarpApiResultAttribute =
                (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo?.IsDefined(
                    typeof(NotWarpApiResultAttribute), false) ?? false;
            if (notWarpApiResultAttribute)
                return;
            if (context.Result is not ObjectResult or|| or.Value is ApiResult)
            {
                if (context.Result is EmptyResult)
                {
                    var emptyResult = new ApiResult(0, string.Empty);
                    if (Activity.Current != null)
                        emptyResult.OperationId = Activity.Current.Id;
                    emptyResult.SetSuccess();
                    context.Result = new OkObjectResult(emptyResult);
                    return;
                }

                return;
            }

            var response = new ApiResult<object>(or.StatusCode ?? 0, string.Empty, or.Value);
            response.SetSuccess();
            if (Activity.Current != null)
                response.OperationId = Activity.Current.Id;
            or.DeclaredType = typeof(ApiResult<>);
            or.Value = response;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            //Nothing
        }
    }
}