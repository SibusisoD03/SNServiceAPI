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
    public class GradeVerificationController : ControllerBase
    {
        private readonly IDapper _dapper;
        public GradeVerificationController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateGradeVerification))]
        public async Task<int> CreateGradeVerification(GradeVerificationModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@GradeId", data.GradeId, DbType.Int32);
            dbparams.Add("@Grade", data.Grade);
            dbparams.Add("@NoLearners", data.NoLearners, DbType.Int32);
            dbparams.Add("@VerificationId", data.VerificationId, DbType.Int32);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddGradeVerification]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@GradeId");
            return retVal;
        }

        [HttpGet(nameof(GetGradeVerification))]
        public Task<List<GradeVerificationModel>> GetGradeVerification()
        {
            var result = Task.FromResult(_dapper.GetAll<GradeVerificationModel>($"select * from [tblGradeN]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetGradeVerificationById))]
        public Task<List<GradeVerificationModel>> GetGradeVerificationById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<GradeVerificationModel>($"select * from [tblGradeN] where GradeId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(UpdateGradeVerification))]
        public Task<int> UpdateGradeVerification(GradeVerificationModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@GradeId", data.GradeId, DbType.Int32);
            dbparams.Add("@Grade", data.Grade);
            dbparams.Add("@NoLearners", data.NoLearners, DbType.Int32);
            dbparams.Add("@VerificationId", data.VerificationId, DbType.Int32);

            var updateGradeVerification = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateGradeVerification]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateGradeVerification;

        }
    }
}
