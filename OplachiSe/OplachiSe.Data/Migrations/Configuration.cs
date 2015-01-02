namespace OplachiSe.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OplachiSe.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OplachiSe.Data.OplachiSeDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(OplachiSe.Data.OplachiSeDbContext context)
        {
            if (context.Roles.Any())
            {
               return; 
            }
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var adminRole = new IdentityRole { Name = "Admin" };
            roleManager.Create(adminRole);

            context.SaveChanges();

            var userManager = new UserManager<User>(new UserStore<User>(context));
            var admin = new User()
            {
                UserName = "admin@abv.com",
            };

            userManager.Create(admin, "admin@gmail.com");
            userManager.AddToRole(admin.Id, "Admin");

            context.SaveChanges();
            if (context.Categories.Any())
            {
                return;
            }

            context.Categories.Add(new Category() { Name = "Лични"});
            context.Categories.Add(new Category() { Name = "Здраве" });
            context.Categories.Add(new Category() { Name = "Работа" });
            context.Categories.Add(new Category() { Name = "Услуги" });
            context.Categories.Add(new Category() { Name = "Транспорт" });
        }
    }
}
