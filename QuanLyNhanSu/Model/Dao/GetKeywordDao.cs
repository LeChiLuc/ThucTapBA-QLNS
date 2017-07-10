using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class GetKeywordDao
    {
        public QuanLyNhanSuDbContext db = null;
        public GetKeywordDao()
        {
            db = new QuanLyNhanSuDbContext();
        }
        public IEnumerable<User> GetKeyword(string keyword)
        {
            var parameters = new object[]
            {
                new SqlParameter("@Email",keyword),
                new SqlParameter("@Name",keyword),
                new SqlParameter("@Phone",keyword),
                new SqlParameter("@ID",keyword),
                new SqlParameter("@UserName",keyword),
                new SqlParameter("@CreatedDate",new DateTime(2017,07,07))
            };
            return db.Database.SqlQuery<User>("SearchUser @Email,@Name,@Phone,@ID,@UserName,@CreatedDate", parameters).ToList();
        }
    }
}
