//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permits
    {
        public int ID { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsContent { get; set; }
        public Nullable<bool> IsPartner { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> Disable { get; set; }
        public string SystemKey { get; set; }
    }
}
