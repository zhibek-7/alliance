using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Car.Models;
using Microsoft.AspNet.Identity;
using SRVTextToImage;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Car.Controllers
{
    [Serializable]
    public class SendMailController : Controller
    {
        // GET: SendMail
        public ActionResult SendEmail()
        {
            
            return View();
        }







        [HttpPost]
        public ActionResult SendReport(ContactModel model, string SelectedReporT, Users u, string SelectedContragent, string SelectedContract, string DateFrom, string DateTo)
        {

        //    UsersCARSEntities dc = new UsersCARSEntities();
          // if (ModelState.IsValid)
          //  {
                WebMail.Send("alliancemotors02@gmail.com", "Имя "+ Session["LogedUserFullName"] + " "+ " Фамилия " + Session["LogedFamilyName"]+ ""+"Id Клиента " + Session["LogedUserId"],
                    "<br/><br/> My Email address  " + model.Email+
                    "<br/> <br/> Вид отчета - " + SelectedReporT+
                    "<br/> <br/> Дата с - " + DateFrom +
                    "<br/> <br/> Дата по - " + DateTo +
                    "<br/> <br/> Контрагент - " + SelectedContragent +
                    "<br/> <br/> Раздел - " + SelectedContract +
                    model.Email
                    , null
                    , null
                    , null
                    , true
                    , null
                    , null
                    , null
                    , null
                    , null
                    , model.Email);
            //  }


            if (Session["LogedUserId"] !=null )
            {
                ViewBag.Message = "На вашу почту будет отправлен отчет";
            }
            else
            {
                ViewBag.Message = " Войдите в систему ";
            }

            return RedirectToAction("Reports", "PrivateOfficce");
        }



        [HttpPost]
        public ActionResult SendEmail(ContactModel model, string CaptchaText) {

            if (this.Session["CaptchaImageText"].ToString() == CaptchaText)
            {
                ViewBag.Message = "Captcha Validation Success";
            }
            else {
                ViewBag.Message = "Captcha Validation Failed";
            }


         if (ModelState.IsValid)
            {
                WebMail.Send("alliancemotors02@gmail.com", model.FullName+"is coming to the party",
                    model.Message+"<br/><br/> My mobile numder"+model.MobilleNo+"<br/><br/> My Email address  "+
                    model.Email
                    ,null
                    , null
                    , null
                    , true
                    , null
                    , null 
                    , null 
                    , null
                    , null 
                    ,model.Email);
            }

            
            return View();
        }




        //This action for get Captcha Image
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]// this is for output cashe false
        public FileResult GetCaptchaImage()
        {
            CaptchaRandomImage CI = new CaptchaRandomImage();
            this.Session["CaptchaImageText"] = CI.GetRandomString(4); //here 4 means I want to get 4 char long captcha
            //CI.GenerateImage(this.Session["CaptchaImageText"].ToString(), 300, 75);
            //Or we can use another one for get custom color Captcha Image
            CI.GenerateImage(this.Session["CaptchaImageText"].ToString(), 300, 75, Color.DarkGray, Color.White);
            MemoryStream stream = new MemoryStream();
            CI.Image.Save(stream, ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "image/png");
        }




    }
}