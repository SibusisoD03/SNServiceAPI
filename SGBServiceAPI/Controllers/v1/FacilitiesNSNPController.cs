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
    public class FacilitiesNSNPController : ControllerBase
    {
        private readonly IDapper _dapper;
        public FacilitiesNSNPController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateFacilitiesNSNP))]
        public async Task<int> CreateFacilitiesNSNP(FacilitiesNSNPModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@FacilityId", data.FacilityId, DbType.Int32);
            dbparams.Add("@Facilities", data.Facilities);
            dbparams.Add("@NSNPId", data.NSNPId, DbType.Int32);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddFacilitiesNSNP]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@FacilityId");
            return retVal;
        }

        [HttpGet(nameof(GetFacilitiesNSNP))]
        public Task<List<FacilitiesNSNPModel>> GetFacilitiesNSNP()
        {
            var result = Task.FromResult(_dapper.GetAll<FacilitiesNSNPModel>($"select * from [tblFacilities]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetFacilitiesNSNPById))]
        public Task<List<FacilitiesNSNPModel>> GetFacilitiesNSNPById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<FacilitiesNSNPModel>($"select * from [tblFacilities] where FacilityId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(UpdateFacilitiesNSNP))]
        public Task<int> UpdateFacilitiesNSNP(FacilitiesNSNPModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@FacilityId", data.FacilityId, DbType.Int32);
            dbparams.Add("@Facilities", data.Facilities);
            dbparams.Add("@NSNPId", data.NSNPId, DbType.Int32);

            var updateFacitiesNSNP = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateFacilitiesNSNP]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateFacitiesNSNP;

        }
    }
}
