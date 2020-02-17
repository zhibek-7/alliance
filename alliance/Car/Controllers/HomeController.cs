using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Car.PhaetonService;
using System.Web.Script.Services;
using System.Web.UI;
using System.ServiceModel.Web;
using Car.ServicePhaeton1;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;
using System.Data.Entity.Validation;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Caching;
using System.IO.Compression;
using DevTrends.MvcDonutCaching;
using Car.Controllers.Models;
using System.Net.Http;

namespace Car.Controllers
{
    [Serializable]
    public class HomeController : Controller
    {
        [ChildActionOnly]
        public String getSessionId()
        {
            return Session["LogedUserId"].ToString();

        }

        [ChildActionOnly]
        public String getSessionName()
        {
            return Session["LogedUserFullName"].ToString();
        }


        public static Encoding Accept { get; private set; }


        public void ReadCart(string xmlPath, List<Item> objOrders)
        {
            List<Item> cart = new List<Item>();
            try
            {
                XDocument xDoc = XDocument.Load(xmlPath);
                List<Remnants> productList = xDoc.Descendants("Product").Select(
                          product => new Remnants
                          {
                              Id = Convert.ToInt32(product.Element("Id").Value),
                              Name = product.Element("NameGood").Value,
                              Code = product.Element("Code").Value,
                              Brend = product.Element("Brend").Value,
                              Articul = product.Element("Articul").Value,
                              Supplies = Convert.ToInt32(product.Element("Supplies").Value),
                              MethodDelivery = product.Element("MethodDelivery").Value,
                              Existence = Convert.ToInt32(product.Element("Existence").Value),
                              Price = Convert.ToDouble(product.Element("Price").Value),
                              CurrancyCode = product.Element("Currancycode").Value,
                              Sklad = product.Element("Sklad").Value
                          }).ToList();


                UsersCARSEntities de = new UsersCARSEntities();

                foreach (var i in productList)
                {
                    cart.Add(new Item(de.Remnants.Add(
                        new Remnants()
                        {
                            Id = Convert.ToInt32(i.Id),
                            Code = i.Code,
                            Brend = i.Brend,
                            Articul = i.Articul,
                            Name = i.Name,
                            Supplies = 0,
                            MethodDelivery = i.MethodDelivery,
                            Existence = null,
                            Days = null,
                            V_P = null,
                            Min_Krat = null,
                            Price = Convert.ToDouble(i.Price),
                            VIN = null,
                            CurrancyCode = i.CurrancyCode,
                            Date = null,
                            ViewOfContract = null,
                            Contragent = null,
                            Status = null,
                            Sklad = i.Sklad
                        }), Convert.ToInt32(i.Existence)));
                }


                Session["cart"] = cart;

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex + "Can't import xml file";
            }
        }


        public void ReadCart(string xmlPath, List<ItemRemoteStore> objOrders)
        {
            List<ItemRemoteStore> cart = new List<ItemRemoteStore>();
            try
            {
                XDocument xDoc = XDocument.Load(xmlPath);
                List<RemoteStore> productList = xDoc.Descendants("Product").Select(
                          product => new RemoteStore
                          {
                              Id = Convert.ToInt32(product.Element("Id").Value),
                              Name = product.Element("NameGood").Value,
                              Code = product.Element("Code").Value,
                              Brend = product.Element("Brend").Value,
                              Articul = product.Element("Articul").Value,

                              Supplies = Convert.ToInt32(product.Element("Supplies").Value),
                              MethodDelivery = product.Element("MethodDelivery").Value,
                              Existence = Convert.ToInt32(product.Element("Existence").Value),
                              Price = Convert.ToDouble(product.Element("Price").Value),


                              Min_Krat = Convert.ToInt32(product.Element("Min_krat").Value),
                              CurrancyCode = product.Element("Currancycode").Value,
                              Service = product.Element("Service").Value,
                              Client = Convert.ToInt32(product.Element("Client").Value)
                          }).ToList();


                UsersCARSEntities de = new UsersCARSEntities();

                foreach (var i in productList)
                {
                    cart.Add(new ItemRemoteStore(de.RemoteStore.Add(
                        new RemoteStore()
                        {
                            Id = i.Id,
                            Code = i.Code,
                            Brend = i.Brend,
                            Articul = i.Articul,
                            Name = i.Name,
                            Supplies = 0,
                            MethodDelivery = i.MethodDelivery,
                            Existence = null,
                            Days = null,
                            V_P = null,
                            Min_Krat = null,
                            Price = i.Price,
                            VIN = null,
                            CurrancyCode = i.CurrancyCode,
                            Date = null,
                            ViewOfContract = null,
                            Contragent = null,
                            Status = null,
                            Service = i.Service,
                            Sklad = null,
                            Client = Convert.ToInt32(i.Client)
                        }), Convert.ToInt32(i.Existence)));
                }
                Session["OrderNowRemoteStore"] = cart;
            }
            catch
            {
                ViewBag.Error = "Can't import xml file";
            }
        }
        private string md5(string sPassword)
        {
            if (sPassword != "" || sPassword != null)
            {
                System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
                bs = x.ComputeHash(bs);
                System.Text.StringBuilder s = new System.Text.StringBuilder();
                foreach (byte b in bs)
                {
                    s.Append(b.ToString("x2").ToLower());
                }
                return s.ToString();
            }
            else
            {
                return sPassword = "false";
            }

        }



        protected string renderPartialViewtostring(string ViewName, object Model) {
            if (string.IsNullOrEmpty(ViewName)) {
                ViewName = ControllerContext.RouteData.GetRequiredString("action");
                ViewData.Model = Model;

                using (StringWriter sw=new StringWriter()) {
                    ViewEngineResult result = ViewEngines.Engines.FindPartialView(ControllerContext, ViewName);
                    ViewContext viewContext = new ViewContext(ControllerContext,result.View,ViewData,TempData,sw);
                    result.View.Render(viewContext,sw);
                    return sw.GetStringBuilder().ToString();
                }

            }
            return "";
        }

        public class JsonModel {
            public string HTMLString { get; set; }
            public bool NoMoreDate { get; set; }
        }

        [ChildActionOnly]
        public ActionResult tableRow(List<usmSelectAllRemnantsByStoreMinSklad_Result> Model) {
            return PartialView(Model);
        }

        [HttpPost]
        ActionResult InfiniteScroll(int pageindex,string code, string brend) {
            System.Threading.Thread.Sleep(1000);
            int pagesize = 10;
            var tbrow = db.GetRemnantsses(pageindex, pagesize,code,brend);
            JsonModel jsonmodel = new JsonModel();
            jsonmodel.NoMoreDate = tbrow.Count < pagesize;
            jsonmodel.HTMLString = renderPartialViewtostring("table_row", tbrow);
            return Json(jsonmodel);
        }

        public ActionResult Index(string SelectedAuto, string Vin)
        {
            ViewBag.Title = "Home";
            var Url = "";

            UsersCARSEntities dc = new UsersCARSEntities();
            var Auto = (from prod in dc.NameOriginalAuto select prod).ToList();

            var contr = (from ctr in Auto select ctr);


            if (!String.IsNullOrEmpty(SelectedAuto))
            {
                contr = contr.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
            }

            var UniqueAuto = (from Ucontr in contr
                              group Ucontr by Ucontr.NameAuto into newGroup
                              where newGroup.Key != null
                              orderby newGroup.Key
                              select new { ViewOfAuto = newGroup.Key }).ToList();
            ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

            ViewBag.Auto = SelectedAuto;


            if (SelectedAuto == "NISSAN")
            {
                Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
            }
            if (SelectedAuto == "AUDIVW")
            {
                Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
            }
            if (SelectedAuto == "BMW")
            {
                Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
            }
            if (SelectedAuto == "HONDA")
            {
                Url = "http://catalog.alliance-motors.kg/honda/?vin=";
            }
            if (SelectedAuto == "MAZDA")
            {
                Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
            }
            if (SelectedAuto == "MERCEDES")
            {
                Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
            }
            if (SelectedAuto == "OPEL")
            {
                Url = "http://catalog.alliance-motors.kg/opel/?vin=";
            }
            if (SelectedAuto == "SUBARU")
            {
                Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
            }
            if (SelectedAuto == "TOYOTA")
            {
                Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
            }


            ViewBag.Vin = Vin;
            ViewBag.URLStr = Url + Vin;

            // Response.Redirect(Url + Vin, true);
            if (Url == null || Url == "")
            {
                return View();
            }
            else
            {
                return Redirect(Url + Vin);
            }
        }

        public ActionResult AboutUs(string SelectedAuto, string Vin)
        {
            UsersCARSEntities dcAuto = new UsersCARSEntities();
            var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();
            var contrAuto = (from ctr in Auto select ctr);
            if (!String.IsNullOrEmpty(SelectedAuto))
            {
                contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
            }

            var UniqueAuto = (from Ucontr in contrAuto
                              group Ucontr by Ucontr.NameAuto into newGroup
                              where newGroup.Key != null
                              orderby newGroup.Key
                              select new { ViewOfAuto = newGroup.Key }).ToList();
            ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

            ViewBag.Auto = SelectedAuto;


            if (SelectedAuto == "NISSAN")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
            }
            if (SelectedAuto == "AUDIVW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
            }
            if (SelectedAuto == "BMW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
            }
            if (SelectedAuto == "HONDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
            }
            if (SelectedAuto == "MAZDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
            }
            if (SelectedAuto == "MERCEDES")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
            }
            if (SelectedAuto == "OPEL")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
            }
            if (SelectedAuto == "SUBARU")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
            }
            if (SelectedAuto == "TOYOTA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
            }


            ViewBag.Vin = Vin;

            List<Section> sections = new List<Section>();
            Section section = new Section();
            using (UsersCARSEntities dcU = new UsersCARSEntities())
            {
                sections = dcU.Section.ToList();
                if (sections != null)
                {
                    ViewBag.Section = sections;
                }
                else
                {
                    ViewBag.Section = "";
                }
            }
            return View(sections);
        }


        public ActionResult PrivateOfficce()
        {
            if (Session["LogedUserFullName"] != null)
            {
                ViewBag.Title = "Личный кабинет";
                return View("PrivateOfficce");
            }
            else
            { return View("LoginPrivateOfficce"); }

        }


        public ActionResult LoginPrivateOfficce()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPrivateOfficce(Users u)
        {
            //this action is for handle post (Login)

            if (!ModelState.IsValid)
            {
                using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                    var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserId"] = v.UserId.ToString();
                        Session["LogedUserFullName"] = v.FullName.ToString();
                        Session["LogedFamilyName"] = v.FamilyName.ToString();
                        Session["LogedContactPhone"] = v.ContactPhone.ToString();
                        Session["LogedEmail"] = v.Email.ToString();
                        Session["LogedUserName"] = v.UserName.ToString();
                        Session["LogedPassword"] = v.Password.ToString();
                        Session["LogedUserRole"] = v.UserRole.ToString();
                        FormsAuthentication.SetAuthCookie(u.UserName, true);

                        string id = Session["LogedUserId"].ToString();
                        string xmlPathCart = Server.MapPath("~/Content/xml/cart" + id + ".xml");
                        string xmlPathCartRemoteStore = Server.MapPath("~/Content/xml/CartRemoteStore" + id + ".xml");
                        ReadCart(xmlPathCart, (List<Item>)Session["cart"]);
                        ReadCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
                        return RedirectToAction("AfterLoginPrivateOfficce");

                    }
                }
            }
            return View(u);
        }

        public ActionResult AfterLoginPrivateOfficce()
        {
            if (Session["LogedUserId"] != null)
            {

                string id = Session["LogedUserId"].ToString();
                string xmlPathCart = Server.MapPath("~/Content/xml/cart" + id + ".xml");
                string xmlPathCartRemoteStore = Server.MapPath("~/Content/xml/CartRemoteStore" + id + ".xml");
                ReadCart(xmlPathCart, (List<Item>)Session["cart"]);
                ReadCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
                return View("PrivateOfficce");
            }
            else { return ViewBag.Message = "Вы не правильно ввели логин или пароль"; }


        }

        public ActionResult Blog(string SelectedAuto, string Vin)
        {

            UsersCARSEntities dcAuto = new UsersCARSEntities();
            var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();
            var contrAuto = (from ctr in Auto select ctr);
            if (!String.IsNullOrEmpty(SelectedAuto))
            {
                contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
            }

            var UniqueAuto = (from Ucontr in contrAuto
                              group Ucontr by Ucontr.NameAuto into newGroup
                              where newGroup.Key != null
                              orderby newGroup.Key
                              select new { ViewOfAuto = newGroup.Key }).ToList();
            ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

            ViewBag.Auto = SelectedAuto;


            if (SelectedAuto == "NISSAN")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
            }
            if (SelectedAuto == "AUDIVW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
            }
            if (SelectedAuto == "BMW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
            }
            if (SelectedAuto == "HONDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
            }
            if (SelectedAuto == "MAZDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
            }
            if (SelectedAuto == "MERCEDES")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
            }
            if (SelectedAuto == "OPEL")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
            }
            if (SelectedAuto == "SUBARU")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
            }
            if (SelectedAuto == "TOYOTA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
            }


            ViewBag.Vin = Vin;
            List<Blog> blogs = new List<Blog>();
            Blog blog = new Blog();
            using (UsersCARSEntities dc = new UsersCARSEntities())
            {
                blogs = dc.Blog.ToList();
                if (blogs != null)
                {
                    ViewBag.Blog = blogs;
                }
                else
                {
                    ViewBag.blog = "";
                }
            }
            return View(blogs);
        }

        public ActionResult GoToBlog(int NumBlog)
        {
            List<Blog> blogs = new List<Blog>();
            using (UsersCARSEntities dc = new UsersCARSEntities())
            {
                blogs = dc.Blog.Where(a => a.Id.Equals(NumBlog)).ToList();
                ViewBag.GoToBlog = blogs;
            }
            return View(blogs);
        }

        public ActionResult Catalog(string SelectedAuto, string Vin)
        {
            if (Session["LogedUserId"] != null)
            {

                UsersCARSEntities dc = new UsersCARSEntities();
                var Auto = (from prod in dc.NameOriginalAuto select prod).ToList();

                var contr = (from ctr in Auto select ctr);


                if (!String.IsNullOrEmpty(SelectedAuto))
                {
                    contr = contr.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
                }

                var UniqueAuto = (from Ucontr in contr
                                  group Ucontr by Ucontr.NameAuto into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { ViewOfAuto = newGroup.Key }).ToList();
                ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

                ViewBag.Auto = SelectedAuto;


                if (SelectedAuto == "NISSAN")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
                }
                if (SelectedAuto == "AUDIVW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
                }
                if (SelectedAuto == "BMW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";

                }
                if (SelectedAuto == "HONDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
                }
                if (SelectedAuto == "MAZDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
                }
                if (SelectedAuto == "MERCEDES")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
                }
                if (SelectedAuto == "OPEL")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
                }
                if (SelectedAuto == "SUBARU")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
                }
                if (SelectedAuto == "TOYOTA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
                }


                ViewBag.Vin = Vin;
                return View();

            }
            else
            {
                return View("LoginCatalog");
            }

        }

        public ActionResult LoginCatalog()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginCatalog(Users u)
        {
            //this action is for handle post (Login)

            if (!ModelState.IsValid)
            {
                using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                    var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserId"] = v.UserId.ToString();
                        Session["LogedUserFullName"] = v.FullName.ToString();
                        Session["LogedFamilyName"] = v.FamilyName.ToString();
                        Session["LogedContactPhone"] = v.ContactPhone.ToString();
                        Session["LogedEmail"] = v.Email.ToString();
                        Session["LogedUserName"] = v.UserName.ToString();
                        Session["LogedPassword"] = v.Password.ToString();
                        Session["LogedUserRole"] = v.UserRole.ToString();
                        FormsAuthentication.SetAuthCookie(u.UserName, true);

                        string id = Session["LogedUserId"].ToString();
                        string xmlPathCart = Server.MapPath("~/Content/xml/cart" + id + ".xml");
                        string xmlPathCartRemoteStore = Server.MapPath("~/Content/xml/CartRemoteStore" + id + ".xml");
                        ReadCart(xmlPathCart, (List<Item>)Session["cart"]);
                        ReadCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
                        return RedirectToAction("Catalog");

                    }
                    else
                    {
                        return View("LoginCatalog");
                    }
                }
            }
            return View(u);
        }

        public ActionResult AfterLoginCatalog()
        {
            if (Session["LogedUserId"] != null)
            { return View(); }
            else { return ViewBag.Message = "Вы не правильно ввели логин или пароль"; }


        }
        public ActionResult Materials(string SelectedAuto, string Vin)
        {
            if (Session["LogedUserFullName"] != null)
            {
                UsersCARSEntities dcAuto = new UsersCARSEntities();
                var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();

                var contrAuto = (from ctr in Auto select ctr);


                if (!String.IsNullOrEmpty(SelectedAuto))
                {
                    contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
                }

                var UniqueAuto = (from Ucontr in contrAuto
                                  group Ucontr by Ucontr.NameAuto into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { ViewOfAuto = newGroup.Key }).ToList();
                ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

                ViewBag.Auto = SelectedAuto;


                if (SelectedAuto == "NISSAN")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
                }
                if (SelectedAuto == "AUDIVW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
                }
                if (SelectedAuto == "BMW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
                }
                if (SelectedAuto == "HONDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
                }
                if (SelectedAuto == "MAZDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
                }
                if (SelectedAuto == "MERCEDES")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
                }
                if (SelectedAuto == "OPEL")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
                }
                if (SelectedAuto == "SUBARU")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
                }
                if (SelectedAuto == "TOYOTA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
                }


                ViewBag.Vin = Vin;



                ViewBag.Title = "Смазочные материалы";
                return View("Materials");
            }
            else
            {
                return View("LoginMaterials");
            }


        }


        public ActionResult AddToCart(string priceForCart, int id, string currancy, string currancyCode, string producer, string url, string discount)
        {
            if (Request.IsAjaxRequest())
            {
                ViewBag.UrlAbsolute = url;
                ViewBag.PriceForCart = priceForCart;
                ViewBag.Id = id;
                ViewBag.CurrancyCode = currancyCode;
                ViewBag.Currancy = currancy;
                ViewBag.Producer = producer;
                ViewBag.DiscountStore = discount;
                return PartialView("AddToCartSearch");
            }
            return View(false);

        }


        public ActionResult AddToCartRS(int idRS, string currancy, decimal currancyCode, decimal currancySklad, string Code,
            string Brend, string Articul, string Name, string Days, Double Price,
            string CurrancyCodeRS, string Service, string Sklad, int Client, string ItemID, string PriceForCart, Double procent, string url)
        {
            if (Request.IsAjaxRequest())
            {
                string PriceOf = Math.Round((Decimal)((Decimal)Price * currancySklad / currancyCode), 2, MidpointRounding.AwayFromZero).ToString().Replace(',', '.').Trim();
                ViewBag.AbsoluteUrl = url;

                ViewBag.ProcentPh = 1;
                ViewBag.PriceForCart = PriceForCart;
                ViewBag.idRS = idRS;
                ViewBag.CurrancyCode = currancyCode;
                ViewBag.Currancy = currancy;


                ViewBag.currancySklad = currancySklad;
                ViewBag.Code = Code;
                ViewBag.Brend = Brend;
                ViewBag.Articul = Articul;



                ViewBag.Name = Name;
                ViewBag.Days = Days;
                ViewBag.Price = Price;
                ViewBag.CurrancyCodeRS = CurrancyCodeRS;

                ViewBag.Service = Service;
                ViewBag.Sklad = Sklad;
                ViewBag.Client = Client;
                ViewBag.ItemID = ItemID;

                return PartialView("AddToCartRS");
            }
            return View(false);

        }



        public ActionResult PriceList()
        {
            ViewBag.Title = "Прайс-лист";
            return View("PriceList");
        }
        public ActionResult Sales(Users u, string SelectedAuto, string Vin)
        {
            if (Session["LogedUserFullName"] != null)
            {
                UsersCARSEntities dcAuto = new UsersCARSEntities();
                var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();

                var contrAuto = (from ctr in Auto select ctr);


                if (!String.IsNullOrEmpty(SelectedAuto))
                {
                    contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
                }

                var UniqueAuto = (from Ucontr in contrAuto
                                  group Ucontr by Ucontr.NameAuto into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { ViewOfAuto = newGroup.Key }).ToList();
                ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

                ViewBag.Auto = SelectedAuto;


                if (SelectedAuto == "NISSAN")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
                }
                if (SelectedAuto == "AUDIVW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
                }
                if (SelectedAuto == "BMW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
                }
                if (SelectedAuto == "HONDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
                }
                if (SelectedAuto == "MAZDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
                }
                if (SelectedAuto == "MERCEDES")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
                }
                if (SelectedAuto == "OPEL")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
                }
                if (SelectedAuto == "SUBARU")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
                }
                if (SelectedAuto == "TOYOTA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
                }


                ViewBag.Vin = Vin;
                ViewBag.Title = "Распродажа";
                UsersCARSEntities dc = new UsersCARSEntities();
                var v = dc.Users.Where(a => a.UserName.Equals(u.Password) && a.Password.Equals(u.Password)).FirstOrDefault();

                var products = (from prod in dc.Sale group prod by prod.Brend into g select new { g }).ToList();

                if (products != null)
                {
                    var ProductsSales = dc.Sale.ToList();
                    ViewBag.SearchParametersSales = ProductsSales.ToList().ToList();
                    ViewBag.SearchParametersSales = ProductsSales.GroupBy(row =>
                      new
                      {
                          FirstField = row.Brend,
                      })
                    .Select(
                      group => new Sale
                      {
                          Brend = group.Key.FirstField,
                      }).ToList();
                }
                else
                {
                    ViewBag.SearchParametersSales = "";
                }

                return View("Sales");
            }
            else
            {

                UsersCARSEntities dc = new UsersCARSEntities();
                var v = dc.Users.Where(a => a.UserName.Equals(u.Password) && a.Password.Equals(u.Password)).FirstOrDefault();
                var products = (from prod in dc.Sale select prod).ToList();

                if (products != null)
                {
                    var ProductsSales = dc.Sale.ToList();
                    ViewBag.SearchParametersSales = ProductsSales.ToList().ToList();
                }
                else
                {
                    ViewBag.SearchParametersSales = "";
                }



                ViewBag.SearchParametersSales = products.GroupBy(row =>
                  new
                  {
                      FirstField = row.Brend,
                      SecondField = row.Articul,
                      ThirdField = row.NameGood,
                      FoursField = row.Adaptation,
                      FiveField = row.Price,
                      SixField = row.Sales

                  })
               .Select(
                  group => new Sale
                  {
                      Brend = group.Key.FirstField,
                      Articul = group.Key.SecondField,
                      NameGood = group.Key.ThirdField,
                      Adaptation = group.Key.FoursField,
                      Price = group.Key.FiveField,
                      Sales = group.Key.SixField
                  }).ToList();
                return View("LoginSales");
            }

        }
        public ActionResult LoginSales()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginSales(Users u)
        {
            //this action is for handle post (Login)

            if (!ModelState.IsValid)
            {
                using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                    var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserId"] = v.UserId.ToString();
                        Session["LogedUserFullName"] = v.FullName.ToString();
                        Session["LogedFamilyName"] = v.FamilyName.ToString();
                        Session["LogedContactPhone"] = v.ContactPhone.ToString();
                        Session["LogedEmail"] = v.Email.ToString();
                        Session["LogedUserName"] = v.UserName.ToString();
                        Session["LogedPassword"] = v.Password.ToString();
                        Session["LogedUserRole"] = v.UserRole.ToString();
                        FormsAuthentication.SetAuthCookie(u.UserName, true);

                        string id = Session["LogedUserId"].ToString();
                        string xmlPathCart = Server.MapPath("~/Content/xml/cart" + id + ".xml");
                        string xmlPathCartRemoteStore = Server.MapPath("~/Content/xml/CartRemoteStore" + id + ".xml");
                        ReadCart(xmlPathCart, (List<Item>)Session["cart"]);
                        ReadCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
                        return RedirectToAction("AfterLoginSales");

                    }
                }
            }
            return View(u);
        }

        public ActionResult AfterLoginSales()
        {
            if (Session["LogedUserId"] != null)
            { return View("Sales"); }
            else { return ViewBag.Message = "Вы не правильно ввели логин или пароль"; }


        }

        public ActionResult Order()
        {
            ViewBag.Title = "Заказ";
            return View("Order");
        }

        public ActionResult AnswersOnQuestions()
        {
            ViewBag.Title = "Ответы на вопросы";
            return View("AnswersOnQuestions");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Контакты";
            UsersCARSEntities dc = new UsersCARSEntities();
            ViewBag.Contacts = dc.UrlAutoOriginal.ToList();
            return View("Contact");
        }

        public ActionResult Vacancy(string SelectedAuto, string Vin)
        {
            UsersCARSEntities dcAuto = new UsersCARSEntities();
            var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();
            var contrAuto = (from ctr in Auto select ctr);
            if (!String.IsNullOrEmpty(SelectedAuto))
            {
                contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
            }

            var UniqueAuto = (from Ucontr in contrAuto
                              group Ucontr by Ucontr.NameAuto into newGroup
                              where newGroup.Key != null
                              orderby newGroup.Key
                              select new { ViewOfAuto = newGroup.Key }).ToList();
            ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

            ViewBag.Auto = SelectedAuto;


            if (SelectedAuto == "NISSAN")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
            }
            if (SelectedAuto == "AUDIVW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
            }
            if (SelectedAuto == "BMW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
            }
            if (SelectedAuto == "HONDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
            }
            if (SelectedAuto == "MAZDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
            }
            if (SelectedAuto == "MERCEDES")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
            }
            if (SelectedAuto == "OPEL")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
            }
            if (SelectedAuto == "SUBARU")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
            }
            if (SelectedAuto == "TOYOTA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
            }


            ViewBag.Vin = Vin;
            List<Vacancies> vacancies = new List<Vacancies>();
            using (UsersCARSEntities dc = new UsersCARSEntities())
            {
                vacancies = dc.Vacancies.ToList();

                if (vacancies != null)
                {
                    ViewBag.Vacancies = vacancies;
                }
                else
                {
                    ViewBag.Vacancies = "";
                }
            }

            ViewBag.Title = "Вакансии";

            return View(vacancies);
        }

        public ActionResult GoToVacancy(int NumVacancy)
        {
            List<Vacancies> vacancies = new List<Vacancies>();
            using (UsersCARSEntities dc = new UsersCARSEntities())
            {
                vacancies = dc.Vacancies.Where(a => a.Id.Equals(NumVacancy)).ToList();
                ViewBag.GoToAksii = vacancies;
            }
            return View(vacancies);
        }
        public ActionResult Aksii(string SelectedAuto, string Vin)
        {
            UsersCARSEntities dcAuto = new UsersCARSEntities();
            var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();
            var contrAuto = (from ctr in Auto select ctr);
            if (!String.IsNullOrEmpty(SelectedAuto))
            {
                contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
            }

            var UniqueAuto = (from Ucontr in contrAuto
                              group Ucontr by Ucontr.NameAuto into newGroup
                              where newGroup.Key != null
                              orderby newGroup.Key
                              select new { ViewOfAuto = newGroup.Key }).ToList();
            ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

            ViewBag.Auto = SelectedAuto;


            if (SelectedAuto == "NISSAN")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
            }
            if (SelectedAuto == "AUDIVW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
            }
            if (SelectedAuto == "BMW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
            }
            if (SelectedAuto == "HONDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
            }
            if (SelectedAuto == "MAZDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
            }
            if (SelectedAuto == "MERCEDES")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
            }
            if (SelectedAuto == "OPEL")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
            }
            if (SelectedAuto == "SUBARU")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
            }
            if (SelectedAuto == "TOYOTA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
            }


            ViewBag.Vin = Vin;
            List<Aksii> aksii = new List<Aksii>();
            using (UsersCARSEntities dc = new UsersCARSEntities())
            {
                aksii = dc.Aksii.ToList();

                if (aksii != null)
                {
                    ViewBag.AksiiClient = aksii;
                }
                else
                {
                    ViewBag.AksiiClient = "";
                }
            }

            ViewBag.Title = "Акции";

            return View(aksii);
        }

        public ActionResult GoToAksii(int NumAksii)
        {
            List<Aksii> aksii = new List<Aksii>();
            using (UsersCARSEntities dc = new UsersCARSEntities())
            {
                aksii = dc.Aksii.Where(a => a.Id.Equals(NumAksii)).ToList();
                ViewBag.GoToAksii = aksii;
            }
            return View(aksii);
        }

        public ActionResult FeedBack()
        {
            ViewBag.Title = "Обратная связь";

            return View("FeedBack");
        }
        public ActionResult Login(string SelectedAuto, string Vin)
        {
            UsersCARSEntities dcAuto = new UsersCARSEntities();
            var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();
            var contrAuto = (from ctr in Auto select ctr);
            if (!String.IsNullOrEmpty(SelectedAuto))
            {
                contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
            }

            var UniqueAuto = (from Ucontr in contrAuto
                              group Ucontr by Ucontr.NameAuto into newGroup
                              where newGroup.Key != null
                              orderby newGroup.Key
                              select new { ViewOfAuto = newGroup.Key }).ToList();
            ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

            ViewBag.Auto = SelectedAuto;


            if (SelectedAuto == "NISSAN")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
            }
            if (SelectedAuto == "AUDIVW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
            }
            if (SelectedAuto == "BMW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
            }
            if (SelectedAuto == "HONDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
            }
            if (SelectedAuto == "MAZDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
            }
            if (SelectedAuto == "MERCEDES")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
            }
            if (SelectedAuto == "OPEL")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
            }
            if (SelectedAuto == "SUBARU")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
            }
            if (SelectedAuto == "TOYOTA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
            }


            ViewBag.Vin = Vin;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users u, string SelectedAuto, string Vin)
        {
            if (!ModelState.IsValid)
            {
                UsersCARSEntities dcAuto = new UsersCARSEntities();
                var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();
                var contrAuto = (from ctr in Auto select ctr);
                if (!String.IsNullOrEmpty(SelectedAuto))
                {
                    contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
                }

                var UniqueAuto = (from Ucontr in contrAuto
                                  group Ucontr by Ucontr.NameAuto into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { ViewOfAuto = newGroup.Key }).ToList();
                ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

                ViewBag.Auto = SelectedAuto;


                if (SelectedAuto == "NISSAN")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
                }
                if (SelectedAuto == "AUDIVW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
                }
                if (SelectedAuto == "BMW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
                }
                if (SelectedAuto == "HONDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
                }
                if (SelectedAuto == "MAZDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
                }
                if (SelectedAuto == "MERCEDES")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
                }
                if (SelectedAuto == "OPEL")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
                }
                if (SelectedAuto == "SUBARU")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
                }
                if (SelectedAuto == "TOYOTA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
                }
                ViewBag.Vin = Vin;

                using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                    var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserId"] = v.UserId.ToString();
                        Session["LogedUserFullName"] = v.FullName.ToString();
                        Session["LogedFamilyName"] = v.FamilyName.ToString();
                        Session["LogedContactPhone"] = v.ContactPhone.ToString();
                        Session["LogedEmail"] = v.Email.ToString();
                        Session["LogedUserName"] = v.UserName.ToString();
                        Session["LogedPassword"] = v.Password.ToString();
                        Session["LogedUserRole"] = v.UserRole.ToString();
                        FormsAuthentication.SetAuthCookie(u.UserName, true);


                        string id = Session["LogedUserId"].ToString();
                        string xmlPathCart = Server.MapPath("~/Content/xml/cart" + id + ".xml");
                        string xmlPathCartRemoteStore = Server.MapPath("~/Content/xml/CartRemoteStore" + id + ".xml");
                        ReadCart(xmlPathCart, (List<Item>)Session["cart"]);
                        ReadCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
                        return RedirectToAction("Catalog");

                    }
                    else
                    {
                        Session["LogedUserId"] = null;
                        Session["LogedUserFullName"] = null;
                        Session["LogedFamilyName"] = null;
                        Session["LogedContactPhone"] = null;
                        Session["LogedEmail"] = null;
                        Session["LogedUserName"] = null;
                        Session["LogedPassword"] = null;
                        Session["LogedUserRole"] = null;
                        ViewBag.MessageLogin = "Вы не правильно ввели логин или пароль";
                    }
                }
            }
            return View(u);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LogedUserId"] != null)
            { return View(); }
            else { return ViewBag.Message = "Вы не правильно ввели логин или пароль"; }
        }

        public ActionResult LoginMaterials(string SelectedAuto, string Vin, string SelectedCurrancyCode)
        {
            UsersCARSEntities dcAuto = new UsersCARSEntities();
            var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();
            var contrAuto = (from ctr in Auto select ctr);
            if (!String.IsNullOrEmpty(SelectedAuto))
            {
                contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
            }

            var UniqueAuto = (from Ucontr in contrAuto
                              group Ucontr by Ucontr.NameAuto into newGroup
                              where newGroup.Key != null
                              orderby newGroup.Key
                              select new { ViewOfAuto = newGroup.Key }).ToList();
            ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

            ViewBag.Auto = SelectedAuto;
            /////////////////////////////////////////////////
            UsersCARSEntities dcCCode = new UsersCARSEntities();
            var contracts = (from prod in dcCCode.CurrancyCode select prod).ToList();

            var contr = (from ctr in contracts select ctr);

            if (String.IsNullOrEmpty(SelectedCurrancyCode))
            {
                SelectedCurrancyCode = "KGS";
            }
            var UniqueContract = (from Ucontr in contr
                                  group Ucontr by Ucontr.Name into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.ElementAt(0).ValueToSom
                                  select new { ViewOfMade = newGroup.Key, ValueOfMade = newGroup.Key }).ToList();

            ViewBag.UniqueCurrancyCode = UniqueContract.Select(m => new SelectListItem { Value = m.ViewOfMade, Text = m.ValueOfMade }).ToList();


            if (SelectedAuto == "NISSAN")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
            }
            if (SelectedAuto == "AUDIVW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
            }
            if (SelectedAuto == "BMW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
            }
            if (SelectedAuto == "HONDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
            }
            if (SelectedAuto == "MAZDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
            }
            if (SelectedAuto == "MERCEDES")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
            }
            if (SelectedAuto == "OPEL")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
            }
            if (SelectedAuto == "SUBARU")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
            }
            if (SelectedAuto == "TOYOTA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
            }


            ViewBag.Vin = Vin;
            return View();

        }


        //for smazochnye materialy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginMaterials(Users u, string SelectedAuto, string Vin, string SelectedCurrancyCode)
        {
            if (!ModelState.IsValid)
            {
                UsersCARSEntities dcAuto = new UsersCARSEntities();
                var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();
                var contrAuto = (from ctr in Auto select ctr);
                if (!String.IsNullOrEmpty(SelectedAuto))
                {
                    contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
                }

                var UniqueAuto = (from Ucontr in contrAuto
                                  group Ucontr by Ucontr.NameAuto into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { ViewOfAuto = newGroup.Key }).ToList();
                ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

                ViewBag.Auto = SelectedAuto;
                UsersCARSEntities dcCCode = new UsersCARSEntities();
                var contracts = (from prod in dcCCode.CurrancyCode select prod).ToList();

                var contr = (from ctr in contracts select ctr);

                if (String.IsNullOrEmpty(SelectedCurrancyCode))
                {
                    SelectedCurrancyCode = "KGS";
                }
                var UniqueContract = (from Ucontr in contr
                                      group Ucontr by Ucontr.Name into newGroup
                                      where newGroup.Key != null
                                      orderby newGroup.ElementAt(0).ValueToSom
                                      select new { ViewOfMade = newGroup.Key, ValueOfMade = newGroup.Key }).ToList();

                ViewBag.UniqueCurrancyCode = UniqueContract.Select(m => new SelectListItem { Value = m.ViewOfMade, Text = m.ValueOfMade }).ToList();


                if (SelectedAuto == "NISSAN")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
                }
                if (SelectedAuto == "AUDIVW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
                }
                if (SelectedAuto == "BMW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
                }
                if (SelectedAuto == "HONDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
                }
                if (SelectedAuto == "MAZDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
                }
                if (SelectedAuto == "MERCEDES")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
                }
                if (SelectedAuto == "OPEL")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
                }
                if (SelectedAuto == "SUBARU")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
                }
                if (SelectedAuto == "TOYOTA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
                }
                ViewBag.Vin = Vin;

                using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                    var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
                    if (v != null)
                    {

                        Session["LogedUserId"] = v.UserId.ToString();
                        Session["LogedUserFullName"] = v.FullName.ToString();
                        Session["LogedFamilyName"] = v.FamilyName.ToString();
                        Session["LogedContactPhone"] = v.ContactPhone.ToString();
                        Session["LogedEmail"] = v.Email.ToString();
                        Session["LogedUserName"] = v.UserName.ToString();
                        Session["LogedPassword"] = v.Password.ToString();
                        Session["LogedUserRole"] = v.UserRole.ToString();


                        FormsAuthentication.SetAuthCookie(u.UserName, true);


                        string id = Session["LogedUserId"].ToString();
                        string xmlPathCart = Server.MapPath("~/Content/xml/cart" + id + ".xml");
                        string xmlPathCartRemoteStore = Server.MapPath("~/Content/xml/CartRemoteStore" + id + ".xml");
                        ReadCart(xmlPathCart, (List<Item>)Session["cart"]);
                        ReadCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
                        return RedirectToAction("Materials");

                    }
                    else
                    {
                        ViewBag.MessageLogin = "Вы не правильно ввели логин или пароль";
                    }
                }
            }
            return View(u);

        }

        public ActionResult AfterLoginMaterials()
        {
            if (Session["LogedUserId"] != null)
            { return View(); }
            else { return ViewBag.Message = "Вы не правильно ввели логин или пароль"; }
        }


        public void CreateXMLFileCart(string xmlPath, List<Item> objOrders)
        {
            UsersCARSEntities dc = new UsersCARSEntities();


            if (System.IO.File.Exists(xmlPath) == true)
            {
                System.IO.File.Delete(xmlPath);
            }

            using (XmlWriter writer = XmlWriter.Create(Server.MapPath(xmlPath)))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Products");
                foreach (Item item in objOrders)
                {
                    writer.WriteStartElement("Product");
                    writer.WriteElementString("Id", item.Pr.Id.ToString());
                    writer.WriteElementString("NameGood", item.Pr.Name);
                    writer.WriteElementString("Code", item.Pr.Articul);
                    writer.WriteElementString("Brend", item.Pr.Brend);
                    writer.WriteElementString("Articul", item.Pr.Articul);
                    writer.WriteElementString("Supplies", Session["LogedUserId"].ToString());/////id  текущего пользователя
                    writer.WriteElementString("MethodDelivery", item.Pr.MethodDelivery);
                    //writer.WriteElementString("Contragent", item.Pr.Contragent);
                    writer.WriteElementString("Existence", item.Quantity.ToString());
                    writer.WriteElementString("Price", item.Pr.Price.ToString());
                    // writer.WriteElementString("Days", item.Pr.Days);
                    //writer.WriteElementString("Min_krat", item.Pr.Min_Krat.ToString());
                    writer.WriteElementString("Currancycode", item.Pr.CurrancyCode.ToString());
                    writer.WriteElementString("Sklad", item.Pr.Sklad.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                dc.SaveChanges();

                ModelState.Clear();
            }
        }


        public void CreateXMLFileCart(string xmlPath, List<ItemRemoteStore> objOrders)
        {
            UsersCARSEntities dc = new UsersCARSEntities();


            if (System.IO.File.Exists(xmlPath) == true)
            {
                System.IO.File.Delete(xmlPath);
            }

            using (XmlWriter writer = XmlWriter.Create(Server.MapPath(xmlPath)))
            {

                writer.WriteStartDocument();
                writer.WriteStartElement("Products");

                foreach (ItemRemoteStore item in objOrders)
                {
                    writer.WriteStartElement("Product");
                    writer.WriteElementString("Id", item.Pr.Id.ToString());
                    writer.WriteElementString("NameGood", item.Pr.Name);
                    writer.WriteElementString("Code", item.Pr.Articul);
                    writer.WriteElementString("Brend", item.Pr.Brend);
                    writer.WriteElementString("Articul", item.Pr.Articul);
                    writer.WriteElementString("Supplies", item.Pr.Supplies.ToString());
                    writer.WriteElementString("MethodDelivery", "шт");
                    //  writer.WriteElementString("Contragent", item.Pr.Contragent);
                    writer.WriteElementString("Existence", item.Quantity.ToString());
                    writer.WriteElementString("Price", item.Pr.Price.ToString());
                    //writer.WriteElementString("Days", item.Pr.Days);
                    writer.WriteElementString("Min_krat", "1");
                    writer.WriteElementString("Currancycode", item.Pr.CurrancyCode.ToString());
                    writer.WriteElementString("Service", item.Pr.Service.ToString());
                    writer.WriteElementString("Client", item.Pr.Client.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                dc.SaveChanges();
                ModelState.Clear();
            }
        }
        public ActionResult Logout()
        {
            string id = Session["LogedUserId"].ToString();
            string xmlPathCart = @"~/Content/xml/cart" + id + ".xml";
            string xmlPathCartRemoteStore = @"~/Content/xml/CartRemoteStore" + id + ".xml";

            if (Session["cart"] != null)
            {

                CreateXMLFileCart(xmlPathCart, (List<Item>)Session["cart"]);
            }

            if (Session["OrderNowRemoteStore"] != null)
            {
                CreateXMLFileCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
            }

            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public decimal Discount(string part)
        {
            UsersCARSEntities dc = new UsersCARSEntities();
            var ProductsRemnants = dc.Remnants.Where(a => a.Articul.Contains(part)).ToList();
            ProductsRemnants.ToList();
            dc.Discount.ToList();
            var ProductsDiscount = dc.Discount.Where(a => a.Name_Artikul.Equals(part)).ToList();
            decimal Discount = 0;

            if (ProductsDiscount != null)
            {
                foreach (var item in ProductsDiscount)
                {
                    if ((item.DateEnd < DateTime.Now.Date || item.DateEnd == DateTime.Now.Date || item.DateEnd == DateTime.MinValue.Date) &&
                         (item.DateBegin > DateTime.Now.Date || item.DateBegin == DateTime.Now.Date))
                    {
                        if (item.Id_Contraget == null || item.Id_Contraget == null)
                        {
                            Discount = (decimal)item.Discount1;
                        }
                        else
                        {
                            if (Session["LogedUserId"] != null && (object)item.Id_Contraget == Session["LogedUserId"])
                            {
                                Discount = (decimal)item.Discount1;
                            }
                        }
                    }
                    else
                    {
                        Discount = 0;
                    }
                }
            }
            else
            {
                Discount = 0;
            }
            return Discount;
        }

        public decimal UsdSom(string SelectedCurrancyCode)
        {
            ////////////////////////////Currancy Code Self
            UsersCARSEntities dcU = new UsersCARSEntities();
            decimal currancy = 1;
            var currancyCode = (from CurCode in dcU.CurrancyCode select CurCode).ToList();
            var curCode = (from cCode in currancyCode select cCode);
            var Currancy = dcU.CurrancyCode.ToList();

            foreach (var item in Currancy)
            {
                if (item.Name == SelectedCurrancyCode)
                {
                    currancy = Convert.ToDecimal(item.Value);
                }
            }
            ///////////////////////////End currancy code
            return currancy;
        }

        public List<CurrancyCode> CurrancyCode()
        {
            List<CurrancyCode> productListCurrancy = new List<CurrancyCode>();
            try
            {

                var xmlPath = Server.MapPath("~/Content/xml/currancyCode.xml");

                ViewBag.xmlPath = xmlPath;
                XDocument xDoc = XDocument.Load(xmlPath);

                string currancces = xDoc.Descendants("currancyCode").Equals("currancces").ToString();

                productListCurrancy = xDoc.Descendants("currancy").Select(
                  currancy => new CurrancyCode
                  {
                      Id = Convert.ToInt32(currancy.Element("id").Value),
                      Name = currancy.Element("name").Value,
                      Value = Convert.ToDouble(currancy.Element("value").Value.Replace(',', '.').Trim().Replace(" ", string.Empty)),
                      ValueToSom = 0,
                      CodeCur = Convert.ToInt32(currancy.Element("id").Value)
                  }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }



            using (UsersCARSEntities dcCC = new UsersCARSEntities())
            {

                foreach (CurrancyCode i in productListCurrancy)
                {
                    if (i.Id == 398 || i.Id == 643 || i.Id == 417 || i.Id == 840 || i.Id == 978)
                    {
                        var vCC1 = dcCC.CurrancyCode.Where(a => a.CodeCur.Equals(i.Id)).FirstOrDefault();
                        if (vCC1 != null)
                        {
                            vCC1.Name = i.Name;
                            vCC1.Value = i.Value;
                            vCC1.CodeCur = i.Id;
                        }
                        else
                        {
                            dcCC.CurrancyCode.Add(i);
                        }
                    }
                    dcCC.SaveChanges();
                    ViewBag.ResultImport = dcCC.Remnants.ToList();
                }
            }

            return productListCurrancy;
        }


        [DonutOutputCache(Duration = 30, VaryByParam = "*", Location = System.Web.UI.OutputCacheLocation.Client)]
        public async Task<ActionResult> Search(string SelectedAuto, string Vin, string part, Users u, string Search, string SearchStore, string SelectedCurrancyCode)
        {
            if (Session["LogedUserId"] != null)
            {
                ViewBag.ProducerPhaeton = "";
                ViewBag.ProducersOfEmeks = "";
                UsersCARSEntities dcAuto = new UsersCARSEntities();
                var Auto = await (from prod in dcAuto.NameOriginalAuto select prod).ToListAsync();
                var contrAuto = (from ctr in Auto select ctr);
                if (!String.IsNullOrEmpty(SelectedAuto))
                { contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim())); }

                var UniqueAuto = (from Ucontr in contrAuto
                                  group Ucontr by Ucontr.NameAuto into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { ViewOfAuto = newGroup.Key }).ToList();
                ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();
                ViewBag.Auto = SelectedAuto;

                if (SelectedAuto == "NISSAN")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin="; }
                if (SelectedAuto == "AUDIVW")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin="; }
                if (SelectedAuto == "BMW")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin="; }
                if (SelectedAuto == "HONDA")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin="; }
                if (SelectedAuto == "MAZDA")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin="; }
                if (SelectedAuto == "MERCEDES")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin="; }
                if (SelectedAuto == "OPEL")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin="; }
                if (SelectedAuto == "SUBARU")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin="; }
                if (SelectedAuto == "TOYOTA")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin="; }
                ViewBag.Vin = Vin;

                ///skidka

                /////////////////////////////////////////////////
                UsersCARSEntities dcCCode = new UsersCARSEntities();
                var contracts = await (from prod in dcCCode.CurrancyCode select prod).ToListAsync();

                var contr = (from ctr in contracts select ctr);

                if (String.IsNullOrEmpty(SelectedCurrancyCode))
                {
                    SelectedCurrancyCode = "KGS";
                }
                var UniqueContract = (from Ucontr in contr
                                      group Ucontr by Ucontr.Name into newGroup
                                      where newGroup.Key != null
                                      orderby newGroup.ElementAt(0).ValueToSom
                                      select new { ViewOfMade = newGroup.Key, ValueOfMade = newGroup.Key }).ToList();

                ViewBag.UniqueCurrancyCode = UniqueContract.Select(m => new SelectListItem { Value = m.ViewOfMade, Text = m.ValueOfMade }).ToList();

               ViewBag.ArrayCaption = "";
                ViewBag.orderedPhaetonCode = "";
                ViewBag.orderedPhaeton = "";

                ViewBag.SearchParameters = "";
                if (!String.IsNullOrEmpty(part))
                {
                    ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                    part = part.Replace(" ", string.Empty);
                    part = part.TrimStart(' ', '/');
                    part = part.TrimEnd(' ', '/');
                    Search = part;
                    UsersCARSEntities dc = new UsersCARSEntities();

                    //if (!string.IsNullOrEmpty(SearchStore))
                    // {
                    /////////////////////////////////////////////Currancy code 
                    try { CurrancyCode(); }
                    catch { ViewBag.Error = "Can't import xml file"; }

                    UsersCARSEntities dcv = new UsersCARSEntities();
                    var productCC = await dcv.CurrancyCode.ToListAsync();


                    System.Web.HttpRuntime.Cache.Insert("listOfproductCC", productCC, null,
                      System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(72));

                    object retrievedObject = null;



                    retrievedObject = System.Web.HttpRuntime.Cache["listOfproductCC"];
                    if (retrievedObject == null)
                    {
                        // Отыскать данные (в базе данных, веб-службе и т.д.)
                        object originalData = null;
                        originalData = await dcv.CurrancyCode.ToListAsync();
                        // Сохранить вновь извлеченные данные в кеше
                        System.Web.HttpRuntime.Cache["listOfproductCC"] = originalData;
                        retrievedObject = System.Web.HttpRuntime.Cache["listOfproductCC"];
                    }

                    productCC = (List<CurrancyCode>)(System.Web.HttpRuntime.Cache["listOfproductCC"]);
                    foreach (var item in productCC)
                    {
                        if (item.CodeCur == 840)
                        {
                            ViewBag.USD = item.Value;
                        }
                        if (item.CodeCur == 978)
                        {
                            ViewBag.EUR = item.Value;
                        }
                        if (item.CodeCur == 398)
                        {
                            ViewBag.KZT = item.Value;
                        }
                        if (item.CodeCur == 643)
                        {
                            ViewBag.RUB = item.Value;
                        }
                    }
                    ViewBag.cur = 1;
                    if (SelectedCurrancyCode == "USD")
                    {
                        ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                        ViewBag.cur = 1 / UsdSom(SelectedCurrancyCode);
                    }
                    if (SelectedCurrancyCode == "KGS")
                    {
                        ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                        ViewBag.cur = UsdSom("USD");
                    }
                    if (SelectedCurrancyCode == "" || SelectedCurrancyCode == null)
                    {
                        ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                        ViewBag.cur = 1;
                        ViewBag.CurNull = UsdSom("USD");
                    }

                    if (SelectedCurrancyCode == "Тенге")
                    {
                        ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                        ViewBag.cur = 1 / UsdSom("Тенге");
                    }

                    if (SelectedCurrancyCode == "РУБЛЬ")
                    {
                        ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                        ViewBag.cur = 1 / UsdSom("РУБЛЬ");
                    }
                    if (SelectedCurrancyCode == "EUR")
                    {
                        ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                        ViewBag.cur = 1 / UsdSom("EUR");
                    }

                    /////////////////////////////////////////////////////////////////
                    UsersCARSEntities store = new UsersCARSEntities();

                    var BrendsAutos = await (from prod in store.Remnants where ((prod.Articul.Equals(part) || prod.Name.Contains(part)) && (prod.Brend != "")) select prod).GroupBy(p => p.Brend).ToListAsync();
                    System.Web.HttpRuntime.Cache.Insert("listOfBrendsAutos", BrendsAutos, null,
                      System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(72));

                    object listOfBrendsAutosObject = null;
                    listOfBrendsAutosObject = System.Web.HttpRuntime.Cache["listOfBrendsAutos"];

                    if (listOfBrendsAutosObject == null)
                    {
                        // Отыскать данные (в базе данных, веб-службе и т.д.)
                        object originalData = null;
                        originalData = await (from prod in store.Remnants where ((prod.Articul.Equals(part) || prod.Name.Contains(part)) && (prod.Brend != "")) select prod).GroupBy(p => p.Brend).ToListAsync();
                        // Сохранить вновь извлеченные данные в кеше
                        System.Web.HttpRuntime.Cache["listOfBrendsAutos"] = originalData;
                        listOfBrendsAutosObject = System.Web.HttpRuntime.Cache["listOfBrendsAutos"];
                    }
                    ParallelOptions options = new ParallelOptions();
                    options.MaxDegreeOfParallelism = 1; // -1 is for unlimited. 1 is for sequential.

                    List<string> brendsAuto = new List<string>();
                    var brAutoName = "";
                    var brAutoNameNext = "";
                    Parallel.ForEach((IEnumerable<System.Linq.IGrouping<String, Remnants>>)listOfBrendsAutosObject, options, item =>
                    {

                        if (item.Key.ToString().Split(' ').Count() > 0)
                        {
                            brAutoName = item.Key.ToString().Split(' ')[0].ToLower();
                            if (brAutoName != brAutoNameNext)
                            {
                                brendsAuto.Add(item.Key.ToString());
                            }

                            brAutoNameNext = brAutoName;
                        }
                    });
                    ViewBag.AllBrends = brendsAuto;
                    var BrAuto = brendsAuto;
                    /////////////////////////////////////////////////////////////////

                    List<string> BrendPhaetos = new List<string>();
                    var webAddr = "https://service.tradesoft.ru/3/";
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);

                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Accept = "application/json; charset=utf-8";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        JArray BrendPh = null;

                        ProducerList cust = new ProducerList()
                        {
                            service = "provider",
                            action = "getProducerList",
                            user = "alliance.cl",
                            password = "alliancemotors",
                            timeLimit = 30,
                            container = new List<Container>() {new Container {
                             provider = "phaeton_kz",
                             login = "Alliance",
                             password = "118e7ac8-edd3-11e2-b524-001517a1f012",
                             code = part,
                              }
                             }
                        };
                        String json = JsonConvert.SerializeObject(cust);
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                        try
                        {
                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();
                                ViewBag.Res = result.ToList();
                                var jsonS = JsonConvert.SerializeObject(result);
                                var jObject = JObject.Parse(result);


                                if (jObject.Last.Last.Last.Path.ToString() == "container[0]" || jObject.Last.Last.Last.Last.Last.Path.ToString() == "container[0].error" || Convert.ToString(jObject["container"][0]["error"]) != "")
                                {
                                    ViewBag.FooDJO = "";
                                }
                                else
                                {
                                    ViewBag.FooDJO = jObject;
                                    var data = (JObject)jObject["container"][0]["data"][0];
                                    if (data != null)
                                    {
                                        var producer = (JValue)jObject["container"][0]["data"][0]["producer"];
                                        ViewBag.Producer = producer;
                                        ViewBag.ProducerPhaeton = (JArray)jObject["container"][0]["data"];
                                        BrendPh = (JArray)jObject["container"][0]["data"];
                                    }
                                }
                            }
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        int userID = Convert.ToInt32(Session["LogedUserId"]);
                        string BrendName1 = "";
                        if (BrendPh != null)
                        {

                            foreach (var item in BrendPh)
                            {
                                string BrendName = Convert.ToString(item["producer"]).Split(' ')[0];
                                if (BrendName != BrendName1)
                                {
                                    BrendPhaetos.Add(BrendName);
                                }
                                BrendName1 = BrendName;
                                var brendExist = (from br in dc.PhaetonBrends where br.BrendName == BrendName select br).ToList();
                                if (brendExist.Count > 0)
                                {

                                }
                                else
                                {
                                    dc.uspBrendInsert(Convert.ToInt32(Session["LogedUserId"]), Convert.ToString(item["code"]), Convert.ToString(item["producer"]), Convert.ToString(item["caption"]));
                                }
                            }
                        }


                    }
                    var BrPh = BrendPhaetos;
                    ViewBag.Union = "";
                    var resultsUnion = (from w in brendsAuto
                                        join e in BrendPhaetos on w equals e
                                        select w);
                    ViewBag.Union = resultsUnion.ToList();

                    ///////////////////////////////
                    var UserRole = "";
                    if (Session["LogedUserName"].ToString() != null)
                    {
                        string userID = Session["LogedUserId"].ToString();
                        var vUserRoleEmex = dc.Users.Where(a => a.UserId.ToString().Equals(userID)).FirstOrDefault();

                        if (vUserRoleEmex != null)
                        {
                            UserRole = vUserRoleEmex.UserRole.ToString();
                        }
                    }
                    List<string> BrendEmex = new List<string>();
                    ///поиск цен и наличия по кодду производителя
                    var Login = "";
                    var Password = "";

                    if (UserRole == "emex")
                    {
                        Login = Session["LogedUserName"].ToString();
                        Password = Session["LogedPassword"].ToString();
                    }
                    else
                    {
                        Login = "1210647";
                        Password = "a413cc93";
                    }

                    var httpWebRequestEmex = (HttpWebRequest)WebRequest.Create(webAddr);
                    httpWebRequestEmex.ContentType = "application/json; charset=utf-8";
                    httpWebRequestEmex.Method = "POST";
                    httpWebRequestEmex.Accept = "application/json; charset=utf-8";
                    ViewBag.ProducersOfEmeks = "";
                    List<JValue> ProducersOfEmeks = new List<JValue>();
                    using (var streamWriter = new StreamWriter(httpWebRequestEmex.GetRequestStream()))
                    {
                        ProducerList cust = new ProducerList()
                        {
                            service = "provider",
                            action = "getProducerList",
                            user = "alliance.cl",
                            password = "alliancemotors",
                            timeLimit = 10,
                            container = new List<Container>() {new Container {
                                provider = "emex",
                                login = Login,
                                password = Password,
                                code = part,
                                }
                              }
                        };
                        String json = JsonConvert.SerializeObject(cust);
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();

                        var httpResponseEmex = (HttpWebResponse)httpWebRequestEmex.GetResponse();
                        using (var streamReader = new StreamReader(httpResponseEmex.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            ViewBag.ResEmex = result.ToList();
                            var jsonSEmex = JsonConvert.SerializeObject(result);

                            dynamic jObjectEmex = JObject.Parse(result);
                            ViewBag.FooDJO = jObjectEmex;
                            var tddt = (JArray)jObjectEmex["container"][0]["data"];

                            string brEmex = "";
                            List<JObject> row = new List<JObject>();
                            if (tddt != null)
                            {
                                Parallel.ForEach((JArray)jObjectEmex["container"][0]["data"], options, good =>
                                {
                                    row.Add((JObject)good);
                                    brEmex = Convert.ToString(good);
                                    BrendEmex.Add(brEmex);
                                });

                                ViewBag.ProducersOfEmeks = row;
                            }
                            ViewBag.ProducersOfEmeks = row;
                        }
                    }


                    var BrEmex = BrendEmex;

                    var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();

                    var brend = dc.Remnants.Where(a => a.Articul.Equals(part)).FirstOrDefault();

                    if (brend == null)
                    {
                        ViewBag.SearchParameters = "";
                    }
                    else
                    {
                        ViewBag.BrendName = brend.Brend;
                        ViewBag.ArtName = part;
                    }

                    int allB = 0;
                    var itemBrend = "";
                    foreach (var item in ViewBag.AllBrends)
                    {
                        allB++;

                        if (item != null)
                        {
                            var splitProducer = item.Split(' ');
                            itemBrend = splitProducer[0];
                        }
                    }
                    int allB1 = 0;
                    var itemBrend1 = "";
                    if (ViewBag.ProducerPhaeton != null)
                    {
                        foreach (var item in ViewBag.ProducerPhaeton)
                        {
                            allB1++;
                            itemBrend1 = item["producer"];
                            itemBrend1 = itemBrend1.Split(' ')[0];

                        }

                    }

                    int allB2 = 0;
                    var itemBrend2 = "";

                    if (ViewBag.ProducersOfEmeks != null)
                    {
                        foreach (var item in ViewBag.ProducersOfEmeks)
                        {
                            allB2++;
                            itemBrend2 = item["producer"];
                        }
                    }


                    if ((allB == 0 && allB1==0 && allB2 == 1) ||
                        (allB == 0 && allB1 == 1 && allB2 == 0)||
                        (allB == 1 && allB1 == 0 && allB2 == 0)||

                        (allB == 1 && allB1 == 1 && allB2 == 0 && itemBrend.ToLower()== itemBrend1.ToLower()) ||
                        (allB == 1 && allB1 == 0 && allB2 == 1&& itemBrend.ToLower()== itemBrend2.ToLower()) ||
                        (allB == 0 && allB1 == 1 && allB2 == 1&& itemBrend1.ToLower()== itemBrend2.ToLower())
                        )
                    {
                        return RedirectToAction("SearchDetail", new { producer = itemBrend, code = part, caption= itemBrend });
                    }
                    else {
                        return View();
                    }


                }
                else
                {
                    ViewBag.AllBrends = "";
                    ViewBag.ProducerPhaeton = "";
                    ViewBag.ProducersOfEmeks = "";
                    return View();
                }




            }
            else
            {
                Session["Part"] = part;
                Session["SelectedCurrancyCode"] = SelectedCurrancyCode;

                return RedirectToAction("LoginSearch");
            }
        }


        /// сортировка с удаленных складов по нименованию, бренду и т.д.
        public static List<usmSelectAllRemnantsByStoreMinSklad_Result> SortSearchStore(string SortOrder, List<usmSelectAllRemnantsByStoreMinSklad_Result> arrayJ, string sort)
        {

            if (arrayJ != null && sort == "down")
            {
                switch (SortOrder)
                {
                    case "Articul":
                        arrayJ = arrayJ.OrderBy(prod => prod.Articul).ToList();
                        break;
                    case "Brend":
                        arrayJ = arrayJ.OrderBy(prod => prod.Brend).ToList();
                        break;
                    case "NameGood":
                        arrayJ = arrayJ.OrderBy(prod => prod.Name).ToList();
                        break;
                    case "Store":
                        arrayJ = arrayJ.OrderBy(prod => prod.Sklad).ToList();
                        break;
                    case "Price":
                        arrayJ = arrayJ.OrderBy(prod => prod.Price).ToList();
                        break;
                    case "Existence":
                        arrayJ = arrayJ.OrderBy(prod => prod.Existence).ToList();
                        break;
                    default:
                        arrayJ = arrayJ.ToList();
                        break;
                }
            }

            if (arrayJ != null && sort == "up")
            {
                switch (SortOrder)
                {
                    case "Articul":
                        arrayJ = arrayJ.OrderBy(prod => prod.Articul).ToList();
                        break;
                    case "Brend":
                        arrayJ = arrayJ.OrderBy(prod => prod.Brend).ToList();
                        break;
                    case "NameGood":
                        arrayJ = arrayJ.OrderBy(prod => prod.Name).ToList();
                        break;
                    case "Store":
                        arrayJ = arrayJ.OrderBy(prod => prod.Sklad).ToList();
                        break;
                    case "Price":
                        arrayJ = arrayJ.OrderBy(prod => prod.Price).Reverse().ToList();
                        break;
                    case "Existence":
                        arrayJ = arrayJ.OrderBy(prod => prod.Existence).ToList();
                        break;
                    default:
                        arrayJ = arrayJ.ToList();
                        break;
                }
            }

            return arrayJ;
        }


        /// /////////////tradesoft сортировка armteck
        public static JArray SortSearchPhaeton(string SortOrder, JArray arrayJ, string sort)
        { /////////сортировака армтек  через tradesoft

            var arrj = arrayJ;


            if (arrayJ != null && sort == "down")
            {
                switch (SortOrder)
                {
                    case "Articul":

                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.First)));

                        break;
                    case "Brend":
                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.First)));

                        break;
                    case "NameGood":
                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.First)));

                        break;
                    case "Store":
                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.Next.Next.First)));

                        break;
                    case "Price":
                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.First)));

                        break;
                    case "Existence":
                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.Next.First)));

                        break;
                    default:
                        arrj = new JArray(arrayJ);

                        break;
                }
            }


            if (arrayJ != null && sort == "up")
            {
                switch (SortOrder)
                {
                    case "Articul":

                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.First)));

                        break;
                    case "Brend":
                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.First)));

                        break;
                    case "NameGood":
                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.First)));

                        break;
                    case "Store":
                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.Next.Next.First)));

                        break;
                    case "Price":
                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.First)));

                        break;
                    case "Existence":
                        arrj = new JArray(arrayJ.OrderBy((prod => prod.First.Next.First)));

                        break;
                    default:
                        arrj = new JArray(arrayJ);

                        break;
                }
            }



            return arrayJ;
        }

        public static JArray SortSearchEmex(string SortOrder, JArray arrayJ)
        { /////////сортировака армтек  через tradesoft

            if (arrayJ != null)
            {
                switch (SortOrder)
                {
                    case "Articul":
                        arrayJ = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.First)));

                        break;
                    case "Brend":

                        arrayJ = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.First)));

                        break;
                    case "NameGood":

                        arrayJ = new JArray(arrayJ.OrderBy((prod => prod.First.First)));

                        break;
                    case "Store":

                        arrayJ = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.Next.Next.First)));


                        break;
                    case "Price":
                        arrayJ = new JArray(arrayJ.OrderBy((prod => prod.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.First)));

                        break;
                    case "Existence":

                        arrayJ = new JArray(arrayJ.OrderBy((prod => prod.First.Next.First)));

                        break;
                    default:
                        arrayJ = new JArray(arrayJ);


                        break;
                }
            }
            return arrayJ;
        }

        [ChildActionOnly]
        public double SummCart()
        {

            double sum = 0;
            int quant = 0;
            double sumRS = 0;
            int quantRS = 0;


            if (Session["OrderNowRemoteStore"] == null)
            {
                quantRS = 0;
                sumRS = 0;
            }
            else
            {
                foreach (ItemRemoteStore item in (List<ItemRemoteStore>)Session["OrderNowRemoteStore"])
                {
                    sumRS = (double)(item.Pr.Price * item.Quantity) + sumRS;
                    quantRS = item.Quantity + quantRS;
                }
            }



            if (Session["cart"] == null)
            {
                quant = 0;
                sum = 0;
            }
            else
            {

                foreach (Item item in (List<Item>)Session["cart"])
                {
                    sum = (double)(item.Pr.Price * item.Quantity) + sum;
                    quant = item.Quantity + quant;

                }
            }




            return sum + sumRS;

        }

        [ChildActionOnly]
        public double CountCart()
        {

            double sum = 0;
            int quant = 0;
            double sumRS = 0;
            int quantRS = 0;


            if (Session["OrderNowRemoteStore"] == null)
            {
                quantRS = 0;
                sumRS = 0;
            }
            else
            {
                foreach (ItemRemoteStore item in (List<ItemRemoteStore>)Session["OrderNowRemoteStore"])
                {
                    sumRS = (double)(item.Pr.Price * item.Quantity) + sumRS;
                    quantRS = item.Quantity + quantRS;
                }
            }



            if (Session["cart"] == null)
            {
                quant = 0;
                sum = 0;
            }
            else
            {

                foreach (Item item in (List<Item>)Session["cart"])
                {
                    sum = (double)(item.Pr.Price * item.Quantity) + sum;
                    quant = item.Quantity + quant;

                }
            }




            return quant + quantRS;

        }



        public static JArray PaginationEmex(JArray arrayJ)
        {
            //  arrayJ = arrayJ.Skip(10);
            return arrayJ;
        }

        public static JArray PaginationPhaeton(JArray arrayJ)
        {
            // arrayJ = arrayJ.Skip(10);
            return arrayJ;

        }


        [DonutOutputCache(Duration = 10, VaryByParam = "*", Location = System.Web.UI.OutputCacheLocation.Downstream)]
        public ActionResult SearchDetail(string caption, string PageEmex, string SortOrder, string producer, string code, string SelectedAuto, string Vin, Users u, string Search, string SearchStore, string SelectedCurrancyCode, string sort, string Pagination)
        {
            
            string brendCaption = caption;
           
            UsersCARSEntities dc1 = new UsersCARSEntities();

            ////////////наценка на бренд фаетон
            var markupPhaeton = dc1.usmSelectBrendPhaeton1(producer).FirstOrDefault();

            if (markupPhaeton != null)
            {
                ViewBag.markupPhaeton = markupPhaeton.Markup;
                if (ViewBag.markupPhaeton == null) { ViewBag.markupPhaeton = 1; }
            }
            else
            { ViewBag.markupPhaeton = 1; }


            //////////наценка на все бренды phaeton 
            //var markupAllPhaeton = dc1.usmSelectBrendPhaetonByID1(5).FirstOrDefault();

            //if (markupAllPhaeton != null)
            //{ ViewBag.Procent = markupAllPhaeton.Markup; }
            //else
            //{ }
            ViewBag.Procent = 1.10;



            var OriginalAutos = dc1.usmSelectNameOriginalAuto().ToList();

            var curCode = dc1.usmSelectCurrancyCode().ToList();

            ViewBag.PagePh = "";
            ViewBag.PageEmex = "";
            if (Session["LogedUserId"] != null)
            {   //////////////////////////скидка для каждого пользователя для Emex
                int discOfEmex = Convert.ToInt32(Session["LogedUserId"].ToString());
                var discountOfEmex = dc1.usmSelectUserID(discOfEmex).FirstOrDefault();
                if (discountOfEmex.Discount != null && discountOfEmex.Discount != 0)
                {
                    ViewBag.discountOfEmex = discountOfEmex.Discount;
                }
                else
                {
                    ViewBag.discountOfEmex = null;
                }

                //////////////////////////////////////////////////////////
                code = code.ToLower();

                string brendProducer = caption.ToLower();
                ViewBag.Producer = producer;

                if (producer != null)
                {
                    // var splitProducer = producer.Split(' ');
                    //  producer = splitProducer[0];
                    producer = producer.ToLower();
                }

                UsersCARSEntities dcAuto = new UsersCARSEntities();

                var Auto = OriginalAutos;
                var contrAuto = (from ctr in Auto select ctr);
                if (!String.IsNullOrEmpty(SelectedAuto))
                { contrAuto = contrAuto.AsParallel().Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim())); }

                var UniqueAuto = (from Ucontr in contrAuto
                                  group Ucontr by Ucontr.NameAuto into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { ViewOfAuto = newGroup.Key }).ToList();
                ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();
                ViewBag.Auto = SelectedAuto;

                if (SelectedAuto == "NISSAN")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin="; }
                if (SelectedAuto == "AUDIVW")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin="; }
                if (SelectedAuto == "BMW")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin="; }
                if (SelectedAuto == "HONDA")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin="; }
                if (SelectedAuto == "MAZDA")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin="; }
                if (SelectedAuto == "MERCEDES")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin="; }
                if (SelectedAuto == "OPEL")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin="; }
                if (SelectedAuto == "SUBARU")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin="; }
                if (SelectedAuto == "TOYOTA")
                { ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin="; }
                ViewBag.Vin = Vin;

                ViewBag.Discounts = null;
                //  Скидка на наш склад
                var discount_brend = dc1.usmSelectDiscountBrend(producer, discOfEmex).FirstOrDefault();

                if (discount_brend != null && discount_brend.Discount != null)
                {
                    ViewBag.Discounts = discount_brend.Discount;
                }
                else
                {
                    ViewBag.Discounts = null;
                }

                /////////////////////////////////////////////////
                UsersCARSEntities dcCCode = new UsersCARSEntities();
                var contracts = curCode;

                var contr = (from ctr in contracts select ctr).AsParallel();

                if (String.IsNullOrEmpty(SelectedCurrancyCode))
                {
                    SelectedCurrancyCode = "KGS";
                }
                var UniqueContract = (from Ucontr in contr
                                      group Ucontr by Ucontr.Name into newGroup
                                      where newGroup.Key != null
                                      orderby newGroup.ElementAt(0).ValueToSom
                                      select new { ViewOfMade = newGroup.Key, ValueOfMade = newGroup.Key }).ToList();

                ViewBag.UniqueCurrancyCode = UniqueContract.AsParallel().Select(m => new SelectListItem { Value = m.ViewOfMade, Text = m.ValueOfMade }).ToList();
                ViewBag.orderedPhaetonCode = "";
                ViewBag.orderedPhaeton = "";
                ViewBag.ArrayCaption = "";

                ViewBag.SearchParameters = "";
                if (!String.IsNullOrEmpty(code))
                {
                    ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                    code = code.Replace(" ", string.Empty);
                    code = code.TrimStart(' ', '/');
                    code = code.TrimEnd(' ', '/');
                    Search = code;
                    UsersCARSEntities dc = new UsersCARSEntities();
                    if (!string.IsNullOrEmpty(Search))
                    {
                        ViewBag.ArtName = code;
                        /////////////////////////////////////////////Currancy Code NBKR KG

                        var productCC = curCode;


                        Parallel.For(0, productCC.Count, i =>
                        {
                            if (productCC[i].CodeCur == 840)
                            {
                                ViewBag.USD = productCC[i].Value;
                            }
                            else
                            {
                                if (productCC[i].CodeCur == 978)
                                {
                                    ViewBag.EUR = productCC[i].Value;
                                }
                                else
                                {
                                    if (productCC[i].CodeCur == 398)
                                    {
                                        ViewBag.KZT = productCC[i].Value;
                                    }
                                    else
                                    {
                                        if (productCC[i].CodeCur == 643)
                                        {
                                            ViewBag.RUB = productCC[i].Value;
                                        }
                                    }
                                }
                            }
                        });
                        ViewBag.cur = 1;
                        if (SelectedCurrancyCode == "USD")
                        {
                            ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                            ViewBag.cur = 1 / UsdSom(SelectedCurrancyCode);
                        }
                        else
                        {

                            if (SelectedCurrancyCode == "KGS")
                            {
                                ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                                ViewBag.cur = UsdSom("USD");
                            }
                            else
                            {

                                if (SelectedCurrancyCode == "" || SelectedCurrancyCode == null)
                                {
                                    ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                                    ViewBag.cur = 1;
                                    ViewBag.CurNull = UsdSom("USD");
                                }
                                else
                                {

                                    if (SelectedCurrancyCode == "Тенге")
                                    {
                                        ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                                        ViewBag.cur = 1 / UsdSom("Тенге");
                                    }
                                    else
                                    {

                                        if (SelectedCurrancyCode == "РУБЛЬ")
                                        {
                                            ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                                            ViewBag.cur = 1 / UsdSom("РУБЛЬ");
                                        }
                                        else
                                        {
                                            if (SelectedCurrancyCode == "EUR")
                                            {
                                                ViewBag.SelectedCurrancyCode = SelectedCurrancyCode;
                                                ViewBag.cur = 1 / UsdSom("EUR");
                                            }
                                        }


                                    }

                                }
                            }
                        }

                        /////////////////////////////////////////////////
                        ViewBag.SearchParameters = "";
                        ViewBag.SearchParametersOsh = "";
                        ///////////////////////////////////////////////////
                        ViewBag.ArtName = code;
                        //  var  originalData = dc.usmSelectSearch(code, brendProducer);
                        var prodRemAnalog = dc.usmSelectAllRemnants().ToList();
                        var ProdRemAnalogs = prodRemAnalog;


                        //////////////////////////////////////////////////////////////////
                        //////////Артикулы которые на Распродажа 
                        var ArticulSale = dc.usmSelectSaleByArticul(code).FirstOrDefault();
                        ViewBag.ArticulSale = "";

                        if (ArticulSale != null)
                        {
                            ViewBag.ArticulSale = ArticulSale.Articul;
                        }
                      

                        string uriArticul = "http://212.112.123.55:88/YT1/odata/standard.odata/Catalog_Номенклатура?$filter=substringof('"+code+"', Артикул)&$format=json";
                        WebClient client = new WebClient();
                        client.Encoding = System.Text.Encoding.UTF8;

                       
                        var data = client.DownloadString(uriArticul);
                       // ViewBag.data = data;

                        JObject obj = JObject.Parse(data);

                        //ViewBag.data = obj["value"];

                        var DataArray = (JArray)obj["value"];
                        foreach (var it in DataArray) {

                            string BrendKey = Convert.ToString(it["Бренд_Key"]);

                            if (BrendKey != "00000000-0000-0000-0000-000000000000")
                            {
                               string uriBrend = "http://212.112.123.55:88/YT1/odata/standard.odata/Catalog_Бренды(guid'" + BrendKey + "')?$format=json";
                               WebClient clientBrend_Key = new WebClient();
                               clientBrend_Key.Encoding = System.Text.Encoding.UTF8;
                               var dataBrend_Key = clientBrend_Key.DownloadString(uriBrend);
                               JObject objBrend = JObject.Parse(dataBrend_Key);
                               var dataBrend = (JValue)objBrend["Description"];
                                string dataBr = Convert.ToString(dataBrend);

                               it["БрендПоступление_Key"] = dataBr;
                            }



                            ///////
                            string uriSclad = "http://212.112.123.55:88/YT1/odata/standard.odata/InformationRegister_ЦеныНоменклатуры(Recorder='a893641a-4b2c-11e7-b305-60a44c3f8064', Recorder_Type='StandardODATA.Document_УстановкаЦенНоменклатуры')";
                            WebClient clientSclad_Key = new WebClient();
                            clientSclad_Key.Encoding = System.Text.Encoding.UTF8;
                            var dataSclad_Key = clientSclad_Key.DownloadString(uriSclad);
                            JObject objSclad = JObject.Parse(dataSclad_Key);
                            var dataSclad = (JValue)objSclad["Description"];
                            string dataSc = Convert.ToString(dataSclad);

                            it["БрендПоступление_Key"] = dataSc;


                        }
                        ViewBag.data = DataArray;
                        //var ProductsRemnants1 = dc.usmSelectAllRemnantsByStoreMinSklad(code, brendProducer).ToList();

                        //if (ProductsRemnants1.Count != 0)
                        //{
                        //    ViewBag.SearchParameters = ProductsRemnants1.ToList();
                        //}


                        //(List<usmSelectAllRemnantsByStoreMinSklad_Result>)


                        ////////////////////////sorting 
                        if (SortOrder != null)
                        {
                            ViewBag.SearchParameters = SortSearchStore(SortOrder, ViewBag.SearchParameters, sort);
                        }




                        //var ProductsRemnantsOsh = dc.usmAllianceSelectAllRemnantsByStore000000006(code, brendProducer).ToList();
                        //if (ProductsRemnantsOsh.Count != 0)
                        //{
                        //    ViewBag.SearchParameters = ProductsRemnants1.ToList();
                        //    //////////////////////////////////////////////
                        //    ViewBag.SearchParametersOsh = ProductsRemnantsOsh.ToList();

                        //}

                        //if (ProductsRemnants1.Where(a => a.Sklad.Equals("000000006")).FirstOrDefault() != null)
                        //{
                        //    ViewBag.SearchParameters = "";

                        //}


                        ////////////////////////////webAnalog
                        var fullAnalogWithDiscunts =dc.usmSelectAllAnalogsMinSklad1(code, producer).ToList();

                        ParallelOptions options = new ParallelOptions();
                        options.MaxDegreeOfParallelism = 1;

                        ///////////////////////////////////////////////////////////////////////////
                        int anI = 0;
                        Parallel.ForEach(fullAnalogWithDiscunts, options, j =>
                        {
                            anI++;
                            string brendProd = j.Brend.Split(' ')[0].ToString();

                            var discountByBrend = dc.usmSelectDiscountBrend(brendProd, discOfEmex).FirstOrDefault();

                            var SaleArticulAnalog = dc.usmSelectSaleByArticul(j.Articul).FirstOrDefault();

                            if (SaleArticulAnalog != null)
                            {
                                j.ColorOfSalon = "sale";
                            }


                            if (anI <= fullAnalogWithDiscunts.Count)
                            {
                                if (discountByBrend != null && discountByBrend.Discount != null)
                                {
                                    j.NumberOfBody = Convert.ToString(discountByBrend.Discount);
                                }
                                else
                                {
                                    j.NumberOfBody = "0";
                                }
                            }
                        });
                        /////////////////////////////////////////




                        var arrAnalogs = fullAnalogWithDiscunts;

                                    ViewBag.ArrAnalogDiscount = arrAnalogs;

                                    ViewBag.ArrAnalog = fullAnalogWithDiscunts;
                           

                        /////////////////////////////End Analog


                        List<JObject> Allrow = new List<JObject>();

                        /////////////////////////////////////////////////////////////////
                        var webAddr = "https://service.tradesoft.ru/3/";
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                        var httpWebRequest1 = (HttpWebRequest)WebRequest.Create(webAddr);

                        httpWebRequest1.ContentType = "application/json; charset=utf-8";
                        httpWebRequest1.Method = "POST";
                        httpWebRequest1.Accept = "application/json; charset=utf-8";
                        using (var streamWriter = new StreamWriter(httpWebRequest1.GetRequestStream()))
                        {
                            ///поиск цен и наличия по коду производителя
                            ProducerList priceAndExist = new ProducerList()
                            {
                                service = "provider",
                                action = "getPriceList",
                                user = "alliance.cl",
                                password = "alliancemotors",
                                timeLimit = 10,
                                container = new List<Container>() {new Container {
                                       provider = "phaeton_kz",
                                       login = "Alliance",
                                       password = "118e7ac8-edd3-11e2-b524-001517a1f012",
                                       code = code,
                                       producer=producer
                                       }
                                     }
                            };
                            String price = JsonConvert.SerializeObject(priceAndExist);
                            streamWriter.Write(price);
                            streamWriter.Flush();
                            streamWriter.Close();

                            try
                            {
                                var httpResponsePriceExist = (HttpWebResponse)httpWebRequest1.GetResponse();
                                using (var streamReader = new StreamReader(httpResponsePriceExist.GetResponseStream()))
                                {
                                    var result = streamReader.ReadToEnd();
                                    var jsonS = JsonConvert.SerializeObject(result);
                                    var jObject1 = JObject.Parse(result);
                                    ViewBag.Price = jObject1;
                                    if (jObject1.Last.Last.Last.Last.Last.Path.ToString() == "container[0].error" || Convert.ToString(jObject1["container"][0]["error"]) != "")
                                    {
                                        ViewBag.orderedPhaetonCode = "";
                                        ViewBag.orderedPhaeton = "";
                                    }
                                    else
                                    {
                                        var producerPrice12 = jObject1["container"][0]["data"];
                                        if (producerPrice12.First != null)
                                        {
                                            var producerPrice = jObject1["container"][0]["data"][0];
                                            ViewBag.ProducerPrice = producerPrice;
                                            var producerPriceCount = (JArray)jObject1["container"][0]["data"];
                                            ViewBag.producerPriceCount = producerPriceCount;
                                            ViewBag.service = "Phaeton";

                                            ////////////////////////sorting 
                                            if (SortOrder != null)
                                            {
                                                ViewBag.orderedPhaetonCode = producerPriceCount;
                                                ViewBag.orderedPhaeton = producerPriceCount;
                                            }
                                            JArray orderedPhaetonCode = new JArray(producerPriceCount.OrderBy(prod => prod.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.First).Where(x => Convert.ToString(x.First.Next.Next.Next.Next.Next.Next.Next.First).Replace(" ", string.Empty).Trim().ToLower() == code));
                                            JArray orderedPhaeton = new JArray(producerPriceCount.OrderBy(prod => prod.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.First).Where(x => Convert.ToString(x.First.Next.Next.Next.Next.Next.Next.Next.First).Replace(" ", string.Empty).Trim().ToLower() != code));
                                            ViewBag.orderedPhaetonCode = orderedPhaetonCode.Take(10);
                                            ViewBag.orderedPhaeton = orderedPhaeton.Take(10);


                                            ////////////////////////pagination Phaeton
                                            if (Pagination != null)
                                            {
                                                ViewBag.PagePh = "phaeton";
                                                ViewBag.orderedPhaetonCode = orderedPhaetonCode;
                                                ViewBag.orderedPhaeton = orderedPhaeton;
                                            }
                                        }
                                        else
                                        {
                                            ViewBag.orderedPhaetonCode = "";
                                            ViewBag.orderedPhaeton = "";
                                        }
                                    }

                                }
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }
                        ////////////////////////////////////////////Start Emex
                        var webAddrEmex = "https://service.tradesoft.ru/3/";
                        var httpWebRequestEmex = (HttpWebRequest)WebRequest.Create(webAddrEmex);
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                        List<List<JObject>> rowAll = new List<List<JObject>>();
                        List<JObject> rowProducerOfEmex = new List<JObject>();
                        var httpWebRequestEmex1 = (HttpWebRequest)WebRequest.Create(webAddrEmex);

                        httpWebRequestEmex1.ContentType = "application/json; charset=utf-8";
                        httpWebRequestEmex1.Method = "POST";
                        httpWebRequestEmex1.Accept = "application/json; charset=utf-8";
                        UsersCARSEntities dcEmex = new UsersCARSEntities();

                        var UserRole = "";
                        if (Session["LogedUserName"].ToString() != null)
                        {
                            string userID = Session["LogedUserId"].ToString();
                            var vUserRoleEmex = dcEmex.Users.Where(a => a.UserId.ToString().Equals(userID)).FirstOrDefault();

                            if (vUserRoleEmex != null)
                            {
                                UserRole = vUserRoleEmex.UserRole.ToString();
                            }
                        }
                        using (var streamWriter = new StreamWriter(httpWebRequestEmex1.GetRequestStream()))
                        {
                            ///поиск цен и наличия по кодду производителя
                            var Login = "";
                            var Password = "";

                            if (UserRole == "emex")
                            {
                                Login = Session["LogedUserName"].ToString();
                                Password = Session["LogedPassword"].ToString();
                            }
                            else
                            {
                                Login = "1210647";
                                Password = "a413cc93";
                            }

                            PreOrderSearch priceAndExist = new PreOrderSearch()
                            {
                                service = "provider",
                                action = "PreOrderSearch",
                                user = "alliance.cl",
                                password = "alliancemotors",
                                timeLimit = 10,
                                container = new List<Container>() {new Container {
                                provider = "emex",
                                login =Login,
                                password =Password,
                                code = code,
                                producer= producer,
                                itemHash=null
                                }
                                   }
                            };
                            String price = JsonConvert.SerializeObject(priceAndExist);
                            streamWriter.Write(price);
                            streamWriter.Close();
                            // streamWriter.Flush();
                            List<JObject> rowProductsEmex = new List<JObject>();
                            var httpResponsePriceExistEmex = (HttpWebResponse)httpWebRequestEmex1.GetResponse();
                            using (var streamReader = new StreamReader(httpResponsePriceExistEmex.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();

                                var jsonS = JsonConvert.SerializeObject(result);
                                if (ViewBag.ProducerEmex != "")
                                {
                                    JObject jObjectEmex1 = JObject.Parse(result);
                                    ViewBag.PriceEmex = jObjectEmex1;


                                    var producerPriceEmex1 = jObjectEmex1["container"][0]["items"];

                                    if (jObjectEmex1.Last.Last.Last.Last.Last.Path.ToString() != "container[0].error")
                                    {
                                        if (producerPriceEmex1 != null)
                                        {
                                            if (producerPriceEmex1.First != null)
                                            {
                                                var producerPriceEmex = jObjectEmex1["container"][0]["items"][0];
                                                ViewBag.ProducerPrice = producerPriceEmex;
                                                var producerPriceCount = (JArray)jObjectEmex1["container"][0]["items"];


                                                JArray orderedEmexCode = new JArray(producerPriceCount.OrderBy(prod => (Double)prod.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.First).Where(x => Convert.ToString(x.First.Next.Next.Next.Next.Next.Next.Next.First).Replace(" ", string.Empty).Trim().ToLower() == code));
                                                JArray orderedEmex = new JArray(producerPriceCount.OrderBy(prod => (Double)prod.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.First).Where(x => Convert.ToString(x.First.Next.Next.Next.Next.Next.Next.Next.First).Replace(" ", string.Empty).Trim().ToLower() != code));

                                                ////////////////////////sorting 
                                                if (SortOrder != null)
                                                {
                                                    ViewBag.orderedEmexCode = SortSearchEmex(SortOrder, orderedEmexCode);
                                                    ViewBag.orderedEmex = SortSearchEmex(SortOrder, orderedEmex);
                                                }
                                              ViewBag.orderedEmexCode = orderedEmexCode.Take(10);
                                              ViewBag.orderedEmex = orderedEmex.Take(10);


                                                ////////////////////////pagination Emex
                                                if (PageEmex != null)
                                                {
                                                    ViewBag.PageEmex = "emex";

                                                    ViewBag.orderedEmexCode = orderedEmexCode;// PaginationEmex(orderedEmexCode);
                                                    ViewBag.orderedEmex = orderedEmex;// PaginationEmex(orderedEmex);
                                                }

                                            }
                                            else
                                            {
                                                ViewBag.orderedEmexCode = "";
                                                ViewBag.orderedEmex = "";
                                                ViewBag.ArrayCaptionEmex = "";
                                                rowProducerOfEmex = null;
                                            }


                                        }
                                        else
                                        {
                                            ViewBag.orderedEmexCode = "";
                                            ViewBag.orderedEmex = "";

                                            ViewBag.orderedEmexCode = "";
                                            ViewBag.orderedEmex = "";
                                            rowProducerOfEmex = null;
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.orderedEmexCode = "";
                                        ViewBag.orderedEmex = "";

                                        rowProducerOfEmex = null;
                                    }

                                }
                                else
                                {
                                    ViewBag.orderedEmexCode = "";
                                    ViewBag.orderedEmex = "";

                                }
                            }
                        }

                        ////////////////////////////////////////////End Emex

                        ViewBag.BrendName = producer;
                        ViewBag.ArtName = code;


                        if (ViewBag.SearchParameters != null)
                        {
                            ViewBag.SearchParameters = ViewBag.SearchParameters;

                            ViewBag.SearchParametersOsh = ViewBag.SearchParametersOsh;
                        }
                        else
                        {
                            ViewBag.SearchParametersOsh = "";
                            ViewBag.SearchParameters = "";
                        }
                    }
                    else
                    {
                        ViewBag.SearchParameters = "";
                        return View();
                    }
                }
                else
                {
                    ViewBag.orderedPhaetonCode = "";
                    ViewBag.orderedPhaeton = "";

                    ViewBag.orderedEmexCode = "";
                    ViewBag.orderedEmex = "";

                }
                return View();
            }
            else
            {
                return RedirectToAction("LoginSearch");
            }

        }

        [HttpPost]
        public JsonResult GetEmployee(string Part)
        {
            List<Remnants> comps = new List<Remnants>();
            UsersCARSEntities dc = new UsersCARSEntities();
            var products = (from prod in dc.Remnants
                            where prod.Articul == Part
                            select prod).ToListAsync();

            var ProductsRemnants = comps.Where(a => a.Articul.Contains(Part)).Select(a => new { value = a.Articul }).Distinct().ToList();

            return Json(ProductsRemnants, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompleteSearch(string term)
        {
            UsersCARSEntities dc = new UsersCARSEntities();

            var originalData = (from prod in dc.Remnants.AsParallel()
                                where prod.Articul.ToLower() == term || prod.Name.ToLower().Contains(term)
                                select prod).ToList();
            return Json(originalData.Select(a => new { value = a.Articul.ToString(), text = a.Name.ToString() }).Select(a => new { label = a.value.ToString() + " " + a.text.ToString(), value = a.value.ToString() }).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoginSearch(string SelectedAuto, string Vin, string SelectedCurrancyCode)
        {
            UsersCARSEntities dcAuto = new UsersCARSEntities();
            var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();
            var contrAuto = (from ctr in Auto select ctr);
            if (!String.IsNullOrEmpty(SelectedAuto))
            {
                contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
            }

            var UniqueAuto = (from Ucontr in contrAuto
                              group Ucontr by Ucontr.NameAuto into newGroup
                              where newGroup.Key != null
                              orderby newGroup.Key
                              select new { ViewOfAuto = newGroup.Key }).ToList();
            ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

            ViewBag.Auto = SelectedAuto;
            UsersCARSEntities dcCCode = new UsersCARSEntities();
            var contracts = (from prod in dcCCode.CurrancyCode select prod).ToList();

            var contr = (from ctr in contracts select ctr);

            if (String.IsNullOrEmpty(SelectedCurrancyCode))
            {
                SelectedCurrancyCode = "KGS";
            }
            var UniqueContract = (from Ucontr in contr
                                  group Ucontr by Ucontr.Name into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.ElementAt(0).ValueToSom
                                  select new { ViewOfMade = newGroup.Key, ValueOfMade = newGroup.Key }).ToList();

            ViewBag.UniqueCurrancyCode = UniqueContract.Select(m => new SelectListItem { Value = m.ViewOfMade, Text = m.ValueOfMade }).ToList();



            if (SelectedAuto == "NISSAN")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
            }
            if (SelectedAuto == "AUDIVW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
            }
            if (SelectedAuto == "BMW")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
            }
            if (SelectedAuto == "HONDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
            }
            if (SelectedAuto == "MAZDA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
            }
            if (SelectedAuto == "MERCEDES")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
            }
            if (SelectedAuto == "OPEL")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
            }
            if (SelectedAuto == "SUBARU")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
            }
            if (SelectedAuto == "TOYOTA")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
            }


            ViewBag.Vin = Vin;
            return View();
        }

        [HttpPost]
        public ActionResult LoginSearch(Users u, string SelectedAuto, string Vin, string SelectedCurrancyCode)
        {
            if (!ModelState.IsValid)
            {
                UsersCARSEntities dcAuto = new UsersCARSEntities();
                var Auto = (from prod in dcAuto.NameOriginalAuto select prod).ToList();
                var contrAuto = (from ctr in Auto select ctr);
                if (!String.IsNullOrEmpty(SelectedAuto))
                {
                    contrAuto = contrAuto.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
                }

                var UniqueAuto = (from Ucontr in contrAuto
                                  group Ucontr by Ucontr.NameAuto into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { ViewOfAuto = newGroup.Key }).ToList();
                ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

                ViewBag.Auto = SelectedAuto;
                UsersCARSEntities dcCCode = new UsersCARSEntities();
                var contracts = (from prod in dcCCode.CurrancyCode select prod).ToList();

                var contr = (from ctr in contracts select ctr);

                if (String.IsNullOrEmpty(SelectedCurrancyCode))
                {
                    SelectedCurrancyCode = "KGS";
                }
                var UniqueContract = (from Ucontr in contr
                                      group Ucontr by Ucontr.Name into newGroup
                                      where newGroup.Key != null
                                      orderby newGroup.ElementAt(0).ValueToSom
                                      select new { ViewOfMade = newGroup.Key, ValueOfMade = newGroup.Key }).ToList();

                ViewBag.UniqueCurrancyCode = UniqueContract.Select(m => new SelectListItem { Value = m.ViewOfMade, Text = m.ValueOfMade }).ToList();



                if (SelectedAuto == "NISSAN")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
                }
                if (SelectedAuto == "AUDIVW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/audivw/?vin=";
                }
                if (SelectedAuto == "BMW")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/bmw/?vin=";
                }
                if (SelectedAuto == "HONDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/honda/?vin=";
                }
                if (SelectedAuto == "MAZDA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mazda/?vin=";
                }
                if (SelectedAuto == "MERCEDES")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/mercedes/?vin=";
                }
                if (SelectedAuto == "OPEL")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/opel/?vin=";
                }
                if (SelectedAuto == "SUBARU")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/subaru/?vin=";
                }
                if (SelectedAuto == "TOYOTA")
                {
                    ViewBag.Url = "http://catalog.alliance-motors.kg/toyota/?vin=";
                }
                ViewBag.Vin = Vin;

                using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                    var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserId"] = v.UserId.ToString();
                        Session["LogedUserFullName"] = v.FullName.ToString();
                        Session["LogedFamilyName"] = v.FamilyName.ToString();
                        Session["LogedContactPhone"] = v.ContactPhone.ToString();
                        Session["LogedEmail"] = v.Email.ToString();
                        Session["LogedUserName"] = v.UserName.ToString();
                        Session["LogedPassword"] = v.Password.ToString();
                        Session["LogedUserRole"] = v.UserRole.ToString();
                        FormsAuthentication.SetAuthCookie(u.UserName, true);

                        string part = Convert.ToString(Session["Part"]);
                        //string
                        //Session["SelectedCurrancyCode"] = SelectedCurrancyCode;

                        string id = Session["LogedUserId"].ToString();
                        string xmlPathCart = Server.MapPath("~/Content/xml/cart" + id + ".xml");
                        string xmlPathCartRemoteStore = Server.MapPath("~/Content/xml/CartRemoteStore" + id + ".xml");
                        ReadCart(xmlPathCart, (List<Item>)Session["cart"]);
                        ReadCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
                        return RedirectToAction("Search", new { SelectedCurrancyCode = "KGS", Part = part });

                    }
                    else
                    {
                        ViewBag.MessageLogin = "Вы не правильно ввели логин или пароль";
                    }
                }
            }
            return View(u);
        }

        public ActionResult VinSearch(string part, Users u, string VinCode, string Search, string SearchStore)
        {


            UsersCARSEntities dcss = new UsersCARSEntities();

            var modelsee = dcss.Remnants.Where(a => a.Articul.Contains("kl9"))
                             .Select(a => new { value = a.Articul })
                             .Select(a => new { label = a.value })
                             .Distinct();

            ViewBag.models = modelsee;

            ////////////webanalog
            List<String> row = new List<String>();
            var webAddrAnalogBrend = "https://service.tradesoft.ru/3/";
            var httpWebRequestAnalogBrend = (HttpWebRequest)WebRequest.Create(webAddrAnalogBrend);
            httpWebRequestAnalogBrend.ContentType = "application/json; charset=utf-8";
            httpWebRequestAnalogBrend.Method = "POST";
            httpWebRequestAnalogBrend.Accept = "application/json; charset=utf-8";


            using (var streamWriter = new StreamWriter(httpWebRequestAnalogBrend.GetRequestStream()))
            {
                VebAnalogBrends analog = new VebAnalogBrends()
                {
                    user = "alliance.cl",
                    password = "alliancemotors",
                    service = "analog",
                    action = "getProducersAdv",
                    rating = 1,
                    data = new List<string>() {
                        "kl9"
                    }
                };
                String jsonAnalog = JsonConvert.SerializeObject(analog);
                streamWriter.Write(jsonAnalog);
                streamWriter.Flush();
                streamWriter.Close();
                var httpResponseAnalog = (HttpWebResponse)httpWebRequestAnalogBrend.GetResponse();
                using (var streamReaderAnalog = new StreamReader(httpResponseAnalog.GetResponseStream()))
                {
                    var resultAnalog = streamReaderAnalog.ReadToEnd();
                    var jObjectAnalog = JObject.Parse(resultAnalog);
                    var dataObj = (JObject)jObjectAnalog["data"];
                    ViewBag.Analog = jObjectAnalog;
                    foreach (var itn in ViewBag.Analog["data"])
                    {
                        string Brend = itn.Path.ToString().Replace("data." + "kl9" + ":", "");
                        row.Add(Brend);
                    }
                    ViewBag.Brend = row;

                }
            }
            ////////////////////End Brends
            UsersCARSEntities dc = new UsersCARSEntities();

            var models = dc.Remnants.Where(a => a.Articul.Contains("kl9"))
                             .Select(a => new { value = a.Articul })
                             .Select(a => new { label = a.value })
                             .Distinct();
            ////////////////////////webAnalog
            List<JToken> arrayAnalog = new List<JToken>();
            List<JValue> arrayVal = new List<JValue>();
            var ArrAnalogs = "";

            var webAddrAnalog = "https://service.tradesoft.ru/3/";
            var httpWebRequestAnalog = (HttpWebRequest)WebRequest.Create(webAddrAnalog);

            httpWebRequestAnalog.ContentType = "application/json; charset=utf-8";
            httpWebRequestAnalog.Method = "POST";
            httpWebRequestAnalog.Accept = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(httpWebRequestAnalog.GetRequestStream()))
            {
                VebAnalog analog = new VebAnalog()
                {
                    user = "alliance.cl",
                    password = "alliancemotors",
                    service = "analog",
                    action = "getAnalogs",
                    data = new List<List<string>>() {
                     new List<string> { "kl9", "knecht" }
                    }
                };
                String jsonAnalog = JsonConvert.SerializeObject(analog);
                streamWriter.Write(jsonAnalog);
                streamWriter.Flush();
                streamWriter.Close();
                var httpResponseAnalog = (HttpWebResponse)httpWebRequestAnalog.GetResponse();
                using (var streamReaderAnalog = new StreamReader(httpResponseAnalog.GetResponseStream()))
                {
                    var resultAnalog = streamReaderAnalog.ReadToEnd();
                    var jObjectAnalog = JObject.Parse(resultAnalog);
                    var dataObj = (JObject)jObjectAnalog["data"]["kl9"]["knecht"];
                    // ArrAnalogs = (JArray)dataObj["analogs"];
                    JArray rsltArray = (JArray)dataObj["analogs"];

                    foreach (var item in rsltArray.Children())
                    {
                        arrayAnalog.Add(item);
                    }



                    var jList = from values in dataObj["analogs"].Children().Children()
                                select values;
                    ViewBag.Analog = arrayAnalog.Select(a => new { value = a.First })
                             .Select(a => new { label = a.value })
                             .Distinct();
                }
            }




            return View();
        }

        public ActionResult SearchByVin(string SelectedAuto, string Vin)
        {

            UsersCARSEntities dc = new UsersCARSEntities();
            var Auto = (from prod in dc.NameOriginalAuto select prod).ToList();

            var contr = (from ctr in Auto select ctr);


            if (!String.IsNullOrEmpty(SelectedAuto))
            {
                contr = contr.Where(ctr => ctr.NameAuto.Trim().Equals(SelectedAuto.Trim()));
            }

            var UniqueAuto = (from Ucontr in contr
                              group Ucontr by Ucontr.NameAuto into newGroup
                              where newGroup.Key != null
                              orderby newGroup.Key
                              select new { ViewOfAuto = newGroup.Key }).ToList();
            ViewBag.UniqueAuto = UniqueAuto.Select(m => new SelectListItem { Value = m.ViewOfAuto, Text = m.ViewOfAuto }).ToList();

            ViewBag.Auto = SelectedAuto;
            if (SelectedAuto == "NISSAN")
            {
                ViewBag.Url = "http://catalog.alliance-motors.kg/nissan/?vin=";
            }

            ViewBag.Vin = Vin;

            return View();
        }

        public ActionResult Original(string part, Users u, string VinCode)
        {
            if (Session["LogedUserId"] != null)
            {
                if (!String.IsNullOrEmpty(VinCode))
                {
                    UsersCARSEntities dc = new UsersCARSEntities();

                    var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();

                    var products = (from prod in dc.Remnants
                                    where prod.VIN == VinCode
                                    select prod).ToList();
                    if (products != null)
                    {
                        // Vin запрос
                        var VinRemnants = dc.Remnants.Where(a => a.VIN.Contains(VinCode)).ToList();
                        ViewBag.VinCode = VinRemnants.ToList();
                        // ViewBag.SearchParameters = VinRemnants.ToList();
                    }


                    ViewBag.VinCode = products.GroupBy(row =>
                      new
                      {
                          FirstField = row.NameAuto,
                          SecondField = row.modProdPeriod,
                          ThirdField = row.Model,
                          FoursField = row.BrendAuto,

                          FiveField = row.ReleaseDate,
                          SixField = row.ColorOfBody,
                          SevenField = row.Options,
                          EightField = row.Description,

                          NineField = row.catProdPeriod,
                          TenField = row.ColorOfSalon



                      })
                   .Select(
                      group => new Remnants
                      {
                          NameAuto = group.Key.FirstField,
                          modProdPeriod = group.Key.SecondField,
                          Model = group.Key.ThirdField,
                          BrendAuto = group.Key.FoursField,
                          ReleaseDate = group.Key.FiveField,
                          ColorOfBody = group.Key.SixField,
                          Options = group.Key.SevenField,
                          Description = group.Key.EightField,
                          catProdPeriod = group.Key.NineField,
                          ColorOfSalon = group.Key.TenField
                      }).ToList();

                    ViewBag.Vin = VinCode;
                }
                else
                {
                    UsersCARSEntities dc = new UsersCARSEntities();
                    var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
                    var products = (from prod in dc.Remnants select prod).ToList();
                    if (products != null)
                    {
                        var VinRemnants = dc.Remnants.Where(a => a.VIN.Contains("")).ToList();
                        ViewBag.VinCode = VinRemnants.ToList();
                    }


                    ViewBag.VinCode = products.GroupBy(row =>
                   new
                   {
                       FirstField = row.NameAuto,
                       SecondField = row.modProdPeriod,
                       ThirdField = row.Model,
                       FoursField = row.BrendAuto,

                       FiveField = row.ReleaseDate,
                       SixField = row.ColorOfBody,
                       SevenField = row.Options,
                       EightField = row.Description,

                       NineField = row.catProdPeriod,
                       TenField = row.ColorOfSalon



                   })
                .Select(
                   group => new Remnants
                   {
                       NameAuto = group.Key.FirstField,
                       modProdPeriod = group.Key.SecondField,
                       Model = group.Key.ThirdField,
                       BrendAuto = group.Key.FoursField,
                       ReleaseDate = group.Key.FiveField,
                       ColorOfBody = group.Key.SixField,
                       Options = group.Key.SevenField,
                       Description = group.Key.EightField,
                       catProdPeriod = group.Key.NineField,
                       ColorOfSalon = group.Key.TenField
                   }).ToList();
                    ViewBag.Vin = VinCode;
                }




                return View();
            }
            else
            {
                return RedirectToAction("LoginSearch");
            }

        }

    }
}