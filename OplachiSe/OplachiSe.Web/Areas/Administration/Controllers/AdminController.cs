namespace OplachiSe.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using OplachiSe.Data;
    using OplachiSe.Commons;
    using OplachiSe.Web.Controllers;
    using OplachiSe.Data.Contracts;

    [Authorize(Roles = GlobalConstants.AdminRole)]
    public abstract class AdminController : BaseController
    {
        public AdminController(IOplachiSeData data)
            : base(data)
        {

        }
    }
}