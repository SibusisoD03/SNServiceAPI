using Dapper;
using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Models;
using SNServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenceDataController : ControllerBase
    {
        private readonly IDapper _dapper;
        public ReferenceDataController(IDapper dapper)
        {
            _dapper = dapper;
        }

        //Get Districts
        //[HttpGet(nameof(Districts))]
        //public Task<List<DistrictModel>> Districts()
        //{
        //    var result = Task.FromResult(_dapper.GetAll<DistrictModel>($" select * from [tblDistrict]", null,
        //            commandType: CommandType.Text));
        //    return result;
        //}

        //Get Schoolist by District Code
        [HttpGet(nameof(SchoolsByDistrictId))]
        public Task<List<SchoolRegionModel>> SchoolsByDistrictId(string DistrictCode)
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolRegionModel>($" select * from [tblSchoolRegion] where DistrictCode LIKE'{DistrictCode}'", null,
                    commandType: CommandType.Text));
            return result;
        }


        //Get District By Id
        [HttpGet(nameof(GetDistrictById))]
        public Task<List<DistrictModel>> GetDistrictById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<DistrictModel>($"select * from [tblDistrict] where DistrictId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Roles
        [HttpGet(nameof(Roles))]
        public Task<List<RoleModel>> Roles()
        {
            var result = Task.FromResult(_dapper.GetAll<RoleModel>($"select * from [tblRoles]", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Roles By User Role
        //[HttpGet(nameof(RolesByUserRole))]
        //public async Task<List<RoleModel>> RolesByUserRole(string Role)
        //{
        //    var dbparams = new DynamicParameters();
        //    dbparams.Add("Role", Role, DbType.String);
        //    var result = await Task.FromResult(_dapper.GetAll<RoleModel>("[dbo].[SP_SelectRolesByUserRole]"
        //        , dbparams,
        //        commandType: CommandType.StoredProcedure));
        //    return result;

        //}
        [HttpGet(nameof(RolesByUserRole))]
        public Task<List<RoleModel>> RolesByUserRole(string Role)
        {
            var result = Task.FromResult(_dapper.GetAll<RoleModel>($"Select Id,RoleName from [tblRoles] where RoleName NOT LIKE'{Role}' AND RoleLevel >= (Select RoleLevel from tblRoles where RoleName LIKE '{Role}')", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Designations
        [HttpGet(nameof(Designations))]
        public Task<List<DesignationModel>> Designations()
        {
            var result = Task.FromResult(_dapper.GetAll<DesignationModel>($" select * from [tblDesignations]", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Designations
        [HttpGet(nameof(Districts))]
        public Task<List<DistrictModel>> Districts()
        {
            var result = Task.FromResult(_dapper.GetAll<DistrictModel>($" select * from [tblDistrict]", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get District By DistrictCode
        [HttpGet(nameof(DistrictByCode))]
        public Task<List<DistrictModel>> DistrictByCode(string Code)
        {
            var result = Task.FromResult(_dapper.GetAll<DistrictModel>($"select * from [tblDistrict] where district_code LIKE'{Code}'", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get District By DistrictCode on tblSchoolRegion
        [HttpGet(nameof(DistrictNameByCode))]
        public Task<List<SchoolRegionModel>> DistrictNameByCode(string Code)
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolRegionModel>($"select * from [tblSchoolRegion] where districtcode LIKE'{Code}'", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get District By DistrictCode on tblSchoolRegion
        [HttpGet(nameof(GetDistrictCodeByName))]
        public Task<List<SchoolRegionModel>> GetDistrictCodeByName(string District)
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolRegionModel>($"select * from [tblSchoolRegion] where districtName LIKE'{District}'", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get School By EmisNo from tblSchoolList
        [HttpGet(nameof(SchoolsByEmisNo))]
        //public Task<List<SchoolModel>> SchoolsByEmisNo(string EmisNo)
        //{
        //    var result = Task.FromResult(_dapper.GetAll<SchoolModel>($"select * from [tblSchoolList] where NatEmis LIKE'{EmisNo}'", null,
        //            commandType: CommandType.Text));
        //    return result;
        //}

        //Get School By EmisNo from tblSchoolRegion
        public Task<List<SchoolRegionModel>> SchoolsByEmisNo(string EmisNo)
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolRegionModel>($"select * from [tblSchoolRegion] where EmisNumber ='{EmisNo}'", null,
                    commandType: CommandType.Text));
            return result;
        }
  
        //Get School, EmisNo, DistrictName, NSNPAllocation By SchoolName
        [HttpGet(nameof(GetDetailsBySchoolName))]
        public Task<List<SchoolsModel>> GetDetailsBySchoolName(string Institution)
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolsModel>($"select InstitutionName, EmisNumber, DistrictName, Level, CircuitNo, ClusterNo from [tblSchools] where InstitutionName LIKE'{Institution}'", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Supplier name by school
        //[HttpGet(nameof(GetSupplierBySchoolName))]
        //public Task<List<SupplierSummaryModel>> GetSupplierBySchoolName(string Institution)
        //{
        //    var result = Task.FromResult(_dapper.GetAll<SupplierSummaryModel>($"select suppliername from tblsupplier where [AssignedSchool] = (select [SchoolName] from [tblSupplierSummary] where [SchoolName]  LIKE'{Institution}')", null,
        //            commandType: CommandType.Text));
        //    return result;
        //}

        //Get Schools
        [HttpGet(nameof(GetSchools))]
        public Task<List<SchoolsModel>> GetSchools()
        { 
            var result = Task.FromResult(_dapper.GetAll<SchoolsModel>($"select InstitutionName from [tblSchools]", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Schools
        [HttpGet(nameof(Schools))]
        public Task<List<SchoolModel>> Schools()
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolModel>($"select * from [tblSchoolList] ", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get All Product Types
        [HttpGet(nameof(GetProductType))]
        public Task<List<ProductTypeModel>> GetProductType()
        {
            var result = Task.FromResult(_dapper.GetAll<ProductTypeModel>($"select * from [tblProductType] ", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Product Types By ID
        [HttpGet(nameof(GetProductTypeByID))]
        public Task<List<ProductTypeModel>> GetProductTypeByID(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<ProductTypeModel>($"select * from [tblProductType] where ProductTypeID={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get All Food Group Details
        [HttpGet(nameof(GetFoodGroup))]
        public Task<List<FoodGroupModel>> GetFoodGroup()
        {
            var result = Task.FromResult(_dapper.GetAll<FoodGroupModel>($"select * from [tblFoodGroup] ", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Food Group Details By ID
        [HttpGet(nameof(GetFoodGroupByID))]
        public Task<List<FoodGroupModel>> GetFoodGroupByID(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<FoodGroupModel>($"select * from [tblFoodGroup] where FoodGroupID={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get All Unit
        [HttpGet(nameof(GetUnit))]
        public Task<List<UnitModel>> GetUnit()
        {
            var result = Task.FromResult(_dapper.GetAll<UnitModel>($"select * from [tblUnit] ", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Unit By ID
        [HttpGet(nameof(GetUnitByID))]
        public Task<List<UnitModel>> GetUnitByID(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<UnitModel>($"select * from [tblUnit] where UnitID={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get All Type
        [HttpGet(nameof(GetType))]
        public Task<List<TypeModel>> GetType()
        {
            var result = Task.FromResult(_dapper.GetAll<TypeModel>($"select * from [tblType] ", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Type By ID
        [HttpGet(nameof(GetTypeByID))]
        public Task<List<TypeModel>> GetTypeByID(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<TypeModel>($"select * from [tblType] where TypeID={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get All Facilities
        [HttpGet(nameof(GetFacilities))]
        public Task<List<FacilitiesModel>> GetFacilities()
        {
            var result = Task.FromResult(_dapper.GetAll<FacilitiesModel>($"select * from [tblFacilities] ", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Facility By ID
        [HttpGet(nameof(GetFacilityByID))]
        public Task<List<FacilitiesModel>> GetFacilityByID(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<FacilitiesModel>($"select * from [tblFacilities] where FacilityID={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get All Places
        [HttpGet(nameof(GetChildLivePlace))]
        public Task<List<PlaceModel>> GetChildLivePlace()
        {
            var result = Task.FromResult(_dapper.GetAll<PlaceModel>($"select * from [tblPlace] ", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Place By ID
        [HttpGet(nameof(GetChildLivePlaceByID))]
        public Task<List<PlaceModel>> GetChildLivePlaceByID(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<PlaceModel>($"select * from [tblPlace] where PlaceID={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Products
        //[HttpGet(nameof(GetProductsList))]
        //public Task<List<ProductModel>> GetProductsList()
        //{
        //    var result = Task.FromResult(_dapper.GetAll<ProductModel>($"SELECT TypeDescription, UnitDescription, FoodGroupDescription FROM tblType t, tblProduct p, tblUnit u, tblFoodGroup f WHERE t.TypeID = p.TypeID AND u.UnitID = p.UnitID AND f.FoodGroupID = p.FoodGroupID", null,
        //            commandType: CommandType.Text));
        //    return result;
        //}
        [HttpGet(nameof(GetProductsList))]
        public Task<List<ProductModel>> GetProductsList()
        {
            var result = Task.FromResult(_dapper.GetAll<ProductModel>($"SELECT t.TypeID, t.TypeDescription, u.UnitID, u.UnitDescription, f.FoodGroupID, f.FoodGroupDescription FROM tblType t, tblProduct p, tblUnit u, tblFoodGroup f WHERE t.TypeID = p.TypeID AND u.UnitID = p.UnitID AND f.FoodGroupID = p.FoodGroupID", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get data per region
        [HttpGet(nameof(GetRegionalData))]
        public Task<List<SchoolRegionModel>> GetRegionalData()
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolRegionModel>($"select * from [tblSchoolRegion] ", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Office Level
        [HttpGet(nameof(GetLevels))]
        public Task<List<LevelModel>> GetLevels()
        {
            var result = Task.FromResult(_dapper.GetAll<LevelModel>($"select * from [tblLevel] ", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Region
        [HttpGet(nameof(GetRegion))]
        public Task<List<SchoolRegionModel>> GetRegion()
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolRegionModel>($"select distinct(region) from [tblSchoolRegion] ", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get District by region
        [HttpGet(nameof(GetDistrictByRegion))]
        public Task<List<SchoolRegionModel>> GetDistrictByRegion(string region)
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolRegionModel>($"select * from [tblSchoolRegion] where Region LIKE '{region}'", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get schoolroll by school
        [HttpGet(nameof(GetSchoolRollBySchoolName))]
        public Task<List<VerificationFormModel>> GetSchoolRollBySchoolName(string institution)
        {
            var result = Task.FromResult(_dapper.GetAll<VerificationFormModel>($"select schoolroll from [tblVerification] where schoolName LIKE (SELECT institutionName from tblSchools where institutionName LIKE '{institution}')", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get supplier by school
        [HttpGet(nameof(GetSupplierBySchoolName))]
        public Task<List<SupplierModel>> GetSupplierBySchoolName(string institution)
        {
            var result = Task.FromResult(_dapper.GetAll<SupplierModel>($"select supplierName from [tblSupplier] where assignedSchool = (SELECT institutionName from tblSchools where institutionName LIKE '{institution}')", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get position by level
        [HttpGet(nameof(GetPositionByOfficeLevel))]
        public Task<List<RoleModel>> GetPositionByOfficeLevel(string level, string role)
        {
            var result = Task.FromResult(_dapper.GetAll<RoleModel>($"select * from [tblRoles] where OfficeLevel LIKE '{level}' AND role != '{role}'", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get school, email address by district name
        [HttpGet(nameof(GetSchoolsEmailByDistrict))]
        public Task<List<SchoolsModel>> GetSchoolsEmailByDistrict(string district)
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolsModel>($"select institutionName, schoolEmail  from [tblSchools] where districtName LIKE '{district}'", null,
                    commandType: CommandType.Text));
            return result;
        }
    }
}
