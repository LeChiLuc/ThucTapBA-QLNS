//namespace Model.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;
    
//    public partial class sp_Search : DbMigration
//    {
//        public override void Up()
//        {
//            CreateStoredProcedure("Search",
//                p => new
//                {
//                    ID = p.String(),
//                    Email = p.String(),
//                    Name = p.String(),
//                    Phone = p.String(),
//                    UserName = p.String(),
//                    CreatedDate = p.String()
//                }
//                ,
//                @"select ID,Name,Email,Phone,UserName,Password,CreatedDate,Status
//                from Users 
//                where 
//                (@Name IS NULL OR Name LIKE '%' + @Name + '%')
//                OR (@Email IS NULL OR Email LIKE '%' + @Email + '%')
//                OR (@Phone IS NULL OR Phone LIKE '%' + @Phone + '%')
//                OR (@ID IS NULL OR ID LIKE '%' + @ID + '%')
//                OR (@UserName IS NULL OR UserName LIKE '%' + @UserName + '%')                
//                OR (@CreatedDate IS NULL OR CreatedDate LIKE '%' + @CreatedDate + '%')
//                "
//                );
//        }

//        public override void Down()
//        {
//            DropStoredProcedure("dbo.Search");
//        }
//    }
//}
