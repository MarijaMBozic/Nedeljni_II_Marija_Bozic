using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.Models
{
    public class Manager:User
    {
        public int Floor { get; set; }
        public int MaxNumOFDoctors { get; set; }
        public int MinNumOfRooms { get; set; }
        public int? NumOfMistake { get; set; }
        public bool IsDeleted { get; set; }
    }
}
