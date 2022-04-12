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
    public class GradeNSNPController : ControllerBase
    {
        private readonly IDapper _dapper;
        public GradeNSNPController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateGradeNSNP))]
        public async Task<int> CreateGradeNSNP(GradeNSNPModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@GradeId", data.GradeId, DbType.Int32);
            dbparams.Add("@Grade", data.Grade);
            dbparams.Add("@NoLearners", data.NoLearners, DbType.Int32);
            dbparams.Add("@NSNPId", data.NSNPId, DbType.Int32);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddGradeN]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@GradeId");
            return retVal;
        }

        [HttpGet(nameof(GetGradeNSNP))]
        public Task<List<GradeNSNPModel>> GetGradeNSNP()
        {
            var result = Task.FromResult(_dapper.GetAll<GradeNSNPModel>($"select * from [tblGradeN]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetGradeNSNPById))]
        public Task<List<GradeNSNPModel>> GetGradeNSNPById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<GradeNSNPModel>($"select * from [tblGradeN] where GradeId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(UpdateGradeNSNP))]
        public Task<int> UpdateGradeNSNP(GradeNSNPModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@GradeId", data.GradeId, DbType.Int32);
            dbparams.Add("@Grade", data.Grade);
            dbparams.Add("@NoLearners", data.NoLearners, DbType.Int32);
            dbparams.Add("@NSNPId", data.NSNPId, DbType.Int32);

            var updateGradeNSNP = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateGradeNSNP]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateGradeNSNP;

        }
    }
}
