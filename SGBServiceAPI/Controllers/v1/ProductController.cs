using Dapper;
using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Models;
using SNServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SNServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDapper _dapper;
        public ProductController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(CreateProduct))]
        public async Task<int> CreateProduct(ProductModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ProductId", data.ProductID, DbType.Int32, ParameterDirection.Output);
            dbparams.Add("@TypeId", data.TypeID);
            dbparams.Add("@FoodGroupId", data.FoodGroupID, DbType.Int32);
            dbparams.Add("@UnitId", data.UnitID);
            dbparams.Add("@Grammage", data.Grammage);
            dbparams.Add("@DateCreated", DateTime.Now);
            dbparams.Add("@GrammageSecondary", data.GrammageSecondary);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddProduct]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@ProductId");
            return retVal;
        }

        [HttpPost(nameof(UpdateProduct))]
        public async Task<int> UpdateProduct(ProductModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@ProductId", data.ProductID, DbType.Int32);
            dbparams.Add("@TypeId", data.TypeID);
            dbparams.Add("@FoodGroupId", data.FoodGroupID, DbType.Int32);
            dbparams.Add("@UnitId", data.UnitID);
            dbparams.Add("@Grammage", data.Grammage);
            dbparams.Add("@GrammageSecondary", data.GrammageSecondary);

            var result = await Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateProduct]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
          
            return result;
        }


        [HttpGet(nameof(GetProduct))]
        public Task<List<ProductModel>> GetProduct()
        {
            var result = Task.FromResult(_dapper.GetAll<ProductModel>($"SELECT [ProductID],[dbo].[tblProduct].[TypeID],[TypeDescription],[dbo].[tblProduct].[FoodGroupID],[FoodGroupDescription],[dbo].[tblProduct].[UnitID],[dbo].[tblUnit].UnitDescription,[Grammage],[GrammageSecondary] FROM [dbo].[tblProduct] INNER JOIN [dbo].[tblType] ON [dbo].[tblType].[TypeID]=[dbo].[tblProduct].[TypeID] INNER JOIN [dbo].[tblFoodGroup] ON [dbo].[tblFoodGroup].[FoodGroupID] = [dbo].[tblProduct].[FoodGroupID] INNER JOIN [dbo].[tblUnit] ON [dbo].[tblUnit].UnitID = [dbo].tblProduct.UnitID", null,
                    commandType: CommandType.Text));
            return result;
        }

    }
}
