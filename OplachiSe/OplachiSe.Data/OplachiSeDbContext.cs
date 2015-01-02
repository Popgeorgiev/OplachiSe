namespace OplachiSe.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using OplachiSe.Models;
    using OplachiSe.Data.Contracts;

    public class OplachiSeDbContext : IdentityDbContext<User>, IOplachiSeDbContext
    {
        public OplachiSeDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Complain> Complains { get; set; }

        public IDbSet<Like> Likes { get; set; }

        public IDbSet<Picture> Pictures { get; set; }

        public IDbSet<Vote> Votes { get; set; }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static OplachiSeDbContext Create()
        {
            return new OplachiSeDbContext();
        }
    }
}
