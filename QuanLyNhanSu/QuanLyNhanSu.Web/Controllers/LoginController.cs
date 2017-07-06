using Common;
using Model.Dao;
using QuanLyNhanSu.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyNhanSu.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //Đăng nhập có validate login
        public ActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                //gọi Login trong UserDao để xử lý tài khoản và mật khẩu
                //Đồng thời mã hóa mật khẩu
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                //result == 1 đăng nhập đúng chuyển đến trang trủ
                if (result==1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    //gán session
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if(result==0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng.");
                }
            }
            return View("Index");
        }
   
    }
}