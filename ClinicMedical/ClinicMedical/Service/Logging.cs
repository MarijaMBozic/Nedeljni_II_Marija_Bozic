using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicMedical.Service
{
    public class Logging
    {
        public static void LoggAction(string typeOfAction, string status)
        {           
            FileInfo txt = new FileInfo(@"..\..\LoggedInfo\Logging.txt");
            StreamWriter sw = txt.AppendText();            
            sw.WriteLine("[{0}][{1}][{2}] ", DateTime.Now, typeOfAction, status);
            sw.Close();
        }
    }
}
