using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Car.Models;
using Car.PhaetonService;
using System.Web.Script.Services;
using System.Web.UI;
using System.ServiceModel.Web;
using Car.ServicePhaeton1;
using System.Text;
using System.IO;

namespace Car.Controllers
{
    //Получение информации о заказанных позициях
    [Serializable]
    class getItemsStatus
    {
        public string service { get; set; }
        public string action { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public List<getItemsStatuscontainer> container { get; set; }
    }

    class getItemsStatuscontainer {
        public string provider { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public List<string> items { get; set; }
    }
    
    class makeOrderOffline {
        public string service { get; set; }
        public string action { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public List<Param> param { get; set; }
    }

    class Param {
        public string provider { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string comment { get; set; }
        public string clientOrderNumber { get; set; }
        public List<ItemsParam> items { get; set; }
    }

    class ItemsParam {
        public string itemId { get; set; }
        public string quantity { get; set; }
        public string reference { get; set; }
        public string comment { get; set; }
    }





    class PreOrderSearch {
        public string service { get; set; }
        public string action { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public int timeLimit { get; set; }
        public List<Container> container { get; set; }
    }

    class Container
    {
        public string provider { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string code { get; set; }
        public string producer { get; set; }
        public string itemHash { get; set; }
  }
    class ProducerList
    {
        public string service { get; set; }
        public string action { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public int timeLimit { get; set; }
        public int rating { get; set; }
        public List<List<string>> data { get; set; }
        public List<Container> container { get; set; }
    }

    /// <summary>
    /// /аналоги по артикулу и бренду
    /// </summary>
    public class VebAnalog
    {
        public string user { get; set; }
        public string password { get; set; }
        public string service { get; set; }
        public string action { get; set; }
        public List<List<string>> data { get; set; }
        public int rating { get; set; }
        
    }



    /// <summary>
    /// /////бренды по артикулу
    /// </summary>
    public class VebAnalogBrends
    {
        public string user { get; set; }
        public string password { get; set; }
        public string service { get; set; }
        public string action { get; set; }
        public List<string> data { get; set; }
        public int rating { get; set; }

    }
    /// //////////////vin номер

    class Parameters
    {
        public string code { get; set; }
        public string brand { get; set; }
        public string lang { get; set; }
    }
    class VinPartInfo
    {
        public string service { get; set; }
        public string action { get; set; }
        public string user { get; set; }
        public string password { get; set; }
      
        public List<Parameters> param { get; set; }
    }




    public class SearchController : Controller
    {
        public static void Main(string[] args)
        {
            String JSONstring = System.IO.File.ReadAllText("");
        }
        public ActionResult Index()
        {
            ServicePhaeton1.SearchServiceClient client1 = new ServicePhaeton1.SearchServiceClient("BasicHttpBinding_ISearchService1");
            PhaetonService.SearchServiceClient client = new PhaetonService.SearchServiceClient("BasicHttpBinding_ISearchService");


            client.SearchArticle(Guid.Parse("118e7ac8-edd3-11e2-b524-001517a1f012"), "KL9");
            client1.SearchArticle(Guid.Parse("118e7ac8-edd3-11e2-b524-001517a1f012"), "KL9");
           
            client.Close();
            client1.Close();
            return View();
        }
    }
}