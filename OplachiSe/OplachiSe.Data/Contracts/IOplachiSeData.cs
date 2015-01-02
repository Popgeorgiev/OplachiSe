namespace OplachiSe.Data.Contracts
{
    using OplachiSe.Models;
    using System.Data.Entity;

    public interface IOplachiSeData
    {
        IOplachiSeDbContext Context { get; }

        IRepository<User> Users { get; }

        IRepository<Picture> Pictures { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Complain> Complains { get; }

        IRepository<Category> Categories { get; }

        IRepository<Like> Likes { get; }

        IRepository<Vote> Votes { get; }

        int SaveChanges();
    }
}
