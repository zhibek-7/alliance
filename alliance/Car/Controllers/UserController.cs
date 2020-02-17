using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Car.Controllers
{
    [Serializable]
    public class UserController : Controller
    {
        public ActionResult ConfirmResult() {
            return View();
        }

        public ActionResult AllClients() {

            UsersCARSEntities dcu = new UsersCARSEntities();
            var UsersClient = (from prod in dcu.Users where (prod.UserRole == "" || prod.UserRole == null) select prod);

            ViewBag.AllUsersForAdmin = UsersClient;
            return View();
        }

        ///Подтверждение регистрации пользователя
        public ActionResult ConfirmRegister(int UserID)
        {
          
               using (UsersCARSEntities dcu = new UsersCARSEntities())
                {
                 
                     var UsersConf = (from prod in dcu.Users where prod.UserId == UserID && (prod.UserRole == "" || prod.UserRole == null) select prod).FirstOrDefault();
                     if (UsersConf != null)
                      {
                    UsersConf.ConfirmPassword = UsersConf.Password;
                        UsersConf.UserRole ="client";
                     
                       }
             
                    dcu.SaveChanges(); // Error  
              

                ModelState.Clear();
                     WebMail.Send(UsersConf.Email, "Имя " + UsersConf.FullName + " " + " Фамилия " + UsersConf.FamilyName + "" + "Id Клиента " + Session["LogedUserId"],
                      "<br/><br/> My Email address  " + UsersConf.Email + "<br/>  Вы успешно зарегистрированы на сайте alliance-motors.kg" +
                      UsersConf.Email
                      , null
                      , null
                      , null
                      , true
                      , null
                      , null
                      , null
                      , null
                      , null
                      , UsersConf.Email);
                     UsersConf = null;
                     ViewBag.Message = "Successfully Registration done";
                   
                }

            return RedirectToAction("AllClients", "User");
        }


        //удалить пользователя
        public ActionResult DeleteUser(int UserID) {
            UsersCARSEntities dcu = new UsersCARSEntities();
            var UsersConf = (from prod in dcu.Users where prod.UserId == UserID && (prod.UserRole == "" || prod.UserRole == null) select prod).FirstOrDefault();
            if (UsersConf != null)
            {
                dcu.Users.Remove(UsersConf);
            }
           dcu.SaveChanges(); // Error 
           return RedirectToAction("AllClients", "User");
        }


        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users U)
        {
            if (ModelState.IsValid)
            {
                using (UsersCARSEntities dcu = new UsersCARSEntities())
                {
                    var vUserName = dcu.Users.Where(a => a.UserName.Equals(U.UserName)).FirstOrDefault();
                    var AllLogin = from UserName in dcu.Users select UserName;


                    var vEmail = dcu.Users.Where(a => a.Email.Equals(U.Email)).FirstOrDefault();
                    if (vEmail != null)
                    {
                        ViewBag.Message = "Пользователь с таким Email адресом уже существует";

                        return RedirectToAction("Register", "User", new { a = ViewBag.Message });
                    }
                    else
                    {

                        foreach (Users UserName in AllLogin)
                        {
                            if (vUserName == UserName)
                            {
                                ///dcu.Users.
                                ViewBag.Message = "Пользователь с таким Логином уже существует";
                                // Session["message"] = "MyMessage";
                                return RedirectToAction("Register", "User", new { a = ViewBag.Message });

                            }
                            else
                            {
                                continue;
                            }

                        }
                    }

                    dcu.Users.Add(U);
                    dcu.SaveChanges();
                    ModelState.Clear();
                    WebMail.Send(U.Email, "Имя " + U.FullName + " " + " Фамилия " + U.FamilyName + "" + "Id Клиента " + Session["LogedUserId"],
                      "<br/><br/> My Email address  " + U.Email + "<br/>Ждите подтверждения регистрации с сайта alliance-motors.kg" +
                      U.Email
                      , null
                      , null
                      , null
                      , true
                      , null
                      , null
                      , null
                      , null
                      , null
                      , U.Email);
                    U = null;

                    ViewBag.Message = "На вашу почту придет сообщение о подтверждении регистрации. Ждите подтверждения регистрации с сайта alliance-motors.kg";
                    return RedirectToAction("Login", "Home");
                }

            }

            return View(U);
        }


        public ActionResult ForgotPassword() {
            return View();
        }

        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(Users U)
        {
           using (UsersCARSEntities dcu = new UsersCARSEntities())
                {
                var userEmail = dcu.Users.Where(a=>a.Email.Equals(U.Email)).FirstOrDefault();

                var link = Url.Action("PasswordConfirm", "User", new { id = userEmail.UserId }, this.Request.Url.Scheme);
                if (userEmail != null) {
                        WebMail.Send(U.Email, " " + userEmail.FullName + "   " + "  " + userEmail.FamilyName + "   ",
                          "<br/> <a href=" + link+ ">Перейдите по ссылке<a/><br/>  " +
                    U.Email
                          , null
                          , null
                          , null
                          , true
                          , null
                          , null
                          , null
                          , null
                          , null
                          , U.Email);
                        U = null;

                        ViewBag.MessageUser = "На вашу почту отправлена ссылка";
                        return View();

                    } else {
                        ViewBag.MessageEmail = "Пользователь с данным Email адресом не существует";
                    }
                    
                }


      
            return View(U);
        }

        public ActionResult PasswordConfirm() {
            return View();
        }
        
        [HttpPost]
        public ActionResult PasswordConfirm(Users U,int id)
        {
           using (UsersCARSEntities dcu = new UsersCARSEntities())
                {
                  var userEmail = dcu.Users.Where(a => a.Email.Equals(U.Email)).FirstOrDefault();
                  if (userEmail != null)
                    {
                        if (userEmail.UserId == id)
                        {
                            userEmail.Password = U.Password;
                            userEmail.ConfirmPassword = U.ConfirmPassword;
                            dcu.SaveChanges();
                        ViewBag.MessagePC = "Ваш пароль успешно изменен";
                          return RedirectToAction("Login", "Home");
                    }
                        else {
                         ViewBag.Message = "Пользователь с данным Email адресом не существует";
                         return RedirectToAction("ForgotPassword", "User");

                        }
                     
                    }
                    else
                    {
                       ViewBag.Message = "Пользователь с данным Email адресом не существует";
                    }
                }
          
            return View(U);
        }









    }
}