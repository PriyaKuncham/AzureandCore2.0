using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAppliccation.Models
{
    public class ActionFilters : ActionFilterAttribute
    {
        //IConfiguration _iconfiguration;
        //public ActionFilters(IConfiguration IConfiguration)
        //{
        //    _iconfiguration = IConfiguration;
        //}
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.Write("OnActionExecuting - {0}", context.RouteData.Values);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //string strValue = _iconfiguration.GetSection("AppSettings").GetSection("ida:ClientId").Value;

            Console.Write("OnActionExecuting - {0}", context.RouteData.Values);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.Write("OnActionExecuting - {0}", context.RouteData.Values);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.Write("OnActionExecuting - {0}", context.RouteData.Values);
        }
    }
}
