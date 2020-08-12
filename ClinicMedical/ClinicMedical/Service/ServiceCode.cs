using ClinicMedical.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.Service
{
    public class ServiceCode
    {
        public List<Gender> GetAllGender()
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    List<Gender> list = new List<Gender>();
                    list = (from p in context.Genders select p).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }

        public ClinicUser AddClinicUser(ClinicUser user)
        {
            bool uniqueUserName = CheckUserName(user.Username);
            bool uniqueUserIdNumber = CheckIDNumber(user.IDNumber);
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    if (user.ClinicUserId == 0)
                    {
                        if (uniqueUserName && uniqueUserIdNumber)
                        {
                            ClinicUser newClinicUser = new ClinicUser();
                            newClinicUser.FullName = user.FullName;
                            newClinicUser.IDNumber = user.IDNumber;
                            newClinicUser.GenderId = user.GenderId;
                            newClinicUser.DateOfBirth = user.DateOfBirth;
                            newClinicUser.Citizenship = user.Citizenship;
                            newClinicUser.Username = user.Username;
                            newClinicUser.RoleId = user.RoleId;
                            newClinicUser.Password = HashPasswordHelper.HashPassword(user.Password);
                            context.ClinicUsers.Add(newClinicUser);
                            context.SaveChanges();
                            user.ClinicUserId = newClinicUser.ClinicUserId;
                        }
                        return user;
                    }
                    else
                    {
                        ClinicUser editUser = (from p in context.ClinicUsers where p.ClinicUserId == user.ClinicUserId select p).First();
                        editUser.FullName = user.FullName;
                        editUser.IDNumber = user.IDNumber;
                        editUser.GenderId = user.GenderId;
                        editUser.DateOfBirth = user.DateOfBirth;
                        editUser.Citizenship = user.Citizenship;
                        editUser.Username = user.Username;
                        editUser.RoleId = user.RoleId;
                        editUser.ClinicUserId = user.ClinicUserId;
                        context.SaveChanges();
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public bool CheckUserName(string username)
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    ClinicUser clinicUser = (from d in context.ClinicUsers where d.Username == username select d).FirstOrDefault();

                    if (clinicUser != null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }

        public bool CheckIDNumber(int IDNumber)
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    ClinicUser clinicUser = (from d in context.ClinicUsers where d.IDNumber == IDNumber select d).FirstOrDefault();

                    if (clinicUser != null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
    }
}
