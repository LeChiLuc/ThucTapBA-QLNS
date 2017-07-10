using Common;
using Model;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QuanLyNhanSu.Web.Controllers
{
    //HomeController được kế thừa BaseController để đảm bảo user đã được đăng nhập hay chưa
    public class HomeController : BaseController
    {
        //Khởi tạo đối tượng data
        private QuanLyNhanSuDbContext _db;
        public HomeController()
        {
            _db = new QuanLyNhanSuDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }
        //Hiển thị dữ liệu trên table đồng thời phân trang
        [HttpGet]
        public JsonResult LoadData(string keyword,string fromDate,string toDate,int page, int pageSize=3)
        {
            IEnumerable<User> model = _db.Users;
            var dao = new GetKeywordDao();
            if(keyword.Count()>0)
            {
                model = dao.GetKeyword(keyword);
            }
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    model = model.Where(x => x.Name.Contains(keyword));
            //}
            int totalRow = model.Count();
            var db = model.OrderByDescending(x=>x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
            return Json(new
            {
                data = db,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        //Tìm bản ghi có id là:
        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            var user = _db.Users.Find(id);
            return Json(new
            {
                data = user,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        //SaveData bao gồm cả thêm mới và cập nhật bản ghi
        [HttpPost]
        public JsonResult SaveData(string strEmployee)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            User user = serializer.Deserialize<User>(strEmployee);
            bool status = false;
            string message = string.Empty;
            //Thêm người dùng nếu id = 0
            if(user.ID==0)
            {
                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;
                user.CreatedDate = DateTime.Now;
                _db.Users.Add(user);
                try
                {
                    _db.SaveChanges();
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }
            }
            else
            {
                //cập nhật người dùng vào data nếu id # 0
                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);              
                var entity = _db.Users.Find(user.ID);
                entity.Password = encryptedMd5Pas;
                entity.Name = user.Name;
                entity.Email = user.Email;
                entity.Phone = user.Phone;
                entity.UserName = user.UserName;
                entity.Status = user.Status;
                try
                {
                    _db.SaveChanges();
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }
                
            }
            return Json(new
            {
                status = status,
                message = message
            }, JsonRequestBehavior.AllowGet);
        }
        //Xóa bản ghi
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var user = _db.Users.Find(id);
            _db.Users.Remove(user);
            try
            {
                _db.SaveChanges();
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }
            
        }
    }
}