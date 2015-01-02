namespace OplachiSe.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using OplachiSe.Data.Contracts;
    using OplachiSe.Web.Models.ComplainModels;
    using OplachiSe.Models;
    using System.IO;
    using System;
    using System.Web;

    public class ComplainController : BaseController
    {
        public ComplainController(IOplachiSeData data)
            : base(data)
        {
        }

        [Authorize]
        public ActionResult Create()
        {
            var complainViewModel = new CreateComplainViewModel
            {
                Categories = new SelectList(this.Data.Categories.All().ToList(), "Id", "Name")
            };

            return View(complainViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CreateComplainViewModel complain)
        {
            if (complain != null && ModelState.IsValid)
            {
                var newComplain = Mapper.Map<Complain>(complain);
                newComplain.AuthorId = this.UserProfile.Id;
                newComplain.CreatedOn = DateTime.Now;
                if (complain.UploadedImage != null)
                {
                    using (var memory = new MemoryStream())
                    {
                        complain.UploadedImage.InputStream.CopyTo(memory);
                        var content = memory.GetBuffer();

                        newComplain.Picture = new Picture
                        {
                            Content = content,
                            Extension = complain.UploadedImage.FileName.Split(new[] { '.' }).Last()
                        };
                    }
                }
                this.Data.Complains.Add(newComplain);
                this.Data.SaveChanges();
                return RedirectToAction("","Home");
            }

            return View(complain);
        }

        [OutputCache(Duration = 30 * 60)]
        public ActionResult Picture(int id)
        {
            var image = this.Data.Pictures.Find(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, "image/" + image.Extension);
        }

        public ActionResult Details(int? id)
        {
            var dbComplain = this.Data
                .Complains
                .All()
                .Where(c => c.Id == id);

            var complain = dbComplain
                .Project()
                .To<ComplainDetailsViewModel>()
                .FirstOrDefault();
                

            if (complain == null)
            {
                throw new HttpException(404, "Complain not found");
            }
            complain.Score = 0;
            if (dbComplain.FirstOrDefault().Votes.Count() != 0)
            {
                double sum = dbComplain.FirstOrDefault().Votes.Sum(v => v.Value);
                complain.Score = (sum / dbComplain.FirstOrDefault().Votes.Count());
            }
            return View(complain);
        }
    }
}