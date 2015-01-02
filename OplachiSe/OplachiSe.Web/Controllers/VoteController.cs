namespace OplachiSe.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using OplachiSe.Data.Contracts;
    using OplachiSe.Web.Models.VoteModels;
    using OplachiSe.Models;
    using System.Web;

    public class VoteController : BaseController
    {
        public VoteController(IOplachiSeData data)
            : base(data)
        {
        }
        
        [ChildActionOnly]
        public ActionResult AddVote(int id)
        {
            var userVote = this.Data
                .Votes
                .All()
                .Where(v => v.ComplainId == id && v.VoterId == this.UserProfile.Id)
                .Project()
                .To<NewVoteViewModel>()
                .FirstOrDefault();

            if (userVote == null)
            {
                return PartialView("_AddVotePartial", new NewVoteViewModel() { ComplainId = id});
            }
            else
            {
                return PartialView("_ChangeVotePartial", userVote);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendVote(NewVoteViewModel vote)

        {
            var dbVote = Mapper.Map<Vote>(vote);
            dbVote.Voter = this.UserProfile;

            var complain = this.Data.Complains.Find(vote.ComplainId);

            if (complain == null)
            {
                throw new HttpException(404, "Ticket not found");
            }

            complain.Votes.Add(dbVote);
            this.Data.SaveChanges();

            return PartialView("_ChangeVotePartial", vote);
        }
    }
}