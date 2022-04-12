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
    public class FacilitiesVerificationController : ControllerBase
    {
        private readonly IDapper _dapper;
        public FacilitiesVerificationController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateFacilitiesVerification))]
        public async Task<int> CreateFacilitiesVerification(FacilitiesVerificationModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@FacilityId", data.FacilityId, DbType.Int32);
            dbparams.Add("@Facilities", data.Facilities);
            dbparams.Add("@VerificationId", data.VerificationId, DbType.Int32);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddFacilitiesVerification]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@FacilityId");
            return retVal;
        }

        [HttpGet(nameof(GetFacilitiesVerification))]
        public Task<List<FacilitiesVerificationModel>> GetFacilitiesVerification()
        {
            var result = Task.FromResult(_dapper.GetAll<FacilitiesVerificationModel>($"select * from [tblFacilities]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetFacilitiesVerificationById))]
        public Task<List<FacilitiesVerificationModel>> GetFacilitiesVerificationById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<FacilitiesVerificationModel>($"select * from [tblFacilities] where FacilityId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(UpdateFacilitiesVerification))]
        public Task<int> UpdateFacilitiesVerification(FacilitiesVerificationModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@FacilityId", data.FacilityId, DbType.Int32);
            dbparams.Add("@Facilities", data.Facilities);
            dbparams.Add("@VerificationId", data.VerificationId, DbType.Int32);

            var updateFacitiesVerification = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateFacilitiesVerification]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateFacitiesVerification;

        }
    }
}
