using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Car.Models;
using System.Xml.Linq;
using System.Threading;
using System.Xml;
using System.Diagnostics;
using System.Web.Hosting;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Car.Controllers
{
    [Serializable]
    public class ImportExportDataController : Controller
    {
        // GET: ImportExportData
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CurrancyCodeNew()
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
            return View();
        }
        public ActionResult ExecuteBatchFile() {

            // System.Diagnostics.Process.Start(@“C:\listfiles.bat”);
            string str_Path = Server.MapPath("~/Content/batchFile") + "\\DB\\Reboot.bat";

            ProcessStartInfo processInfo = new ProcessStartInfo(str_Path);

            processInfo.UseShellExecute = false;

            Process batchProcess = new Process();

            batchProcess.StartInfo = processInfo;

            batchProcess.Start();
            return View();
        }

        public ActionResult Discount()
        {
           UsersCARSEntities dcv = new UsersCARSEntities();

            var AllDiscount = dcv.Discount.ToList();
            if(AllDiscount!=null) {
                foreach (var it in AllDiscount)
                {
                  dcv.Discount.Remove(it);
                }
                dcv.SaveChanges();
            } 

            try
            {
                var xmlPath = Server.MapPath("~/Content/xml/skidka.xml");

                ViewBag.xmlPath = xmlPath;

                XDocument xDoc = XDocument.Load(xmlPath);

                List<Discount> productList = xDoc.Descendants("Order").Select(
                        product => new Discount
                        {
                            id = Convert.ToInt32(product.Element("id").Value),
                            //Id_Contraget = product.Element("name").Value,
                            //Name_Contragent = Convert.ToString(product.Element("value").Value),
                            Discount1 = Convert.ToDouble(product.Element("skidka").Value),
                            DateBegin = Convert.ToDateTime(product.Element("dateN").Value),
                            DateEnd = Convert.ToDateTime(product.Element("dateK").Value),
                            Name_Artikul = Convert.ToString(product.Element("articul").Value),
                            LimitPrice = Convert.ToDouble(product.Element("ogranichenie").Value)

                        }).ToList();

                ViewBag.DiscountS = dcv.Discount.ToList();

                using (UsersCARSEntities dcCC = new UsersCARSEntities())
                {
                    foreach (var i in productList)
                    {

                        var vCC = dcCC.Discount.Where(a => a.id.Equals(i.id)).FirstOrDefault();

                        if (vCC != null)
                        {
                            vCC.id = i.id;
                            vCC.DateBegin = i.DateBegin;
                            vCC.DateEnd = i.DateEnd;
                            vCC.Discount1 = i.Discount1;
                            vCC.Id_Contraget = i.Id_Contraget;
                            vCC.LimitPrice = i.LimitPrice;
                            vCC.Name_Artikul = i.Name_Artikul;
                            vCC.Name_Contragent = i.Name_Contragent;
                            vCC.Name_Sklad = i.Name_Sklad;
                        }
                        else
                        {
                            dcCC.Discount.Add(i);
                        }
                        dcCC.SaveChanges();
                        ViewBag.DiscountS = dcCC.Discount.ToList();
                    }
                }
            }
            catch
            {
                ViewBag.Error = "Can't import xml file";
            }

            ////////////////////////////////////////////////////////////////
            return View();
        }

        public ActionResult ExportXml()
        {
            UsersCARSEntities dc = new UsersCARSEntities();
            XmlDocument xml = new XmlDocument();
          
            List<Orders> StatusOrders = new List<Orders>();
            StatusOrders = dc.Orders.ToList();
            XDocument xDoc = XDocument.Load(Server.MapPath("~/Content/xml/zakaz.xml"));

            foreach (var item in xDoc.Descendants("Order"))
            {
                string numOrder = (string)item.Element("NumOrder");
                string status = (string)item.Element("status");
                StatusOrders = dc.Orders.Where(a => a.NumOrder.Equals(numOrder)).ToList();
                foreach (var itemNOrder in StatusOrders) {
                  itemNOrder.Status = status;
                    if (itemNOrder.Service=="Emex") {
                        itemNOrder.CodeOfBody = status;
                    }
                }
                dc.SaveChanges();
                ModelState.Clear();
            }

            List<Orders> objOrders = new List<Orders>();
            string xmlPath = @"~/Content/xml/productsOrder.xml";
            if (System.IO.File.Exists(Server.MapPath("~/Content/xml/productsOrder.xml")))
             {
                System.IO.File.Delete(Server.MapPath("~/Content/xml/productsOrder.xml"));
             }
            


            objOrders = dc.Orders.ToList();
            List<Orders> objOrders1C = new List<Orders>();
            foreach (Orders item in objOrders)
            {
                if (item.Status == "В Ожидании" || item.CodeOfBody== "В Ожидании")
                 {
                   objOrders1C.Add(item);
                 }
            }



           if (objOrders1C.Count != 0)
            {
                CreateXMLFile(xmlPath, objOrders);
                ViewBag.Message = "XML file created successfully.";
                ViewBag.ResultImport = objOrders;
            }
            else {
                ViewBag.ResultImport = "!!!!!CCCCCCCCCC";
              
            }

            
            return View("Success");
        }

       public void CreateXMLFile(string xmlPath, List<Orders> objOrders)
        {
          UsersCARSEntities dc = new UsersCARSEntities();

           var   orders = dc.Orders.ToList();
            if (System.IO.File.Exists(xmlPath) == true)
            {
                System.IO.File.Delete(xmlPath);
            }

            using (XmlWriter writer = XmlWriter.Create(Server.MapPath(xmlPath)))
            {

                writer.WriteStartDocument();
                writer.WriteStartElement("Products");

               foreach (Orders item in orders)
                {
                    if (item.Status == "В Ожидании" || item.CodeOfBody== "В Ожидании")
                    {
                        writer.WriteStartElement("Product");
                        writer.WriteElementString("NameGood", item.NameGood);
                        writer.WriteElementString("code", item.Articul);
                        writer.WriteElementString("Brend", item.Brend);
                        writer.WriteElementString("Articul", item.Articul);
                        writer.WriteElementString("supplies", item.Supplies.ToString());
                        writer.WriteElementString("methodDelivery", item.MethodDelivery);
                        writer.WriteElementString("Clients", item.Clients.ToString());
                        writer.WriteElementString("NumOrder", item.NumOrder);
                        writer.WriteElementString("Service", item.Service);
                        writer.WriteElementString("Contragent", item.Contragent);
                        writer.WriteElementString("existence", item.Existence.ToString());
                        writer.WriteElementString("price", item.Price.ToString());
                        writer.WriteElementString("days", item.Days);
                        writer.WriteElementString("Discount", item.Discount.ToString());
                        writer.WriteEndElement();
                        item.VIN = "1C";
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();

                dc.SaveChanges();
                ModelState.Clear();
            }
        }
       public ActionResult Success()
       {
            var StringS = "";
         try
          {
              List<Remnants> productList = new List<Remnants>();
               var xmlPath = Server.MapPath("~/Content/xml/products.xml");
               ViewBag.xmlPath = xmlPath;
               XDocument xDoc = XDocument.Load(xmlPath);

                   productList = xDoc.Descendants("product").Select(
                      product => new Remnants
                      {
                          Id = Convert.ToInt32(product.Element("id").Value),
                          Code = product.Element("code").Value,
                          Brend = product.Element("brend").Value,
                          Articul = product.Element("articul").Value,
                          Name = product.Element("name").Value,
                          Supplies = Convert.ToInt32(product.Element("supplies").Value),
                          MethodDelivery = product.Element("methodDelivery").Value,
                          Price = Convert.ToDouble(product.Element("price").Value.Trim().Replace(" ", string.Empty)),
                          Existence = Convert.ToInt32(product.Element("existence").Value),
                          Days = product.Element("days").Value,
                          V_P = product.Element("v_p").Value,
                          Min_Krat = Convert.ToInt32(product.Element("min_krat").Value),
                          // VIN = product.Element("vin").Value,
                          CurrancyCode = product.Element("currancycode").Value,
                          Sklad = product.Element("Sklad").Value
                      }).ToList();
               

                

                StringS = productList.ToString();


                UsersCARSEntities dc = new UsersCARSEntities();
              

                ParallelOptions options = new ParallelOptions();
                    options.MaxDegreeOfParallelism = 1; // -1 is for unlimited. 1 is for sequential.
               
                    Parallel.ForEach(productList, options, i =>
                     {
                         var v = dc.Remnants.Where(a => a.Articul.Equals(i.Articul) && a.Sklad.Equals(i.Sklad)).FirstOrDefault();

                         if (v != null)
                         {
                             v.Name = i.Name;
                             v.Price = i.Price;
                             v.Existence = i.Existence;
                             v.Articul = i.Articul;
                             v.Brend = i.Brend;
                             v.Code = i.Code;
                             v.CurrancyCode = "USD";
                             //v.Date = i.Date;
                             v.Days = "";
                             v.MethodDelivery = i.MethodDelivery;
                             v.Min_Krat = i.Min_Krat;
                             v.Supplies = i.Supplies;
                             // v.VIN = i.VIN;
                             v.V_P = i.V_P;
                             v.Sklad = i.Sklad;
                         }
                         else
                         {
                             if (v.Brend!="EMEX") {
                                 dc.Remnants.Add(i);
                             }
                            
                         }
                         try
                         {
                             dc.SaveChanges();
                         }
                         catch (Exception ex)
                         {
                           ViewBag.Error = "Can't import xml file" + ex.Message;
                         }

                         ViewBag.ResultImport = dc.Remnants.ToList();
                         
                     });
              
               return View("Success");
            }
            catch(Exception ex)
            {

                ViewBag.Error = "Can't import xml file"+ StringS+ ex.Message;

                return View("Index");
            }
            
        }

        ///////////////////////////
        public ActionResult SuccessAllProducts()
        {
            var StringS = "";
            try
            {
                List<Remnants> productList = new List<Remnants>();
                var xmlPath = Server.MapPath("~/Content/xml/products(1).xml");
                ViewBag.xmlPath = xmlPath;
                XDocument xDoc = XDocument.Load(xmlPath);
                 productList = xDoc.Descendants("product").Select(
                      product => new Remnants
                      {
                          Id = Convert.ToInt32(product.Element("id").Value),
                          Code = product.Element("code").Value,
                          Brend = product.Element("brend").Value,
                          Articul = product.Element("articul").Value,
                          Name = product.Element("name").Value,
                          Supplies = Convert.ToInt32(product.Element("supplies").Value),
                          MethodDelivery = product.Element("methodDelivery").Value,
                          Price = Convert.ToDouble(product.Element("price").Value.Trim().Replace(" ", string.Empty)),
                          Existence = Convert.ToInt32(product.Element("existence").Value),
                          Days = product.Element("days").Value,
                          V_P = product.Element("v_p").Value,
                          Min_Krat = Convert.ToInt32(product.Element("min_krat").Value),
                                          // VIN = product.Element("vin").Value,
                          CurrancyCode = product.Element("currancycode").Value,
                          Sklad = product.Element("Sklad").Value
                      }).ToList();
 
                StringS = productList.ToString();
                
                UsersCARSEntities dc = new UsersCARSEntities();
                
                ParallelOptions options = new ParallelOptions();
                options.MaxDegreeOfParallelism = 1; // -1 is for unlimited. 1 is for sequential.

                Parallel.ForEach(productList, options, i =>
                {
                    var v = dc.Remnants.Where(a => a.Articul.Equals(i.Articul) && a.Sklad.Equals(i.Sklad)).FirstOrDefault();

                    if (v != null)
                    {
                        v.Name = i.Name;
                        v.Price = i.Price;
                        v.Existence = i.Existence;
                        v.Articul = i.Articul;
                        v.Brend = i.Brend;
                        v.Code = i.Code;
                        v.CurrancyCode = "USD";
                        //v.Date = i.Date;
                        v.Days = "";
                        v.MethodDelivery = i.MethodDelivery;
                        v.Min_Krat = i.Min_Krat;
                        v.Supplies = i.Supplies;
                        // v.VIN = i.VIN;
                        v.V_P = i.V_P;
                        v.Sklad = i.Sklad;
                    }
                    else
                    {
                        if (v.Brend != "EMEX")
                        {
                            dc.Remnants.Add(i);
                        }
                    }
                    try
                    {
                        dc.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "Can't import xml file" + ex.Message;
                    }

                    ViewBag.ResultImport = dc.Remnants.ToList();

                });

                return View("Success");
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't import xml file" + StringS + ex.Message;
                return View("Index");
            }

        }

        /// ///////////
        public ActionResult SuccessAnalogs()
        {
            var StringS = "";
            try
            {
                List<Analogs> productList = new List<Analogs>();
                var xmlPath = Server.MapPath("~/Content/xml/products(1).xml");
                ViewBag.xmlPath = xmlPath;
                XDocument xDoc = XDocument.Load(xmlPath);
                productList = xDoc.Descendants("product").Select(
                 product => new Analogs
                 {
                     Id = Convert.ToInt32(product.Element("id").Value),
                     CodeAnalog = product.Element("code").Value,
                     BrendAnalog = product.Element("brend").Value,
                     Brend = product.Element("brend").Value,
                     Articul = product.Element("articul").Value,
                     Name = product.Element("name").Value,
                     Supplies = Convert.ToInt32(product.Element("supplies").Value),
                     MethodDelivery = product.Element("methodDelivery").Value,
                         // Price = Convert.ToDouble(product.Element("price").Value.Trim().Replace(" ", string.Empty)),
                         Existence = Convert.ToInt32(product.Element("existence").Value),
                     Days = product.Element("days").Value,
                     V_P = product.Element("v_p").Value,
                     Min_Krat = Convert.ToInt32(product.Element("min_krat").Value),
                         // VIN = product.Element("vin").Value,
                         CurrancyCode = product.Element("currancycode").Value,
                     Sklad = product.Element("Sklad").Value
                 }).ToList();





                UsersCARSEntities dc = new UsersCARSEntities();


                ParallelOptions options = new ParallelOptions();
                options.MaxDegreeOfParallelism = 1; // -1 is for unlimited. 1 is for sequential.


                Parallel.ForEach(productList, options, i =>
                {
                    string code = i.CodeAnalog;
                    string producer = i.Brend;

                    ////////////////////////////webAnalog
                    ViewBag.ProductsRemnantsOshAnalog = "";
                    ViewBag.arrAnalog = "";

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
                                     new List<string> { code, producer}
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
                            var jObj = jObjectAnalog.First.ToString();

                            List<decimal> arrDiscount = new List<decimal>();

                            JArray rsltArray = null;

                            ViewBag.Analogues = "";
                            if (jObj == "\"data\": []")
                            {
                                ViewBag.arrAnalog = null;
                            }
                            else
                            {
                                var dataObj = (JObject)jObjectAnalog["data"][code][producer];
                                rsltArray = (JArray)dataObj["analogs"];
                                ViewBag.Analogues = rsltArray;

                                List<usmAnalogRemnantsInsert_1_Result> fullAnalogWithDiscunts = new List<usmAnalogRemnantsInsert_1_Result>();

                                foreach (var item in dataObj["analogs"])
                                {
                                    StringS = Convert.ToString(item[0]) + "// " + Convert.ToString(item[1]) + "// " + Convert.ToString(item[2]) + "// " + Convert.ToString(item[3]);

                                    string CodeAnalog = Convert.ToString(item[0]);
                                    string BrendAnalog = Convert.ToString(item[1]);
                                    Analogs vAnalog = dc.Analogs.Where(a => a.CodeAnalog.Equals(CodeAnalog) && a.BrendAnalog.Equals(BrendAnalog) && a.Code.Equals(code) && a.Brend.Equals(producer)).FirstOrDefault();


                                    if (vAnalog != null)
                                    {
                                        vAnalog.Name = Convert.ToString(item[3]);
                                        vAnalog.Existence = Convert.ToInt32(item[2]);
                                        vAnalog.Articul = code;
                                        vAnalog.Brend = producer;
                                        vAnalog.CodeAnalog = CodeAnalog;
                                        vAnalog.BrendAnalog = BrendAnalog;
                                        vAnalog.CurrancyCode = "USD";

                                    }
                                    else
                                    {
                                        dc.Analogs.Add(
                                        new Analogs
                                        {
                                            Name = Convert.ToString(item[3]),
                                            Price = null,
                                            Id = 1,
                                            VIN = null,
                                            catProdPeriod = null,
                                            CodeOfBody = null,
                                            V_P = null,
                                            ColorOfSalon = null,
                                            ColorOfBody = null,
                                            NumberOfBody = null,
                                            Contragent = null,
                                            ReleaseDate = null,
                                            Sale = null,
                                            Date = null,
                                            Days = null,
                                            Description = null,
                                            MethodDelivery = null,
                                            Min_Krat = null,
                                            NameAuto = null,
                                            Model = null,
                                            modProdPeriod = null,
                                            Options = null,
                                            Sklad = null,
                                            Status = null,
                                            Supplies = null,
                                            ViewOfContract = null,
                                            Existence = Convert.ToInt32(item[2]),
                                            Articul = code,
                                            Brend = producer,
                                            CodeAnalog = CodeAnalog,
                                            BrendAnalog = BrendAnalog,
                                            CurrancyCode = "USD",
                                        });
                                    }
                                }

                            }
                        }
                    }
                    /////////////////////////////End Analog





                    try
                    {
                        dc.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "Can't import xml file" + ex.Message;
                    }



                });

                return View("Success");
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't import xml file" + StringS + ex.Message;
                return View("Index");
            }

        }

        /////////////// 

        public ActionResult Success3()
        {
            var StringS = "";
            try
            {
                List<Remnants> productList = new List<Remnants>();
                var xmlPath = Server.MapPath("~/Content/xml/products.xml");
                ViewBag.xmlPath = xmlPath;
                XDocument xDoc = XDocument.Load(xmlPath);

                if (productList.Count > 15000 && productList.Count < 20000) {

                    productList = xDoc.Descendants("product").Select(
                      product => new Remnants
                      {
                          Id = Convert.ToInt32(product.Element("id").Value),
                          Code = product.Element("code").Value,
                          Brend = product.Element("brend").Value,
                          Articul = product.Element("articul").Value,
                          Name = product.Element("name").Value,
                          Supplies = Convert.ToInt32(product.Element("supplies").Value),
                          MethodDelivery = product.Element("methodDelivery").Value,
                          Price = Convert.ToDouble(product.Element("price").Value.Replace(',', '.').Trim().Replace(" ", string.Empty)),
                          Existence = Convert.ToInt32(product.Element("existence").Value),
                          Days = product.Element("days").Value,
                          V_P = product.Element("v_p").Value,
                          Min_Krat = Convert.ToInt32(product.Element("min_krat").Value),
                                              // VIN = product.Element("vin").Value,
                                              CurrancyCode = product.Element("currancycode").Value,
                          Sklad = product.Element("Sklad").Value
                      }).ToList();
                }



                StringS = productList.ToString();


                UsersCARSEntities dc = new UsersCARSEntities();


                ParallelOptions options = new ParallelOptions();
                options.MaxDegreeOfParallelism = 1; // -1 is for unlimited. 1 is for sequential.

                Parallel.ForEach(productList, options, i =>
                {
                    var v = dc.Remnants.Where(a => a.Articul.Equals(i.Articul) && a.Sklad.Equals(i.Sklad)).FirstOrDefault();

                    if (v != null)
                    {
                        v.Name = i.Name;
                        v.Price = i.Price;
                        v.Existence = i.Existence;
                        v.Articul = i.Articul;
                        v.Brend = i.Brend;
                        v.Code = i.Code;
                        v.CurrancyCode = "USD";
                        //v.Date = i.Date;
                        v.Days = "";
                        v.MethodDelivery = i.MethodDelivery;
                        v.Min_Krat = i.Min_Krat;
                        v.Supplies = i.Supplies;
                        // v.VIN = i.VIN;
                        v.V_P = i.V_P;
                        v.Sklad = i.Sklad;
                    }
                    else
                    {
                        dc.Remnants.Add(i);
                    }
                    try
                    {
                        dc.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "Can't import xml file" + ex.Message;
                    }

                    ViewBag.ResultImport = dc.Remnants.ToList();

                });

                return View("Success");
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't import xml file" + StringS + ex.Message;
                return View("Index");
            }

        }
        /// //////////////////////////////

        public ActionResult Success5()
        {
            var StringS = "";
            try
            {
                List<Remnants> productList = new List<Remnants>();
                var xmlPath = Server.MapPath("~/Content/xml/products.xml");
                ViewBag.xmlPath = xmlPath;
                XDocument xDoc = XDocument.Load(xmlPath);
                if (productList.Count > 25000)
                 {

                    productList = xDoc.Descendants("product").Select(
                      product => new Remnants
                      {
                          Id = Convert.ToInt32(product.Element("id").Value),
                          Code = product.Element("code").Value,
                          Brend = product.Element("brend").Value,
                          Articul = product.Element("articul").Value,
                          Name = product.Element("name").Value,
                          Supplies = Convert.ToInt32(product.Element("supplies").Value),
                          MethodDelivery = product.Element("methodDelivery").Value,
                          Price = Convert.ToDouble(product.Element("price").Value.Replace(',', '.').Trim().Replace(" ", string.Empty)),
                          Existence = Convert.ToInt32(product.Element("existence").Value),
                          Days = product.Element("days").Value,
                          V_P = product.Element("v_p").Value,
                          Min_Krat = Convert.ToInt32(product.Element("min_krat").Value),
                          // VIN = product.Element("vin").Value,
                          CurrancyCode = product.Element("currancycode").Value,
                          Sklad = product.Element("Sklad").Value
                      }).ToList();
                }



                StringS = productList.ToString();


                UsersCARSEntities dc = new UsersCARSEntities();


                ParallelOptions options = new ParallelOptions();
                options.MaxDegreeOfParallelism = 1; // -1 is for unlimited. 1 is for sequential.

                Parallel.ForEach(productList, options, i =>
                {
                    var v = dc.Remnants.Where(a => a.Articul.Equals(i.Articul) && a.Sklad.Equals(i.Sklad)).FirstOrDefault();

                    if (v != null)
                    {
                        v.Name = i.Name;
                        v.Price = i.Price;
                        v.Existence = i.Existence;
                        v.Articul = i.Articul;
                        v.Brend = i.Brend;
                        v.Code = i.Code;
                        v.CurrancyCode = "USD";
                        //v.Date = i.Date;
                        v.Days = "";
                        v.MethodDelivery = i.MethodDelivery;
                        v.Min_Krat = i.Min_Krat;
                        v.Supplies = i.Supplies;
                        // v.VIN = i.VIN;
                        v.V_P = i.V_P;
                        v.Sklad = i.Sklad;
                    }
                    else
                    {
                        dc.Remnants.Add(i);
                    }
                    try
                    {
                        dc.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "Can't import xml file" + ex.Message;
                    }

                    ViewBag.ResultImport = dc.Remnants.ToList();

                });

                return View("Success");
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't import xml file" + StringS + ex.Message;
                return View("Index");
            }

        }

        ////////////////////
        public ActionResult Success4()
        {
            var StringS = "";
            try
            {
                List<Remnants> productList = new List<Remnants>();
                var xmlPath = Server.MapPath("~/Content/xml/products.xml");
                ViewBag.xmlPath = xmlPath;
                XDocument xDoc = XDocument.Load(xmlPath);

                if (productList.Count > 20000 && productList.Count < 25000)
                {

                    productList = xDoc.Descendants("product").Select(
                      product => new Remnants
                      {
                          Id = Convert.ToInt32(product.Element("id").Value),
                          Code = product.Element("code").Value,
                          Brend = product.Element("brend").Value,
                          Articul = product.Element("articul").Value,
                          Name = product.Element("name").Value,
                          Supplies = Convert.ToInt32(product.Element("supplies").Value),
                          MethodDelivery = product.Element("methodDelivery").Value,
                          Price = Convert.ToDouble(product.Element("price").Value.Replace(',', '.').Trim().Replace(" ", string.Empty)),
                          Existence = Convert.ToInt32(product.Element("existence").Value),
                          Days = product.Element("days").Value,
                          V_P = product.Element("v_p").Value,
                          Min_Krat = Convert.ToInt32(product.Element("min_krat").Value),
                          // VIN = product.Element("vin").Value,
                          CurrancyCode = product.Element("currancycode").Value,
                          Sklad = product.Element("Sklad").Value
                      }).ToList();
                }



                StringS = productList.ToString();


                UsersCARSEntities dc = new UsersCARSEntities();


                ParallelOptions options = new ParallelOptions();
                options.MaxDegreeOfParallelism = 1; // -1 is for unlimited. 1 is for sequential.

                Parallel.ForEach(productList, options, i =>
                {
                    var v = dc.Remnants.Where(a => a.Articul.Equals(i.Articul) && a.Sklad.Equals(i.Sklad)).FirstOrDefault();

                    if (v != null)
                    {
                        v.Name = i.Name;
                        v.Price = i.Price;
                        v.Existence = i.Existence;
                        v.Articul = i.Articul;
                        v.Brend = i.Brend;
                        v.Code = i.Code;
                        v.CurrancyCode = "USD";
                        //v.Date = i.Date;
                        v.Days = "";
                        v.MethodDelivery = i.MethodDelivery;
                        v.Min_Krat = i.Min_Krat;
                        v.Supplies = i.Supplies;
                        // v.VIN = i.VIN;
                        v.V_P = i.V_P;
                        v.Sklad = i.Sklad;
                    }
                    else
                    {
                        dc.Remnants.Add(i);
                    }
                    try
                    {
                        dc.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "Can't import xml file" + ex.Message;
                    }

                    ViewBag.ResultImport = dc.Remnants.ToList();

                });

                return View("Success");
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't import xml file" + StringS + ex.Message;
                return View("Index");
            }

        }

        /// /////////////
        public ActionResult NBKR(HttpPostedFileBase xmlFile)
        {
           try
            {
                string xmlURL = "http://www.nbkr.kg/XML/daily.xml";
                XmlTextReader xmlReader = new XmlTextReader(xmlURL);
                List<CurrancyCode> productList = new List<CurrancyCode>();
                using (UsersCARSEntities dc = new UsersCARSEntities())
                {
                    productList = dc.CurrancyCode.ToList();
                    if (productList!=null) {
                      foreach (var ietm in productList)
                        {
                          dc.CurrancyCode.Remove(ietm);
                        }
                        dc.SaveChanges();
                        ModelState.Clear();

                    }
                   var NameCC = "";
                   
                    while (xmlReader.Read())
                    {
                        
                        switch (xmlReader.NodeType)
                        {
                          case XmlNodeType.Element: // The node is an element.
                                Console.Write("<" + xmlReader.Name);

                                while (xmlReader.MoveToNextAttribute()) // Read the attributes.
                                    Console.Write(" " + xmlReader.Name + "=’" + xmlReader.Value + "’");
                                Console.WriteLine(">");
                                if (xmlReader.Name== "ISOCode") {

                                    dc.CurrancyCode.Add(new CurrancyCode()
                                    {
                                        Name = xmlReader.Value,
                                        // Value = Convert.ToDouble(xmlReader.Value)
                                    });
                                    NameCC = xmlReader.Value;
                                    dc.SaveChanges();
                                    ModelState.Clear();
                                }
                                 break;
                            case XmlNodeType.Text: //Display the text in each element.
                                Console.WriteLine(xmlReader.Value);
                                if (NameCC!=null || NameCC!="") {

                                    productList = dc.CurrancyCode.Where(a => a.Name.Contains(NameCC)).ToList();
                                    foreach (var item in productList)
                                    {
                                        item.Value = Convert.ToDouble(xmlReader.Value);
                                    }

                                } 
                                dc.SaveChanges();
                                ModelState.Clear();
                                break;
                            case XmlNodeType.EndElement: //Display the end of the element.
                                Console.Write("</" + xmlReader.Name);
                                Console.WriteLine(">");
                                break;
                        }
                       }
                
                }
                return View("NBKR");
            }
            catch
            {
               ViewBag.Error = "Can't import xml file";
               return View("Index");
            }

        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase xmlFile)
        {
           
          //  if (xmlFile.ContentType.Equals("application/xml") || xmlFile.ContentType.Equals("text/xml"))
            //{
                try {
                    var xmlPath = Server.MapPath("~/Content/xml/products.xml");
              //      xmlFile.SaveAs(xmlPath);
                    XDocument xDoc = XDocument.Load(xmlPath);

                    List<Remnants> productList = xDoc.Descendants("product").Select(
                            product => new Remnants {
                            Id = Convert.ToInt32(product.Element("id").Value),
                            Code=product.Element("code").Value,
                            Brend=product.Element("brend").Value,
                            Articul=product.Element("articul").Value,
                            Name = product.Element("name").Value,
                            Supplies= Convert.ToInt32(product.Element("supplies").Value),
                            MethodDelivery=product.Element("methodDelivery").Value,
                            Price = Convert.ToDouble(product.Element("price").Value),
                            Existence=Convert.ToInt32(product.Element("existence").Value),
                            Days=product.Element("days").Value,
                            V_P=product.Element("v_p").Value,
                            Min_Krat= Convert.ToInt32(product.Element("min_krat").Value),
                           // VIN=product.Element("vin").Value,
                            CurrancyCode=product.Element("currancycode").Value,
                            Date=Convert.ToDateTime(product.Element("date").Value)
                            //Supplies1=Convert.ToString(product.Element("supplies1").Value)

                        }).ToList();
                    using (UsersCARSEntities dc=new UsersCARSEntities()) {
                        foreach (var i in productList) {

                            var v = dc.Remnants.Where(a => a.Id.Equals(i.Id)).FirstOrDefault();

                            if (v != null)
                            {
                                v.Id = i.Id;
                                v.Name = i.Name;
                                v.Price = i.Price;
                                v.Existence = i.Existence;
                                v.Articul = i.Articul;
                                v.Brend = i.Brend;
                                v.Code = i.Code;
                                v.CurrancyCode = i.CurrancyCode;
                                v.Date = i.Date;
                                v.Days = i.Days;
                                v.MethodDelivery = i.MethodDelivery;
                                v.Min_Krat = i.Min_Krat;
                                v.Supplies = i.Supplies;
                             //   v.VIN = i.VIN;
                                v.V_P = i.V_P;
                            }
                            else {
                            if (v.Brend != "EMEX")
                            {
                                dc.Remnants.Add(i);
                            }
                        }
                            dc.SaveChanges();
                            ViewBag.ResultImport = dc.Remnants.ToList();

                        }
                    }



                        return View("Success");
                }
                catch {
                    ViewBag.Error = "Can't import xml file";
                    return View("Index");
                }
           
        }

    }
    
}