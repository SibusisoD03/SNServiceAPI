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
    public class NSNPController : ControllerBase
    {
        private readonly IDapper _dapper;
        public NSNPController(IDapper dapper)
        {
            _dapper = dapper;
        }

        //Get grade
        [HttpGet(nameof(GetGrade))]
        public Task<List<GradeModel>> GetGrade()
        {
            var result = Task.FromResult(_dapper.GetAll<GradeModel>($"select * from [tblGrade]", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Add to nsnp grade
        [HttpPost(nameof(CreateNSNPGrade))]
        public async Task<int> CreateNSNPGrade(NSNPGradeModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@NSNPGradeID", data.NSNPGradeID, DbType.Int32);
            dbparams.Add("@Grade", data.Grade);
            dbparams.Add("@NoOfLearners", data.NoOfLearners, DbType.Int32);            
            dbparams.Add("@OtherGrade", data.OtherGrade);       
           
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SpAddNSNPGrade]"
                , dbparams,
                commandType: CommandType.StoredProcedure));           
            return result;
        }

        //Add to nsnp grade
        [HttpPost(nameof(CreateSendNSNP))]
        public async Task<int> CreateSendNSNP(SendNSNPModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@SendNSNPId", data.SendNSNPId, DbType.Int32);
            dbparams.Add("@EmisNumber", data.EmisNumber);
            dbparams.Add("@DistrictName", data.DistrictName);
            dbparams.Add("@SchoolName", data.SchoolName);
            dbparams.Add("@SchoolEmail", data.SchoolEmail);
            dbparams.Add("@DateReceived", DateTime.Now);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddSendNSNP]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        //Get nsnp grade
        [HttpGet(nameof(GetSendNSNP))]
        public Task<List<SendNSNPModel>> GetSendNSNP()
        {
            var result = Task.FromResult(_dapper.GetAll<SendNSNPModel>($"select * from [tblSendNSNP]", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get nsnp grade
        [HttpGet(nameof(GetNSNPGrade))]
        public Task<List<NSNPGradeModel>> GetNSNPGrade()
        {
            var result = Task.FromResult(_dapper.GetAll<NSNPGradeModel>($"select * from [tblNSNPGrade]", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get nsnp grade by id
        [HttpGet(nameof(GetNSNPGradeByID))]
        public Task<List<NSNPGradeModel>> GetNSNPGradeByID(int id)
        {
            var result = Task.FromResult(_dapper.GetAll<NSNPGradeModel>($"select * from [tblNSNPGrade] where NSNPGradeID={id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Update nsnp grade       
        [HttpPost(nameof(UpdateNSNPGradeByID))]
        public async Task<int> UpdateNSNPGradeByID(int NSNPGradeID, string Grade, int NoOfLearners, string OtherGrade)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Grade", Grade);
            dbparams.Add("NoOfLearners", NoOfLearners);
            dbparams.Add("OtherGrade", OtherGrade);
            dbparams.Add("NSNPGradeID", NSNPGradeID);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SpUpdateNSNPGrade]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }


        //NSNP Form Functions
        [HttpPost(nameof(CreateNSNP))]
        public async Task<int> CreateNSNP(NSNPModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("NSNPId", data.NSNPId, DbType.Int32);
            dbparams.Add("Quintile", data.Quintile);
            dbparams.Add("ChildrenLive", data.ChildrenLive);
            dbparams.Add("Schoolroll", data.Schoolroll, DbType.Int32);
            dbparams.Add("Facility", data.Facility);
            dbparams.Add("EmisNumber", data.EmisNumber);
            dbparams.Add("DistrictName", data.DistrictName);
            dbparams.Add("SchoolName", data.SchoolName);
            dbparams.Add("LearnersInfo", data.LearnersInfo);
            dbparams.Add("DateReceived", DateTime.Now);
            dbparams.Add("Status", "Awaiting Approval");

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddNSNP]"
            , dbparams,
            commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("NSNPId");
            return retVal;
        }

        [HttpGet(nameof(GetNSNP))]
        public Task<List<NSNPModel>> GetNSNP()
        {
            var result = Task.FromResult(_dapper.GetAll<NSNPModel>($"select * from [tblNSNP]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetNSNPStatus))]
        public Task<List<NSNPStatusModel>> GetNSNPStatus()
        {
            var result = Task.FromResult(_dapper.GetAll<NSNPStatusModel>($"select * from [tblNSNPStatus]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetNSNPById))]
        public Task<List<NSNPModel>> GetNSNPById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<NSNPModel>($"select * from [tblNSNP] where NSNPId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetLastNSNP))]
        public Task<List<NSNPModel>> GetLastNSNP()
        {
            var Output = Task.FromResult(_dapper.GetAll<NSNPModel>($"SELECT[NSNPId] FROM[dbo].[tblNSNP] WHERE[NSNPId] = (SELECT IDENT_CURRENT('[tblNSNP]'))", null,
            commandType: CommandType.Text));
            return Output;
        }
        [HttpPatch(nameof(UpdateNSNP))]
        public Task<int> UpdateNSNP(NSNPModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("NSNPId", data.NSNPId, DbType.Int32);
            dbparams.Add("Quintile", data.Quintile);
            dbparams.Add("ChildrenLive", data.ChildrenLive);
            dbparams.Add("Schoolroll", data.Schoolroll, DbType.Int32);
            dbparams.Add("Facility", data.Facility);
            dbparams.Add("EmisNumber", data.EmisNumber);
            dbparams.Add("DistrictName", data.DistrictName);
            dbparams.Add("SchoolName", data.SchoolName);
            dbparams.Add("LearnersInfo", data.LearnersInfo);
            dbparams.Add("DateReceived", DateTime.Now);
            dbparams.Add("Status", data.Status);

            var updateNSNP = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateNSNP]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateNSNP;

        }
    }
}
