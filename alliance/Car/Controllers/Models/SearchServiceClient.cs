using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace Car.Models
{
    public class SearchServiceClient
    {
        private string BASE_URL= "http://localhost:3530/Service1.svc/";

        public SearchArticle find() {
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(BASE_URL+"find");
            var json_serializer = new JavaScriptSerializer();
            return json_serializer.Deserialize<SearchArticle>(content);


        }



        public List<SearchArticle> findAll()
        {
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(BASE_URL + "findAll");
            var json_serializer = new JavaScriptSerializer();
            return json_serializer.Deserialize<List<SearchArticle>>(content);


        }
    }




}