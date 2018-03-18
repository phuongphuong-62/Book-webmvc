using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizeAttribute :AuthorizeAttribute
    {
        public string ViewName { get; set; }
        public CustomAuthorizeAttribute()
        {
            ViewName = "AuthorizeFailed";
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }

        private void IsUserAuthorized(AuthorizationContext filterContext)
        {
            if (filterContext.Result == null)
                return;
            if(filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                ViewDataDictionary dicti = new ViewDataDictionary();
                dicti.Add("Message", "");
                var result = new ViewResult()
                {
                    ViewName = this.ViewName,
                    ViewData = dicti
                };
                filterContext.Result = result;
            }
        }
    }
}