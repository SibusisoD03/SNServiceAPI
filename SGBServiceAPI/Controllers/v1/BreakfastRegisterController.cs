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
    public class BreakfastRegisterController : ControllerBase
    {

        private readonly IDapper _dapper;
        public BreakfastRegisterController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(AddBreakfastRegister))]
        public async Task<int> AddBreakfastRegister(BreakfastRegisterModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@breakfastId", data.BreakfastId, DbType.Int32);
            dbparams.Add("@breakfastdate", data.BreakfastDate);
            dbparams.Add("@breakfastday", data.BreakfastDay, DbType.Int32);
            dbparams.Add("@breakfaststarttime", data.BreakfastStarttime);
            dbparams.Add("@breakfastendtime", data.BreakfastEndtime);
            dbparams.Add("@cerealserved", data.CerealServed);
            dbparams.Add("@totnumlearnersfed", data.TotNumLearnersFed);
            dbparams.Add("@principalornsnpeducatorsignature", data.PrincipalORNsnpEducatorSignature);


            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_addbreakfastregister]"
                , dbparams,
                commandType: CommandType.StoredProcedure));

            var retVal = dbparams.Get<int>("@breakfastId");
            return retVal;
        }

        [HttpGet(nameof(GetAllBreakfastRegisters))]
        public Task<List<BreakfastRegisterModel>> GetAllBreakfastRegisters()
        {
            var result = Task.FromResult(_dapper.GetAll<BreakfastRegisterModel>($"SELECT * FROM [dbo].[tblBreakfastFeedRegister]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetBreakfastRegisterById))]
        public Task<BreakfastRegisterModel> GetBreakfastRegisterById(int Id)
        {
            var result = Task.FromResult(_dapper.Get<BreakfastRegisterModel>($"SELECT * FROM [dbo].[tblBreakfastFeedRegister] WHERE breakfastId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(UpdateBreakfastRegister))]
        public Task<int> UpdateBreakfastRegister(BreakfastRegisterModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@breakfastId ", data.BreakfastId, DbType.Int32);
            dbparams.Add("@breakfastdate", data.BreakfastDate);
            dbparams.Add("@breakfastday", data.BreakfastDay);
            dbparams.Add("@breakfaststarttime", data.BreakfastStarttime);
            dbparams.Add("@breakfastendtime", data.BreakfastEndtime);
            dbparams.Add("@cerealserved", data.CerealServed);
            dbparams.Add("@totnumlearnersfed", data.TotNumLearnersFed);
            dbparams.Add("@principalornsnpeducatorsignature", data.PrincipalORNsnpEducatorSignature);

            var update = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateBreakfastRegister]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return update;

        }

    }
}