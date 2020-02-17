using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Sql;
using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Text;
using System.Collections;
using System.Xml;

namespace Car.Controllers
{
    [Serializable]
    public class ShoppingCartController : Controller
    {

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


            if (System.IO.File.Exists(Server.MapPath(xmlPath)) == true)
            {
                System.IO.File.Delete(Server.MapPath(xmlPath));
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



        private UsersCARSEntities de = new UsersCARSEntities();
        [HttpPost]
        public ActionResult EditQuantity(int id, int quant)
        {
            if (Request.IsAjaxRequest())
            {
                List<Item> cart = (List<Item>)Session["cart"];
                foreach (Item item in cart)
                {
                   if (item.Pr.Id == id)
                    {
                        item.Quantity = quant;
                    }
                }
               return PartialView("AddToCart");
            }

            return View("Index");
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
          return View();
        }

        public ActionResult Edit()
        {
            List<Item> cart = (List<Item>)Session["cart"];
            Session["cart"] = cart;
            return PartialView(cart);
        }

        [HttpPost]
        public ActionResult Edit(int id, int quant)
        {
            List<Item> cart = (List<Item>)Session["cart"];

            foreach (Item item in cart)
            {
                if (item.Pr.Id == id)
                {
                    item.Quantity = quant;
                    Session["cart"] = cart;
                }
            }
            Session["cart"] = cart;
            return View("Index");
        }
        public ActionResult EditRS()
        {
            
            List<ItemRemoteStore> cart = (List<ItemRemoteStore>)Session["OrderNowRemoteStore"];
            
            Session["OrderNowRemoteStore"] = cart;

           
            return PartialView(cart);
        }

        [HttpPost]
        public ActionResult EditRS(int id,int quant)
        {
            List<ItemRemoteStore> cart = (List<ItemRemoteStore>)Session["OrderNowRemoteStore"];

            foreach (ItemRemoteStore item in cart) {
                if (item.Pr.Id==id) {
                    item.Quantity = quant;
                    Session["OrderNowRemoteStore"] = cart;
                }
            }
            Session["OrderNowRemoteStore"] = cart;
            return View("Index");
        }

        private int isExisting(int id) {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++) {
            if (cart[i].Pr.Id==id)
                    return i;
            }
                
              return -1;
  
        }

        public ActionResult Delete(int id) {
            string idUser = Session["LogedUserId"].ToString();
            string xmlPathCart = @"~/Content/xml/cart" + idUser + ".xml";
            string xmlPathCartRemoteStore = @"~/Content/xml/CartRemoteStore" + idUser + ".xml";
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;

            //// в xml запись
            if (Session["cart"] != null)
            {
                CreateXMLFileCart(xmlPathCart, (List<Item>)Session["cart"]);
            }
            return RedirectToAction("Index");
        }
       
        public ActionResult OrderNow(int id,string currancy, decimal currancyCode, int quantity_d1, string producer, string url, decimal discount)
        {
            string idUser = Session["LogedUserId"].ToString();
            string xmlPathCart = @"~/Content/xml/cart" + idUser + ".xml";
            string xmlPathCartRemoteStore = @"~/Content/xml/CartRemoteStore" + idUser + ".xml";

            ///////////корзина бишкек доллар в долларах передают в базу
            Session["userinsession"] = new Orders() { Clients =Convert.ToInt32(Session["LogedUserId"])};
            int quntity = Convert.ToInt32(quantity_d1);
            var part = "";

            UsersCARSEntities dc = new UsersCARSEntities();
            List<Remnants> remnantses = new List<Remnants>();

            //////////////////////////////в корзину со скидкой
            decimal discountCart = 0;
            discountCart=Convert.ToDecimal(de.Remnants.Find(id).Price)* discount/100;
            de.Remnants.Find(id).Price = de.Remnants.Find(id).Price - Convert.ToDouble(discountCart);
            //////////////////////////////

            if (Session["cart"] == null)
            {
               List<Item> cart = new List<Item>();

              

               cart.Add(new Item(de.Remnants.Find(id), quntity));
                ///добавление в заказы в таблицу Orders
               Session["cart"] = cart;
               foreach (var rem in cart)
                {
                    //dc.Orders.Add()
                  part = rem.Pr.Articul;
                    if (rem.Pr.Id == id)
                    {
                        part = rem.Pr.Articul;
                        rem.Pr.CurrancyCode = currancy;
                        rem.Pr.Price = Math.Round((Double)((Decimal)rem.Pr.Price), 2, MidpointRounding.AwayFromZero);
                    }
                }
            }
            else {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                {
                    cart.Add(new Item(de.Remnants.Find(id), quntity));
                   foreach (var rem in cart)
                    {
                       part = rem.Pr.Articul;
                       if (rem.Pr.Id == id)
                        {
                            part = rem.Pr.Articul;
                            rem.Pr.CurrancyCode = currancy;
                            rem.Pr.Price = Math.Round((Double)((Decimal)rem.Pr.Price), 2, MidpointRounding.AwayFromZero);
                        }
                    }
                }
                else {
                    foreach (var rem in cart)
                    {
                        part = rem.Pr.Articul;
                    }
                      cart[index].Quantity++;
                      Session["cart"] = cart;
                   }
            }
            ViewBag.Cart = Session["cart"];
            //// в xml запись
            if (Session["cart"] != null)
            {
                CreateXMLFileCart(xmlPathCart, (List<Item>)Session["cart"]);
            }
            return Redirect(url);
        }

        private int isExistingRemoteOrder(int Id)
        {
            List<ItemRemoteStore> cart = (List<ItemRemoteStore>)Session["OrderNowRemoteStore"];
            for (int i = 0; i < cart.Count; i++)
             {
                if (cart[i].Pr.Id == Id)
                    return i;
             }

            return -1;
        }

        public ActionResult OrderNowRemoteStore(int idRS, string currancy, decimal currancyCode, decimal currancySklad, string Code,
            string Brend, string Articul, string Name, string Days, Double Price,
            string CurrancyCodeRS, string Service, string Sklad, int Client, string ItemID, int quantity_d1, Double Procent, string url)
        {
            string idUser = Session["LogedUserId"].ToString();
            string xmlPathCart = @"~/Content/xml/cart" + idUser + ".xml";
            string xmlPathCartRemoteStore = @"~/Content/xml/CartRemoteStore" + idUser + ".xml";







            var part = Articul;
              if (Service == "Emex")
                {
                   if (Session["OrderNowRemoteStore"] == null)
                    {
                        List<ItemRemoteStore> cart = new List<ItemRemoteStore>();
                        cart.Add(new ItemRemoteStore(de.RemoteStore.Add(
                            new RemoteStore()
                            {
                                Id = idRS,
                                Code = Code,
                                Brend = Brend,
                                Articul = Articul,
                                Name = Name,
                                Supplies = 0,
                                MethodDelivery = null,
                                Existence = null,
                                Days = Days,
                                V_P = ItemID,//itemid
                                Min_Krat = null,
                                Price = Price,
                                VIN = null,
                                CurrancyCode = CurrancyCodeRS,
                                Date = null,
                                ViewOfContract = null,
                                Contragent = null,
                                Status = null,
                                Service = Service,
                                Sklad = Sklad,
                                Client = Convert.ToInt32(Session["LogedUserId"])
                            }), quantity_d1));
                        foreach (var rem in cart)
                        {

                            if (rem.Pr.Id == idRS)
                            {
                                rem.Pr.CurrancyCode = currancy;
                                rem.Pr.Price = Math.Round((Double)((Decimal)rem.Pr.Price / currancyCode), 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                rem.Pr.CurrancyCode = currancy;
                            }
                        }
                        Session["OrderNowRemoteStore"] = cart;

                    }
                    else
                    {
                        List<ItemRemoteStore> cart = (List<ItemRemoteStore>)Session["OrderNowRemoteStore"];
                        int index = isExistingRemoteOrder(idRS);
                        if (index == -1)
                        {
                            cart.Add(new ItemRemoteStore(de.RemoteStore.Add(
                            new RemoteStore()
                            {
                                Id = idRS,
                                Code = Code,
                                Brend = Brend,
                                Articul = Articul,
                                Name = Name,
                                Supplies = 0,
                                MethodDelivery = null,
                                Existence = null,
                                Days = Days,
                                V_P = ItemID,//itemid
                                Min_Krat = null,
                                Price = Price,
                                VIN = null,
                                CurrancyCode = CurrancyCodeRS,
                                Date = null,
                                ViewOfContract = null,
                                Contragent = null,
                                Status = null,
                                Service = Service,
                                Sklad = Sklad,
                                Client = Convert.ToInt32(Session["LogedUserId"])
                            }), quantity_d1));
                            foreach (var rem in cart)
                            {
                                if (rem.Pr.Id == idRS)
                                {
                                    rem.Pr.CurrancyCode = currancy;
                                    rem.Pr.Price = Math.Round((Double)((Decimal)rem.Pr.Price / currancyCode), 2, MidpointRounding.AwayFromZero);
                                }
                            }
                        }
                        else
                        {
                            cart[index].Quantity++;
                            Session["OrderNowRemoteStore"] = cart;
                        }
                    }
                }
                else
                {
                  if (Session["OrderNowRemoteStore"] == null)
                    {
                        List<ItemRemoteStore> cart = new List<ItemRemoteStore>();
                    var v = cart.Where(a => a.Pr.Articul.Equals(Articul) && a.Pr.Sklad.Equals(Sklad)).FirstOrDefault();

                  
                    cart.Add(new ItemRemoteStore(de.RemoteStore.Add(
                            new RemoteStore()
                            {
                                Id = idRS,
                                Code = Code,
                                Brend = Brend,
                                Articul = Articul,
                                Name = Name,
                                Supplies = 0,
                                MethodDelivery = null,
                                Existence = null,
                                Days = Days,
                                V_P = null,
                                Min_Krat = null,
                                Price = Price,
                                VIN = null,
                                CurrancyCode = CurrancyCodeRS,
                                Date = null,
                                ViewOfContract = null,
                                Contragent = null,
                                Status = null,
                                Service = Service,
                                Sklad = Sklad,
                                Client = Convert.ToInt32(Session["LogedUserId"])
                            }), quantity_d1));
                        foreach (var rem in cart)
                        {
                            if (rem.Pr.Id == idRS)
                            {
                                rem.Pr.CurrancyCode = currancy;
                                rem.Pr.Price = Math.Round((Double)((Decimal)rem.Pr.Price / currancyCode), 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                rem.Pr.CurrancyCode = currancy;
                            }
                        }
                        Session["OrderNowRemoteStore"] = cart;

                    }
                    else
                    {
                        List<ItemRemoteStore> cart = (List<ItemRemoteStore>)Session["OrderNowRemoteStore"];

                     int idCart = 0;

                     var v = cart.Where(a => a.Pr.Articul.Equals(Articul) && a.Pr.Sklad==Sklad).FirstOrDefault();
                     if (v != null)
                     {
                        idCart = v.Pr.Id;
                     }
                    else {

                        foreach (var rem in cart)
                        {
                            idCart = idCart + rem.Quantity;
                        }
                      
                    }
                   
                   int index = isExistingRemoteOrder(idCart);
                        if (index == -1)
                        {
                            cart.Add(new ItemRemoteStore(de.RemoteStore.Add(
                            new RemoteStore()
                            {
                                Id = idCart,
                                Code = Code,
                                Brend = Brend,
                                Articul = Articul,
                                Name = Name,
                                Supplies = 0,
                                MethodDelivery = null,
                                Existence = null,
                                Days = Days,
                                V_P = null,
                                Min_Krat = null,
                                Price = Price,
                                VIN = null,
                                CurrancyCode = CurrancyCodeRS,
                                Date = null,
                                ViewOfContract = null,
                                Contragent = null,
                                Status = null,
                                Service = Service,
                                Sklad = Sklad,
                                Client = Convert.ToInt32(Session["LogedUserId"])
                            }), quantity_d1));
                            foreach (var rem in cart)
                            {
                                if (rem.Pr.Id == idCart)
                                {
                                    rem.Pr.CurrancyCode = currancy;
                                    rem.Pr.Price = Math.Round((Double)((Decimal)rem.Pr.Price/ currancyCode), 2, MidpointRounding.AwayFromZero);
                                }
                            }
                        }
                        else
                        {
                            cart[index].Quantity++;
                            Session["OrderNowRemoteStore"] = cart;
                        }
                    }


                }
               
                ViewBag.Cart = Session["OrderNowRemoteStore"];

            if (Session["OrderNowRemoteStore"] != null)
            {
                CreateXMLFileCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
            }
            return Redirect(url);
        }
      
        public ActionResult DeleteRS(int id)
        {
            string idUser = Session["LogedUserId"].ToString();
            string xmlPathCart = @"~/Content/xml/cart" + idUser + ".xml";
            string xmlPathCartRemoteStore = @"~/Content/xml/CartRemoteStore" + idUser + ".xml";

            int index = isExistingRemoteOrder(id);
            List<ItemRemoteStore> cart = (List<ItemRemoteStore>)Session["OrderNowRemoteStore"];
            cart.RemoveAt(index);
            Session["OrderNowRemoteStore"] = cart;

            if (Session["OrderNowRemoteStore"] != null)
            {
                CreateXMLFileCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
            }
            return RedirectToAction("Index");
        }
      
        public async Task<ActionResult> ContentOrder(string id, Users u,string service)
        {
            int id1 = Convert.ToInt32(id);
            if (Request.IsAjaxRequest())
            {
                using (UsersCARSEntities dc = new UsersCARSEntities())
                {
                    var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();
                    int UserID = 1;
                    string UserNameContragent = "";
                    string FamilyName = "";
                    var AllUsers = from UserId in dc.Users select UserId;
                    foreach (Users UserId in AllUsers)
                     {
                        if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                        {
                            UserID = UserId.UserId;
                            UserNameContragent = UserId.UserName;
                            FamilyName = UserId.FamilyName;
                        }
                     }

                    string ItemNumber = "P" + string.Format("A-{0:0000}", id1++);
                    dc.DecoratedOrder.Add(new DecoratedOrder() { Name = ItemNumber });
                    try
                     {
                        dc.SaveChanges();
                     }
                    catch (OptimisticConcurrencyException)
                     {

                        Console.WriteLine("Обнаружен конфликт параллельного доступа - обновить данные.");
                        dc.SaveChanges();
                     }

                    //////////////////////
                   // var skidka = dc.Brends.Where(a => a.UserId == UserID).FirstOrDefault();
                    /////////////////////


                    int lastId = dc.DecoratedOrder.Max(item => item.Id);
                    ItemNumber = "P" + string.Format("A-{0:0000}", lastId);
                    List<Orders> orders = new List<Orders>();
                    List<Item> cart = (List<Item>)Session["cart"];
                    List<Item> cartBishkek = new List<Item>();
                    orders = await dc.Orders.ToListAsync();
                    double Price = 0;
                    var Status = "";
                   if (cart != null)
                    {
                       if (service == "Бишкек")
                        {
                          foreach (var ord in cart)
                            {
                               var remnantses = dc.Remnants.Where(a => a.Articul.Equals(ord.Pr.Articul) && a.Sklad.Equals(ord.Pr.Sklad)).ToList();
                                foreach (var it in remnantses)
                                 {
                                    if (ord.Quantity > it.Existence)
                                    {
                                        ord.Quantity = Convert.ToInt32(it.Existence);

                                        ViewBag.QuntityMessage = "Цена и Количество доступного товара изменились " + ord.Quantity + ", товары с нулевым количеством были перемещены в категорию под заказ";
                                    }
                                    else {
                                        ViewBag.QuntityMessage = "";
                                    }
                                 }
                               
                                string hhh = ord.Pr.Brend.Split(' ')[0].ToString();
                                var skidka = dc.Brends.Where(a => a.UserId == UserID && a.BrendName.Contains(hhh)).FirstOrDefault();
                                int discont = 0;
                                if (skidka != null)
                            {
                                if (skidka.Discount == 0|| skidka.Discount ==null)
                                {
                                    discont = 0;
                                }
                                else {
                                    discont = (int)skidka.Discount;
                                }
                                   
                                }

                                cartBishkek.Add(ord);
                                dc.Orders.Add(new Orders()
                                {
                                    Id = ord.Pr.Id,
                                    Code = ord.Pr.Code,
                                    Brend = ord.Pr.Brend,
                                    Articul = ord.Pr.Articul,
                                    NameGood = ord.Pr.Name,
                                    Supplies = 1,
                                    MethodDelivery = ord.Pr.MethodDelivery,
                                    Existence = ord.Quantity,
                                    Days = "",
                                    V_P = "0",
                                    Min_Krat = 1,
                                    Price = ord.Pr.Price,
                                    NumOrder = ItemNumber,
                                    Category = "",
                                    VIN = "",
                                    CurrancyCode = ord.Pr.CurrancyCode,
                                    Date = DateTime.Now,
                                    ViewOfContract = ord.Pr.ViewOfContract,
                                    Contragent = UserNameContragent + " " + FamilyName,
                                    Clients = UserID,
                                    Sklad = ord.Pr.Sklad,
                                    Discount= discont,
                                    PriceAfterDiscount = ord.Pr.Price

                                });
                                Price = ord.Pr.Price.Value + Price;
                                Status = ord.Pr.Status;

                                try
                                {
                                   await dc.SaveChangesAsync();
                                }
                                catch (OptimisticConcurrencyException)
                                {
                                    Console.WriteLine("Обнаружен конфликт параллельного доступа - обновить данные.");
                                    await dc.SaveChangesAsync();
                                }
                                ModelState.Clear();
                                ViewBag.ItemNumber = ItemNumber;
                                ViewBag.Message = "Successfully submitted";
                            }
                            ViewBag.AllPriceOrder = Price;
                            ViewBag.StatusOrder = Status;
                            ViewBag.CartOrder = cartBishkek; 
                            cart = null;
                            Session["cart"] = null;

                            if (ViewBag.QuntityMessage!="") {
                                ViewBag.QuntityMessage = "Цена и Количество доступного товара изменились, товары с нулевым количеством были перемещены в категорию под заказ";
                            }
                        }

                    }
                    else {

                    }

                    //////////////////////////////////////// удалить из RemoteStore
                    using (UsersCARSEntities rs = new UsersCARSEntities())
                    {
                        List<RemoteStore> ordersRS = new List<RemoteStore>();
                        ordersRS = await rs.RemoteStore.ToListAsync();
                        if (ordersRS != null)
                        {
                            foreach (var ord in ordersRS)
                            {
                                bool saveFailed;
                                do
                                {
                                    saveFailed = false;
                                    try
                                    {
                                        rs.RemoteStore.Remove(ord);
                                        await rs.SaveChangesAsync();
                                    }
                                    catch (DbEntityValidationException ex)
                                    {
                                        saveFailed = true;
                                        // Get the current entity values and the values in the database 
                                        var entry = ex.EntityValidationErrors.Single();
                                        var currentValues = entry.Entry.CurrentValues;
                                        var databaseValues = entry.Entry.GetDatabaseValues();
                                        // Choose an initial set of resolved values. In this case we 
                                        // make the default be the values currently in the database. 
                                        var resolvedValues = databaseValues.Clone();
                                        // Have the user choose what the resolved values should be 
                                        HaveUserResolveConcurrency(currentValues, databaseValues, resolvedValues);

                                        // Update the original values with the database values and 
                                        // the current values with whatever the user choose. 
                                        entry.Entry.OriginalValues.SetValues(databaseValues);
                                        entry.Entry.CurrentValues.SetValues(resolvedValues);
                                        string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                                        throw new DbEntityValidationException(errorMessages);
                                    }
                                    ModelState.Clear();
                                    //  ViewBag.Message = "Successfully submitted";
                                } while (saveFailed);


                            }
                        }
                    }
                    /////////////////////////////////////////////////////////////////////////////////

                    foreach (Users UserId in AllUsers)
                     {
                        if (UserId.UserId.ToString() == Session["LogedUserId"].ToString())
                        {
                            UserID = UserId.UserId;
                            UserNameContragent = UserId.UserName;
                            FamilyName = UserId.FamilyName;
                        }
                     }
                    //string ItemNumber = "P" + string.Format("A-{0:0000}", id++);
                    dc.DecoratedOrder.Add(new DecoratedOrder() { Name = ItemNumber });
                    await dc.SaveChangesAsync();

                    List<ItemRemoteStore> cartRS = (List<ItemRemoteStore>)Session["OrderNowRemoteStore"];
                    List<ItemRemoteStore> cartRSEmex = new List<ItemRemoteStore>();
                    List<ItemRemoteStore> cartRSPhaeton = new List<ItemRemoteStore>();
                    var skidkaRS = dc.Users.Where(a=>a.UserId== UserID).FirstOrDefault();

                    var Service = "";
                    double PriceRS = 0;
                    var StatusRS = "";
                   if (cartRS != null)
                    {
                       if (service == "Emex")
                        {

                           foreach (var ord in cartRS)
                            {
                               if (ord.Pr.Service == service)
                                {
                                    cartRSEmex.Add(ord);

                                    dc.Orders.Add(new Orders()
                                    {
                                        Id = ord.Pr.Id,
                                        Code = ord.Pr.Code,
                                        Brend = ord.Pr.Brend,
                                        Articul = ord.Pr.Articul,
                                        NameGood = ord.Pr.Name,
                                        MethodDelivery = ord.Pr.MethodDelivery,
                                        Existence = ord.Quantity,
                                        Days = ord.Pr.Days,
                                        V_P = ord.Pr.V_P,
                                        Min_Krat = Convert.ToInt32(ord.Pr.Min_Krat),
                                        Price = ord.Pr.Price,
                                        NumOrder = ItemNumber,
                                        Category = "",
                                        VIN = ord.Pr.VIN,
                                        CurrancyCode = ord.Pr.CurrancyCode,
                                        Date = DateTime.Now,
                                        ViewOfContract = ord.Pr.ViewOfContract,
                                        Contragent = UserNameContragent + " " + FamilyName,
                                        Service = ord.Pr.Service,
                                        Clients = UserID,
                                        Sklad = ord.Pr.Service,
                                        Discount = skidkaRS.DiscountStore,
                                        PriceAfterDiscount = ord.Pr.Price
                                    });
                                    Service = ord.Pr.Service;
                                    PriceRS = ord.Pr.Price.Value + Price;
                                    StatusRS = ord.Pr.Status;
                                    try
                                    {
                                        await dc.SaveChangesAsync();
                                        ModelState.Clear();
                                        ViewBag.ItemNumber = ItemNumber;
                                        ViewBag.Message = "Successfully submitted";
                                    }
                                    catch (DbUpdateConcurrencyException ex)
                                    {
                                        ViewBag.Error = "Объект ранее уже был изменен";
                                        return View(u);
                                    }
                                }

                            }
                            ViewBag.CartOrderRSEmex = cartRSEmex;


                            foreach (var item in cartRSEmex)
                            {
                                cartRS.Remove(item);
                            }

                            cartRSEmex = null;
                        }
                        if (service == "Phaeton")
                        {
                            foreach (var ord in cartRS)
                            {
                                if (ord.Pr.Service == service)
                                {
                                    cartRSPhaeton.Add(ord);
                                    dc.Orders.Add(new Orders()
                                    {
                                        Id = ord.Pr.Id,
                                        Code = ord.Pr.Code,
                                        Brend = ord.Pr.Brend,
                                        Articul = ord.Pr.Articul,
                                        NameGood = ord.Pr.Name,
                                        MethodDelivery = ord.Pr.MethodDelivery,
                                        Existence = ord.Quantity,
                                        Days = ord.Pr.Days,
                                        V_P = ord.Pr.V_P,
                                        Min_Krat = Convert.ToInt32(ord.Pr.Min_Krat),
                                        Price = ord.Pr.Price,
                                        NumOrder = ItemNumber,
                                        Category = "",
                                        VIN = ord.Pr.VIN,
                                        CurrancyCode = ord.Pr.CurrancyCode,
                                        Date = DateTime.Now,
                                        ViewOfContract = ord.Pr.ViewOfContract,
                                        Contragent = UserNameContragent + " " + FamilyName,
                                        Service = ord.Pr.Service,
                                        Clients = UserID,
                                        Sklad = ord.Pr.Service,
                                        Discount = skidkaRS.DiscountPhaeton,
                                        PriceAfterDiscount = ord.Pr.Price
                                    });
                                    Service = ord.Pr.Service;
                                    PriceRS = ord.Pr.Price.Value + Price;
                                    StatusRS = ord.Pr.Status;
                                    try
                                    {
                                        await dc.SaveChangesAsync();
                                        ModelState.Clear();
                                        ViewBag.ItemNumber = ItemNumber;
                                        ViewBag.Message = "Successfully submitted";
                                    }
                                    catch (DbUpdateConcurrencyException ex)
                                    {
                                        ViewBag.Error = "Объект ранее уже был изменен";
                                        return View(u);
                                    }
                                }

                            }
                            ViewBag.CartOrderRSPhaeton = cartRSPhaeton;
                            foreach (var item in cartRSPhaeton)
                            {
                                cartRS.Remove(item);
                            }
                            cartRSPhaeton = null;
                        }

                    }
                    orders = await dc.Orders.ToListAsync();
                    ViewBag.AllPriceOrder = PriceRS;
                    ViewBag.service = Service;
                    ViewBag.StatusOrder = StatusRS;
                    ViewBag.CartOrderRS = cartRS;

                }
                return PartialView("_ContentOrders");
        }
            
                return RedirectToAction("Index");

    }

        public void HaveUserResolveConcurrency(DbPropertyValues currentValues,
                                       DbPropertyValues databaseValues,
                                       DbPropertyValues resolvedValues)
        {
            // Show the current, database, and resolved values to the user and have 
            // them edit the resolved values to get the correct resolution. 
        }
      
        public ActionResult Ship(Users u, string NumOrder)
        {
            string idUser = Session["LogedUserId"].ToString();
            string xmlPathCart = @"~/Content/xml/cart" + idUser + ".xml";
            string xmlPathCartRemoteStore = @"~/Content/xml/CartRemoteStore" + idUser + ".xml";
            ViewBag.Message = "";
            var Service = "";
            var ServiceEmex = "";
            var ordersPart = "";
            using (UsersCARSEntities dc = new UsersCARSEntities())
            {
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
              List<Orders> orders = new List<Orders>();
                List<Remnants> RemnantsAll = new List<Remnants>();
                RemnantsAll = dc.Remnants.ToList();
                orders = dc.Orders.Where(a => a.NumOrder.Contains(NumOrder)).ToList();

                List<Remnants> DetailRemnants = new List<Remnants>();
               


                var Status = "";
                foreach (var ord in orders)
                 {
                    DetailRemnants = RemnantsAll.Where(a => a.Sklad.Equals(ord.Sklad) && a.Articul.Equals(ord.Articul)).ToList();

                    
                    ordersPart = ord.Articul;
                    if (ord.Clients== UserID) {
                       
                       if (ord.Service == "Phaeton")
                        {
                          ord.Status = "В Ожидании";
                          Service = ord.Service;
                        }

                        if (ord.Service != "Emex") {
                            ord.Status = "В Ожидании";
                            ord.Service= ord.Sklad;
                            Service = ord.Sklad;
                            foreach (var item in DetailRemnants)
                            {
                               item.Existence = item.Existence - ord.Existence;
                            }
                            dc.SaveChanges();
                        }

                        if (ord.Service == "Emex")
                        {
                            ord.CodeOfBody = "В Ожидании";
                            dc.SaveChanges();
                            UsersCARSEntities dcEmex = new UsersCARSEntities();

                            var UserRole = "";
                            if (Session["LogedUserName"].ToString() != null)
                            {

                                var AllUsersEmex = from UserRoleEmex in dcEmex.Users select UserRoleEmex;
                                foreach (Users UserRoleEmex in AllUsersEmex)
                                {
                                   if(UserRoleEmex.UserId.ToString() == Session["LogedUserId"].ToString())
                                    {
                                      UserRole = UserRoleEmex.UserRole;
                                    }
                                }
                            }
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

                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                            var webAddr = "https://service.tradesoft.ru/3/";
                                var httpWebRequestEmex1MakeOrder = (HttpWebRequest)WebRequest.Create(webAddr);

                                httpWebRequestEmex1MakeOrder.ContentType = "application/json; charset=utf-8";
                                httpWebRequestEmex1MakeOrder.Method = "POST";
                                httpWebRequestEmex1MakeOrder.Accept = "application/json; charset=utf-8";
                                using (var streamWriter = new StreamWriter(httpWebRequestEmex1MakeOrder.GetRequestStream()))
                                {
                                    ///поиск цен и наличия по кодду производителя
                                    makeOrderOffline priceAndExist = new makeOrderOffline()
                                    {
                                        service = "provider",
                                        action = "makeOrderOffline",
                                        user = "alliance.cl",
                                        password = "alliancemotors",

                                        param = new List<Param>() {new Param {
                                        provider = "emex",
                                        login =Login,//"1210647",// Session["LogedUserName"].ToString(),
                                        password =Password,//"a413cc93",// Session["LogedPassword"].ToString(),
                                        comment="50ae6272d7dd871411000000",
                                        clientOrderNumber=ord.NumOrder,
                                        items= new List<ItemsParam>() {new ItemsParam {
                                            itemId=ord.V_P,
                                            quantity=Convert.ToString(ord.Existence),
                                            reference="",
                                            comment="Тестовое оформление заказа"
                                        } }
                                        }
                                     }
                                    };
                                    String price = JsonConvert.SerializeObject(priceAndExist);
                                    streamWriter.Write(price);
                                    streamWriter.Flush();
                                    streamWriter.Close();

                                    var httpResponsePriceExistEmex = (HttpWebResponse)httpWebRequestEmex1MakeOrder.GetResponse();
                                    using (var streamReader = new StreamReader(httpResponsePriceExistEmex.GetResponseStream()))
                                    {
                                       var result = streamReader.ReadToEnd();
                                       var jsonS = JsonConvert.SerializeObject(result);
                                       JObject jObjectEmex1 = JObject.Parse(result);
                                       var getItemsStatusEmex = jObjectEmex1["result"][0]["items"][0];
                                       ViewBag.getItemsStatusEmex = getItemsStatusEmex;

                                      var detailgetItemsStatusEmex = (JArray)jObjectEmex1["result"][0]["items"];
                                      ViewBag.detailgetItemsStatusEmex = detailgetItemsStatusEmex;
                                      List<JObject> row = new List<JObject>();
                                      foreach (JObject good in detailgetItemsStatusEmex)
                                       {
                                         row.Add(good);
                                       }
                                      ViewBag.ArrayGetItemsStatusEmex = row.ToList().ToList();
                                      
                                    }
                                }
                      
                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                           
                            var httpWebRequestgetItemsStatus = (HttpWebRequest)WebRequest.Create(webAddr);
                            httpWebRequestgetItemsStatus.ContentType = "application/json; charset=utf-8";
                            httpWebRequestgetItemsStatus.Method = "POST";
                            httpWebRequestgetItemsStatus.Accept = "application/json; charset=utf-8";
                            using (var streamWriter = new StreamWriter(httpWebRequestgetItemsStatus.GetRequestStream()))
                            {
                                ///поиск цен и наличия по кодду производителя
                                ///
                                List<string> orderItemId = new List<string>();
                                foreach (var item in ViewBag.ArrayGetItemsStatusEmex)
                                {
                                    orderItemId.Add(string.Join(",",item["orderItemId"]).ToString());
                                }


                                StringBuilder strinbuilder = new StringBuilder();
                                foreach (var item in ViewBag.ArrayGetItemsStatusEmex)
                                {
                                    strinbuilder.Append(item["orderItemId"].ToString());
                                   // strinbuilder.Append(',');
                                }


                                getItemsStatus priceAndExist = new getItemsStatus()
                                 {
                                    service = "provider",
                                    action = "getItemsStatus",
                                    user = "alliance.cl",
                                    password = "alliancemotors",

                                    container = new List<getItemsStatuscontainer>() {new getItemsStatuscontainer {
                                            provider = "emex",
                                            login =Login,//"1210647",// Session["LogedUserName"].ToString(),
                                            password =Password,//"a413cc93",// Session["LogedPassword"].ToString(),
                                            items= new List<string>() {
                                                strinbuilder.ToString()
                                             }
                                          }
                                  }
                                 };

                                String price = JsonConvert.SerializeObject(priceAndExist);
                                streamWriter.Write(price);
                                streamWriter.Flush();
                                streamWriter.Close();
                                var httpResponsehttpWebRequestgetItemsStatus = (HttpWebResponse)httpWebRequestgetItemsStatus.GetResponse();
                                using (var streamReader = new StreamReader(httpResponsehttpWebRequestgetItemsStatus.GetResponseStream()))
                                 {
                                    var result = streamReader.ReadToEnd();
                                    var jsonS = JsonConvert.SerializeObject(result);
                                    JObject jObjectEmex1 = JObject.Parse(result);
                                    ViewBag.PriceEmexMakeOrder = jObjectEmex1;
                                    var producerPriceEmex = jObjectEmex1["container"][0]["items"];
                                    ViewBag.ProducerPrice = producerPriceEmex.First.First["stateName"];
                                 }
                            }
                            ////////////////////////////////////////////End Emex
                            ord.Status = ViewBag.ProducerPrice;
                            ServiceEmex = ord.Service;
                        }
                    }
                 }

                dc.SaveChanges();
                ModelState.Clear();

                ViewBag.StatusOrderUp = Status;
                
            }

            if (Session["OrderNowRemoteStore"] != null)
            {
                CreateXMLFileCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
            }
            else {

                if (System.IO.File.Exists(Server.MapPath(xmlPathCartRemoteStore) ))
                {
                    System.IO.File.Delete(Server.MapPath(xmlPathCartRemoteStore));
                }
            }


            //// в xml запись
            if (Session["cart"] != null)
            {
                CreateXMLFileCart(xmlPathCart, (List<Item>)Session["cart"]);
            }
            else {

                if (System.IO.File.Exists(Server.MapPath(xmlPathCart)))
                {
                    System.IO.File.Delete(Server.MapPath(xmlPathCart));
                }
              
            }


            if ((Service == "Phaeton" || ServiceEmex == "Emex") || (Service == "Phaeton" && ServiceEmex == "Emex")
               || (Service == "Phaeton" && ServiceEmex == "") || (Service == "" && ServiceEmex == "Emex"))
            {
                ViewBag.Message = "Ваш заказ подтвержден";
                ViewBag.CartCart = "Корзина";
                return RedirectToAction("Index");
                //return RedirectToAction("Search", "Home", new { Part = ordersPart, SearchStore = ordersPart });
            }
            else
            {
                ViewBag.Message = "Ваш заказ подтвержден";
                ViewBag.CartCart = "Корзина";
                return RedirectToAction("Index");
                //  return RedirectToAction("Search", "Home", new { Part = ordersPart, SearchStore= ordersPart });
            }


        }

        public ActionResult Refuse(Users u, string NumOrder) {

            string idUser = Session["LogedUserId"].ToString();
            string xmlPathCart = @"~/Content/xml/cart" + idUser + ".xml";
            string xmlPathCartRemoteStore = @"~/Content/xml/CartRemoteStore" + idUser + ".xml";

            var Service = "";
            var ServiceEmex = "";
            using (UsersCARSEntities dc = new UsersCARSEntities())
            {
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
                List<Orders> orders = new List<Orders>();
               
                orders = dc.Orders.Where(a => a.NumOrder.Equals(NumOrder)).ToList();

               foreach (var ord in orders)
                { if (ord.Clients == UserID)
                    {
                        if (ord.Service == "Phaeton")
                        {
                            Service = ord.Service;
                        }

                        if (ord.Service == "Emex")
                        {
                            ServiceEmex = ord.Service;
                        }
                        dc.Orders.Remove(ord);
                    }
                }

                dc.SaveChanges();
                ModelState.Clear();
             }

            if (Session["OrderNowRemoteStore"] != null)
            {
                CreateXMLFileCart(xmlPathCartRemoteStore, (List<ItemRemoteStore>)Session["OrderNowRemoteStore"]);
            }


            //// в xml запись
            if (Session["cart"] != null)
            {
                CreateXMLFileCart(xmlPathCart, (List<Item>)Session["cart"]);
            }

            if ((Service == "Phaeton" || ServiceEmex == "Emex") || (Service == "Phaeton" && ServiceEmex == "Emex")
                || (Service == "Phaeton" && ServiceEmex == "")|| (Service == "" && ServiceEmex == "Emex"))
            {

                return View("OrderNowRemoteStore");
            }
            else
            {
                return View("Cart");
            }
             

            
        }

        public ActionResult ServiceCart(string part, Users u, string Search, string SearchStore) {
            if (Session["LogedUserId"] != null)
            {
                ViewBag.ArrayCaption = "";
                ViewBag.SearchParameters = "";
                if (!String.IsNullOrEmpty(part))
                {
                    Search = part;
                    if (!string.IsNullOrEmpty(SearchStore))
                    {

                        var webAddr = "https://service.tradesoft.ru/3/";
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);

                        httpWebRequest.ContentType = "application/json; charset=utf-8";
                        httpWebRequest.Method = "POST";
                        httpWebRequest.Accept = "application/json; charset=utf-8";

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            ProducerList cust = new ProducerList()
                            {
                                service = "provider",
                                action = "getProducerList",
                                user = "alliance.cl",
                                password = "alliancemotors",
                                timeLimit = 10,
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

                                    ViewBag.FooDJO = jObject;

                                    var producer = (JValue)jObject["container"][0]["data"][0]["producer"];
                                    ViewBag.Producer = producer;
                                }
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }


                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

                        var httpWebRequest1 = (HttpWebRequest)WebRequest.Create(webAddr);

                        httpWebRequest1.ContentType = "application/json; charset=utf-8";
                        httpWebRequest1.Method = "POST";
                        httpWebRequest1.Accept = "application/json; charset=utf-8";
                        using (var streamWriter = new StreamWriter(httpWebRequest1.GetRequestStream()))
                        {
                            ///поиск цен и наличия по коду производителя
                            ///
                            if (ViewBag.Producer == "" || ViewBag.Producer == null)
                            {

                                ViewBag.ArrayCaption = "";
                            }
                            else
                            {

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
                                    code = part,
                                    producer=ViewBag.Producer
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
                                        var producerPrice = jObject1["container"][0]["data"][0];
                                        ViewBag.ProducerPrice = producerPrice;

                                        var producerPriceCount = (JArray)jObject1["container"][0]["data"];

                                        ViewBag.producerPriceCount = producerPriceCount;

                                        List<JObject> row = new List<JObject>();
                                        foreach (JObject good in producerPriceCount)
                                        {
                                            row.Add(good);
                                        }
                                        ViewBag.ArrayCaption = row.ToList().ToList();
                                        //   ViewBag.ArrayCaption = (List<JObject>)producerPriceCount;

                                        ViewBag.ArrayCapt = row;
                                        ViewBag.service = "Phaeton";
                                    }
                                }
                                catch (ArgumentOutOfRangeException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }

                        }
                        ////////////////////////////////////////////Start Emex

                        var httpWebRequestEmex = (HttpWebRequest)WebRequest.Create(webAddr);
                        httpWebRequestEmex.ContentType = "application/json; charset=utf-8";
                        httpWebRequestEmex.Method = "POST";
                        httpWebRequestEmex.Accept = "application/json; charset=utf-8";

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
                        login = "580022",
                        password = "10051980",
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

                                var jObjectEmex = JObject.Parse(result);
                                ViewBag.FooDJO = jObjectEmex;

                                var producer = (JValue)jObjectEmex["container"][0]["data"][0]["producer"];
                                ViewBag.ProducerEmex = producer;
                            }
                        }


                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

                        var httpWebRequestEmex1 = (HttpWebRequest)WebRequest.Create(webAddr);

                        httpWebRequestEmex1.ContentType = "application/json; charset=utf-8";
                        httpWebRequestEmex1.Method = "POST";
                        httpWebRequestEmex1.Accept = "application/json; charset=utf-8";
                        using (var streamWriter = new StreamWriter(httpWebRequestEmex1.GetRequestStream()))
                        {
                            ///поиск цен и наличия по кодду производителя

                            ProducerList priceAndExist = new ProducerList()
                            {
                                service = "provider",
                                action = "getPriceList",
                                user = "alliance.cl",
                                password = "alliancemotors",
                                timeLimit = 10,
                                container = new List<Container>() {new Container {
                        provider = "emex",
                        login = "580022",
                        password = "10051980",
                        code = part,
                        producer=ViewBag.ProducerEmex
                        }
                    }
                            };
                            String price = JsonConvert.SerializeObject(priceAndExist);
                            streamWriter.Write(price);
                            streamWriter.Flush();
                            streamWriter.Close();


                            var httpResponsePriceExistEmex = (HttpWebResponse)httpWebRequestEmex1.GetResponse();
                            using (var streamReader = new StreamReader(httpResponsePriceExistEmex.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();

                                var jsonS = JsonConvert.SerializeObject(result);

                                JObject jObjectEmex1 = JObject.Parse(result);
                                ViewBag.PriceEmex = jObjectEmex1;
                                var producerPriceEmex = jObjectEmex1["container"][0]["data"][0];
                                ViewBag.ProducerPrice = producerPriceEmex;

                                var producerPriceCount = (JArray)jObjectEmex1["container"][0]["data"];

                                ViewBag.producerPriceCount = producerPriceCount;

                                List<JObject> row = new List<JObject>();
                                foreach (JObject good in producerPriceCount)
                                {
                                    row.Add(good);
                                }
                                ViewBag.ArrayCaptionEmex = row.ToList().ToList();
                            }
                        }
                        ////////////////////////////////////////////End Emex
                        UsersCARSEntities dc = new UsersCARSEntities();
                        // {

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
                            //  if (v != null)
                            //   {

                            var products = (from prod in dc.Remnants
                                            where prod.Articul == part
                                            select prod).ToList();

                            if (products != null)
                            {
                                var ProductsRemnants = dc.Remnants.Where(a => a.Articul.Contains(part)).ToList();
                                ViewBag.SearchParameters = ProductsRemnants.ToList();
                            }
                            else
                            {
                                ViewBag.SearchParameters = "";
                            }
                            // }
                        }

                    }
                    else
                    {
                        ViewBag.ArrayCaption = "";
                        ViewBag.ArrayCaptionEmex = "";

                    }




                    if (!string.IsNullOrEmpty(Search))
                    {
                        UsersCARSEntities dc = new UsersCARSEntities();
                        // {

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
                            //  if (v != null)
                            //   {

                            var products = (from prod in dc.Remnants
                                            where prod.Articul == part
                                            select prod).ToList();

                            if (products != null)
                            {
                                var ProductsRemnants = dc.Remnants.Where(a => a.Articul.Contains(part)).ToList();
                                ViewBag.SearchParameters = ProductsRemnants.ToList();
                            }
                            else
                            {
                                ViewBag.SearchParameters = "";
                            }
                            // }
                        }
                    }

                }
                else
                {
                    ViewBag.ArrayCaption = "";
                    ViewBag.ArrayCaptionEmex = "";
                    UsersCARSEntities dc = new UsersCARSEntities();
                    // {

                    var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password) && (a.UserRole.Equals("client") || a.UserRole.Equals("admin") || a.UserRole.Equals("emex"))).FirstOrDefault();

                    //  if (v != null)
                    //   {

                    var products = (from prod in dc.Remnants select prod).ToList();

                    if (products != null)
                    {
                        var ProductsRemnants = dc.Remnants.Where(a => a.Articul.Contains("")).ToList();
                        ViewBag.SearchParameters = ProductsRemnants.ToList();
                    }

                }




                return View();
            }
            else
            {
                return View("LoginSearch");
            }
           

        }
    }
}