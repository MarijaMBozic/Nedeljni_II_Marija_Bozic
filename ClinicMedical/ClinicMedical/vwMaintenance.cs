//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClinicMedical
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwMaintenance
    {
        public int ClinicUserId { get; set; }
        public string FullName { get; set; }
        public int IDNumber { get; set; }
        public int GenderId { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Citizenship { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public bool PermissionToExpandClinic { get; set; }
        public bool ResponsibleForVehicleAccessibility { get; set; }
        public bool ResponsibleForAccessOfHandicaps { get; set; }
        public int ClinicMaintenanceId { get; set; }
    }
}
