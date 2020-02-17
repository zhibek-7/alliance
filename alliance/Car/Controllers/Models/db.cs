using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car.Controllers.Models
{
    
    public class db
    {
        

        public class Remnantss {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Brend { get; set; }
            public string Articul { get; set; }
            public string Name { get; set; }
            public Nullable<int> Supplies { get; set; }
            public string MethodDelivery { get; set; }
            public Nullable<int> Existence { get; set; }
            public string Days { get; set; }
            public string V_P { get; set; }
            public Nullable<int> Min_Krat { get; set; }
            public Nullable<double> Price { get; set; }
            public string VIN { get; set; }
            public string CurrancyCode { get; set; }
            public Nullable<System.DateTime> Date { get; set; }
            public string ViewOfContract { get; set; }
            public string Contragent { get; set; }
            public string Status { get; set; }
            public Nullable<System.DateTime> ReleaseDate { get; set; }
            public string NameAuto { get; set; }
            public string ColorOfBody { get; set; }
            public string modProdPeriod { get; set; }
            public string Options { get; set; }
            public string Description { get; set; }
            public string Model { get; set; }
            public string catProdPeriod { get; set; }
            public string ColorOfSalon { get; set; }
            public string BrendAuto { get; set; }
            public string CodeOfBody { get; set; }
            public string NumberOfBody { get; set; }
            public string Sklad { get; set; }
            public string Sale { get; set; }

            public virtual Supplies Supplies1 { get; set; }
        }


        public static List<usmSelectAllRemnantsByStoreMinSklad_Result> GetRemnantsses(int pageindex, int pagesize, string code, string brend) {
            UsersCARSEntities dc = new UsersCARSEntities();
            List<Remnantss> listrem = new List<Remnantss>();

            return dc.usmSelectAllRemnantsByStoreMinSklad(code, brend).ToList();
        }
    }
}