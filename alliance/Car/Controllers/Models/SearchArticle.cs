using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace Car.Models
{
    [DataContract]
    public class SearchArticle
    {
        [DataMember]
        public string service { get; set; }
        [DataMember]
        public string action { get; set; }
        [DataMember]
        public string user { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public int timeLimit { get; set; }

        [DataContract]
        public class container
        {
            [DataMember]
            public string provider;
            [DataMember]
            public string login;
            [DataMember]
            public string password;
            [DataMember]
            public string code;
            [DataMember]
            public string producer;

        }
   }



    




}