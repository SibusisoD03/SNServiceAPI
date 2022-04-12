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
    public class LunchRegisterController : ControllerBase
    {
        private readonly IDapper _dapper;
        public LunchRegisterController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(AddLunchRegister))]
        public async Task<int> AddLunchRegister(LunchRegisterModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@lunchId", data.LunchId, DbType.Int32);
            dbparams.Add("@lunchdate", data.LunchDate);
            dbparams.Add("@lunchday", data.LunchDay, DbType.Int32);
            dbparams.Add("@lunchstarttime", data.LunchStarttime);
            dbparams.Add("@lunchendtime", data.LunchEndtime);
            dbparams.Add("@cerealserved", data.CerealServed);
            dbparams.Add("@totnumlearnersfed", data.TotnNumLearnersFed);
            dbparams.Add("@principalornsnpeducatorsignature", data.PrincipalORNsnpEducatorSignature);


            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddLunchFeedingRegister]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@lunchId");
            return retVal;
        }

        [HttpGet(nameof(GetAllLunchRegister))]
        public Task<List<LunchRegisterModel>> GetAllLunchRegister()
        {
            var result = Task.FromResult(_dapper.GetAll<LunchRegisterModel>($"SELECT * FROM [dbo].[tblLunchFeedingRegister]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetLunchRegisterById))]
        public Task<LunchRegisterModel> GetLunchRegisterById(int Id)
        {
            var result = Task.FromResult(_dapper.Get<LunchRegisterModel>($"SELECT * FROM [dbo].[tblLunchFeedingRegister] WHERE lunchId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(UpdateLunchRegister))]
        public Task<int> UpdateLunchRegister(LunchRegisterModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@lunchId ", data.LunchId, DbType.Int32);
            dbparams.Add("@lunchdate", data.LunchDate);
            dbparams.Add("@lunchday", data.LunchDay);
            dbparams.Add("@lunchstarttime", data.LunchStarttime);
            dbparams.Add("@lunchendtime", data.LunchEndtime);
            dbparams.Add("@cerealserved", data.CerealServed);
            dbparams.Add("@totnumlearnersfed", data.TotnNumLearnersFed);
            dbparams.Add("@principalornsnpeducatorsignature", data.PrincipalORNsnpEducatorSignature);

            var update = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateLunchRegister]",
                           dbparams,

                           commandType: CommandType.StoredProcedure));
            return update;

        }

    }
}
