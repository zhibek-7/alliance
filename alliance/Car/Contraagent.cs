//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Car
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public partial class Contraagent
    {
        public int Id { get; set; }
        public string Contragent { get; set; }

        public virtual Contraagent Contraagent1 { get; set; }
        public virtual Contraagent Contraagent2 { get; set; }
    }
}
