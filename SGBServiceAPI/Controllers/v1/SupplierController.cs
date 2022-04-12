using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SNServiceAPI.Models;
using System.Data;
using SNServiceAPI.Services;

namespace SNServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IDapper _dapper;
        public SupplierController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateSupplier))]
        public async Task<int> CreateSupplier(SupplierModel data)
        {
            string tempPassword = LoginController.GenerateRandomAlphanumericString();
            var dataBaseParams = new DynamicParameters();
            string RoleName = "SUPPLIER";
            dataBaseParams.Add("@SupplierId", data.SupplierId, DbType.Int32);
            dataBaseParams.Add("@DistrictCode", data.DistrictCode, DbType.String);
            dataBaseParams.Add("@DistrictName", data.DistrictName, DbType.String);
            dataBaseParams.Add("@SupplierName", data.SupplierName);
            dataBaseParams.Add("@VendorNo", data.VendorNo);
            dataBaseParams.Add("@EmailAddress", data.EmailAddress);
            dataBaseParams.Add("@TelephoneNo", data.TelephoneNo);
            dataBaseParams.Add("@Line1Address", data.Line1Address);
            dataBaseParams.Add("@Line2Address", data.Line2Address);
            dataBaseParams.Add("@Town", data.Town);
            dataBaseParams.Add("@City", data.City);
            dataBaseParams.Add("@Province", data.Province);
            dataBaseParams.Add("@PostalCode", data.PostalCode);
            dataBaseParams.Add("@ContactPersonName", data.ContactPersonName);
            dataBaseParams.Add("@ContactPersonSurname", data.ContactPersonSurname);
            dataBaseParams.Add("@ContactNo", data.ContactNo);
            dataBaseParams.Add("@CSDNumber", data.CSDNumber);
            dataBaseParams.Add("@ContractStartDate", data.ContractStartDate, DbType.DateTime);
            dataBaseParams.Add("@ContractEndDate", data.ContractEndDate, DbType.DateTime);
            dataBaseParams.Add("@Status", "Undefined");
            dataBaseParams.Add("@AssignedSchool", data.AssignedSchool);
            dataBaseParams.Add("@Password", tempPassword);
            dataBaseParams.Add("@RoleName", RoleName);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddSupplier]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var returnVal = dataBaseParams.Get<int>("SupplierId");
            return returnVal;
        }


        [HttpGet(nameof(GetSupplierList))]
        public Task<List<SupplierModel>> GetSupplierList()
        {
            var SupplierList = Task.FromResult(_dapper.GetAll<SupplierModel>($"select * from [dbo].[tblSupplier]", null,
                    commandType: CommandType.Text));
            return SupplierList;
        }
        [HttpGet(nameof(GetSupplierByID))]
        public Task<List<SupplierModel>> GetSupplierByID(int ID)
        {
            var Supplier = Task.FromResult(_dapper.GetAll<SupplierModel>($"select * from [dbo].[tblSupplier] where [SupplierId] = {ID}", null,
                    commandType: CommandType.Text));
            return Supplier;
        }

        [HttpGet(nameof(GetSupplierDetails))]
        public Task<List<SupplierModel>> GetSupplierDetails()
        {
            var SupplierList = Task.FromResult(_dapper.GetAll<SupplierModel>($"Select [SupplierId], [SupplierName], [ContactPersonName] + ' ' + [ContactPersonSurname] as ContactPerson, [TelephoneNo], [Status] from [dbo].[tblSupplier]", null,
                    commandType: CommandType.Text));
            return SupplierList;
        }
        [HttpPost(nameof(UpdateSuppliers))]
        public Task<int> UpdateSuppliers(int Id, string DistrictCode, string DistrictName, string SupplierName, string VendorNo, string EmailAddress, string Tel, string Line1Address, string Line2Address, string Town, string City, string Province, string PCode, string ContactPersonName, string ContactPersonSurname, string ContactNo, string CSDNumber, DateTime ContractStart, DateTime ContractEnd, string Status, string AssignToSchool)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@SupplierId", Id);
            dataBaseParams.Add("@DistrictCode", DistrictCode);
            dataBaseParams.Add("@DistrictName", DistrictName);
            dataBaseParams.Add("@SupplierName", SupplierName);
           dataBaseParams.Add("@VendorNo", VendorNo);
            dataBaseParams.Add("@EmailAddress", EmailAddress);
            dataBaseParams.Add("@TelephoneNo", Tel);
            dataBaseParams.Add("@Line1Address", Line1Address);
            dataBaseParams.Add("@Line2Address", Line2Address);
            dataBaseParams.Add("@Town", Town);
            dataBaseParams.Add("@City", City);
            dataBaseParams.Add("@Province", Province);
            dataBaseParams.Add("@PostalCode", PCode);
            dataBaseParams.Add("@ContactPersonName", ContactPersonName);
            dataBaseParams.Add("@ContactPersonSurname", ContactPersonSurname);
            dataBaseParams.Add("@ContactNo", ContactNo);
            dataBaseParams.Add("@CSDNumber", CSDNumber);
            dataBaseParams.Add("@ContractStartDate", ContractStart);
            dataBaseParams.Add("@ContractEndDate", ContractEnd);
            dataBaseParams.Add("@Status", Status);
            dataBaseParams.Add("@AssignedSchool", AssignToSchool);

            var updateSupplier = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSupplier]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateSupplier;
        }
        [HttpPost(nameof(ActivateSupplierLoginProfile))]
        public Task<int> ActivateSupplierLoginProfile(string VendorNo, string Password)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@VendorNo", VendorNo);
            dataBaseParams.Add("@Password", Password);


            var updateSupplierProfile = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSupplierProfile]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateSupplierProfile;
        }
        [HttpGet(nameof(GetSupplierByVendorNo))]
        public Task<List<SupplierModel>> GetSupplierByVendorNo(string vendorNo)
        {
            var vendor = Task.FromResult(_dapper.GetAll<SupplierModel>($"select * from [dbo].[tblSupplier] where [VendorNo] = '{vendorNo}'", null,
                    commandType: CommandType.Text));
            return vendor;
        }
        [HttpPatch(nameof(CreateSupplierLoginProfile1))]
        public Task<int> CreateSupplierLoginProfile1(string VendorNo, string Password)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@VendorNo", VendorNo);
            dataBaseParams.Add("@Password", Password);

            var updateVD = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSupplierProfile]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return updateVD;
        }

        [HttpGet(nameof(IsCellphoneAvailable))]
        public Task<bool> IsCellphoneAvailable(string TelephoneNo)
        {
            var results = Task.FromResult(_dapper.GetAll<string>($"Select TelephoneNo from [dbo].[tblSupplier] WHERE TelephoneNo='{TelephoneNo}'", null,
            commandType: CommandType.Text));


            if (results.Result.Count > 0)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);


        }
        
        [HttpGet(nameof(IsSupplierVendorNumberAvailable))]
        public Task<bool> IsSupplierVendorNumberAvailable(string VendorNo)
        {
            var results = Task.FromResult(_dapper.GetAll<string>($"Select VendorNo from [dbo].[tblSupplier] WHERE VendorNo='{VendorNo}'", null,
            commandType: CommandType.Text));


            if (results.Result.Count > 0)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);


        }

        [HttpPost(nameof(UpdateSupplier))]
        public Task<int> UpdateSupplier(SupplierModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@SupplierId", data.SupplierId, DbType.Int32);
            dataBaseParams.Add("@DistrictCode", data.DistrictCode);
            dataBaseParams.Add("@DistrictName", data.DistrictName);
            dataBaseParams.Add("@SupplierName", data.SupplierName);
            dataBaseParams.Add("@VendorNo", data.VendorNo);
            dataBaseParams.Add("@EmailAddress", data.EmailAddress);
            dataBaseParams.Add("@TelephoneNo", data.TelephoneNo);
            dataBaseParams.Add("@Line1Address", data.Line1Address);
            dataBaseParams.Add("@Line2Address", data.Line2Address);
            dataBaseParams.Add("@Town", data.Town);
            dataBaseParams.Add("@City", data.City);
            dataBaseParams.Add("@Province", data.Province);
            dataBaseParams.Add("@PostalCode", data.PostalCode);
            dataBaseParams.Add("@ContactPersonName", data.ContactPersonName);
            dataBaseParams.Add("@ContactPersonSurname", data.ContactPersonSurname);
            dataBaseParams.Add("@ContactNo", data.ContactNo);
            dataBaseParams.Add("@CSDNumber", data.CSDNumber);
            dataBaseParams.Add("@ContractStartDate", data.ContractStartDate);
            dataBaseParams.Add("@ContractEndDate", data.ContractEndDate);
            dataBaseParams.Add("@Status", data.Status);
            dataBaseParams.Add("@AssignedSchool", data.AssignedSchool);

            var UpdatedSupplier = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSupplier]",
                            dataBaseParams,
                            commandType: CommandType.StoredProcedure));
            return UpdatedSupplier;
        }
        /*[HttpPost(nameof(UpdateSupplier))]
        public Task<int> UpdateSupplier(SupplierModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@SupplierId", data.SupplierId, DbType.Int32);
            dataBaseParams.Add("@DistrictCode", data.SupplierName);
            dataBaseParams.Add("@DistrictName", data.VendorNo);
            dataBaseParams.Add("@SupplierName", data.SupplierName);
            dataBaseParams.Add("@VendorNo", data.VendorNo);
            dataBaseParams.Add("@EmailAddress", data.EmailAddress);
            dataBaseParams.Add("@TelephoneNo", data.TelephoneNo);
            dataBaseParams.Add("@Line1Address", data.Line1Address);
            dataBaseParams.Add("@Line2Address", data.Line2Address);
            dataBaseParams.Add("@Town", data.Town);
            dataBaseParams.Add("@City", data.City);
            dataBaseParams.Add("@Province", data.Province);
            dataBaseParams.Add("@PostalCode", data.PostalCode);
            dataBaseParams.Add("@ContactPersonName", data.ContactPersonName);
            dataBaseParams.Add("@ContactPersonSurname", data.ContactPersonSurname);
            dataBaseParams.Add("@ContactNo", data.ContactNo);
            dataBaseParams.Add("@CSDNumber", data.CSDNumber);
            dataBaseParams.Add("@ContractStartDate", data.ContractStartDate);
            dataBaseParams.Add("@ContractEndDate", data.ContractEndDate);
            dataBaseParams.Add("@Status", data.Status);
            dataBaseParams.Add("@AssignedSchool", data.AssignedSchool);



            var updateSupplier = Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateSupplier]",
            dataBaseParams,
            commandType: CommandType.StoredProcedure));
            return updateSupplier;
        }*/
    }
}