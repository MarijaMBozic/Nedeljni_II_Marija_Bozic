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
    
    public partial class ClinicMaintenance
    {
        public int ClinicMaintenanceId { get; set; }
        public int ClinicUserId { get; set; }
        public bool PermissionToExpandClinic { get; set; }
        public bool ResponsibleForVehicleAccessibility { get; set; }
        public bool ResponsibleForAccessOfHandicaps { get; set; }
    
        public virtual ClinicUser ClinicUser { get; set; }
    }
}