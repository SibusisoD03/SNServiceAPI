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
    public class VerificationFormController : ControllerBase
    {
        private readonly IDapper _dapper;
        public VerificationFormController(Services.IDapper dapper)
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

        //Add to verification grade
        [HttpPost(nameof(CreateVerificationGrade))]
        public async Task<int> CreateVerificationGrade(VerificationGradeModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@VerificationGradeID", data.VerificationGradeID, DbType.Int32);
            dbparams.Add("@Grade", data.Grade);
            dbparams.Add("@NoOfLearners", data.NoOfLearners, DbType.Int32);
            dbparams.Add("@OtherGrade", data.OtherGrade);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SpAddVerificationGrade]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        //Get verification grade
        [HttpGet(nameof(GetVerificationGrade))]
        public Task<List<VerificationGradeModel>> GetVerificationGrade()
        {
            var result = Task.FromResult(_dapper.GetAll<VerificationGradeModel>($"select * from [tblVerificationGrade]", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get verification grade by id
        [HttpGet(nameof(GetVerificationGradeByID))]
        public Task<List<VerificationGradeModel>> GetVerificationGradeByID(int id)
        {
            var result = Task.FromResult(_dapper.GetAll<VerificationGradeModel>($"select * from [tblVerificationGrade] where VerificationGradeID={id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Update verification grade       
        [HttpPost(nameof(UpdateVerificationGradeByID))]
        public async Task<int> UpdateVerificationGradeByID(int VerificationGradeID, string Grade, int NoOfLearners, string OtherGrade)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Grade", Grade);
            dbparams.Add("NoOfLearners", NoOfLearners);
            dbparams.Add("OtherGrade", OtherGrade);
            dbparams.Add("VerificationGradeID", VerificationGradeID);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SpUpdateVerificationGrade]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        //Verification functionality
        [HttpPost(nameof(Create))]
        public async Task<int> Create(VerificationFormModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("VerificationId", data.VerificationId, DbType.Int32);
            dbparams.Add("Quintile", data.Quintile);
            dbparams.Add("ChildrenLive", data.ChildrenLive);            
            dbparams.Add("Schoolroll", data.Schoolroll, DbType.Int32);
            dbparams.Add("Facility", data.Facility);
            dbparams.Add("EmisNumber", data.EmisNumber);
            dbparams.Add("DistrictName", data.DistrictName);
            dbparams.Add("SchoolName", data.SchoolName);
            dbparams.Add("LearnersInfo", data.LearnersInfo);
            dbparams.Add("DateReceived", DateTime.Now);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddVerificationForm]"
            , dbparams,
            commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("VerificationId");
            return retVal;
        }

        [HttpGet(nameof(GetVerificationList))]
        public Task<List<VerificationFormModel>> GetVerificationList()
        {
            var result = Task.FromResult(_dapper.GetAll<VerificationFormModel>($"select * from [tblVerification]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetLastVerification))]
        public Task<List<VerificationFormModel>> GetLastVerification()
        {
            var varOutput = Task.FromResult(_dapper.GetAll<VerificationFormModel>($"SELECT[VerificationId] FROM[dbo].[tblVerification] WHERE[VerificationId] = (SELECT IDENT_CURRENT('[tblVerification]'))", null,
            commandType: CommandType.Text));
            return varOutput;
        }

    }
}
