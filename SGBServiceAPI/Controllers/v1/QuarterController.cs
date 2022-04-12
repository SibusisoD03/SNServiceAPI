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
    public class QuarterController : ControllerBase
    {
        private readonly IDapper _dapper;
        public QuarterController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpGet(nameof(GetQuarterList))]
        public Task<List<QuarterModel>> GetQuarterList()
        {
            var MonthList = Task.FromResult(_dapper.GetAll<QuarterModel>($"select * from [dbo].[tblFeedingQuarters]", null,
                    commandType: CommandType.Text));
            return MonthList;
        }
        [HttpGet(nameof(GetQuarterByID))]
        public Task<List<QuarterModel>> GetQuarterByID(int ID)
        {
            var Month = Task.FromResult(_dapper.GetAll<QuarterModel>($"select [Quarters] from [dbo].[tbFeedinglQuarters] where [Id] = {ID}", null,
                    commandType: CommandType.Text));
            return Month;
        }
    }
}


