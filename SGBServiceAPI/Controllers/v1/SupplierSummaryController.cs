using Dapper;
using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Models;
using SNServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SNServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierSummaryController : ControllerBase
    {
        private readonly IDapper _dapper;
        public SupplierSummaryController(IDapper dapper)
        {
            _dapper = dapper;
        }
        
        [HttpPost(nameof(CreateSupplierSummary))]
        public async Task<int> CreateSupplierSummary(SupplierSummaryModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SupplierSummaryId", data.SupplierSummaryId, DbType.Int32);
            dbparams.Add("@Quarter", data.Quarter);
            dbparams.Add("@StartDate", DateTime.Now);
            dbparams.Add("@EndDate", DateTime.Now);
            dbparams.Add("@SupplierName", data.SupplierName);
            dbparams.Add("@NSNPAllocation", data.NSNPAllocation);
            dbparams.Add("@DistrictName", data.DistrictName);
            dbparams.Add("@NumberOfLearners", data.NumberOfLearners, DbType.Int32);
            dbparams.Add("@SchoolName", data.SchoolName);
            dbparams.Add("@Month", data.Month);
            dbparams.Add("@EmisNo", data.EmisNo, DbType.Int32);
            dbparams.Add("@ClusterNo", data.ClusterNo, DbType.Int32);
            dbparams.Add("@CircuitNo", data.CircuitNo, DbType.Int32);
            string perishableJson = JsonConvert.SerializeObject(data.PerishablesList);
            //perishableJson = perishableJson.Replace('\\','');
            dbparams.Add("@PerishableJSON", perishableJson);


            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddSupplierSummary]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@SupplierSummaryId");
            return retVal;
        }
        
        [HttpPost(nameof(SaveSupplierDelivery))]
        public async Task<int> SaveSupplierDelivery(PerishableModel data)
        {
            var dbparams = new DynamicParameters();
            
            dbparams.Add("@FoodGroupId", data.FoodGroupId, DbType.Int32);
            dbparams.Add("@ProductId", data.FoodGroupId, DbType.Int32);
            dbparams.Add("@Quantity", data.Quantity);
            dbparams.Add("@Unit", data.Unit);
            dbparams.Add("@Grammage", data.Grammage);
            dbparams.Add("@EmisNumber", data.EmisNumber);
            dbparams.Add("@PerishableProductId", data.PerishableProductId, DbType.Int32);
            dbparams.Add("@SupplierSummaryId", data.SupplierSummaryId);
            dbparams.Add("@isQualityWrong", data.isQualityWrong);
            dbparams.Add("@isProductWrong", data.isProductWrong);
            dbparams.Add("@isQuantityWrong", data.isQuantityWrong);
            dbparams.Add("@QuantityRecieved", data.QuantityRecieved);
            dbparams.Add("@ItemDelieveryDate", data.ItemDelieveryDate);
            dbparams.Add("@WrongProductName", data.WrongProductName);
            dbparams.Add("@DocumentPath", data.DocumentPath);
            dbparams.Add("@TypeDescription", data.TypeDescription);
            dbparams.Add("@FoodGroupDescription", data.FoodGroupDescription);


            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddDeliveryQuery]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@SupplierSummaryId");
            return retVal;
        }

        [HttpPost(nameof(AddMonthlyDelieveryDates))]
        public async Task<int> AddMonthlyDelieveryDates(MonthlyDelieveryDates data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@id", data.id, DbType.Int32);
            dbparams.Add("@productType", data.productType);
            dbparams.Add("@deliveryDates", data.deliveryDates);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddMonthlyDeliveryDates]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@id");
            return retVal;
        }

        
        [HttpPatch(nameof(UpdateMonthlyDelieveryDates))]
        public Task<int> UpdateMonthlyDelieveryDates(MonthlyDelieveryDates data)
        {
           var dbparams = new DynamicParameters();
            dbparams.Add("@id", data.id, DbType.Int32);
            dbparams.Add("@productType", data.productType);
            dbparams.Add("@deliveryDates", data.deliveryDates);

            var updateMonthlyDelivery = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateMonthlyDeliveryDates]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateMonthlyDelivery;

        }

        [HttpGet(nameof(GetMonthlyDelieveryDates))]
        public Task<List<MonthlyDelieveryDates>> GetMonthlyDelieveryDates()
        {
            var result = Task.FromResult(_dapper.GetAll<MonthlyDelieveryDates>($"select * from tblMonthlyDeliveryDates", null,
                    commandType: CommandType.Text));
            return result;
        }
    
        [HttpGet(nameof(GetSupplierSummary))]
        public Task<List<SupplierSummaryModel>> GetSupplierSummary()
        {
            var result = Task.FromResult(_dapper.GetAll<SupplierSummaryModel>($"select * from [tblSupplierSummary]", null,
                    commandType: CommandType.Text));
            return result;
           
        }

        [HttpGet(nameof(GetAllPerishableNonperishable))]
        public Task<List<PerishableModel>> GetAllPerishableNonperishable()
        {
            var result = Task.FromResult(_dapper.GetAll<PerishableModel>($"SELECT * FROM tblPerishableProduct ", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(SchoolsByEmisNo))]
        public Task<List<SchoolModel>> SchoolsByEmisNo(string EmisNo)
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolModel>($"select * from [tblSchoolList] where NatEmis LIKE'{EmisNo}'", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetLastSupplierSummary))]
        public Task<List<SupplierSummaryModel>> GetLastSupplierSummary()
        {
            var Output = Task.FromResult(_dapper.GetAll<SupplierSummaryModel>($"SELECT[SupplierSummaryId] FROM[dbo].[tblSupplierSummary] WHERE[SupplierSummaryId] = (SELECT IDENT_CURRENT('[tblSupplierSummary]'))", null,
            commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetAllQueries))]
        public Task<List<PerishableModel>> GetAllQueries()
        {
            var Output = Task.FromResult(_dapper.GetAll<PerishableModel>($"SELECT * FROM [dbo].[tblQueries]", null,
            commandType: CommandType.Text));
            return Output;
        }

        [HttpGet(nameof(GetSupplierSummaryById))]
        public Task<SupplierSummaryModel> GetSupplierSummaryById(int Id)
        {
            var result = Task.FromResult(_dapper.Get<SupplierSummaryModel>($"select * from [tblSupplierSummary] where SupplierSummaryId={Id}", null,
                    commandType: CommandType.Text));

            var resultPerishable = Task.FromResult(_dapper.GetAll<PerishableModel>($"SELECT pp.[PerishableProductId],pp.[FoodGroupId],pp.[ProductId],pp.[Quantity],pp.[Unit], pp.[SupplierSummaryId], pp.[Grammage], pp.[EmisNumber], pp.[IsQualityWrong], pp.[IsProductWrong], pp.[IsQuantityWrong], pp.[ItemDelieveryDate], pp.[WrongProductName], pp.[DocumentPath], pp.[quantityRecieved], fg.FoodGroupDescription, t.TypeDescription, p.GrammageSecondary FROM[dbo].[tblPerishableProduct] pp, tblFoodGroup fg, tblType t, tblProduct p WHERE pp.[FoodGroupId] = fg.[FoodGroupID] AND pp.[FoodGroupId] = t.[FoodGroupId] AND pp.[FoodGroupId] = p.[FoodGroupID] AND pp.[SupplierSummaryId] = {Id}", null,
                    commandType: CommandType.Text));

            if (resultPerishable != null)
                result.Result.PerishablesList = resultPerishable.Result;

            return result;
        }

        //[HttpGet(nameof(GetSupplierSummaryById))]
        //public Task<SupplierSummaryModel> GetSupplierSummaryById(int Id)
        //{
        //    var result = Task.FromResult(_dapper.Get<SupplierSummaryModel>($"select * from [tblSupplierSummary] where SupplierSummaryId={Id}", null,
        //            commandType: CommandType.Text));

        //    var resultPerishable = Task.FromResult(_dapper.GetAll<PerishableModel>($"SELECT * FROM tblPerishableProduct INNER JOIN tblFoodGroup ON tblFoodGroup.FoodGroupID = tblPerishableProduct.FoodGroupID INNER JOIN tblProduct ON tblProduct.ProductID=tblPerishableProduct.ProductID INNER JOIN tblType ON tblType.TypeID = tblProduct.TypeID WHERE SupplierSummaryId={Id}", null,
        //            commandType: CommandType.Text));

        //    if(resultPerishable!=null)
        //        result.Result.PerishablesList = resultPerishable.Result;

        //    return result;
        //}


        [HttpGet(nameof(GetSupplierSummaryByEmisNo))]
        public Task<List<SupplierSummaryModel>> GetSupplierSummaryByEmisNo(int EmisNo)
        {
            var result = Task.FromResult(_dapper.GetAll<SupplierSummaryModel>($"select * from [tblSupplierSummary] where EmisNo={EmisNo}", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpPatch(nameof(UpdateSupplierSummary))]
        public Task<int> UpdateSupplierSummary(SupplierSummaryModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SupplierSummaryId", data.SupplierSummaryId, DbType.Int32);
            dbparams.Add("@Quarter", data.Quarter);
            dbparams.Add("@StartDate", DateTime.Now);
            dbparams.Add("@EndDate", DateTime.Now);
            dbparams.Add("@SupplierName", data.SupplierName);
            dbparams.Add("@NSNPAllocation", data.NSNPAllocation);
            dbparams.Add("@DistrictName", data.DistrictName);
            dbparams.Add("@NumberOfLearners", data.NumberOfLearners, DbType.Int32);
            dbparams.Add("@SchoolName", data.SchoolName);
            dbparams.Add("@Month", data.Month);
            dbparams.Add("@EmisNo", data.EmisNo, DbType.Int32);
            dbparams.Add("@ClusterNo", data.ClusterNo, DbType.Int32);
            dbparams.Add("@CircuitNo", data.CircuitNo, DbType.Int32);

            var updateSupplierSummary = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateSupplierSummary]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateSupplierSummary;

        }


    }
}
