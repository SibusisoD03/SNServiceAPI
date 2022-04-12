using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SNServiceAPI.Models;
using System.Data;
using SNServiceAPI.Services;

namespace SNServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedingCalenderTaskNameController : ControllerBase
    {
        private readonly IDapper _dapper;
        public FeedingCalenderTaskNameController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateActivityName))]
        public async Task<int> CreateActivityName(FeedingCalenderTaskNameModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@Id", data.Id, DbType.Int32);
            dataBaseParams.Add("@TaskName", data.TaskName);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddFeedingCalenderTaskName]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var TaskName = dataBaseParams.Get<int>("Id");
            return TaskName;
        }
        [HttpGet(nameof(GetTaskNameList))]
        public Task<List<FeedingCalenderTaskNameModel>> GetTaskNameList()
        {
            var TaskNameList = Task.FromResult(_dapper.GetAll<FeedingCalenderTaskNameModel>($"select * from [dbo].[tblFeedingCalenderTaskName]", null,
                    commandType: CommandType.Text));
            return TaskNameList;
        }
        [HttpGet(nameof(GetTaskNameByID))]
        public Task<List<FeedingCalenderTaskNameModel>> GetTaskNameByID(int ID)
        {
            var TaskName = Task.FromResult(_dapper.GetAll<FeedingCalenderTaskNameModel>($"select [TaskName] from [dbo].[tblFeedingCalenderTaskName] where [Id] = {ID}", null,
                    commandType: CommandType.Text));
            return TaskName;
        }

    }
}
