using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SNServiceAPI.Models;
using System.Data;
using SNServiceAPI.Controllers.v1;
using System;

namespace SNServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IDapper _dapper;
        private readonly IHashingService _hashingService;
        public UserController(IDapper dapper, IHashingService hashingService)
        {
            _dapper = dapper;
            _hashingService = hashingService;
        }

        //Create Users
        [HttpPost(nameof(Create))]
        public async Task<int> Create(UserModel data)
        {
            var dbparams = new DynamicParameters();
            //dbparams.Add("RoleId", data.RoleId, DbType.Int32);
            dbparams.Add("Citizenship", data.Citizenship);
            dbparams.Add("Persal", data.Persal);
            dbparams.Add("IDNumber", data.IDNumber);
            dbparams.Add("Firstname", data.Firstname);
            dbparams.Add("Surname", data.Surname);
            dbparams.Add("Gender", data.Gender);
            dbparams.Add("HouseNumber", data.HouseNumber);            
            dbparams.Add("StreetName", data.StreetName);
            dbparams.Add("Suburb", data.Suburb);           
            dbparams.Add("City", data.City);
            dbparams.Add("PostalCode", data.PostalCode);
            dbparams.Add("IsInformalSettlement", data.IsInformalSettlement, DbType.Boolean);
            dbparams.Add("CellNumber", data.CellNumber);
            dbparams.Add("EmailAddress", data.EmailAddress);
            dbparams.Add("Password", data.Password);
            //dbparams.Add("UserType", data.UserType);
            dbparams.Add("DistrictCode", data.DistrictCode);
            dbparams.Add("EmisNumber", data.EmisNumber);
            dbparams.Add("IsEmployee", data.IsEmployee, DbType.Boolean);
            dbparams.Add("Status", data.Status);
            dbparams.Add("Level", data.Level);
            dbparams.Add("Region", data.Region);
            dbparams.Add("Region", data.Position);
            dbparams.Add("DateCreated", DateTime.Now);
            dbparams.Add("Id", data.Id, DbType.Int32, ParameterDirection.Output);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddUsers]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpPost(nameof(UserProfile))]
        public async Task<int> UserProfile(UserModel data)
        {
            var dbparams = new DynamicParameters();
            //dbparams.Add("RoleId", data.RoleId, DbType.Int32);
            dbparams.Add("Citizenship", data.Citizenship);
            dbparams.Add("Persal", data.Persal);
            dbparams.Add("IDNumber", data.IDNumber);
            dbparams.Add("Firstname", data.Firstname);
            dbparams.Add("Surname", data.Surname);
            dbparams.Add("Gender", data.Gender);
            dbparams.Add("HouseNumber", data.HouseNumber);            
            dbparams.Add("StreetName", data.StreetName);
            dbparams.Add("Suburb", data.Suburb);           
            dbparams.Add("City", data.City);
            dbparams.Add("PostalCode", data.PostalCode);
            dbparams.Add("IsInformalSettlement", data.IsInformalSettlement, DbType.Boolean);
            dbparams.Add("CellNumber", data.CellNumber);
            dbparams.Add("EmailAddress", data.EmailAddress);
            dbparams.Add("Password", data.Password);
            //dbparams.Add("UserType", data.UserType);
            dbparams.Add("DistrictCode", data.DistrictCode);
            dbparams.Add("EmisNumber", data.EmisNumber);
            dbparams.Add("IsEmployee", data.IsEmployee, DbType.Boolean);
            dbparams.Add("Status", data.Status);
            dbparams.Add("Level", data.Level);
            dbparams.Add("Region", data.Region);
            dbparams.Add("Position", data.Position);
            dbparams.Add("DateCreated", DateTime.Now);
            dbparams.Add("Id", data.Id, DbType.Int32, ParameterDirection.Output);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertUpdateUser]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@Id");
            return retVal;
        }

        //Create UserProfileEmployeeViaUserMan
        [HttpPost(nameof(UserProfileEmployeeViaUserMan))]
        public async Task<string> UserProfileEmployeeViaUserMan(string Citizenship, string Persal, string IDNumber, string Firstname, string Surname, string Gender, string HouseNumber, string StreetName, string Suburb, string City, string PostalCode, bool IsInformalSettlement,string CellNumber, string EmailAddress, string Password, string DistrictCode, string EmisNumber, bool IsEmployee, string Status, string Level, string Region, string Position)
        {
            int Id = 0;
            string tempPassword = LoginController.GenerateRandomAlphanumericString();
            //string hash = _hashingService.HashPassword(tempPassword);

            var dbparams = new DynamicParameters();
            //dbparams.Add("RoleId", RoleId);
            dbparams.Add("Citizenship", Citizenship);
            dbparams.Add("Persal", Persal);
            dbparams.Add("IDNumber", IDNumber);
            dbparams.Add("Firstname", Firstname);
            dbparams.Add("Surname", Surname);
            dbparams.Add("Gender", Gender);
            dbparams.Add("HouseNumber", HouseNumber);           
            dbparams.Add("StreetName", StreetName);
            dbparams.Add("Suburb", Suburb);           
            dbparams.Add("City", City);
            dbparams.Add("PostalCode", PostalCode);
            dbparams.Add("IsInformalSettlement", IsInformalSettlement);
            dbparams.Add("CellNumber", CellNumber);
            dbparams.Add("EmailAddress", EmailAddress);
            //string hash = _hashingService.HashPassword(tempPassword);
            dbparams.Add("Password", tempPassword);
            //dbparams.Add("UserType", UserType);
            dbparams.Add("DistrictCode", DistrictCode);
            dbparams.Add("EmisNumber", EmisNumber);
            dbparams.Add("IsEmployee", IsEmployee);
            dbparams.Add("Status", Status);
            dbparams.Add("Level", Level);
            dbparams.Add("Region", Region);
            dbparams.Add("Position", Position);
            dbparams.Add("DateCreated", DateTime.Now);
            dbparams.Add("Id", Id, DbType.Int32, ParameterDirection.Output);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddUsers]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@Id") + ":" + tempPassword;
            return retVal;
        }

        //Create UserProfileSGBViaUserMan
        [HttpPost(nameof(UserProfileSGBViaUserMan))]
        public async Task<string> UserProfileSGBViaUserMan(string Citizenship, string IDNumber, string Firstname, string Surname, string Gender, string HouseNumber, string StreetName, string Suburb, string City, string PostalCode, bool IsInformalSettlement, string CellNumber, string EmailAddress, string Password, string DistrictCode, string EmisNumber, string Status, string Level, string Region, string Position)
        {
            int Id = 0;
            string tempPassword = LoginController.GenerateRandomAlphanumericString();
            //string hash = _hashingService.HashPassword(tempPassword);

            var dbparams = new DynamicParameters();
            //dbparams.Add("RoleId", RoleId);
            dbparams.Add("Citizenship", Citizenship);
            dbparams.Add("Persal", null);
            dbparams.Add("IDNumber", IDNumber);
            dbparams.Add("Firstname", Firstname);
            dbparams.Add("Surname", Surname);
            dbparams.Add("Gender", Gender);
            dbparams.Add("HouseNumber", HouseNumber);            
            dbparams.Add("StreetName", StreetName);
            dbparams.Add("Suburb", Suburb);            
            dbparams.Add("City", City);
            dbparams.Add("PostalCode", PostalCode);
            dbparams.Add("IsInformalSettlement", IsInformalSettlement);
            dbparams.Add("CellNumber", CellNumber);
            dbparams.Add("EmailAddress", EmailAddress);
            //string hash = _hashingService.HashPassword(tempPassword);
            dbparams.Add("Password", tempPassword);
            //dbparams.Add("UserType", UserType);
            dbparams.Add("DistrictCode", DistrictCode);
            dbparams.Add("EmisNumber", EmisNumber);
            dbparams.Add("IsEmployee", false);
            dbparams.Add("Status", Status);
            dbparams.Add("Level", Level);
            dbparams.Add("Region", Region);
            dbparams.Add("Position", Position);
            dbparams.Add("DateCreated", DateTime.Now);
            dbparams.Add("Id", Id, DbType.Int32, ParameterDirection.Output);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddUsers]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@Id") + ":" + tempPassword;
            return retVal;
        }

        //Update UserProfile
        [HttpPost(nameof(UpdateUserProfile))]
        public Task<int> UpdateUserProfile(int Id, string Citizenship,string Persal, string IDNumber, string Firstname, string Surname, string Gender, string HouseNumber, string StreetName, string Suburb, string City, string PostalCode, bool IsInformalSettlement, string CellNumber, string EmailAddress, string DistrictCode, string EmisNumber, string Level, string Region, string Position)
        {     
            var dbparams = new DynamicParameters();            
            dbparams.Add("Citizenship", Citizenship);
            dbparams.Add("Persal", Persal);
            dbparams.Add("IDNumber", IDNumber);
            dbparams.Add("Firstname", Firstname);
            dbparams.Add("Surname", Surname);
            dbparams.Add("Gender", Gender);
            dbparams.Add("HouseNumber", HouseNumber);
            dbparams.Add("StreetName", StreetName);          
            dbparams.Add("Suburb", Suburb);
            dbparams.Add("City", City);
            dbparams.Add("PostalCode", PostalCode);
            dbparams.Add("IsInformalSettlement", IsInformalSettlement);
            dbparams.Add("CellNumber", CellNumber);
            dbparams.Add("EmailAddress", EmailAddress);         
            dbparams.Add("DistrictCode", DistrictCode);
            dbparams.Add("EmisNumber", EmisNumber);
            dbparams.Add("Level", Level);
            dbparams.Add("Region", Region);
            dbparams.Add("Position", Position);
            dbparams.Add("Id", Id);

            var updateUserProfile = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateUserProfile]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updateUserProfile;
        }

        //Update Emp by Persal
        [HttpPost(nameof(UpdateExistingEmployeeProfileByPersal))]
        public async Task<int> UpdateExistingEmployeeProfileByPersal(string Persal, string Credentials)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Persal", Persal);
            dbparams.Add("Credentials", Credentials);
            // dbparams.Add("Credentials", _hashingService.HashPassword(Credentials));

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_UpdateExistingEmployeeProfileByPersal]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        //Update User By Id
        [HttpPost(nameof(UpdateExistingEmployeeProfileByIDNumber))]
        public async Task<int> UpdateExistingEmployeeProfileByIDNumber(string IDNumber, string Credentials)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("IDNumber", IDNumber);
            //dbparams.Add("Credentials", _hashingService.HashPassword(Credentials));
            dbparams.Add("Credentials", Credentials);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_UpdateExistingEmployeeProfileByIDNumber]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        //Reset Password
        [HttpPost(nameof(ResetUserPassword))]
        public async Task<int> ResetUserPassword(int Id)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", Id);
            //dbPara.Add("Credentials", LoginController.GenerateTempPassword());
            dbPara.Add("Credentials", LoginController.GenerateRandomAlphanumericString());

            var updateUser = await Task.FromResult(_dapper.Update<int>("[dbo].[SP_ResetPassword]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateUser;

        }

        //Update User
        [HttpPost(nameof(UpdateUser))]
        public Task<int> UpdateUser(int Id, string Credentials)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", Id);
            //string hash = _hashingService.HashPassword(Credentials);
            dbPara.Add("Credentials", Credentials);

            var updateUser = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateUser]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateUser;
        }

        //Deactivate User
        [HttpPost(nameof(DeactivateUser))]
        public Task<int> DeactivateUser(int Id)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", Id);
            //string hash = _hashingService.HashPassword(Credentials);
            //dbPara.Add("Credentials", Credentials);

            var updateUser = Task.FromResult(_dapper.Update<int>("[dbo].[SP_DeactivateUser]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateUser;
        }

        //Validate Password
        [HttpGet(nameof(ValidatePassword))]
        public async Task<bool> ValidatePassword(int Id, string Pass)
        {
            var result = await Task.FromResult(_dapper.Get<UserModel>($"Select * from [tblUsers] where id = {Id} and password = '{Pass}' ", null, commandType: CommandType.Text));

            if (result != null)
                return true;
            else
                return false;
        }

        // Check Cellphone Number on DB
        [HttpGet(nameof(IsCellphoneAvailable))]
        public Task<bool> IsCellphoneAvailable(string Cellphonenumber)
        {
            var results = Task.FromResult(_dapper.GetAll<string>($"Select CellNumber from [tblUsers] WHERE CellNumber LIKE '{Cellphonenumber}'", null,
                    commandType: CommandType.Text));

            if (results.Result.Count > 0)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        // Check Persal Number on DB
        [HttpGet(nameof(IsPersalAvailable))]
        public Task<bool> IsPersalAvailable(string Persal)
        {
            var results = Task.FromResult(_dapper.GetAll<string>($"Select Persal from [tblUsers] WHERE Persal LIKE '{Persal}'", null,
                    commandType: CommandType.Text));

            if (results.Result.Count > 0)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        // Check ID Number on DB
        [HttpGet(nameof(IsIDAvailable))]
        public Task<bool> IsIDAvailable(string IDNumber)
        {
            var results = Task.FromResult(_dapper.GetAll<string>($"Select IDNumber from [tblUsers] WHERE IDNumber LIKE '{IDNumber}'", null,
                    commandType: CommandType.Text));

            if (results.Result.Count > 0)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        [HttpGet(nameof(GetUserByPersaNumber))]
        public async Task<UserModel> GetUserByPersaNumber(string Persal)
        {
            var result = await Task.FromResult(_dapper.Get<UserModel>($" SELECT * FROM tblUsers WHERE Persal = '{Persal}'", null, commandType: CommandType.Text));
            return result;
          
        }

        [HttpGet(nameof(GetUserByIdNumber))]
        public async Task<UserModel> GetUserByIdNumber(string IdNumber)
        {
            var result = await Task.FromResult(_dapper.Get<UserModel>($" SELECT * FROM tblUsers WHERE IDNumber = '{IdNumber}'", null, commandType: CommandType.Text));
            return result;

        }

        //Get Users
        [HttpGet(nameof(UserList))]
        public Task<List<UserModel>> UserList()
        {
            var result = Task.FromResult(_dapper.GetAll<UserModel>($"SELECT tblUsers.*,(SELECT STRING_AGG(tblRoles.RoleName,',') from [dbo].[tblUserRole] INNER JOIN tblRoles ON tblUserRole.RoleId = tblRoles.Id where [tblUserRole].UserId=tblUsers.Id) UserType FROM tblUsers ORDER BY [Id] DESC", null,
                    commandType: CommandType.Text));

            return result;
        }
        
        //Get Users By Role to be continue....
        [HttpGet(nameof(GetUsersByRole))]
        public Task<List<UserRoleModel>> GetUsersByRole(int UserId)
        {
            var result = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select * from [tblUserRole]", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get HO_DIRECTOR User
        [HttpGet(nameof(GetListOfHeadOfficeUsers))]
        public Task<List<UserRoleModel>> GetListOfHeadOfficeUsers()
        {
            var result = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select * from [tblUserRole] where RoleId = 3", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get PRINCIPAL USERS
        [HttpGet(nameof(GetListOfPrincipals))]
        public Task<List<UserRoleModel>> GetListOfPrincipals()
        {
            var result = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select * from [tblUserRole] where RoleId = 12", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get SGB CHAIRPERSON USERS
        [HttpGet(nameof(GetListOfSGB))]
        public Task<List<UserRoleModel>> GetListOfSGB()
        {
            var result = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select * from [tblUserRole] where RoleId = 12", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get PROVINCIAL MONITORS
        [HttpGet(nameof(GetListOfProvincialUsers))]
        public Task<List<UserRoleModel>> GetListOfProvincialUsers()
        {
            var result = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select * from [tblUserRole] where RoleId = 2", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get DISTRICT DIRECTOR
        [HttpGet(nameof(GetListOfDistrictUsers))]
        public Task<List<UserRoleModel>> GetListOfDistrictUsers()
        {
            var result = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select * from [tblUserRole] where RoleId = 8", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get SCHOOL COORDINATOR
        [HttpGet(nameof(GetListOfSchoolUsers))]
        public Task<List<UserRoleModel>> GetListOfSchoolUsers()
        {
            var result = Task.FromResult(_dapper.GetAll<UserRoleModel>($"select * from [tblUserRole] where RoleId = 14", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get user by Id
        [HttpGet(nameof(GetById))]
        public async Task<UserModel> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<UserModel>($"Select * from [tblUsers] where Id = {Id}", null, commandType: CommandType.Text));
            return result;
        }

        //Get user role by Id
        [HttpGet(nameof(UserRole))]
        public async Task<UserRoleModel> UserRole(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<UserRoleModel>($"Select * from [tblUserRole] where UserId = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var result = await Task.FromResult(_dapper.Execute($"Delete [tblUsers] Where Id = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(Count))]
        public Task<int> Count(int num)
        {
            var totalcount = Task.FromResult(_dapper.Get<int>($"select COUNT(*) from [tblUsers] WHERE SchoolId like '%{num}%'", null,
                    commandType: CommandType.Text));
            return totalcount;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(UserModel data)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", data.Id);
            dbPara.Add("Name", data.Firstname, DbType.String);

            var updateArticle = Task.FromResult(_dapper.Update<int>("[dbo].[SP_Update_Article]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }

        [HttpPatch(nameof(UpdateParent))]
        public Task<int> UpdateParent(UserModel data)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", data.Id);
            dbPara.Add("Cell", data.CellNumber);

            var updateParent = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateUser]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateParent;
        }

        //Get Emp by ID/Persal
        [HttpGet(nameof(GetEmployeeByPersalOrIDNumber))]
        public async Task<UserModel> GetEmployeeByPersalOrIDNumber(string Id)
        {
            if (Id.Length < 13)
            {
                var result = await Task.FromResult(_dapper.Get<UserModel>($"Select * from [tblUsers] where persal = '{Id}'", null, commandType: CommandType.Text));
                return result;
            }
            else
            {
                var result = await Task.FromResult(_dapper.Get<UserModel>($"Select * from [tblUsers] where idnumber = '{Id}' and IsEmployee=0", null, commandType: CommandType.Text));
                return result;
            }
        }

        //Deactivate user
        [HttpPost(nameof(UpdateInActiveUser))] 
        public Task<int> UpdateInActiveUser(int Id, string Status)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("@Id", Id);
            dbPara.Add("@Status", Status);
            var UpdateInActiveUser = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateUserActive]",
            dbPara,
            commandType: CommandType.StoredProcedure));
            return UpdateInActiveUser;
        }
    }
}
