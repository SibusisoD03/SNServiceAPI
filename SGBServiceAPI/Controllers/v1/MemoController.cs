using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SNServiceAPI.Models;
using System.Data;

namespace SNServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoController : ControllerBase
    {
        private readonly IDapper _dapper;
        public MemoController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(MemoModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DistrictId", data.DistrictId);
            dbparams.Add("@EmisNo", data.EmisNo);
            dbparams.Add("@SubmittedById", data.SubmittedById);
            dbparams.Add("@DateSubmitted", data.DateSubmitted);
            dbparams.Add("@DocumentPath", data.DocumentPath);
            dbparams.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertMemo]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@Id");
            return retVal;
        }
        [HttpGet(nameof(MemoList))]
        public Task<List<MemoModel>> MemoList()
        {
            var result = Task.FromResult(_dapper.GetAll<MemoModel>($"Select * from [tblMemos]", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetById))]
        public async Task<MemoModel> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<MemoModel>($"Select * from [tblMemos] where UserId = {Id}", null, commandType: CommandType.Text));
            return result;
        }


        [HttpPatch(nameof(Update))]
        public Task<int> Update(MemoModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@DistrictId", data.DistrictId);
            dbparams.Add("@EmisNo", data.EmisNo);
            dbparams.Add("@SubmittedById", data.SubmittedById);
            dbparams.Add("@DateSubmitted", data.DateSubmitted);
            dbparams.Add("@DocumentPath", data.DocumentPath);
            dbparams.Add("@Id", data.Id);

            var updateMeeting = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateMeeting]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updateMeeting;
        }
    }
}
