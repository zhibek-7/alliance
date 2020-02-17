//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Car
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [Serializable]
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Brends = new HashSet<Brends>();
            this.Orders = new HashSet<Orders>();
        }
    
        public int UserId { get; set; }
        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        public string FamilyName { get; set; }

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "required!", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Your password must be at least 8 characters.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "required!")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please provide valid email")]
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string Service { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<int> DiscountPhaeton { get; set; }
        public Nullable<int> DiscountStore { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Brends> Brends { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}