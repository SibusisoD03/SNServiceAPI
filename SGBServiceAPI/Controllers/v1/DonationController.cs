//using Dapper;
//using Microsoft.AspNetCore.Mvc;
//using SNServiceAPI.Models;
//using SNServiceAPI.Services;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SNServiceAPI.Controllers.v1
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DonationController : ControllerBase
//    {
//        private readonly IDapper _dapper;
//        public DonationController(Services.IDapper dapper)
//        {
//            _dapper = dapper;
//        }

//        //Add Donation
//        [HttpPost(nameof(Create))]
//        public async Task<int> Create(SchoolDonation data)
//        {
//            var dbparams = new DynamicParameters();
//            dbparams.Add("@DonationID", data.DonationId, DbType.Int32);
//            dbparams.Add("@PrincipalId", data.PrincipalId, DbType.Int32);
//            dbparams.Add("@DateCaptured", DateTime.Now);
//            dbparams.Add("@FoodGroup", data.FoodGroup);
//            dbparams.Add("@Product", data.Product);
//            dbparams.Add("@Quantity", data.Quantity, DbType.Int32);
//            dbparams.Add("@Unit", data.Unit);            
//            dbparams.Add("@Reason", data.Reason);
//            dbparams.Add("@NoLearners", data.NoLearners, DbType.Int32);
//            dbparams.Add("@CaseNo", data.CaseNo);
//            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddSchoolDonation]"
//            , dbparams,
//            commandType: CommandType.StoredProcedure));           
//            return result;
//        }

//        //Get Donation
//        [HttpGet(nameof(GetDonationList))]
//        public Task<List<SchoolDonation>> GetDonationList()
//        {
//            var ComponentList = Task.FromResult(_dapper.GetAll<SchoolDonation>($"select * from [dbo].[tblSchoolDonation]  ", null,
//                    commandType: CommandType.Text));
//            return ComponentList;
//        }


//        //[HttpDelete(nameof(Delete))]
//        //public async Task<int> Delete(int Id)
//        //{
//        //    var result = await Task.FromResult(_dapper.Execute($"Delete [dbo].[tblDonation] Where DonationID = {Id}", null, commandType: CommandType.Text));
//        //    return result;
//        //}
//        //[HttpGet(nameof(GetDonationByID))]
//        //public Task<List<DonationModel>> GetDonationByID(int ID)
//        //{
//        //    var ComponentList = Task.FromResult(_dapper.GetAll<DonationModel>($"select * from [dbo].[tblDonation] where DonationID = {ID}", null,
//        //            commandType: CommandType.Text));
//        //    return ComponentList;
//        //}
//        //[HttpGet(nameof(GetDonation))]
//        //public Task<List<DonationModel>> GetDonation()
//        //{
//        //    var ComponentList = Task.FromResult(_dapper.GetAll<DonationModel>($"select * from [dbo].[tblDonation]  ", null,
//        //            commandType: CommandType.Text));
//        //    return ComponentList;
//        //}
//    }
//}
