using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SearchService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
        [WebInvoke(Method="GET", UriTemplate ="find", ResponseFormat =WebMessageFormat.Json)]
        public SearchArticle find()
        {
            return new SearchArticle { service = "provider", action= "getPriceList", user= "alliance.cl", password= "118e7ac8-edd3-11e2-b524-001517a1f012", timeLimit=10 };
        }

        [WebInvoke(Method = "GET", UriTemplate = "findAll", ResponseFormat = WebMessageFormat.Json)]

        public List<SearchArticle> findAll()
        {
            List<SearchArticle> result = new List<SearchArticle>();
            result.Add(new SearchArticle { service = "provider", action = "getPriceList", user = "alliance.cl", password = "118e7ac8-edd3-11e2-b524-001517a1f012", timeLimit = 10 });
            return result;
        }
    }
}
