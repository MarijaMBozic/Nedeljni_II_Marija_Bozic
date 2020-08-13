using ClinicMedical.Helper;
using ClinicMedical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
        public List<Department> GetAllDepartment()
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    List<Department> list = new List<Department>();
                    list = (from p in context.Departments select p).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }
        public List<Workshift> GetAllWorkshift()
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    List<Workshift> list = new List<Workshift>();
                    list = (from p in context.Workshifts select p).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }
        public List<ClinicUser> GetAllManager()
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    List<ClinicUser> list = new List<ClinicUser>();
                    list = (from p in context.ClinicUsers where p.RoleId==3 select p).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }
        public List<ClinicUser> GetAllDoctors()
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    List<ClinicUser> list = new List<ClinicUser>();
                    list = (from p in context.ClinicUsers where p.RoleId == 4 select p).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }

        public List<Institution> GetAllInstitution()
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    List<Institution> list = new List<Institution>();
                    list = (from p in context.Institutions select p).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }
        public int GetDoctorUniqueNumberByDoctorId(int doctorId)
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {

                    int result = (from p in context.ClinicDoctors where p.ClinicUserId == doctorId select p.UniqueNumber).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return 0;
            }
        }
        public void DeleteUser(int userId)
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    ClinicUser userToDelete = (from r in context.ClinicUsers where r.ClinicUserId == userId select r).First();
                   
                    context.ClinicUsers.Remove(userToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
                
        public int AddInstitution(Institution institution)
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    if (institution.InstitutionId == 0)
                    {
                        Institution newInstitution = new Institution();
                        newInstitution.Name = institution.Name;
                        newInstitution.Owner = institution.Owner;
                        newInstitution.BuildDate = institution.BuildDate;
                        newInstitution.Address = institution.Address;
                        newInstitution.NumberOfFloors = institution.NumberOfFloors;
                        newInstitution.NumberOfRoomsPerFloor = institution.NumberOfRoomsPerFloor;
                        newInstitution.Terrace = institution.Terrace;
                        newInstitution.Backyard = institution.Backyard;
                        newInstitution.AccessPointsForAmbulances = institution.AccessPointsForAmbulances;
                        newInstitution.AccessPointsForhandicaps = institution.AccessPointsForhandicaps;
                        context.Institutions.Add(newInstitution);
                        context.SaveChanges();
                        institution.InstitutionId = newInstitution.InstitutionId;
                        return institution.InstitutionId;
                    }
                    else
                    {
                        Institution editInstitution = (from p in context.Institutions where p.InstitutionId == institution.InstitutionId select p).First();
                        editInstitution.Name = institution.Name;
                        editInstitution.Owner = institution.Owner;
                        editInstitution.BuildDate = institution.BuildDate;
                        editInstitution.Address = institution.Address;
                        editInstitution.NumberOfFloors = institution.NumberOfFloors;
                        editInstitution.NumberOfRoomsPerFloor = institution.NumberOfRoomsPerFloor;
                        editInstitution.Terrace = institution.Terrace;
                        editInstitution.Backyard = institution.Backyard;
                        editInstitution.AccessPointsForAmbulances = institution.AccessPointsForAmbulances;
                        editInstitution.AccessPointsForhandicaps = institution.AccessPointsForhandicaps;
                        context.SaveChanges();
                        return institution.InstitutionId;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                Logging.LoggAction("MasterAminViewModel", "Error", ex.ToString());
                return 0;
            }
        }

        public int AddClinicUser(ClinicUser user)
        {
            bool uniqueUserName = CheckUserName(user.Username);
            bool uniqueUserIdNumber = CheckIDNumber(user.IDNumber);
            string password = HashPasswordHelper.HashPassword(user.Password);
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    if (user.ClinicUserId == 0 && uniqueUserName && uniqueUserIdNumber)
                    {
                        ClinicUser newClinicUser = new ClinicUser();
                        newClinicUser.FullName = user.FullName;
                        newClinicUser.IDNumber = user.IDNumber;
                        newClinicUser.GenderId = user.GenderId;
                        newClinicUser.DateOfBirth = user.DateOfBirth;
                        newClinicUser.Citizenship = user.Citizenship;
                        newClinicUser.Username = user.Username;
                        newClinicUser.RoleId = user.RoleId;
                        newClinicUser.Password = password;
                        context.ClinicUsers.Add(newClinicUser);
                        context.SaveChanges();
                        user.ClinicUserId = newClinicUser.ClinicUserId;
                        return user.ClinicUserId;
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
                        return user.ClinicUserId;
                    }
                }                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                Logging.LoggAction("MasterAminViewModel", "Error", ex.ToString());
                return 0;
            }
        }

        public int AddNewDoctor(ClinicDoctor user)
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    if (user.ClinicDoctorId == 0)
                    {
                        ClinicDoctor newClinicUser = new ClinicDoctor();
                        newClinicUser.ClinicUserId = user.ClinicUserId;
                        newClinicUser.UniqueNumber = user.UniqueNumber;
                        newClinicUser.BancAccount = user.BancAccount;
                        newClinicUser.DepartmentId = user.DepartmentId;
                        newClinicUser.WorkShiftId = user.WorkShiftId;
                        newClinicUser.InChargeOfAdmission = user.InChargeOfAdmission;
                        newClinicUser.ClinicManagerId = user.ClinicManagerId;

                        context.ClinicDoctors.Add(newClinicUser);
                        context.SaveChanges();
                        user.ClinicDoctorId = newClinicUser.ClinicDoctorId;
                        return user.ClinicDoctorId;
                    }
                    else
                    {
                        ClinicDoctor editClinicUser = (from p in context.ClinicDoctors where p.ClinicDoctorId == user.ClinicDoctorId select p).First();
                        editClinicUser.ClinicUserId = user.ClinicUserId;
                        editClinicUser.UniqueNumber = user.UniqueNumber;
                        editClinicUser.BancAccount = user.BancAccount;
                        editClinicUser.DepartmentId = user.DepartmentId;
                        editClinicUser.WorkShiftId = user.WorkShiftId;
                        editClinicUser.InChargeOfAdmission = user.InChargeOfAdmission;
                        editClinicUser.ClinicManagerId = user.ClinicManagerId;
                        editClinicUser.ClinicDoctorId = user.ClinicDoctorId;
                        context.SaveChanges();
                        return user.ClinicDoctorId;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                Logging.LoggAction("AddDoctorViweModel", "Error", ex.ToString());
                return 0;
            }
        }

        public int AddNewManager(ClinicManager user)
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    if (user.ClinicManagerId == 0)
                    {
                        ClinicManager newClinicUser = new ClinicManager();
                        newClinicUser.ClinicUserId = user.ClinicUserId;
                        newClinicUser.ClinicFloor = user.ClinicFloor;
                        newClinicUser.MaxNumOfDoctorsSupervised = user.MaxNumOfDoctorsSupervised;
                        newClinicUser.MinNumOfRoomSupervised = user.MinNumOfRoomSupervised;
                        newClinicUser.Deleted = false;
                        newClinicUser.NumberOfMistake = 0;

                        context.ClinicManagers.Add(newClinicUser);
                        context.SaveChanges();
                        user.ClinicManagerId = newClinicUser.ClinicManagerId;
                        return user.ClinicManagerId;
                    }
                    else
                    {
                        ClinicManager editClinicUser = (from p in context.ClinicManagers where p.ClinicManagerId == user.ClinicManagerId select p).First();
                        editClinicUser.ClinicUserId = user.ClinicUserId;
                        editClinicUser.ClinicFloor = user.ClinicFloor;
                        editClinicUser.MinNumOfRoomSupervised = user.MinNumOfRoomSupervised;
                        editClinicUser.MaxNumOfDoctorsSupervised = user.MaxNumOfDoctorsSupervised;
                        editClinicUser.Deleted = user.Deleted;
                        editClinicUser.NumberOfMistake = user.NumberOfMistake;
                        editClinicUser.ClinicManagerId = user.ClinicManagerId;
                        context.SaveChanges();
                        return user.ClinicManagerId;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                Logging.LoggAction("AddManagerViweModel", "Error", ex.ToString());
                return 0;
            }
        }

        public int AddNewPatient(ClinicPatient user)
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    if (user.ClinicPatientId == 0)
                    {
                        ClinicPatient newClinicUser = new ClinicPatient();
                        newClinicUser.ClinicUserId = user.ClinicUserId;
                        newClinicUser.InsuranceNumber = user.InsuranceNumber;
                        newClinicUser.InsuranceExpirationDate = user.InsuranceExpirationDate;
                        newClinicUser.UniqueDoctorNumber = user.UniqueDoctorNumber;

                        context.ClinicPatients.Add(newClinicUser);
                        context.SaveChanges();
                        user.ClinicPatientId = newClinicUser.ClinicPatientId;
                        return user.ClinicPatientId;
                    }
                    else
                    {
                        ClinicPatient editClinicUser = (from p in context.ClinicPatients where p.ClinicPatientId == user.ClinicPatientId select p).First();
                        editClinicUser.ClinicUserId = user.ClinicUserId;
                        editClinicUser.InsuranceNumber = user.InsuranceNumber;
                        editClinicUser.InsuranceExpirationDate = user.InsuranceExpirationDate;
                        editClinicUser.UniqueDoctorNumber = user.UniqueDoctorNumber;
                        editClinicUser.ClinicPatientId = user.ClinicPatientId;
                        context.SaveChanges();
                        return user.ClinicPatientId;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                Logging.LoggAction("AddPatientIdViweModel", "Error", ex.ToString());
                return 0;
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

        public bool CheckUniqueNumber(int uniquNumber)
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    ClinicDoctor clinicUser = (from d in context.ClinicDoctors where d.UniqueNumber == uniquNumber select d).FirstOrDefault();

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
        public bool CheckBancAccount(int bancAccount)
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    ClinicDoctor clinicUser = (from d in context.ClinicDoctors where d.BancAccount == bancAccount select d).FirstOrDefault();

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
        public bool CheckInsuranceNumber(int insuranceNumber)
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    ClinicPatient clinicUser = (from d in context.ClinicPatients where d.InsuranceNumber == insuranceNumber select d).FirstOrDefault();

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
        public ClinicUser LoginUser(string username, string password)
        {
            password = HashPasswordHelper.HashPassword(password);
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    ClinicUser user = (from d in context.ClinicUsers
                                         where d.Username.Equals(username)
                                         where d.Password.Equals(password)
                                         select d).FirstOrDefault();
                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<vwManager> GetAllvwDoctorsList()
        {
            try
            {
                using (MedicaClinicEntities1 context = new MedicaClinicEntities1())
                {
                    List<vwManager> list = new List<vwManager>();
                    list = (from p in context.vwManagers  select p).ToList();
                    return list;
                }      
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }

    }
}
