using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.Service
{
    public class MasterLogin
    {
        public static bool Login(string username, string password)
        {
            string locationFile = @"..\..\ClinicAccess\ClinicAccess.txt";
            if (File.Exists(locationFile))
            {
                string[] credentials = File.ReadAllLines(locationFile);
                string[] usernameCredentials = credentials[0].Split(':');
                string[] passwordCredentials = credentials[1].Split(':');

                if (username == usernameCredentials[1] && password == passwordCredentials[1])
                {
                    return true;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("File not found!");
            }
            return false;
        }

        public static bool ChangeCredentials(string username, string password)
        {
            try
            {
                string newCredentials = string.Format("[username]:{0}\n[password]:{1}", username, password);
                File.WriteAllText(@"..\..\ClinicAccess\ClinicAccess.txt", newCredentials);
                return true;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("File not found!");
            }
            return false;
        }
    }
}
