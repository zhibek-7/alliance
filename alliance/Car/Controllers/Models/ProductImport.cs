using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Car.Models
{
    [Serializable]
    [XmlRoot("product")]
    public class ProductImport
    {

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("brend")]
        public string Brend { get; set; }

        [XmlElement("articul")]
        public string Articul { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("supplies")]
        public Nullable<int> Supplies { get; set; }

        [XmlElement("methodDelivery")]
        public string MethodDelivery { get; set; }

        [XmlElement("existence")]
        public Nullable<int> Existence { get; set; }

        [XmlElement("days")]
        public string Days { get; set; }

        [XmlElement("v_p")]
        public string V_P { get; set; }

        [XmlElement("min_krat")]
        public Nullable<int> Min_Krat { get; set; }

        [XmlElement("price")]
        public Nullable<double> Price { get; set; }

        [XmlElement("vin")]
        public string VIN { get; set; }

        [XmlElement("currancycode")]
        public string CurrancyCode { get; set; }

        [XmlElement("date")]
        public Nullable<System.DateTime> Date { get; set; }

      //  [XmlElement("supplies1")]
      //  public virtual Supplies Supplies1 { get; set; }

    }

    [MetadataType(typeof(ProductImport))]
    public partial class Remnants
    {

    }

}