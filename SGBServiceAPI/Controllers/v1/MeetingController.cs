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
    public class MeetingController : ControllerBase
    {
        private readonly IDapper _dapper;
        public MeetingController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(MeetingModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Title", data.Title);
            dbparams.Add("@StartDate", data.StartDate);
            dbparams.Add("@StartTime", data.StartTime);
            dbparams.Add("@EndDate", data.EndDate);
            dbparams.Add("@EndTime", data.EndTime);
            dbparams.Add("@Venue", data.Venue);
            dbparams.Add("@Description", data.Description);
            dbparams.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertMeeting]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@Id");
            return retVal;
        }
        [HttpGet(nameof(MeetingList))]
        public Task<List<MeetingModel>> MeetingList()
        {
            var result = Task.FromResult(_dapper.GetAll<MeetingModel>($"Select * from [tblMeetings]", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetById))]
        public async Task<MeetingModel> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<MeetingModel>($"Select * from [tblMeetings] where UserId = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var result = await Task.FromResult(_dapper.Execute($"Delete [tblMeetings] Where UserId = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(Count))]
        public Task<int> Count(string title)
        {
            var totalcount = Task.FromResult(_dapper.Get<int>($"Select COUNT(*) from [tblMeetings] WHERE Title like '%{title}%'", null,
                    commandType: CommandType.Text));
            return totalcount;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(MeetingModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Title", data.Title);
            dbparams.Add("@StartDate", data.StartDate);
            dbparams.Add("@StartTime", data.StartTime);
            dbparams.Add("@EndDate", data.EndDate);
            dbparams.Add("@EndTime", data.EndTime);
            dbparams.Add("@Venue", data.Venue);
            dbparams.Add("@Description", data.Description);
            dbparams.Add("@Minutes", data.Minutes);
            dbparams.Add("@Id", data.Id);

            var updateMeeting = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateMeeting]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updateMeeting;
        }
    }
}
