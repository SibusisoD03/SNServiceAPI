using Dapper;
using Microsoft.AspNetCore.Http;
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
    public class FeedingCalendarController : ControllerBase
    {
        private readonly IDapper _dapper;
        public FeedingCalendarController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateFeedingCalendar))]
        public async Task<int> CreateFeedingCalendar(FeedingCalendarModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@FeedingId", data.FeedingId, DbType.Int32);
            dbparams.Add("@TaskName", data.TaskName);
            dbparams.Add("@Quarter", data.Quarter);
            dbparams.Add("@StartDate", data.StartDate, DbType.DateTime);
            dbparams.Add("@EndDate", data.EndDate, DbType.DateTime);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Sp_AddFeedingCalendar]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@FeedingId");
            return retVal;
        }

        [HttpGet(nameof(GetFeedingCalendar))]
        public Task<List<FeedingCalendarModel>> GetFeedingCalendar()
        {
            var result = Task.FromResult(_dapper.GetAll<FeedingCalendarModel>($"select * from [tblFeedingCalendar]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetFeedingCalendarById))]
        public Task<List<FeedingCalendarModel>> GetFeedingCalendarById(int Id)
        {
            var result = Task.FromResult(_dapper.GetAll<FeedingCalendarModel>($"select * from [tblFeedingCalendar] where FeedingId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(UpdateFeedingCalendar))]
        public Task<int> UpdateFeedingCalendar(FeedingCalendarModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@FeedingId", data.FeedingId, DbType.Int32);
            dbparams.Add("@TaskName", data.TaskName);
            dbparams.Add("@Quarter", data.Quarter);
            dbparams.Add("@StartDate", data.StartDate, DbType.DateTime);
            dbparams.Add("@EndDate", data.EndDate, DbType.DateTime);

            var updateAcknowledgement = Task.FromResult(_dapper.Update<int>("[dbo].[Sp_UpdateFeedingCalendar]",
                           dbparams,
                           commandType: CommandType.StoredProcedure));
            return updateAcknowledgement;

        }
        [HttpGet(nameof(GetQuarterNames))]
        public Task<List<FeedingCalendarModel>> GetQuarterNames()
        {
            var Output = Task.FromResult(_dapper.GetAll<FeedingCalendarModel>($"select distinct [Quarter] from [dbo].[tblFeedingCalendar]", null,
                    commandType: CommandType.Text));
            return Output;
        }
    }
}