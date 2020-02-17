using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car.Models
{
    public class ContactModel
    {
        //public int FeedBackId { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Введите ваше Имя", AllowEmptyStrings = false)]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mobile No")]
        public string MobilleNo { get; set; }

        [Display(Name = "States")]
        [Required(ErrorMessage = "Выберите город", AllowEmptyStrings = false)]
        public string StateID { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}