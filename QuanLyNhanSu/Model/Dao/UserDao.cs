using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        //Khởi tạo đối tượng data
        QuanLyNhanSuDbContext db = null;
        public UserDao()
        {
            db = new QuanLyNhanSuDbContext();
        }
        //Tìm bản ghi có tên là...
        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x=>x.UserName == userName);
        }
        //Xử lý tên đăng nhập và password
        public int Login(string userName,string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if(result==null)
            {
                return 0;
            }
            else
            {
                if(result.Status==false)
                {
                    return -2;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -1;
                }
            }
        }
    }
}
