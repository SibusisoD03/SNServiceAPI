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
    public class FeedingMonthController : ControllerBase
    {
        private readonly IDapper _dapper;
        public FeedingMonthController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpGet(nameof(GetMonthList))]
        public Task<List<FeedingMonthModel>> GetMonthList()
        {
            var MonthList = Task.FromResult(_dapper.GetAll<FeedingMonthModel>($"select * from [dbo].[tblMonth]", null,
                    commandType: CommandType.Text));
            return MonthList;
        }
        [HttpGet(nameof(GetMonthByID))]
        public Task<List<FeedingMonthModel>> GetMonthByID(int ID)
        {
            var Month = Task.FromResult(_dapper.GetAll<FeedingMonthModel>($"select [Month] from [dbo].[tblMonth] where [Id] = {ID}", null,
                    commandType: CommandType.Text));
            return Month;
        }
    }
}
