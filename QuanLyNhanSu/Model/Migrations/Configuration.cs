namespace Model.Migrations
{
    using Model.EF;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Model.QuanLyNhanSuDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Model.QuanLyNhanSuDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //context.Users.AddOrUpdate(
            //    new User { Name ="Nguyễn Văn A", Email="nguyenvana@gmail.com",Password="123",Phone="0988182473"},
            //    new User { Name = "Nguyễn Văn B", Email = "nguyenvanb@gmail.com", Password = "1234", Phone = "0988141223" },
            //    new User { Name = "Nguyễn Văn C", Email = "nguyenvanc@gmail.com", Password = "12345", Phone = "0988112357" }
            //);
            //context.Users.AddOrUpdate(
            //  new User { Name = "Andrew Peters", Email = "peters@gmail.com",Phone="0909039219", CreatedDate = DateTime.Now, Status = true },
            //  new User { Name = "Brice Lambson", Email = "lambson@gmail.com", Phone = "0909039219", CreatedDate = DateTime.Now, Status = true },
            //  new User { Name = "Andrew Johnson", Email = "johnson@gmail.com", Phone = "0909039219", CreatedDate = DateTime.Now, Status = true },
            //  new User { Name = "Rowan Miller", Email = "miller@gmail.com",Phone="0909039219", CreatedDate = DateTime.Now, Status = true },
            //  new User { Name = "Michael Peters", Email = "michael@gmail.com", Phone = "0909039219", CreatedDate = DateTime.Now, Status = true },
            //  new User { Name = "John Miller", Email = "baker@gmail.com", Phone = "0909039219", CreatedDate = DateTime.Now, Status = true },
            //  new User { Name = "Rowan Baker", Email = "rowan@gmail.com", Phone = "0909039219", CreatedDate = DateTime.Now, Status = true }
            //);

            context.SaveChanges();
        }
    }
}
