using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car.Controllers
{
    [Serializable]
    public class FeedBackController : Controller
    {
        // GET: FeedBack
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Submit() {
            List<State> allStates = new List<State>();

            using (UsersCARSEntities dc=new UsersCARSEntities()) {

                allStates = dc.State.OrderBy(a => a.StateName).ToList();

            }
            ViewBag.StateID = new SelectList(allStates, "StateID", "StateName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//this is for prevent crfAttack
        public ActionResult Submit(FeedBack fb) {
            List<State> allState = new List<State>();
            using (UsersCARSEntities dc=new UsersCARSEntities()) {
                allState = dc.State.OrderBy(a=>a.StateName).ToList();
            }

            ViewBag.StateID = new SelectList(allState,"StateId","StateName", fb.StateID);

                if (ModelState.IsValid)
                {

                using (UsersCARSEntities dc=new UsersCARSEntities()) {
                    dc.FeedBack.Add(fb);
                    dc.SaveChanges();
                    ModelState.Clear();
                    fb = null;
                    ViewBag.Message = " Successfully submitted";
                }

                }
                else {
                ViewBag.Message = "Failed! Please try again";
            }

            return View(fb);
        }

       

    }
}