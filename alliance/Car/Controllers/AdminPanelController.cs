using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using Exel = ExcelLibrary;//Microsoft.Office.Interop.Excel;
using ExcelLibrary.SpreadSheet;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Threading;
using System.Globalization;
using System.Xml.Linq;
using System.Xml;

namespace Car.Controllers
{
    [Serializable]
    public class AdminPanelController : Controller
    {



        /// <summary>
        /// ////для клиентов выгрузки товаров
        /// </summary>

        // Отправка массива байтов
        public FileResult Download()
        {

            string path = Server.MapPath("~/Content/excel/remnants.xls");
            var ii = path;
            var fileExists = System.IO.File.Exists(path);
            var fs = System.IO.File.OpenRead(path);
            var doc = new byte[0];
            doc = System.IO.File.ReadAllBytes(path);
            return File(doc, "application/vnd.ms-excel", "remnants.xlsx");
        }

        public void CreateXMLFileCartForClient(string xmlPath, List<Item> objOrders)
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


        public void CreateXMLFileCartForClient(string xmlPath, List<ItemRemoteStore> objOrders)
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








        ////controller Action
        //public void Excel()
        //{
        //    ExportToExcel();
        //}

        //helper class
     
            //public ActionResult ExportToExcel()
            //{
            //    var products = new System.Data.DataTable("Remnants");
            //    products.Columns.Add("Id", typeof(int));
            //    products.Columns.Add("NameGood", typeof(string));
            //    products.Columns.Add("Code", typeof(string));
            //    products.Columns.Add("Brend", typeof(string));
            //    products.Columns.Add("MethodDelivery", typeof(string));
            //    products.Columns.Add("Existence", typeof(string));
            //    products.Columns.Add("Price", typeof(string));
            //    products.Columns.Add("Currancycode", typeof(string));
            //    products.Columns.Add("Sklad", typeof(string));

            //    UsersCARSEntities dc = new UsersCARSEntities();
            //    var rwmn = dc.Remnants;
            //    foreach (var item in rwmn)
            //    {
            //        if (item.Sklad != "EMEX")
            //        {
            //            products.Rows.Add(item.Id, item.Name, item.Code, item.Brend, item.MethodDelivery, item.Existence, item.Price, item.CurrancyCode, item.Sklad);
            //        }
            //    }





            //    var grid = new GridView();
            //    grid.DataSource = products;
            //    grid.DataBind();

            //    Response.ClearContent();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment; filename=remnants.xls");
            //    Response.ContentType = "application/ms-excel";

            //    Response.Charset = "";
            //    StringWriter sw = new StringWriter();
            //    HtmlTextWriter htw = new HtmlTextWriter(sw);

            //    grid.RenderControl(htw);

            //    Response.Output.Write(sw.ToString());
            //    Response.Flush();
            //    Response.End();
            //    string path = Server.MapPath("~/Content/excel/remnants.xls");
            //    return View();
            //}
      










        public ActionResult SaveXML()
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
            return RedirectToAction("LoginAdminIndex");
        }




        public ActionResult UploadExcelSaleFileForClient(HttpPostedFileBase excelfile)
        {

            UsersCARSEntities dcC = new UsersCARSEntities();
            int UserID = 0;
            if (Session["LogedUserId"] != null)
            {
                foreach (Users UserId in dcC.Users.ToList())
                {
                    if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                    {
                        UserID = UserId.UserId;
                    }
                }
                var Users = (from prod in dcC.Users where prod.UserId == UserID && prod.UserRole == "client" select prod).FirstOrDefault();
                if (Users != null)
                {


                    if (excelfile == null || excelfile.ContentLength == 0)
                    {
                        ViewBag.Error = "Please select excel file";
                        return View("UploadSaleFile");
                    }
                    else
                    {
                        if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                        {
                            string path = Server.MapPath("~/Content/excel/" + excelfile.FileName);
                            excelfile.SaveAs(path);
                            try
                            {
                                Workbook workbook = Workbook.Load(path);
                                Worksheet worksheet = workbook.Worksheets[0];
                                List<Sale> listProduct = new List<Sale>();
                                using (UsersCARSEntities dc = new UsersCARSEntities())
                                {
                                    var remnantsRemove = dc.Sale.ToList();
                                    foreach (var item in remnantsRemove)
                                    {
                                        dc.Sale.Remove(item);
                                        dc.SaveChanges();
                                    }

                                    for (int row = 1; row <= worksheet.Cells.Rows.Count; row++)
                                    {
                                        int i = 1;
                                        listProduct.Add(new Sale()
                                        {
                                            Id = i++,
                                            Articul = (worksheet.Cells[row, 0]).ToString(),
                                            Brend = (worksheet.Cells[row, 1]).ToString(),
                                            Description = (worksheet.Cells[row, 4]).ToString(),
                                            Category = (worksheet.Cells[row, 5]).ToString(),
                                            NameGood = (worksheet.Cells[row, 2]).ToString(),
                                            Adaptation = (worksheet.Cells[row, 3]).ToString(),
                                            CurrancyCode = "KGS"
                                        });
                                    }
                                    foreach (Sale i in listProduct)
                                    {
                                        i.NameGood = i.NameGood.Replace("  ", string.Empty);


                                        if (i.Adaptation.Length > 500)
                                        {

                                            while (i.Adaptation.Length > 500)
                                            {
                                                i.Adaptation = i.Adaptation.Remove(500, i.Adaptation.Length - 500);
                                            }


                                        }

                                        var v = dc.Sale.Where(a => a.Articul.Equals(i.Articul) && a.Brend.Equals(i.Brend)).FirstOrDefault();

                                        if (i.Description != "" && i.Category != "")
                                        {
                                            if (v != null)
                                            {
                                                if ((float.Parse(i.Description) % 1 == 0 && float.Parse(i.Description) != 0)
                                                && (float.Parse(i.Category)) % 1 == 0 && float.Parse(i.Category) != 0)
                                                {
                                                    v.NameGood = i.NameGood;
                                                    i.Price = Math.Round((float)(float.Parse(i.Description)), 2, MidpointRounding.AwayFromZero);
                                                    i.Sales = Math.Round((float)(float.Parse(i.Category)), 2, MidpointRounding.AwayFromZero);
                                                    v.Adaptation = i.Adaptation;
                                                    v.Articul = i.Articul;
                                                    v.Brend = i.Brend;
                                                    v.Code = i.Code;
                                                    v.CurrancyCode = i.CurrancyCode;
                                                    v.Days = i.Days;
                                                    v.MethodDelivery = i.MethodDelivery;
                                                    v.Min_Krat = i.Min_Krat;
                                                    v.Supplies = i.Supplies;
                                                    v.V_P = i.V_P;
                                                }
                                                else
                                                {
                                                    v.NameGood = i.NameGood;
                                                    i.Price = Math.Round((float)(float.Parse(i.Description)), 2, MidpointRounding.AwayFromZero);
                                                    i.Sales = Math.Round((float)(float.Parse(i.Category)), 2, MidpointRounding.AwayFromZero);
                                                    v.Adaptation = i.Adaptation;
                                                    v.Articul = i.Articul;
                                                    v.Brend = i.Brend;
                                                    v.Code = i.Code;
                                                    v.CurrancyCode = i.CurrancyCode;
                                                    v.Days = i.Days;
                                                    v.MethodDelivery = i.MethodDelivery;
                                                    v.Min_Krat = i.Min_Krat;
                                                    v.Supplies = i.Supplies;
                                                    v.V_P = i.V_P;
                                                }
                                            }
                                            else
                                            {
                                                if ((float.Parse(i.Description) % 1 == 0 && float.Parse(i.Description) != 0)
                                                  && (float.Parse(i.Category) % 1 == 0 && float.Parse(i.Category) != 0))
                                                {
                                                    i.Price = Math.Round((float)(float.Parse(i.Description)), 2, MidpointRounding.AwayFromZero);
                                                    i.Sales = Math.Round((float)(float.Parse(i.Category)), 2, MidpointRounding.AwayFromZero);

                                                    i.Description = "";
                                                    i.Category = "";
                                                    dc.Sale.Add(i);
                                                }
                                                else
                                                {
                                                    i.Price = Math.Round((float)(float.Parse(i.Description)), 2, MidpointRounding.AwayFromZero);
                                                    i.Sales = Math.Round((float)(float.Parse(i.Category)), 2, MidpointRounding.AwayFromZero);
                                                    i.Description = "";
                                                    i.Category = "";
                                                    dc.Sale.Add(i);
                                                }
                                            }

                                        }
                                        dc.SaveChanges();
                                        ViewBag.ResultImport = dc.Remnants.ToList();
                                    }
                                }
                            }
                            catch (System.OutOfMemoryException e)
                            {

                                throw new System.ArgumentOutOfRangeException(
                                    "Parameter index is out of range.", e);
                                ViewBag.Error = "Can't import xml file";
                            }
                            return View("UploadSaleFile");

                        }
                        else
                        {
                            ViewBag.Error = "File type is incorrect<br>";
                            return View();
                        }
                    }
                }

                return View();
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }



        public void XMLforClient(string xmlPath, List<Item> objOrders)
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


        public void XMLforClient(string xmlPath, List<ItemRemoteStore> objOrders)
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


        public ActionResult BrendsPhaeton() {

            UsersCARSEntities dc = new UsersCARSEntities();
            var brend= dc.PhaetonBrends.ToList();
            return View(brend);

        }



        public ActionResult CreateBrendsPhaeton()
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
            var brends= db.PhaetonBrends;
                db.SaveChanges();
                return View();
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }

        }



        [HttpPost]
        public ActionResult CreateBrendsPhaeton(PhaetonBrends brends)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                db.PhaetonBrends.Add(brends);
                db.SaveChanges();
                return RedirectToAction("BrendsPhaeton");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }

        }


        public ActionResult EditBrendsPhaeton(int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var brend =db.usmSelectBrendPhaetonByID1(id).FirstOrDefault() ;
                PhaetonBrends brends = new PhaetonBrends();
                brends.BrendName = brend.BrendName;
                brends.Code = brend.Code;
                brends.Description = brend.Description;
                brends.UserId = brend.UserId;
                brends.Id=brend.Id;
                brends.Markup = brend.Markup;

                var brendModel = db.PhaetonBrends.ToList();
                return View(brends);
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }

        }

        [HttpPost]
        public ActionResult EditBrendsPhaeton(PhaetonBrends brends, int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var brend = db.PhaetonBrends.Find(id);

                  if (brend.Id== id)
                    {
                        brend.BrendName= brends.BrendName;
                        brend.Code= brends.Code;
                        brend.Description= brends.Description;
                        brend.UserId= brends.UserId;
                        brend.Id= brends.Id ;
                        brend.Markup = brends.Markup;
                    db.SaveChanges();
                }

               
                return RedirectToAction("BrendsPhaeton");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }

        public ActionResult DeleteBrendsPhaeton(int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var brend = db.PhaetonBrends.Where(a => a.Id.Equals(id)).FirstOrDefault();


                if (brend.Id == id)
                {
                    db.PhaetonBrends.Remove(brend);
                }
                db.SaveChanges();
                return RedirectToAction("BrendsPhaeton");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }


















        public ActionResult Contact() {
            var db = new UsersCARSEntities();
            var contacts = db.UrlAutoOriginal.ToList();
            ViewBag.contacts = contacts;
            return View();
        }
        public ActionResult CreateContact()
        {
            UrlAutoOriginal contants = new UrlAutoOriginal();
            return View(contants);
        }

        [HttpPost]
        public ActionResult CreateContact(UrlAutoOriginal contants)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                db.UrlAutoOriginal.Add(contants);
                db.SaveChanges();
                return RedirectToAction("Contact");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }

        }



        public ActionResult EditContact(int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                UrlAutoOriginal contact = db.UrlAutoOriginal.Where(a => a.id == id).FirstOrDefault();
                UrlAutoOriginal contacts = new UrlAutoOriginal();
                contacts.UrlAuto = contact.UrlAuto;
               
               
                return View(contacts);
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }

        }

        [HttpPost]
        public ActionResult EditContact(UrlAutoOriginal contact, int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var contacts = db.UrlAutoOriginal.Where(a => a.id.Equals(id)).FirstOrDefault();
               

                if (contacts.id == id)
                {

                    contacts.UrlAuto = contact.UrlAuto;
                  
                }

                db.SaveChanges();
                return RedirectToAction("Contact");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }


        public ActionResult DeleteContact(int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var contact = db.UrlAutoOriginal.Where(a => a.id.Equals(id)).FirstOrDefault();


                if (contact.id == id)
                {
                    db.UrlAutoOriginal.Remove(contact);
                }
                db.SaveChanges();
                return RedirectToAction("Contact");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }


        


        public ActionResult AllBrends(int id)
        {
          if (Session["LogedUserId"] != null)
            {
                UsersCARSEntities dc = new UsersCARSEntities();
                var AllBrend = (from prod in dc.usmSelectAllRemnants() group prod by prod.Brend into newGroup where newGroup.Key != null select new { Brand = newGroup.Key }).ToList();
                ViewBag.AllBrend = AllBrend;

               foreach (var i in AllBrend)
                {
                   var listOfUsers = (from prod in dc.Users select prod.UserId);
                    string shortBrend = Convert.ToString(i.Brand.Split(' ').ToList()[0].ToString());
                
                   var AllBrands = dc.Brends.Where(a => a.BrendName.ToString() == shortBrend && a.UserId == id).FirstOrDefault();
                   if (AllBrands != null)
                    {
                       if (i.Brand.Split(' ').ToList()[0].ToString() == AllBrands.BrendName.Split(' ').ToList()[0].ToString())
                        {  }
                        else
                        {
                           dc.Brends.Add(
                            new Brends
                            {
                                BrendName = i.Brand.Split(' ').ToList()[0].ToString(),
                                UserId = id
                            });
                        }
                    }
                    else
                    {

                        dc.Brends.Add(
                         new Brends
                         {
                             BrendName = i.Brand.Split(' ').ToList()[0].ToString(),
                             UserId = id
                         });


                     
                    }
                    dc.SaveChanges();
                    ViewBag.ResultBrends = dc.Brends.ToList();
                }

                var AllBrends = (from prod in dc.Brends where (int)prod.UserId == id select prod).ToList();
                var UsersOfBrend = (from prod in dc.Users where (int)prod.UserId == id select prod).FirstOrDefault();
                ViewBag.UsersOfBrend = UsersOfBrend.UserName + " / " + UsersOfBrend.FamilyName;
                ViewBag.AllBrends = AllBrends;
            }
            else
            {
                return View("LoginAllOrders");
            }

            return View();
        }

        public ActionResult EditBrend(String brandd, int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();

                //  Brends brand = db.Brends.Where(a => a.UserId.Equals(id) && a.BrendName.Equals(brandd)).FirstOrDefault();
                Brends brand = (from prod in db.Brends where (int)prod.UserId == id && prod.BrendName == (string)brandd select prod).FirstOrDefault();

                Brends brands = new Brends();
                brands.UserId = brand.UserId;
                brands.BrendName = brand.BrendName;
                brands.Discount = brand.Discount;

                return View(brands);
            }
            else
            {
                return View("LoginAdminIndex");
            }


        }
        [HttpPost]
        public ActionResult EditBrend(Brends brends, string brandd, int id)
        {
            if (Session["LogedUserId"] != null)
            {
                UsersCARSEntities db = new UsersCARSEntities();
                Brends brend = (from prod in db.Brends where (int)prod.UserId == brends.Id && prod.BrendName == brends.BrendName select prod).FirstOrDefault();

                brend.UserId = brends.UserId;
                brend.BrendName = brends.BrendName;
                brend.Discount = brends.Discount;
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
                return RedirectToAction("AllBrends/" + brends.UserId);
            }
            else
            {
                return View("LoginAdminIndex");
            }

        }
        //собираем все бренды в xml
        public void CreateXMLFileBrends(string xmlPath)
        {
            UsersCARSEntities dc = new UsersCARSEntities();


            var brends = dc.Remnants;

            var listBrends = (from prod in dc.Brends select prod).ToList();


            if (System.IO.File.Exists(xmlPath) == true)
            {
                System.IO.File.Delete(xmlPath);
            }

            using (XmlWriter writer = XmlWriter.Create(Server.MapPath(xmlPath)))
            {

                writer.WriteStartDocument();
                writer.WriteStartElement("Brands");

                foreach (var item in listBrends)
                {
                    writer.WriteStartElement("Brand");
                    writer.WriteElementString("Id", Convert.ToString(item.Id));
                    writer.WriteElementString("BrendName", Convert.ToString(item.BrendName));
                    writer.WriteElementString("UserId", Convert.ToString(item.UserId));
                    writer.WriteElementString("Discount", Convert.ToString(item.Discount));
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                ModelState.Clear();
            }
        }



        ///загружаем все бренды в таблицу dbo.Brends
        public List<Brends> Discount_xml(string xmlPath)
        {
            UsersCARSEntities dc = new UsersCARSEntities();

            var listBrends = (from prod in dc.Remnants group prod by prod.Brend).ToList();



            List<Brends> listOfBrends = new List<Brends>();
            try
            {
                XDocument xDoc = XDocument.Load(xmlPath);



                List<Brends> BrendsList = xDoc.Descendants("Brend").Select(
                        product => new Brends
                        {
                            Id = Convert.ToInt32(product.Element("Id").Value),
                            BrendName = product.Element("BrendName").Value,
                            Discount = Convert.ToInt32(product.Element("Discount").Value),
                            UserId = Convert.ToInt32(product.Element("UserId").Value),
                        }).ToList();




                foreach (Brends i in BrendsList)
                {
                    var listOfUsers = (from prod in dc.Users select prod.UserId);

                    var AllBrands = dc.Brends.Where(a => a.BrendName.Equals(i.BrendName) && a.UserId.Equals(i.UserId)).FirstOrDefault();

                    if (AllBrands != null)
                    {
                        AllBrands.Id = i.Id;
                        AllBrands.BrendName = i.BrendName;
                        AllBrands.UserId = i.UserId;
                        AllBrands.Discount = i.Discount;
                    }
                    else
                    {
                        dc.Brends.Add(
                          new Brends
                          {
                              BrendName = i.BrendName,
                              UserId = i.UserId,
                              Discount = i.Discount,
                              Id = i.Id
                          });
                    }
                    dc.SaveChanges();
                    ViewBag.ResultBrends = dc.Brends.ToList();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex + "Can't import xml file";
            }
            return listOfBrends;
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
        public ActionResult AllOrderses(string SortOrder, Users u, string SelectedContragent, string SelectedContract, string DateFrom, string DateTo) {
            UsersCARSEntities dc = new UsersCARSEntities();
            var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
            if (Session["LogedUserId"] != null)
            {
                var UserID = 0;
                var AllUsers = from UserId in dc.Users select UserId;
                foreach (Users UserId in AllUsers)
                {
                    if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                    {
                        UserID = UserId.UserId;
                    }
                }
                var products = (from prod in dc.Orders orderby prod.Date descending select prod).ToList();
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
                            products = (from prod in products where prod.Status == "В ожидании"  select prod).ToList();
                            break;
                        case "OnMyWay_desc":
                            products = (from prod in products where prod.Status == "В пути"  select prod).ToList();
                            break;
                        case "inReserve_desc":
                            products = (from prod in products where prod.Status == "В резерве"  select prod).ToList();
                            break;
                        case "Ordered_desc":
                            products = (from prod in products where prod.Status == "Заказан"  select prod).ToList();
                            break;
                        case "InTheAssembly_desc":
                            products = (from prod in products where prod.Status == "На сборке"  select prod).ToList();
                            break;
                        case "Shipped_desc":
                            products = (from prod in products where prod.Status == "Отгружен" select prod).ToList();
                            break;
                        case "Canceled_desc":
                            products = (from prod in products where prod.Status == "Отменен"  select prod).ToList();
                            break;
                        case "PartiallyShipped_desc":
                            products = (from prod in products where prod.Status == "Частично отгружен"  select prod).ToList();
                            break;
                        default:
                            products = (from prod in products select prod).ToList();
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
                    products = (from prod in products where ((prod.Date > dtDateFrom|| prod.Date == dtDateFrom) && (prod.Date < dtDateTo|| prod.Date == dtDateTo))  select prod).ToList();
                 
                }
                else
                {
                    dtDateFrom = DateTime.Now;
                    dtDateTo = DateTime.Now;
                    products = products.ToList();
                  
                }

                
                ///////////////////View of contragent

                var contragents = (from ctg in dc.Users select ctg).ToList();

                var ctrag = (from ctag in contragents select ctag);


                if (!String.IsNullOrEmpty(SelectedContragent))
                {
                    ctrag = ctrag.Where(ctag => ctag.UserName.Trim().Equals(SelectedContragent.Trim()));

                    var UniqueContragent = (from Ucontrag in ctrag
                                            group Ucontrag by Ucontrag.UserName into newGroup
                                            where newGroup.Key != null
                                            orderby newGroup.Key
                                            select new { Contragent = newGroup.Key }).ToList();
                    ViewBag.UniqueContragent = UniqueContragent.Select(m => new SelectListItem
                    { Value = m.Contragent, Text = m.Contragent }).ToList();

                    ViewBag.SelectedContragent = SelectedContragent;


                    products = (from prod in products where prod.Users.UserName == SelectedContragent select prod).ToList();
                }
                else {
                    var UniqueContragent = (from Ucontrag in ctrag
                                            group Ucontrag by Ucontrag.UserName into newGroup
                                            where newGroup.Key != null
                                            orderby newGroup.Key
                                            select new { Contragent = newGroup.Key }).ToList();
                    ViewBag.UniqueContragent = UniqueContragent.Select(m => new SelectListItem
                    { Value = m.Contragent, Text = m.Contragent }).ToList();

                    ViewBag.SelectedContragent = SelectedContragent;

                }

              


                ViewBag.AllOrders1 = products.GroupBy(row =>
                  new
                  {
                      FirstField = row.NumOrder,
                      SecondField = row.Sklad,
                      ThirdField = row.Date,
                      FoursField = row.Status,
                      FiveField=row.Contragent
                  })
               .Select(
                  group => new Orders
                  {
                      NumOrder = group.Key.FirstField,
                      Sklad = group.Key.SecondField,
                      Date = group.Key.ThirdField,
                      Status = group.Key.FoursField,
                      Contragent= group.Key.FiveField
                  }).ToList();
                return View();
            }
            else
            {
                ViewBag.Message = "Вы не правильно ввели логин или пароль";
                return View("LoginAllOrders");
            }

        }

        public ActionResult LoginAllOrders()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoginAllOrders(Users u)
        {
            if (!ModelState.IsValid)
            {
                using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                    var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && a.UserRole.Equals("admin")).FirstOrDefault();
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
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("LoginAdminIndex");
                    }
                }
            }
            return View(u);
        }
      

        public ActionResult Go(string NumOrder)
        {
            using (UsersCARSEntities dc = new UsersCARSEntities())
            {
                //var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password)).FirstOrDefault();
                List<Orders> orders = new List<Orders>();
                orders = dc.Orders.Where(a => a.NumOrder.Equals(NumOrder)).ToList();
                ViewBag.OrdersBByNumOrder = orders;
            }
            return View("Go");
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

        public ActionResult UploadSaleFile() {
            return View();
        }

        public ActionResult UploadExcelSaleFile(HttpPostedFileBase excelfile) {

            UsersCARSEntities dcC = new UsersCARSEntities();
            int UserID = 0;
            if (Session["LogedUserId"]!= null)
                {
                    foreach (Users UserId in dcC.Users.ToList())
                    {
                        if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                        {
                            UserID = UserId.UserId;
                        }
                    }
                    var Users = (from prod in dcC.Users where prod.UserId == UserID && prod.UserRole == "admin" select prod).FirstOrDefault();
                    if (Users != null)
                    {
                   

                        if (excelfile == null || excelfile.ContentLength == 0)
                            {
                                ViewBag.Error = "Please select excel file";
                                return View("UploadSaleFile");
                            }
                           else
                            {
                               if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                                {
                                    string path = Server.MapPath("~/Content/excel/" + excelfile.FileName);
                                    excelfile.SaveAs(path);
                                    try
                                     {
                                        Workbook workbook = Workbook.Load(path);
                                        Worksheet worksheet = workbook.Worksheets[0];
                                        List<Sale> listProduct = new List<Sale>();
                                        using (UsersCARSEntities dc = new UsersCARSEntities())
                                        {
                                            var remnantsRemove = dc.Sale.ToList();
                                            foreach (var item in remnantsRemove)
                                            {
                                                dc.Sale.Remove(item);
                                                dc.SaveChanges();
                                            }

                                           for(int row = 1; row <= worksheet.Cells.Rows.Count; row++)
                                             {
                                                int i = 1;
                                                listProduct.Add(new Sale()
                                                {
                                                    Id = i++,
                                                    Articul = (worksheet.Cells[row, 0]).ToString(),
                                                    Brend = (worksheet.Cells[row, 1]).ToString(),
                                                    Description =(worksheet.Cells[row, 4]).ToString(),
                                                    Category = (worksheet.Cells[row, 5]).ToString(),
                                                    NameGood = (worksheet.Cells[row, 2]).ToString(),
                                                    Adaptation = (worksheet.Cells[row, 3]).ToString(),
                                                    CurrancyCode = "KGS"
                                                });
                                             }
                                            foreach (Sale i in listProduct)
                                             {
                                            i.NameGood= i.NameGood.Replace("  ", string.Empty);
                                   

                                            if (i.Adaptation.Length > 500) {

                                                while (i.Adaptation.Length > 500) {
                                                    i.Adaptation= i.Adaptation.Remove(500, i.Adaptation.Length- 500);
                                                }
                                        

                                            }
                                    
                                               var v = dc.Sale.Where(a => a.Articul.Equals(i.Articul)&& a.Brend.Equals(i.Brend)).FirstOrDefault();

                                             if (i.Description != "" && i.Category != "") 
                                              {
                                                if (v != null)
                                                 {
                                                    if ((float.Parse(i.Description) % 1 == 0 && float.Parse(i.Description) != 0)
                                                    && (float.Parse(i.Category)) % 1 == 0 && float.Parse(i.Category) != 0)
                                                     {
                                                        v.NameGood = i.NameGood;
                                                    i.Price = Math.Round((float)(float.Parse(i.Description)), 2, MidpointRounding.AwayFromZero);
                                                    i.Sales = Math.Round((float)(float.Parse(i.Category)), 2, MidpointRounding.AwayFromZero);
                                                    v.Adaptation = i.Adaptation;
                                                        v.Articul = i.Articul;
                                                        v.Brend = i.Brend;
                                                        v.Code = i.Code;
                                                        v.CurrancyCode = i.CurrancyCode;
                                                        v.Days = i.Days;
                                                        v.MethodDelivery = i.MethodDelivery;
                                                        v.Min_Krat = i.Min_Krat;
                                                        v.Supplies = i.Supplies;
                                                        v.V_P = i.V_P;
                                                     }
                                                    else
                                                     {
                                                        v.NameGood = i.NameGood;
                                                    i.Price = Math.Round((float)(float.Parse(i.Description)), 2, MidpointRounding.AwayFromZero);
                                                    i.Sales = Math.Round((float)(float.Parse(i.Category)), 2, MidpointRounding.AwayFromZero);
                                                    v.Adaptation = i.Adaptation;
                                                        v.Articul = i.Articul;
                                                        v.Brend = i.Brend;
                                                        v.Code = i.Code;
                                                        v.CurrancyCode = i.CurrancyCode;
                                                        v.Days = i.Days;
                                                        v.MethodDelivery = i.MethodDelivery;
                                                        v.Min_Krat = i.Min_Krat;
                                                        v.Supplies = i.Supplies;
                                                        v.V_P = i.V_P;
                                                     }
                                                 }
                                                else
                                                 {
                                                   if ((float.Parse(i.Description) % 1 == 0 && float.Parse(i.Description) != 0)
                                                     && (float.Parse(i.Category) % 1 == 0 && float.Parse(i.Category) != 0))
                                                    {
                                                        i.Price = Math.Round((float)(float.Parse(i.Description)), 2, MidpointRounding.AwayFromZero);
                                                        i.Sales = Math.Round((float)(float.Parse(i.Category)), 2, MidpointRounding.AwayFromZero) ;

                                                        i.Description = "";
                                                        i.Category = "";
                                                        dc.Sale.Add(i);
                                                    }
                                                    else
                                                    {
                                                    i.Price = Math.Round((float)(float.Parse(i.Description)), 2, MidpointRounding.AwayFromZero);
                                                    i.Sales = Math.Round((float)(float.Parse(i.Category)), 2, MidpointRounding.AwayFromZero);
                                                    i.Description = "";
                                                        i.Category = "";
                                                        dc.Sale.Add(i);
                                                    }
                                                 }

                                            }
                                                dc.SaveChanges();
                                                ViewBag.ResultImport = dc.Remnants.ToList();
                                            }
                                        }
                                    }
                                catch (System.OutOfMemoryException e)
                                {
                          
                                    throw new System.ArgumentOutOfRangeException(
                                        "Parameter index is out of range.", e);
                                        ViewBag.Error = "Can't import xml file";
                                }
                                  return View("UploadSaleFile");

                            }
                            else
                             {
                                 ViewBag.Error = "File type is incorrect<br>";
                                return View();
                             }
                          }
                }

                return View();
            }
           else
                {
                return RedirectToAction("LoginAdminIndex");
            }
        }



        // GET: AdminPanel
        public ActionResult Index()
        {
            UsersCARSEntities dc = new UsersCARSEntities();
            int UserID = 0;
            if (Session["LogedUserId"] != null)
            {

                foreach (Users UserId in dc.Users.ToList())
                {
                    if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                    {
                        UserID = UserId.UserId;
                    }
                }
                var Users = (from prod in dc.Users where prod.UserId == UserID && prod.UserRole == "admin" select prod).FirstOrDefault();
                if (Users != null) {

                    return View();
                }
               return View();
            }
            else {
                return RedirectToAction("LoginAdminIndex");
            }
              
        }


        public ActionResult LoginAdminIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoginAdminIndex(Users u)
        {
           if (!ModelState.IsValid)
            {
                using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                   var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && a.UserRole.Equals("admin")).FirstOrDefault();
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
                        return RedirectToAction("Index");
                    }
                    else
                    {
                       return View("LoginAdminIndex");
                    }
                }
            }
            return View(u);
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
            return RedirectToAction("LoginAdminIndex");
        }


        public ActionResult AllUsers() {

            UsersCARSEntities dc = new UsersCARSEntities();
            
            int UserID = 0;
            if (Session["LogedUserId"] != null)
            {

                foreach (Users UserId in dc.Users.ToList())
                {
                    if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                    {
                        UserID = UserId.UserId;
                    }
                }
                var Users = (from prod in dc.Users where prod.UserId == UserID && prod.UserRole == "admin" select prod).FirstOrDefault();
                if (Users != null)
                {
                    ViewBag.AllUsersForAdmin = dc.Users.ToList();
                }
               return View();
            }
            else {
                return RedirectToAction("LoginAdminIndex");
            }
          
        }
        public ActionResult DeleteUser(int id) {

            UsersCARSEntities dc = new UsersCARSEntities();

            int UserID = 0;
            if (Session["LogedUserId"] != null)
            {

                foreach (Users UserId in dc.Users.ToList())
                {
                    if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                    {
                        UserID = UserId.UserId;
                    }
                }
                var Users = (from prod in dc.Users where prod.UserId == UserID && prod.UserRole == "admin" select prod).FirstOrDefault();
                if (Users != null)
                {
                    var db = new UsersCARSEntities();
                    var user = db.Users.Where(a => a.UserId.Equals(id)).FirstOrDefault();

                    if (user.UserId == id)
                    {
                        db.Users.Remove(user);
                    }
                    db.SaveChanges();

                }

            }
            else {
                return RedirectToAction("LoginAdminIndex");
            }
          return View();

        }


        public ActionResult EditUser(int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                Users user = db.Users.Where(a => a.UserId == id).FirstOrDefault();

                Users users = new Users();
                users.ConfirmPassword = user.Password;
                users.Email = user.Email;
                users.FamilyName = user.FamilyName;
                users.FullName = user.FullName;
                users.UserRole = user.UserRole;
                users.Password = user.Password;
                users.Orders = user.Orders;
                users.ContactPhone = user.ContactPhone;
                users.UserId = user.UserId;
                users.UserName = user.UserName;
                users.Discount = user.Discount;
                users.DiscountPhaeton = user.DiscountPhaeton;

                users.Service = user.Service;
                return View(users);
            }
            else
            {
                return View("LoginAdminIndex");
            }

        }

        [HttpPost]
        public ActionResult EditUser(Users users, int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                Users user = db.Users.Where(a => a.UserId.Equals(id)).FirstOrDefault();

                if (user.UserId == id)
                {
                    user.ConfirmPassword= users.ConfirmPassword ;
                    user.Email= users.Email ;
                    user.FamilyName= users.FamilyName ;
                    user.FullName=users.FullName  ;
                    user.UserRole= users.UserRole ;
                    user.Password= users.Password;
                    user.Orders= users.Orders  ;
                    user.ContactPhone= users.ContactPhone ;
                   
                    user.UserName=users.UserName ;
                    user.Discount=users.Discount;
                    user.DiscountPhaeton= users.DiscountPhaeton;

                    user.Service= users.Service  ;
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
                return RedirectToAction("AllUsers");
            }
            else
            {
                return View("LoginAdminIndex");
            }
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(Users user)
        {
            UsersCARSEntities dc = new UsersCARSEntities();

            int UserID = 0;
            if (Session["LogedUserId"] != null)
            {

                foreach (Users UserId in dc.Users.ToList())
                {
                    if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                    {
                        UserID = UserId.UserId;
                    }
                }
                var Users = (from prod in dc.Users where prod.UserId == UserID && prod.UserRole == "admin" select prod).FirstOrDefault();
                if (Users != null)
                {
                    var db = new UsersCARSEntities();

                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return View("AllUsers");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }


        }

     

        public ActionResult AddPicture()
        {
            Aksii aksiya = new Aksii();
            return View(aksiya);
        }

        [HttpPost]
        public ActionResult AddPicture(Aksii aksiya, HttpPostedFileBase image1)
        {
            var db = new UsersCARSEntities();
            if (image1!=null) {
                aksiya.FullPic = new byte[image1.ContentLength];
                image1.InputStream.Read(aksiya.FullPic,0,image1.ContentLength);
            }
            db.Aksii.Add(aksiya);
            db.SaveChanges();
            return View(aksiya);
        }


        public ActionResult LoginDescriptionFullAksii()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginDescriptionFullAksii(Users u) {
           if (!ModelState.IsValid)
            {
               using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                    var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && a.UserRole.Equals("admin")).FirstOrDefault();
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
                        return RedirectToAction("DescriptionFullAksii");

                    }
                    else
                    {
                        return View("LoginDescriptionFullAksii");
                    }
                }
            }
            return View(u);
        }

        public ActionResult DescriptionFullAksii()
        {
            List<Aksii> aksii = new List<Aksii>();
            if (Session["LogedUserId"] != null)
            {

                Aksii aksiya = new Aksii();
              
                using (UsersCARSEntities dc = new UsersCARSEntities())
                {
                    aksii = dc.Aksii.ToList();
                    if (aksii != null)
                    {
                        ViewBag.Aksii = aksii;
                    }
                    else
                    {
                        ViewBag.Aksii = "";
                    }
                }
                return View(aksii);
            }
            else {
                return RedirectToAction("LoginAdminIndex");
            }
         

        }


        public ActionResult CreateAksii() {

            Aksii aksiya = new Aksii();
            return View(aksiya);
        }

        [HttpPost]
        public ActionResult CreateAksii(Aksii aksiya, HttpPostedFileBase image1, HttpPostedFileBase imageLogo)
        {

            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                if (imageLogo != null)
                {
                    aksiya.BasePic = new byte[imageLogo.ContentLength];
                    imageLogo.InputStream.Read(aksiya.BasePic, 0, imageLogo.ContentLength);
                }

               if (image1 != null)
                {
                    aksiya.FullPic = new byte[image1.ContentLength];
                    image1.InputStream.Read(aksiya.FullPic, 0, image1.ContentLength);
                }

                db.Aksii.Add(aksiya);
                db.SaveChanges();
                // return View(aksiya);
                return RedirectToAction("DescriptionFullAksii");
            }
            else {
                return RedirectToAction("LoginAdminIndex");
            }
        }
        public ActionResult EditAksii(int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                Aksii aksiya = db.Aksii.Where(a => a.Id == id).FirstOrDefault();

                Aksii aksiyaa = new Aksii();
                aksiyaa.BasePic = aksiya.BasePic;
                aksiyaa.FullDescription = aksiya.FullDescription;
                aksiyaa.FullPic = aksiya.FullPic;
                aksiyaa.ShortDescription = aksiya.ShortDescription;
                aksiyaa.TitleText = aksiya.TitleText;
                return View(aksiyaa);
            }
            else {
                return RedirectToAction("LoginAdminIndex");
            }
        }

        [HttpPost]
        public ActionResult EditAksii(Aksii aksiyaa,int id, HttpPostedFileBase image1, HttpPostedFileBase imageLogo)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var aksiya = db.Aksii.Where(a => a.Id.Equals(id)).FirstOrDefault();
                if (imageLogo != null)
                {
                    aksiyaa.BasePic = new byte[imageLogo.ContentLength];
                    imageLogo.InputStream.Read(aksiyaa.BasePic, 0, imageLogo.ContentLength);
                }

                if (image1 != null)
                {
                    aksiyaa.FullPic = new byte[image1.ContentLength];
                    image1.InputStream.Read(aksiyaa.FullPic, 0, image1.ContentLength);
                }

                if (aksiya.Id == id)
                {
                    aksiya.BasePic = aksiyaa.BasePic;
                    aksiya.FullDescription = aksiyaa.FullDescription;
                    aksiya.FullPic = aksiyaa.FullPic;
                    aksiya.ShortDescription = aksiyaa.ShortDescription;
                    aksiya.TitleText = aksiyaa.TitleText;
                }
                db.SaveChanges();
                return RedirectToAction("DescriptionFullAksii");
            }
            else {
                return RedirectToAction("LoginAdminIndex");
            }
        }

        public ActionResult DeleteAksii(Aksii aksiyaa, int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var aksiya = db.Aksii.Where(a => a.Id.Equals(id)).FirstOrDefault();
               

                if (aksiya.Id == id)
                {
                    db.Aksii.Remove(aksiya);
                }
                db.SaveChanges();
                return RedirectToAction("DescriptionFullAksii");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }

        public ActionResult Vacancies() {
            if (Session["LogedUserId"] != null)
            {
                List<Vacancies> vacancies = new List<Vacancies>();
                Vacancies vacancie = new Vacancies();

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
                return View(vacancies);
            }
            else {
                return RedirectToAction("LoginAdminIndex");
            }
           
        }

        public ActionResult CreateVacancies() {
            Vacancies vacancie = new Vacancies();
            return View(vacancie);
        }

        [HttpPost]
        public ActionResult CreateVacancies(Vacancies vacancie)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                db.Vacancies.Add(vacancie);
                db.SaveChanges();
              return RedirectToAction("Vacancies");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
            
        }

        public ActionResult EditVacancies(int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                Vacancies vacancie = db.Vacancies.Where(a => a.Id == id).FirstOrDefault();
                Vacancies vacancies = new Vacancies();
                vacancies.Title = vacancie.Title;
                vacancies.Demands = vacancie.Demands;
                vacancies.Duties = vacancie.Duties;
                vacancies.WorkConditions = vacancie.WorkConditions;
                return View(vacancies);
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }

        }

        [HttpPost]
        public ActionResult EditVacancies(Vacancies vacancie, int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var vacancies = db.Vacancies.Where(a => a.Id.Equals(id)).FirstOrDefault();
                //  Vacancies vacancies = new Vacancies();


                if (vacancies.Id==id) {

                    vacancies.Title = vacancie.Title;
                    vacancies.Demands = vacancie.Demands;
                    vacancies.Duties = vacancie.Duties;
                    vacancies.WorkConditions = vacancie.WorkConditions;

                }
               
                db.SaveChanges();
                return RedirectToAction("Vacancies");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }


        public ActionResult DeleteVacancies(int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var vacancie = db.Vacancies.Where(a => a.Id.Equals(id)).FirstOrDefault();


                if (vacancie.Id == id)
                {
                    db.Vacancies.Remove(vacancie);
                }
                db.SaveChanges();
                return RedirectToAction("Vacancies");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }

        public ActionResult LoginVacancies()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginVacancies(Users u)
        {
            if (!ModelState.IsValid)
            {
                using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                    var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && a.UserRole.Equals("admin")).FirstOrDefault();
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
                        return RedirectToAction("Vacancies");

                    }
                    else
                    {
                        return RedirectToAction("LoginAdminIndex");
                    }
                }
            }
            return View(u);
        }

        public ActionResult Blog()
        {
           List<Blog> blogs = new List<Blog>();
           if (Session["LogedUserId"] != null)
            {

                Blog blog = new Blog();

                using (UsersCARSEntities dc = new UsersCARSEntities())
                {
                    blogs = dc.Blog.ToList();
                    if (blogs != null)
                    {
                        ViewBag.Blogs = blogs;
                    }
                    else
                    {
                        ViewBag.blogs = "";
                    }
                }
                return View(blogs);
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }

        public ActionResult CreateBlog()
        {
            Blog blog = new Blog();
            return View(blog);
        }

        [HttpPost]
        public ActionResult CreateBlog(Blog blog, HttpPostedFileBase image1, HttpPostedFileBase imageLogo)
        {
          if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                if (imageLogo != null)
                {
                    blog.BasePic = new byte[imageLogo.ContentLength];
                    imageLogo.InputStream.Read(blog.BasePic, 0, imageLogo.ContentLength);
                }

                if (image1 != null)
                {
                    blog.FullPic = new byte[image1.ContentLength];
                    image1.InputStream.Read(blog.FullPic, 0, image1.ContentLength);
                }

                db.Blog.Add(blog);
                db.SaveChanges();
               return RedirectToAction("Blog");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }
        public ActionResult EditBlog(int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                Blog blog = db.Blog.Where(a => a.Id == id).FirstOrDefault();

                Blog blogs = new Blog();
                blogs.BasePic = blog.BasePic;
                blogs.FullDescription = blog.FullDescription;
                blogs.FullPic = blog.FullPic;
                blogs.ShortDescription = blog.ShortDescription;
                blogs.TitleText = blog.TitleText;
                return View(blogs);
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }

        [HttpPost]
        public ActionResult EditBlog(Blog blogs, int id, HttpPostedFileBase image1, HttpPostedFileBase imageLogo)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var blog = db.Blog.Where(a => a.Id.Equals(id)).FirstOrDefault();
                if (imageLogo != null)
                {
                    blogs.BasePic = new byte[imageLogo.ContentLength];
                    imageLogo.InputStream.Read(blogs.BasePic, 0, imageLogo.ContentLength);
                }

                if (image1 != null)
                {
                    blogs.FullPic = new byte[image1.ContentLength];
                    image1.InputStream.Read(blogs.FullPic, 0, image1.ContentLength);
                }

                if (blog.Id == id)
                {
                    blog.BasePic = blogs.BasePic;
                    blog.FullDescription = blogs.FullDescription;
                    blog.FullPic = blogs.FullPic;
                    blog.ShortDescription = blogs.ShortDescription;
                    blog.TitleText = blogs.TitleText;
                }
                db.SaveChanges();
                return RedirectToAction("Blog");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }

        public ActionResult DeleteBlog(Blog blogs, int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var blog = db.Blog.Where(a => a.Id.Equals(id)).FirstOrDefault();


                if (blog.Id == id)
                {
                    db.Blog.Remove(blog);
                }
                db.SaveChanges();
                return RedirectToAction("Blog");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }


        public ActionResult LoginBlog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginBlog(Users u)
        {
            if (!ModelState.IsValid)
            {
                using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                    var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && a.UserRole.Equals("admin")).FirstOrDefault();
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
                        return RedirectToAction("Blog");

                    }
                    else
                    {
                        return View("LoginBlog");
                    }
                }
            }
            return View(u);
        }


        public ActionResult About() {
            List<Section> sections = new List<Section>();
            if (Session["LogedUserId"] != null)
            {
               Section section = new Section();

                using (UsersCARSEntities dc = new UsersCARSEntities())
                {
                    sections = dc.Section.ToList();
                    if (sections != null)
                    {
                        ViewBag.Sections = sections;
                    }
                    else
                    {
                        ViewBag.Sections = "";
                    }
                }
                return View(sections);
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }
        public ActionResult CreateAbout()
        {
            Section section = new Section();
            return View(section);
        }

        [HttpPost]
        public ActionResult CreateAbout(Section section)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
               
                db.Section.Add(section);
                db.SaveChanges();
                return RedirectToAction("About");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }

        public ActionResult EditAbout(int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                Section section = db.Section.Where(a => a.Id == id).FirstOrDefault();

                Section sections = new Section();
                sections.NameOfSection = section.NameOfSection;
              
                return View(sections);
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }

        [HttpPost]
       public ActionResult EditAbout(Section sections, int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var section = db.Section.Where(a => a.Id.Equals(id)).FirstOrDefault();
               

                if (section.Id == id)
                {
                    section.NameOfSection = sections.NameOfSection;
                  
                }
                db.SaveChanges();
                return RedirectToAction("About");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }

        public ActionResult DeleteAbout(Section sections, int id)
        {
            if (Session["LogedUserId"] != null)
            {
                var db = new UsersCARSEntities();
                var section = db.Section.Where(a => a.Id.Equals(id)).FirstOrDefault();


                if (section.Id == id)
                {
                    db.Section.Remove(section);
                }
                db.SaveChanges();
                return RedirectToAction("About");
            }
            else
            {
                return RedirectToAction("LoginAdminIndex");
            }
        }


        public ActionResult LoginAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAbout(Users u)
        {
            if (!ModelState.IsValid)
            {
                using (UsersCARSEntities dcl = new UsersCARSEntities())
                {
                  var v = dcl.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && a.UserRole.Equals("admin")).FirstOrDefault();
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
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("LoginAbout");
                    }
                }
            }
            return View(u);
        }


    }
}