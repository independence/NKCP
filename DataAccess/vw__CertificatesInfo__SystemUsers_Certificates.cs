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
    
    public partial class vw__CertificatesInfo__SystemUsers_Certificates
    {
        public int SystemUsers_ID { get; set; }
        public string SystemUsers_Email { get; set; }
        public Nullable<int> SystemUsers_UserGroup { get; set; }
        public string SystemUsers_Username { get; set; }
        public string SystemUsers_Name { get; set; }
        public Nullable<System.DateTime> SystemUsers_Birthday { get; set; }
        public byte[] SystemUsers_Image { get; set; }
        public Nullable<int> SystemUsers_Gender { get; set; }
        public Nullable<int> SystemUsers_Type { get; set; }
        public Nullable<int> SystemUsers_Status { get; set; }
        public Nullable<bool> SystemUsers_Disable { get; set; }
        public int Certificates_ID { get; set; }
        public string Certificates_Certificates { get; set; }
        public Nullable<System.DateTime> SystemUsers_Certificates_CreatedDate { get; set; }
        public Nullable<System.DateTime> SystemUsers_Certificates_ExpirationDate { get; set; }
        public string SystemUsers_Certificates_Organization { get; set; }
        public string Certificates_Organization { get; set; }
        public Nullable<int> Certificates__Type { get; set; }
        public string SystemUsers_Certificates_Level { get; set; }
        public string SystemUsers_Certificates_TrainingType { get; set; }
    }
}
