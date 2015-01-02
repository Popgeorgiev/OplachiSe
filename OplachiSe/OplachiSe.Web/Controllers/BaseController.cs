namespace OplachiSe.Web.Controllers
{
    using System;
    using System.Web.Routing;
    using System.Web.Mvc;
    using System.Linq;

    using OplachiSe.Data.Contracts;
    using OplachiSe.Models;

    public class BaseController : Controller
    {
        public BaseController(IOplachiSeData data)
        {
            this.Data = data;
        }
        
        protected IOplachiSeData Data { get; set; }

        protected User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.UserProfile = this.Data.Users.All().Where(u => u.UserName == requestContext.HttpContext.User.Identity.Name).FirstOrDefault();

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}