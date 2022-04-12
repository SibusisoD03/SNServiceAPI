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
    public class AcknowledgementController : ControllerBase
    {
        private readonly IDapper _dapper;
        public AcknowledgementController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateAcknowledgement))]
        public async Task<int> CreateAcknowledgement(AcknowledgementModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@AcknowledgementId", data.AcknowledgementId, DbType.Int32);
            dbparams.Add("@Name", data.Name);
            dbparams.Add("@Surname", data.Surname);
            dbparams.Add("@IdNumber", data.IdNumber);
            dbparams.Add("@Date", data.Date);
            dbparams.Add("@Month", data.Month);
            dbparams.Add("@Status", data.Status);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddAcknowledgement]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@AcknowledgementId");
            return retVal;
        }

        [HttpGet(nameof(GetAcknowledgement))]
        public Task<List<AcknowledgementModel>> GetAcknowledgement()
        {
            var result = Task.FromResult(_dapper.GetAll<AcknowledgementModel>($"select * from [tblAcknowledgement]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetAcknowledgementById))]
        public Task<List<AcknowledgementModel>> GetAcknowledgementById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<AcknowledgementModel>($"select * from [tblAcknowledgement] where AcknowledgementId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(UpdateAcknowledgement))]
        public Task<int> UpdateAcknowledgement(AcknowledgementModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@AcknowledgementId", data.AcknowledgementId, DbType.Int32);
            dbparams.Add("@Name", data.Name);
            dbparams.Add("@Surname", data.Surname);
            dbparams.Add("@IdNumber", data.IdNumber);
            dbparams.Add("@Date", data.Date);
            dbparams.Add("@Month", data.Month);
            dbparams.Add("@Status", data.Status);

            var updateAcknowledgement = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateAcknowledgement]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateAcknowledgement;

        }
    }
}
