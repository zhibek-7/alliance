using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car.Models
{
    public class Statuses
    {
        public int IdStatus { get; set;}
        public string NameStatus { get; set; }
        public bool IsCheck { get; set; }
    }


    public class StatusesList {

        public List<Statuses> statuses { get; set; }
    }

    public class DateFilter {

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }
    }

}