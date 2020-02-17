using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car.Controllers
{
    [Serializable]
    public class ResponseController : Controller
    {
        // GET: Response
        public ActionResult Index()
        {
            return View();
        }
    }
}