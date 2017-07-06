using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class QuanLyNhanSuDbContext: DbContext
    {
        public QuanLyNhanSuDbContext(): base("QuanLyNhanSuConnectionString")
        {

        }
        public DbSet<User> Users { set; get; }
    }
}
