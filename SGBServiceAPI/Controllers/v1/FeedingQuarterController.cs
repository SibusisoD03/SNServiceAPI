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
    public class FeedingQuarterController : ControllerBase
    {
        private readonly IDapper _dapper;
        public FeedingQuarterController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpGet(nameof(GetQuarterList))]
        public Task<List<FeedingQuarterModel>> GetQuarterList()
        {
            var QuarterList = Task.FromResult(_dapper.GetAll<FeedingQuarterModel>($"select * from [dbo].[tblQuarter]", null,
                    commandType: CommandType.Text));
            return QuarterList;
        }
        [HttpGet(nameof(GetMonthByID))]
        public Task<List<FeedingQuarterModel>> GetMonthByID(int ID)
        {
            var Quarter = Task.FromResult(_dapper.GetAll<FeedingQuarterModel>($"select [Quarter] from [dbo].[tblQuarter] where [QuarterId] = {ID}", null,
                    commandType: CommandType.Text));
            return Quarter;
        }
        //[HttpGet(nameof(GetMonthByQuarter))]
        //public Task<List<FeedingMonthModel>> GetMonthByQuarter(int quarterNo)
        //{
        //    /*string query = $"DECLARE @VarQuarter INTEGER, @InvalidInput NVARCHAR(255)"
        //                    + "SET @VarQuarter = {quarterNo}"
        //                    + "SET @InvalidInput = 'Invalid quater has been detected!...Please enter a valid quater number.'"
        //                    + "IF @VarQuarter = (SELECT[QuarterId] from[dbo].[tblQuarter] WHERE[QuarterId] = '1')"
        //                    + "SELECT[Month]"
        //                    + "FROM[dbo].[tblMonth] WHERE[Id] IN('1', '2', '3')"
        //                    + "ELSE"
        //                    + "IF @VarQuarter = (SELECT[QuarterId] from[dbo].[tblQuarter] WHERE[QuarterId] = '2')"
		      //              + "SELECT[Month]"
        //                    + "FROM[dbo].[tblMonth] WHERE[Id] IN('4', '5', '6')"
        //                    + "ELSE"
        //                    + "IF @VarQuarter = (SELECT[QuarterId] from[dbo].[tblQuarter] WHERE[QuarterId] = '3')"
			     //           + "SELECT[Month]"
        //                    + "FROM[dbo].[tblMonth] WHERE[Id] IN('7', '8', '9')"
        //                    + "ELSE"
        //                    + "IF @VarQuarter = (SELECT[QuarterId] from[dbo].[tblQuarter] WHERE[QuarterId] = '4')"
				    //        + "SELECT[Month]"
        //                    + "FROM[dbo].[tblMonth] WHERE[Id] IN('10', '11', '12')"
        //                    + "ELSE"
        //                    + "SELECT DISTINCT(@InvalidInput)"
        //                    + "FROM[dbo].[tblQuarter]";*/

        //    //var MonthByQuarter = Task.FromResult(_dapper.GetAll<MonthModel>(query, null,
        //    //        commandType: CommandType.Text));
        //    //return MonthByQuarter;


        //    var MonthByQuarter = Task.FromResult(_dapper.GetAll<FeedingMonthModel>($"DECLARE @VarQuarter INTEGER, @InvalidInput NVARCHAR(255) SET @VarQuarter = {quarterNo} SET @InvalidInput = 'Invalid quater has been detected!...Please enter a valid quater number.' IF @VarQuarter = (SELECT [QuarterId] from [dbo].[tblQuarter] WHERE [QuarterId] = '1') SELECT [Month] FROM [dbo].[tblMonth] WHERE [Id] IN('1','2','3') ELSE IF @VarQuarter = (SELECT [QuarterId] from [dbo].[tblQuarter] WHERE [QuarterId] = '2') SELECT [Month] FROM [dbo].[tblMonth] WHERE [Id] IN('4','5','6') ELSE IF @VarQuarter = (SELECT [QuarterId] from [dbo].[tblQuarter] WHERE [QuarterId] = '3') SELECT [Month] FROM [dbo].[tblMonth] WHERE [Id] IN('7','8','9') ELSE IF @VarQuarter = (SELECT [QuarterId] from [dbo].[tblQuarter] WHERE [QuarterId] = '4') SELECT [Month] FROM [dbo].[tblMonth] WHERE [Id] IN('10','11','12') ELSE SELECT DISTINCT(@InvalidInput) FROM [dbo].[tblQuarter]", null,
        //            commandType: CommandType.Text));
        //    return MonthByQuarter;
        //}

        [HttpGet(nameof(GetMonthByQuarter))]
        public Task<List<FeedingMonthModel>> GetMonthByQuarter(string quarter)
        {         
            var MonthByQuarter = Task.FromResult(_dapper.GetAll<FeedingMonthModel>($"DECLARE @VarQuarter NVARCHAR(50), @InvalidInput NVARCHAR(255) SET @VarQuarter = '{quarter}' SET @InvalidInput = 'Invalid quater has been detected!...Please enter a valid quater number.' IF @VarQuarter = (SELECT [Quarter] from [dbo].[tblQuarter] WHERE [Quarter] = 'Quarter 1') SELECT [Month] FROM [dbo].[tblMonth] WHERE [Id] IN('1','2','3') ELSE IF @VarQuarter = (SELECT [Quarter] from [dbo].[tblQuarter] WHERE [Quarter] = 'Quarter 2') SELECT [Month] FROM [dbo].[tblMonth] WHERE [Id] IN('4','5','6') ELSE IF @VarQuarter = (SELECT [Quarter] from [dbo].[tblQuarter] WHERE [Quarter] = 'Quarter 3') SELECT [Month] FROM [dbo].[tblMonth] WHERE [Id] IN('7','8','9') ELSE IF @VarQuarter = (SELECT [Quarter] from [dbo].[tblQuarter] WHERE [Quarter] = 'Quarter 4') SELECT [Month] FROM [dbo].[tblMonth] WHERE [Id] IN('10','11','12') ELSE SELECT DISTINCT(@InvalidInput) FROM [dbo].[tblQuarter]", null,
                    commandType: CommandType.Text));
            return MonthByQuarter;
        }
    }
}
