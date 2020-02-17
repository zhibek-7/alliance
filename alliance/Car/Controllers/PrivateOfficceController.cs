using Car.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using SRVTextToImage;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Security;
using System.Net;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Xml.Linq;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Car.Controllers
{
    [Serializable]
    public class PrivateOfficceController : Controller
    {


        public ActionResult ExportToExcel()
        {
            var products = new System.Data.DataTable("Remnants");
            products.Columns.Add("Id", typeof(int));
            products.Columns.Add("NameGood", typeof(string));
            products.Columns.Add("Code", typeof(string));
            products.Columns.Add("Brend", typeof(string));
            products.Columns.Add("MethodDelivery", typeof(string));
            products.Columns.Add("Existence", typeof(string));
            products.Columns.Add("Price", typeof(string));
            products.Columns.Add("Currancycode", typeof(string));
            products.Columns.Add("Sklad", typeof(string));

            UsersCARSEntities dc = new UsersCARSEntities();
            var rwmn = dc.Remnants;
            foreach (var item in rwmn)
            {
                
                if (item.Sklad != "EMEX")
                {
                    var exisct = item.Existence.ToString();
                    if (item.Existence>10) {
                        exisct = "больше 10";
                    }
                    products.Rows.Add(item.Id, item.Name, item.Code, item.Brend, item.MethodDelivery, exisct, item.Price, item.CurrancyCode, item.Sklad);
                }
            }





            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=remnants.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            string path = Server.MapPath("~/Content/excel/remnants.xls");
            return View();
        }



        // Отправка массива байтов
        public FileResult Download()
        {
 
            string path = Server.MapPath("~/Content/excel/sales.xls");
            var ii = path;
            var fileExists = System.IO.File.Exists(path);
            var fs = System.IO.File.OpenRead(path);
            var doc = new byte[0];
            doc = System.IO.File.ReadAllBytes(path);
            return File(doc, "application/vnd.ms-excel", "Sales.xlsx");
        }

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
                              // Convert.ToDouble(product.Element("Price").Value),
                              // writer.WriteElementString("Sklad", item.Pr.Sklad.ToString());
                              //  Days = product.Element("Days").Value,
                              //Min_Krat = Convert.ToInt32(product.Element("Min_krat").Value),
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

        // GET: PrivateOfficce
        public ActionResult Index(string SelectedAuto, string Vin)
        {
           if (Session["LogedUserId"] != null)
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


                UsersCARSEntities dc = new UsersCARSEntities();
                var UserID = 0;
                var AllUsers = from UserId in dc.Users select UserId;
                foreach (Users UserId in AllUsers)
                {
                    if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                    {
                        UserID = UserId.UserId;
                        ViewBag.UserID=UserID;
                    }
                }
                return View();
            }
            else {
                return RedirectToAction("Login","Home");
            }
            
        }

         
        public ActionResult AllOrders(string Search,string Part, string SortOrder, Users u, string SelectedContragent, string SelectedContract, string DateFrom, string DateTo)
        {
           UsersCARSEntities dc = new UsersCARSEntities();
           var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
           if (Session["LogedUserId"] != null)
            {
               var UserID = 0;
               var AllUsers = from UserId in dc.Users select UserId;
                foreach (Users UserId in AllUsers)
                {
                    if (UserId.UserId.ToString()== Session["LogedUserId"].ToString())
                    {
                       UserID = UserId.UserId;
                    }
                }
                var products = (from prod in dc.Orders where prod.Clients== UserID select prod).ToList();
                ViewBag.Pending = String.IsNullOrEmpty(SortOrder) ? "Pending_desc" : "";
                ViewBag.OnMyWay = String.IsNullOrEmpty(SortOrder) ? "OnMyWay_desc" : "";
                ViewBag.inReserve = String.IsNullOrEmpty(SortOrder) ? "inReserve_desc" : "";
                ViewBag.Ordered = String.IsNullOrEmpty(SortOrder) ? "Ordered_desc" : "";
                ViewBag.InTheAssembly = String.IsNullOrEmpty(SortOrder) ? "InTheAssembly_desc" : "";
                ViewBag.Shipped = String.IsNullOrEmpty(SortOrder) ? "Shipped_desc" : "";
                ViewBag.Canceled = String.IsNullOrEmpty(SortOrder) ? "Canceled_desc" : "";
                ViewBag.PartiallyShipped = String.IsNullOrEmpty(SortOrder) ? "PartiallyShipped_desc" : "";
           if (products != null)
            {
                switch (SortOrder)
                {
                    case "Pending_desc":
                        products = (from prod in products where prod.Status == "В ожидании" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "OnMyWay_desc":
                        products = (from prod in products where prod.Status == "В пути" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "inReserve_desc":
                        products = (from prod in products where prod.Status == "В резерве" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "Ordered_desc":
                        products = (from prod in products where prod.Status == "Заказан" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "InTheAssembly_desc":
                        products = (from prod in products where prod.Status == "На сборке" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "Shipped_desc":
                        products = (from prod in products where prod.Status == "Отгружен" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "Canceled_desc":
                        products = (from prod in products where prod.Status == "Отменен" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "PartiallyShipped_desc":
                        products = (from prod in products where prod.Status == "Частично отгружен" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    default:
                        products = (from prod in products where (prod.Clients == UserID) select prod).ToList();
                        break;
                }
            }
                //filter by date
                ViewBag.dtDateFrom = DateFrom;
                ViewBag.dtDateTo = DateTo;
                DateTime dtDateFrom = Convert.ToDateTime(DateFrom);
                DateTime dtDateTo = Convert.ToDateTime(DateTo);
                if (DateFrom != null || DateTo != null)
                {
                   products = (from prod in products where (prod.Date>= dtDateFrom && prod.Date <= dtDateTo) && (prod.Clients == UserID) select prod).ToList();
                   StringBuilder sb = new StringBuilder();
                    foreach (var prodFilter in products)
                    {
                       if ((dtDateFrom > prodFilter.Date || dtDateFrom == prodFilter.Date)
                          && (dtDateTo == prodFilter.Date || dtDateTo < prodFilter.Date))
                        {
                          var productsFilterByDate = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();
                          sb.Append(productsFilterByDate);
                          products.ToList();
                        }
                    }
                }
                else
                {
                   dtDateFrom = DateTime.Now;
                   dtDateTo = DateTime.Now;
                    products = products.ToList();
                    StringBuilder sb = new StringBuilder();
                   foreach (var prodFilter in products)
                    {
                       if ((dtDateFrom > prodFilter.Date || dtDateFrom == prodFilter.Date)
                            && (dtDateTo == prodFilter.Date || dtDateTo < prodFilter.Date))
                        {
                            var productsFilterByDate = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();
                            sb.Append(productsFilterByDate);
                            products.ToList();
                        }
                    }
                }

                //////////////////View of contract

                var contracts = (from prod in dc.ViewOfMade select prod).ToList();

                var contr = (from ctr in contracts select ctr);


                if (!String.IsNullOrEmpty(SelectedContract))
                {
                    contr = contr.Where(ctr => ctr.ViewMade.Trim().Equals(SelectedContract.Trim()));
                }
               var UniqueContract = (from Ucontr in contr
                                      group Ucontr by Ucontr.ViewMade into newGroup
                                      where newGroup.Key != null
                                      orderby newGroup.Key
                                      select new { ViewOfMade = newGroup.Key }).ToList();
                ViewBag.UniqueContract = UniqueContract.Select(m => new SelectListItem { Value = m.ViewOfMade, Text = m.ViewOfMade }).ToList();

                ViewBag.SelectedContract = SelectedContract;
                ///////////////////View of contragent

                var contragents = (from ctg in dc.Contraagent select ctg).ToList();

                var ctrag = (from ctag in contragents select ctag);


                if (!String.IsNullOrEmpty(SelectedContragent))
                {
                    ctrag = ctrag.Where(ctag => ctag.Contragent.Trim().Equals(SelectedContragent.Trim()));
                }
                var UniqueContragent = (from Ucontrag in ctrag
                                        group Ucontrag by Ucontrag.Contragent into newGroup
                                        where newGroup.Key != null
                                        orderby newGroup.Key
                                        select new { Contragent = newGroup.Key }).ToList();
                ViewBag.UniqueContragent = UniqueContragent.Select(m => new SelectListItem
                { Value = m.Contragent, Text = m.Contragent }).ToList();

                ViewBag.SelectedContragent = SelectedContragent;
                
                if (Search!=null) {
                    if (Part!=null) {
                        string code = Part;
                        code = code.Replace(" ", string.Empty);
                        code = code.TrimStart(' ', '/');
                        code = code.TrimEnd(' ', '/');
                      
                        products = (from prod in products where (prod.Articul.ToLower() == Part.ToLower()
                                    || prod.Articul.ToLower() == Part.ToLower().Replace("-", string.Empty)
                                    || prod.Articul.ToLower() == code.ToLower()
                                    || prod.Articul == code.Replace("-", string.Empty)) select prod).ToList();
                    }
                }
                ViewBag.AllOrders1 = products.OrderByDescending(a=>a.Date).GroupBy(row =>
                  new
                  {
                      FirstField = row.NumOrder,
                      SecondField = row.Sklad,
                      ThirdField = row.Date,
                      FoursField = row.Status,
                      
                  })
               .Select(
                  group => new Orders
                  {
                      NumOrder = group.Key.FirstField,
                      Sklad = group.Key.SecondField,
                      Date = group.Key.ThirdField,
                     Status = group.Key.FoursField,
                     Price =(double) sumNumOrder((string)group.Key.FirstField),
                     Existence=(int) ExistenceNumOrder((string)group.Key.FirstField)
                  }).ToList();


             

                return View();
            }
            else
            {
                ViewBag.Message = "Вы не правильно ввели логин или пароль";
                return View("LoginAllOrders");
            }

          

        }

        public double sumNumOrder(string NumOrder) {
            UsersCARSEntities dc = new UsersCARSEntities();

            double sum = 0;

            List<Orders> orders = new List<Orders>();
            orders = dc.Orders.Where(a => a.NumOrder.Equals(NumOrder)).ToList();
            ViewBag.OrdersBByNumOrder = orders;

            foreach (var item in orders) {
                sum = sum + (double)(item.Price*item.Existence);
            }
            return sum;
        }


        public int ExistenceNumOrder(string NumOrder)
        {
            UsersCARSEntities dc = new UsersCARSEntities();

            int sum = 0;

            List<Orders> orders = new List<Orders>();
            orders = dc.Orders.Where(a => a.NumOrder.Equals(NumOrder)).ToList();
            ViewBag.OrdersBByNumOrder = orders;

            foreach (var item in orders)
            {
                sum = sum + (int)item.Existence;
            }
            return sum;
        }

        public ActionResult LoginAllOrders(Users u) {
          
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
                    return RedirectToAction("AllOrders");

                }
                else { 
                    return View("LoginAllOrders");
            }
                }
            
           // return View(u);
        }


        public ActionResult Go(string NumOrder) {
         using (UsersCARSEntities dc=new UsersCARSEntities()) {
         //var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password)).FirstOrDefault();
            List<Orders> orders = new List<Orders>();
            orders = dc.Orders.Where(a => a.NumOrder.Equals(NumOrder)).ToList();
            ViewBag.OrdersBByNumOrder = orders;
          }
        return View("Go");
        }


        public ActionResult GoSale(string Brend)
        {
            using (UsersCARSEntities dc = new UsersCARSEntities())
            {
                //var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password)).FirstOrDefault();
                List<Sale> brends = new List<Sale>();
                brends = dc.Sale.Where(a => a.Brend.Equals(Brend)).ToList();
                ViewBag.OrdersBByBrend = brends;
            }
            return View("GoSale");
        }



        public ActionResult ShowPartnersOrders(string SortOrder, Users u, string SelectedContragent, string SelectedContract, string DateFrom, string DateTo)
        {
            if (Session["LogedUserId"] != null)
            {

             
            ViewBag.Pending = String.IsNullOrEmpty(SortOrder) ? "Pending_desc" : "";
            ViewBag.InOrder = String.IsNullOrEmpty(SortOrder) ? "InOrder_desc" : "";
            ViewBag.NotAvailable = String.IsNullOrEmpty(SortOrder) ? "NotAvailable_desc" : "";
            ViewBag.Redeemed = String.IsNullOrEmpty(SortOrder) ? "Redeemed_desc" : "";
            ViewBag.OnTheWayPhaeton = String.IsNullOrEmpty(SortOrder) ? "OnTheWayPhaeton_desc" : "";
            ViewBag.InTheStorePhaeton = String.IsNullOrEmpty(SortOrder) ? "InTheStorePhaeton_desc" : "";
            ViewBag.OnTheWayEmex = String.IsNullOrEmpty(SortOrder) ? "OnTheWayEmex_desc" : "";
            ViewBag.InTheStoreEmex = String.IsNullOrEmpty(SortOrder) ? "InTheStoreEmex_desc" : "";

            UsersCARSEntities dc = new UsersCARSEntities();
            // {
            var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
                var UserID = 0;


        var AllUsers = from UserId in dc.Users select UserId;
                foreach (Users UserId in AllUsers)
                {
                    if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                    {
                        UserID = UserId.UserId;
                    }

                }
        var products = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();

            if (products != null)
            {
                switch (SortOrder)
                {
                    case "Pending_desc":
                        products = (from prod in dc.Orders where prod.Status == "В ожидании" && (prod.Clients== UserID) select prod).ToList();
                        break;
                    case "InOrder_desc":
                        products = (from prod in dc.Orders where prod.Status == "В заказе" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "NotAvailable_desc":
                        products = (from prod in dc.Orders where prod.Status == "Нет в наличии" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "Redeemed_desc":
                        products = (from prod in dc.Orders where prod.Status == "Выкуплено" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "OnTheWayPhaeton_desc":
                        products = (from prod in dc.Orders where prod.Status == "В пути до склада поставщика Phaeton" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "InTheStorePhaeton_desc":
                        products = (from prod in dc.Orders where prod.Status == "Пришло на склад поставщика Phaeton" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "OnTheWayEmex_desc":
                        products = (from prod in dc.Orders where prod.Status == "В пути до склада Emex" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    case "InTheStoreEmex_desc":
                        products = (from prod in dc.Orders where prod.Status == "Пришло на склад поставщика Emex" && (prod.Clients == UserID) select prod).ToList();
                        break;
                    default:
                        products = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();
                        break;
                }

           }




            //filter by date
            ViewBag.dtDateFrom = DateFrom;
            ViewBag.dtDateTo = DateTo;

            if (DateFrom != null || DateTo != null)
            {

                DateTime dtDateFrom = Convert.ToDateTime(DateFrom);
                DateTime dtDateTo = Convert.ToDateTime(DateTo);



                products = (from prod in dc.Orders where (dtDateFrom <= prod.Date && dtDateTo >= prod.Date) && (prod.Clients == UserID) select prod).ToList();

                StringBuilder sb = new StringBuilder();


                foreach (var prodFilter in products)
                {

                    if ((dtDateFrom > prodFilter.Date || dtDateFrom == prodFilter.Date)
                        && (dtDateTo == prodFilter.Date || dtDateTo < prodFilter.Date))
                    {

                        var productsFilterByDate = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();

                        sb.Append(productsFilterByDate);
                        products.ToList();
                    }

                }

            }
            else
            {
                DateTime dtDateFrom = DateTime.Now;
                DateTime dtDateTo = DateTime.Now;

                StringBuilder sb = new StringBuilder();


                foreach (var prodFilter in products)
                {
                    if ((dtDateFrom > prodFilter.Date || dtDateFrom == prodFilter.Date)
                        && (dtDateTo == prodFilter.Date || dtDateTo < prodFilter.Date))
                    {
                        var productsFilterByDate = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();
                        sb.Append(productsFilterByDate);
                        products.ToList();
                    }

                }

            }

            //////////////////View of contract

            var contracts = (from prod in dc.ViewOfMade select prod).ToList();

            var contr = (from ctr in contracts select ctr);


            if (!String.IsNullOrEmpty(SelectedContract))
            {
                contr = contr.Where(ctr => ctr.ViewMade.Trim().Equals(SelectedContract.Trim()));
            }



            var UniqueContract = (from Ucontr in contr
                                  group Ucontr by Ucontr.ViewMade into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { ViewOfMade = newGroup.Key }).ToList();
            ViewBag.UniqueContract = UniqueContract.Select(m => new SelectListItem { Value = m.ViewOfMade, Text = m.ViewOfMade }).ToList();

            ViewBag.SelectedContract = SelectedContract;
            ///////////////////View of contragent

            var contragents = (from ctg in dc.Contraagent select ctg).ToList();

            var ctrag = (from ctag in contragents select ctag);


            if (!String.IsNullOrEmpty(SelectedContragent))
            {
                ctrag = ctrag.Where(ctag => ctag.Contragent.Trim().Equals(SelectedContragent.Trim()));
            }


            var UniqueContragent = (from Ucontrag in ctrag
                                    group Ucontrag by Ucontrag.Contragent into newGroup
                                    where newGroup.Key != null
                                    orderby newGroup.Key
                                    select new { Contragent = newGroup.Key }).ToList();
            ViewBag.UniqueContragent = UniqueContragent.Select(m => new SelectListItem
            { Value = m.Contragent, Text = m.Contragent }).ToList();

            ViewBag.SelectedContragent = SelectedContragent;




            ViewBag.AllOrders = products.GroupBy(row =>
              new
              {

                  FirstField = row.NumOrder,
                  SecondField = row.Contragent,
                  ThirdField = row.Date,
                  FoursField = row.Status
              },
              (key, group) => new
              {
                  NumOrder = key.FirstField,
                  Contragent = key.SecondField,
                  Date = key.ThirdField,
                  Status = key.FoursField,
                  Count = group.Count()
              }).ToList();


            ViewBag.AllOrders1 = products.GroupBy(row =>
              new
              {
                  FirstField = row.NumOrder,
                 // SecondField = row.Contragent,
                //  ThirdField = row.Date,
                  FoursField = row.Status
              })
           .Select(
              group => new Orders
              {
                  NumOrder = group.Key.FirstField,
                 // Contragent = group.Key.SecondField,
                 // Date = group.Key.ThirdField,
                  Status = group.Key.FoursField

              }).ToList();

                return View();
            }
            else
            {
                ViewBag.Message = "Вы не правильно ввели логин или пароль";
                return View("LoginShowPartnersOrders");
            }
        }
        public ActionResult LoginShowPartnersOrders(Users u) {
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
                    return RedirectToAction("ShowPartnersOrders");

                }
                else
                {
                    return View("LoginShowPartnersOrders");
                }
            }

        }


        public ActionResult Reports(string SelectedReporT, Users u, string SelectedContragent, string SelectedContract, string DateFrom, string DateTo)
        {
            if (Session["LogedUserId"] != null)
            {
                UsersCARSEntities dc = new UsersCARSEntities();
            // {
            var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
                int UserID = 1;
                var AllUsers = from UserId in dc.Users select UserId;
                foreach (Users UserId in AllUsers)
                {
                    if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                    {
                        UserID = UserId.UserId;
                    }

                }
                var products = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();
            //filter by date
            ViewBag.dtDateFrom = DateFrom;
            ViewBag.dtDateTo = DateTo;

            if (DateFrom != null || DateTo != null)
            {

                DateTime dtDateFrom = Convert.ToDateTime(DateFrom);
                DateTime dtDateTo = Convert.ToDateTime(DateTo);



         products = (from prod in dc.Orders where (dtDateFrom <= prod.Date && dtDateTo >= prod.Date) && (prod.Clients == UserID) select prod).ToList();

                StringBuilder sb = new StringBuilder();


                foreach (var prodFilter in products)
                {

                    if ((dtDateFrom > prodFilter.Date || dtDateFrom == prodFilter.Date)
                        && (dtDateTo == prodFilter.Date || dtDateTo < prodFilter.Date))
                    {

                        var productsFilterByDate = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();

                        sb.Append(productsFilterByDate);
                        products.ToList();
                    }

                }

            }
            else
            {

                DateTime dtDateFrom = DateTime.Now;
                DateTime dtDateTo = DateTime.Now;

                StringBuilder sb = new StringBuilder();


                foreach (var prodFilter in products)
                {
                    if ((dtDateFrom > prodFilter.Date || dtDateFrom == prodFilter.Date)
                        && (dtDateTo == prodFilter.Date || dtDateTo < prodFilter.Date))
                    {
                        var productsFilterByDate = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();
                        sb.Append(productsFilterByDate);
                        products.ToList();
                    }

                }

            }


            //////////////////View of Report

            var report = (from prod in dc.ViewOfReport select prod).ToList();

            var reporT = (from ctr in report select ctr);


            if (!String.IsNullOrEmpty(SelectedReporT))
            {
                reporT = reporT.Where(ctr => ctr.ReportName.Trim().Equals(SelectedReporT.Trim()));
            }



            var UniqueReporT = (from Ucontr in reporT
                                group Ucontr by Ucontr.ReportName into newGroup
                                where newGroup.Key != null
                                orderby newGroup.Key
                                select new { ViewOfReport = newGroup.Key }).ToList();
            ViewBag.UniqueReporT = UniqueReporT.Select(m => new SelectListItem { Value = m.ViewOfReport, Text = m.ViewOfReport }).ToList();

            ViewBag.SelectedReporT = SelectedReporT;


            //////////////////View of contract

            var contracts = (from prod in dc.ViewOfMade select prod).ToList();

            var contr = (from ctr in contracts select ctr);


            if (!String.IsNullOrEmpty(SelectedContract))
            {
                contr = contr.Where(ctr => ctr.ViewMade.Trim().Equals(SelectedContract.Trim()));
            }



            var UniqueContract = (from Ucontr in contr
                                  group Ucontr by Ucontr.ViewMade into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { ViewOfMade = newGroup.Key }).ToList();
            ViewBag.UniqueContract = UniqueContract.Select(m => new SelectListItem { Value = m.ViewOfMade, Text = m.ViewOfMade }).ToList();

            ViewBag.SelectedContract = SelectedContract;
            ///////////////////View of contragent

            var contragents = (from ctg in dc.Contraagent select ctg).ToList();

            var ctrag = (from ctag in contragents select ctag);


            if (!String.IsNullOrEmpty(SelectedContragent))
            {
                ctrag = ctrag.Where(ctag => ctag.Contragent.Trim().Equals(SelectedContragent.Trim()));
            }


            var UniqueContragent = (from Ucontrag in ctrag
                                    group Ucontrag by Ucontrag.Contragent into newGroup
                                    where newGroup.Key != null
                                    orderby newGroup.Key
                                    select new { Contragent = newGroup.Key }).ToList();
            ViewBag.UniqueContragent = UniqueContragent.Select(m => new SelectListItem
            { Value = m.Contragent, Text = m.Contragent }).ToList();

            ViewBag.SelectedContragent = SelectedContragent;

                return View("Reports");
            }
            else
            {
                return RedirectToAction("LoginReports");
            }

            
        }
        public ActionResult LoginReports(Users u) {
          
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
                    return RedirectToAction("Reports");

                    }
                    else
                    {
                        return View("LoginReports");
                    }
                }
            
        }

        public ActionResult Invoice(Users u, string SelectedContragent, string SelectedContract, string DateFrom, string DateTo)
        {
            if (Session["LogedUserId"] != null)
            {
                UsersCARSEntities dc = new UsersCARSEntities();
                // {
                var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
                int UserID = 1;
                var AllUsers = from UserId in dc.Users select UserId;
                foreach (Users UserId in AllUsers)
                {
                    if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                    {
                        UserID = UserId.UserId;
                    }

                }

              
            var products = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();


            //////////////////View of contract

            var contracts = (from prod in dc.ViewOfMade select prod).ToList();

            var contr = (from ctr in contracts select ctr);


            if (!String.IsNullOrEmpty(SelectedContract))
            {
                contr = contr.Where(ctr => ctr.ViewMade.Trim().Equals(SelectedContract.Trim()));
            }



            var UniqueContract = (from Ucontr in contr
                                  group Ucontr by Ucontr.ViewMade into newGroup
                                  where newGroup.Key != null
                                  orderby newGroup.Key
                                  select new { ViewOfMade = newGroup.Key }).ToList();
            ViewBag.UniqueContract = UniqueContract.Select(m => new SelectListItem { Value = m.ViewOfMade, Text = m.ViewOfMade }).ToList();

            ViewBag.SelectedContract = SelectedContract;
            ///////////////////View of contragent

            var contragents = (from ctg in dc.Orders where (ctg.Clients == UserID) select ctg).ToList();

            var ctrag = (from ctag in contragents select ctag);


            if (!String.IsNullOrEmpty(SelectedContragent))
            {
                ctrag = ctrag.Where(ctag => ctag.Contragent.Trim().Equals(SelectedContragent.Trim()));
            }


            var UniqueContragent = (from Ucontrag in ctrag
                                    group Ucontrag by Ucontrag.Contragent into newGroup
                                    where newGroup.Key != null
                                    orderby newGroup.Key
                                    select new { Contragent = newGroup.Key }).ToList();
            ViewBag.UniqueContragent = UniqueContragent.Select(m => new SelectListItem
            { Value = m.Contragent, Text = m.Contragent }).ToList();

            ViewBag.SelectedContragent = SelectedContragent;

            //filter by date


            if (DateFrom != null || DateTo != null)
            {

                DateTime dtDateFrom = Convert.ToDateTime(DateFrom);
                DateTime dtDateTo = Convert.ToDateTime(DateTo);

                products = (from prod in dc.Orders where (dtDateFrom <= prod.Date && dtDateTo >= prod.Date) && (prod.Clients == UserID) select prod).ToList();

                StringBuilder sb = new StringBuilder();
                foreach (var prodFilter in products)
                {

                    if ((dtDateFrom > prodFilter.Date || dtDateFrom == prodFilter.Date)
                        && (dtDateTo == prodFilter.Date || dtDateTo < prodFilter.Date))
                    {

                        var productsFilterByDate = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();

                        sb.Append(productsFilterByDate);
                        products.ToList();
                    }

                }
            }
            else
            {

                DateTime dtDateFrom = DateTime.Now;
                DateTime dtDateTo = DateTime.Now;

                StringBuilder sb = new StringBuilder();
                foreach (var prodFilter in products)
                {
                    if ((dtDateFrom > prodFilter.Date || dtDateFrom == prodFilter.Date)
                        && (dtDateTo == prodFilter.Date || dtDateTo < prodFilter.Date))
                    {
                        var productsFilterByDate = (from prod in dc.Orders where (prod.Clients == UserID) select prod).ToList();
                        sb.Append(productsFilterByDate);
                        products.ToList();
                    }

                }

            }

        // group r by new { р1 = r["р1"], р2 = r["р2"], р3 = r["р3"], р4 = r["р4"] }

        //  var  productsGr=(from prod in dc.Orders group prod by new { р1 = prod.Description, р2 = prod.Contragent, р3 = prod.Date, р4 = prod.Existence } into gr select gr).ToList();

            ViewBag.AllOrders = products.ToList();

            return View();
            }
            else
            {
                return RedirectToAction("LoginInvoice");
            }


        }

        public ActionResult LoginInvoice(Users u) {
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
                    return RedirectToAction("Invoice");

                }
                else
                {
                    return View("LoginInvoice");
                }
            }
        }

        public ActionResult EditUserEmail(int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                Users user = db.Users.Where(a => a.UserId == id).FirstOrDefault();

                Users users = new Users();
                users.ConfirmPassword = user.Password;
                users.Email = user.Email;
                //users.FamilyName = user.FamilyName;
                //users.FullName = user.FullName;
                //users.UserRole = user.UserRole;
                //users.Password = user.Password;
                //users.Orders = user.Orders;
                //users.ContactPhone = user.ContactPhone;
                //users.UserId = user.UserId;
                //users.UserName = user.UserName;
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditUserEmail(Users users, int id)
        {
           if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                Users user = db.Users.Where(a => a.UserId == id).FirstOrDefault();

                if (user != null)
                {
                    user.ConfirmPassword = user.Password;
                    user.Email = users.Email;
                    //user.FamilyName = users.FamilyName;
                    //user.FullName = users.FullName;
                    //user.UserRole = users.UserRole;
                    //user.Password = users.Password;
                    //user.Orders = users.Orders;
                    //user.ContactPhone = users.ContactPhone;
                    ////user.UserId = users.UserId;
                    //user.UserName = users.UserName;
                   
                }

               try
                {
                   db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        Response.Write("Object: " + validationError.Entry.Entity.ToString());
                        Response.Write("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            Response.Write(err.ErrorMessage + "");
                        }
                    }
                }
                Session["LogedEmail"] = user.Email;
                return RedirectToAction("Index", "PrivateOfficce");
            }
            else
            {
               return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult EditUserPassword(int id) {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                Users user = db.Users.Where(a => a.UserId == id).FirstOrDefault();

                Users users = new Users();
                users.ConfirmPassword = user.Password;
                // users.Email = user.Email;
                //users.FamilyName = user.FamilyName;
                //users.FullName = user.FullName;
                //users.UserRole = user.UserRole;
                users.Password = user.Password;
                //users.Orders = user.Orders;
                //users.ContactPhone = user.ContactPhone;
                //users.UserId = user.UserId;
                //users.UserName = user.UserName;
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditUserPassword(Users users, int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                Users user = db.Users.Where(a => a.UserId == id).FirstOrDefault();

                if (user != null)
                {
                     user.ConfirmPassword = users.Password;
                    //user.Email = users.Email;
                    //user.FamilyName = users.FamilyName;
                    //user.FullName = users.FullName;
                    //user.UserRole = users.UserRole;
                    user.Password = users.Password;
                    //user.Orders = users.Orders;
                    //user.ContactPhone = users.ContactPhone;
                    ////user.UserId = users.UserId;
                    //user.UserName = users.UserName;
                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        Response.Write("Object: " + validationError.Entry.Entity.ToString());
                        Response.Write("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            Response.Write(err.ErrorMessage + "");
                        }
                    }
                }
                return RedirectToAction("Index", "PrivateOfficce");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Contracts()
        {
            return View();
        }
        public ActionResult PriceList()
        {
            return View();
        }
        public ActionResult PrivateData()
        {
            return View();
        }

    }
}