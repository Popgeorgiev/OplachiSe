namespace OplachiSe.Data.Contracts
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using OplachiSe.Models;

    public interface IOplachiSeDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Complain> Complains { get; set; }

        IDbSet<Like> Likes { get; set; }

        IDbSet<Vote> Votes { get; set; }

        IDbSet<Picture> Pictures { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
