Use MedicaClinic

drop table if exists ClinicPatient
drop table if exists ClinicDoctor
drop table if exists ClinicManager
drop table if exists ClinicMaintenance
drop table if exists ClinicUser
drop table if exists Institution 
drop table if exists Gender
drop table if exists Role
drop table if exists Workshift
drop table if exists Department 

create table Gender(
   GenderId      int            identity (1,1) primary key,   
   Name          nvarchar(100)   unique not null
   )

create table Role(
   RoleId      int            identity (1,1) primary key,   
   Name        nvarchar(100)   unique not null
   )

create table Workshift(
   WorkShiftId  int            identity (1,1) primary key,   
   Name         nvarchar(100)   unique not null
   )

create table Department(
   DepartmentId     int            identity (1,1) primary key,   
   Name             nvarchar(100)   unique not null
   )

create table Institution(
   InstitutionId               int            identity (1,1) primary key,
   BuildDate				   date			   default getDate(),
   Name                        nvarchar(100)   unique not null,
   Owner                       nvarchar(100)          not null,
   Address                     nvarchar(100)          not null,
   NumberOfFloors              int                    not null,
   NumberOfRoomsPerFloor       int                    not null,
   Terrace                     bit                    not null,
   Backyard                    bit		              not null,
   AccessPointsForAmbulances   int                    not null,
   AccessPointsForhandicaps    int                    not null   		 
)

create table ClinicUser(
   ClinicUserId                int            identity (1,1) primary key,   
   FullName                    nvarchar(100)           not null,
   IDNumber                    int             unique  not null,
   GenderId                    int                    not null,
   FOREIGN KEY (GenderId)  REFERENCES Gender(GenderId),
   DateOfBirth                 date                   default getDate(),
   Citizenship                 nvarchar(100)          not null,
   Username                    nvarchar(100)  unique  not null,
   Password                    nvarchar(100)          not null,
   RoleId                      int                    not null,
   FOREIGN KEY (RoleId)  REFERENCES Role(RoleId)
)

create table ClinicMaintenance(
 ClinicMaintenanceId                    int            identity (1,1) primary key,   
 ClinicUserId                           int                    not null,
 FOREIGN KEY (ClinicUserId)  REFERENCES ClinicUser(ClinicUserId),
 PermissionToExpandClinic               bit                    not null,
 ResponsibleForVehicleAccessibility     bit                    not null,
 ResponsibleForAccessOfHandicaps        bit                    not null
)

create table ClinicManager(
 ClinicManagerId                        int            identity (1,1) primary key,   
 ClinicUserId                           int                    not null,
 FOREIGN KEY (ClinicUserId)  REFERENCES ClinicUser(ClinicUserId),
 ClinicFloor                            int                    not null,
 MaxNumOfDoctorsSupervised              int                    not null,
 MinNumOfRoomSupervised                 int                    not null,
 NumberOfMistake                        int                    default 0,
 Deleted                                bit                    not null
)

create table ClinicDoctor(
 ClinicDoctorId                         int            identity (1,1) primary key,   
 ClinicUserId                           int                    not null,
 FOREIGN KEY (ClinicUserId)  REFERENCES ClinicUser(ClinicUserId),
 UniqueNumber                           int           unique   not null,
 BancAccount                            int           unique   not null,
 DepartmentId                           int                    not null,
 FOREIGN KEY (DepartmentId)  REFERENCES Department(DepartmentId),
 WorkShiftId                            int                    not null,
 FOREIGN KEY (WorkShiftId)  REFERENCES WorkShift(WorkShiftId),
 InChargeOfAdmission                    bit                    not null,
 ClinicManagerId                        int                    not null,
 FOREIGN KEY (ClinicManagerId)  REFERENCES ClinicManager(ClinicManagerId)
)

create table ClinicPatient(
 ClinicPatientId                        int            identity (1,1) primary key,   
 ClinicUserId                           int                    not null,
 FOREIGN KEY (ClinicUserId)  REFERENCES ClinicUser(ClinicUserId),
 InsuranceNumber                        int                    not null,
 InsuranceExpirationDate                date                   default getDate(),
 UniqueDoctorNumber                     int                    not null, 
 FOREIGN KEY (UniqueDoctorNumber)  REFERENCES ClinicDoctor(UniqueNumber)
)

Insert into Gender(Name)
values('Female'),
      ('Male')

Insert into Role(Name)
values('Administrator'),
      ('Maintenance'),
      ('Manager'),
	  ('Doctor'),
      ('Patient')
	  

Insert into Workshift(Name)
values('Morning'),
      ('Afternon'),
	  ('Night'),
      ('24-hours')

Insert into Department(Name)
values('Gynecology'),
      ('Dermatology'),
	  ('Psychiatry'),
      ('Orthopedics'),
	  ('Pediatrics'),
	  ('Ophthalmology')
