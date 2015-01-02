namespace OplachiSe.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<User>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}