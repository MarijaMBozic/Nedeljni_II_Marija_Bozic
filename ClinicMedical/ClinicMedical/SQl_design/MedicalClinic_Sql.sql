CREATE DATABASE MedicaClinic;
GO
Use MedicaClinic

drop table if exists ClinicPatient
drop table if exists ClinicDoctor
drop table if exists ClinicManager
drop table if exists ClinicMaintenance
drop table if exists Institution 
drop table if exists ClinicUser
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
   FOREIGN KEY (RoleId)  REFERENCES Role(RoleId), 
   IsDeleted                    bit                   default 0 not null
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
   AccessPointsForhandicaps    int                    not null, 
   ClinicUserId                      int              not null,
   FOREIGN KEY (ClinicUserId)  REFERENCES ClinicUser(ClinicUserId), 	 
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
 NumberOfMistake                        int                    default 0
)

create table ClinicDoctor(
 ClinicDoctorId                         int            identity (1,1) primary key,   
 ClinicUserId                           int                    not null,
 FOREIGN KEY (ClinicUserId)  REFERENCES ClinicUser(ClinicUserId),
 UniqueNumber                           int           unique   not null,
 BancAccount                            bigint           unique   not null,
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

go

CREATE VIEW vwDoctor AS
	SELECT        dbo.ClinicUser.ClinicUserId, dbo.ClinicUser.FullName, dbo.ClinicUser.IDNumber, dbo.ClinicUser.GenderId, dbo.ClinicUser.DateOfBirth, dbo.ClinicUser.Citizenship, dbo.ClinicUser.Username, dbo.ClinicUser.Password, 
                         dbo.ClinicUser.RoleId, dbo.ClinicUser.IsDeleted, dbo.Department.Name AS DepartmanName, dbo.Workshift.Name AS WorkShiftName, dbo.ClinicDoctor.ClinicManagerId, dbo.ClinicDoctor.WorkShiftId, 
                         dbo.ClinicDoctor.InChargeOfAdmission, dbo.ClinicDoctor.DepartmentId, dbo.ClinicDoctor.BancAccount, dbo.ClinicDoctor.UniqueNumber, dbo.ClinicDoctor.ClinicDoctorId
	FROM            dbo.ClinicUser INNER JOIN
                         dbo.ClinicDoctor ON dbo.ClinicUser.ClinicUserId = dbo.ClinicDoctor.ClinicUserId INNER JOIN
                         dbo.Department ON dbo.ClinicDoctor.DepartmentId = dbo.Department.DepartmentId INNER JOIN
                         dbo.Workshift ON dbo.ClinicDoctor.WorkShiftId = dbo.Workshift.WorkShiftId

go

CREATE VIEW vwMaintenance AS
	SELECT        dbo.ClinicUser.ClinicUserId, dbo.ClinicUser.FullName, dbo.ClinicUser.IDNumber, dbo.ClinicUser.GenderId, dbo.ClinicUser.DateOfBirth, dbo.ClinicUser.Citizenship, dbo.ClinicUser.Username, dbo.ClinicUser.Password, 
                         dbo.ClinicUser.RoleId, dbo.ClinicUser.IsDeleted, dbo.Gender.Name, dbo.ClinicMaintenance.PermissionToExpandClinic, dbo.ClinicMaintenance.ResponsibleForVehicleAccessibility, 
                         dbo.ClinicMaintenance.ResponsibleForAccessOfHandicaps, dbo.ClinicMaintenance.ClinicMaintenanceId
    FROM            dbo.ClinicUser INNER JOIN
                         dbo.Gender ON dbo.ClinicUser.GenderId = dbo.Gender.GenderId INNER JOIN
                         dbo.ClinicMaintenance ON dbo.ClinicUser.ClinicUserId = dbo.ClinicMaintenance.ClinicUserId

go

CREATE VIEW vwManagers AS
	SELECT        dbo.ClinicUser.ClinicUserId, dbo.ClinicUser.FullName, dbo.ClinicUser.IDNumber, dbo.ClinicUser.GenderId, dbo.ClinicUser.DateOfBirth, dbo.ClinicUser.Citizenship, dbo.ClinicUser.Username, dbo.ClinicUser.Password, 
                         dbo.ClinicUser.RoleId, dbo.ClinicUser.IsDeleted, dbo.Gender.Name AS GenderName, dbo.ClinicManager.ClinicManagerId, dbo.ClinicManager.ClinicUserId AS Expr1, dbo.ClinicManager.ClinicFloor, 
                         dbo.ClinicManager.MaxNumOfDoctorsSupervised, dbo.ClinicManager.MinNumOfRoomSupervised, dbo.ClinicManager.NumberOfMistake
	FROM            dbo.ClinicUser INNER JOIN
                         dbo.Gender ON dbo.ClinicUser.GenderId = dbo.Gender.GenderId INNER JOIN
                         dbo.ClinicManager ON dbo.ClinicUser.ClinicUserId = dbo.ClinicManager.ClinicUserId
go

CREATE VIEW vwPatient AS
	SELECT        dbo.ClinicUser.ClinicUserId, dbo.ClinicUser.FullName, dbo.ClinicUser.IDNumber, dbo.ClinicUser.GenderId, dbo.ClinicUser.DateOfBirth, dbo.ClinicUser.Citizenship, dbo.ClinicUser.Username, dbo.ClinicUser.Password, 
                         dbo.ClinicUser.RoleId, dbo.ClinicUser.IsDeleted, dbo.ClinicPatient.InsuranceNumber, dbo.ClinicPatient.InsuranceExpirationDate, dbo.ClinicPatient.UniqueDoctorNumber, dbo.ClinicPatient.ClinicPatientId
	FROM            dbo.ClinicUser INNER JOIN
                         dbo.ClinicPatient ON dbo.ClinicUser.ClinicUserId = dbo.ClinicPatient.ClinicUserId
