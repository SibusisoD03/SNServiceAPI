using Dapper;
using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Models;
using SNServiceAPI.Services;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SNServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerishableProductController : ControllerBase
    {
        private readonly IDapper _dapper;
        public PerishableProductController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreatePerishableProduct))]
        public async Task<int> CreatePerishableProduct(PerishableProductModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PerishableProductId", data.PerishableProductId, DbType.Int32);
            dbparams.Add("@Expected", data.Expected);
            dbparams.Add("@SupplierSummaryId", data.SupplierSummaryId, DbType.Int32);
            dbparams.Add("@ProductType", data.ProductType);
            dbparams.Add("@FoodGroup", data.FoodGroup);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddPerishableProduct]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@PerishableProductId");
            return retVal;
        }

        [HttpGet(nameof(GetPerishableProduct))]
        public Task<List<PerishableProductModel>> GetPerishableProduct()
        {
            var result = Task.FromResult(_dapper.GetAll<PerishableProductModel>($"select * from [tblPerishableProduct]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetPerishableProductById))]
        public Task<List<PerishableProductModel>> GetPerishableProductById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<PerishableProductModel>($"select * from [tblPerishableProduct] where PerishableProductId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(UpdatePerishableProduct))]
        public Task<int> UpdatePerishableProduct(PerishableProductModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@PerishableProductId", data.PerishableProductId, DbType.Int32);
            dbparams.Add("@Expected", data.Expected);
            dbparams.Add("@SupplierSummaryId", data.SupplierSummaryId, DbType.Int32);
            dbparams.Add("@ProductType", data.ProductType);
            dbparams.Add("@FoodGroup", data.FoodGroup);

            var updatePerishableProduct = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdatePerishableProduct]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updatePerishableProduct;

        }

        //Queries

        //Query by Id
        [HttpGet(nameof(GetQueryById))]
        public Task<List<QueryModel>> GetQueryById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<QueryModel>($"select * from [tblQueries] where Id={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Query all
        [HttpGet(nameof(GetQueryList))]
        public Task<List<QueryModel>> GetQueryList()
        {
            var result = Task.FromResult(_dapper.GetAll<QueryModel>($"SELECT * FROM [dbo].[tblQueries] INNER JOIN [dbo].[tblSupplierSummary] ON [dbo].[tblSupplierSummary].SupplierSummaryId=[dbo].[tblQueries].SupplierSummaryId", null,
                    commandType: CommandType.Text));
            return result;
        }
    }
}
