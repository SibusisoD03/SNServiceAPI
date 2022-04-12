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
    public class SchoolDonationController
    {
        private readonly IDapper _dapper;
        public SchoolDonationController(Services.IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(Create))]
        public async Task<int> Create(ProductDonation data)
        {
           
            var dbparams = new DynamicParameters();
            dbparams.Add("@DonationId", data.DonationId, DbType.Int32, ParameterDirection.Output);
            dbparams.Add("@PrincipalId", data.PrincipalId, DbType.Int32);
            dbparams.Add("@DateCaptured", DateTime.Now);
            dbparams.Add("@Reason", data.Reason);
            dbparams.Add("@NoLearners", data.NoLearners, DbType.Int32);
            dbparams.Add("@CaseNo", data.CaseNo);

            string sql = $@"insert into dbo.tblProductDonation (PrincipalId, Reason, NoLearners, CaseNo)
                            values (@PrincipalId, @Reason, @NoLearners, @CaseNo)
                            select @DonationId = @@IDENTITY";
            int NoOfProductDonation = await Task.FromResult(_dapper.Execute(sql, dbparams));
            int newDonationID = dbparams.Get<int>("@DonationId");
            Console.WriteLine($"new donation id {newDonationID}");

            string po_sql = $@"insert into dbo.tblProductOrder (DonationId,TypeID, UnitID, FoodGroupID, Quantity)
                                   values (@DonationId,@TypeID, @UnitID, @FoodGroupID, @Quantity)
                                   select @ProductOrderId = @@IDENTITY";
            await Task.Delay(4000);
            foreach (ProductOrder product in data.products)
            {
                var po = new DynamicParameters();
                po.Add("@ProductOrderId", product.ProductOrderId, DbType.Int32, ParameterDirection.Output);
                po.Add("@DonationId", newDonationID, DbType.Int32);
                po.Add("@TypeID", product.TypeID, DbType.Int32);
                po.Add("@UnitID", product.UnitID, DbType.Int32);
                po.Add("@FoodGroupID", product.FoodGroupID, DbType.Int32);
                po.Add("@Quantity", product.Quantity, DbType.Int32);

                _dapper.Execute(po_sql, po);
                int newProductOrderId = po.Get<int>("@ProductOrderId");

                Console.WriteLine($"new product order id {newProductOrderId}");
            }

          

            return NoOfProductDonation;
         /*   var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddProductOrder]"
            , dbparams,
            commandType: CommandType.StoredProcedure));
            return result;*/
        }


        //Get Donation
        [HttpGet(nameof(GetDonationList))]
        public Task<List<ProductDonation>> GetDonationList()
        {
            var ComponentList = Task.FromResult(_dapper.GetAll<ProductDonation>($"select * from [dbo].[tblSchoolDonation]  ", null,
                    commandType: CommandType.Text));
            return ComponentList;
        }

    }


}


