using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WinnipegTomatoes.ActionFilters
{
    public class SupportsJson : ActionFilterAttribute
    {

        // we will use the framework default but allow users to override in the constructor
        private JsonRequestBehavior _jsonRequestBehavior = JsonRequestBehavior.DenyGet;

        public SupportsJson() {}

        // if you want to allow GET (lame!) then be my guest...
        public SupportsJson(JsonRequestBehavior behavior)
        {
            _jsonRequestBehavior = behavior;
        }

        // this happens AFTER the action is executed and BEFORE the result is returned
        // so it's the perfect place to swap out the result object for our JSON one
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // capture the application types
            var applicationTypes = (filterContext.HttpContext.Request.AcceptTypes ?? new string[] {""});

            // check to see if json is in there
            if (applicationTypes.Contains("application/json"))
            {
                // swap out the result if they requested Json
                var model = filterContext.Controller.ViewData.Model;
                filterContext.Result = new JsonResult { Data = model, JsonRequestBehavior = _jsonRequestBehavior };
            }
        }
    }
}
