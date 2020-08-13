using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicMedical.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int IDNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Citizenship { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
    }
}
