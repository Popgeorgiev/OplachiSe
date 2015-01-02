using OplachiSe.Models;
using OplachiSe.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OplachiSe.Web.Models.ComplainModels
{
    public class LatestComplainViewModel : IMapFrom<Complain>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}