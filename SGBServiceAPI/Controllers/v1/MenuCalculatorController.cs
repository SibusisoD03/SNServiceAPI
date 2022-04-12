using Dapper;
using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Models;
using SNServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuCalculatorController : ControllerBase
    {
        private readonly IDapper _dapper;

        public MenuCalculatorController(IDapper dapper)
        {
            _dapper = dapper;
        }

        //Get Weekdays
        [HttpGet(nameof(WeekDays))]
        public Task<List<WeekDaysModel>> WeekDays()
        {
            var result = Task.FromResult(_dapper.GetAll<WeekDaysModel>($"SELECT * from tblWeekDays", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(WeekDayById))]
        public async Task<WeekDaysModel> WeekDayById(int Id)
        {
            var result =  await Task.FromResult(_dapper.Get<WeekDaysModel>($"SELECT * from tblWeekDays where DayId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Menu
        [HttpGet(nameof(Menu))]
        public Task<List<MenuModel>> Menu()
        {
            var result = Task.FromResult(_dapper.GetAll<MenuModel>($"SELECT * from tblMenu", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(MenuById))]
        public async Task<MenuModel> MenuById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<MenuModel>($"SELECT * from tblMenu where MenuId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Foodgroup
        [HttpGet(nameof(FoodGroup))]
        public Task<List<FoodGroupMenuModel>> FoodGroup()
        {
            var result = Task.FromResult(_dapper.GetAll<FoodGroupMenuModel>($"SELECT * from tblFoodGroup", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(FoodGroupById))]
        public async Task<FoodGroupMenuModel> FoodGroupById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<FoodGroupMenuModel>($"SELECT * from tblFoodGroup where FoodGroupID={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Starch
        [HttpGet(nameof(Starch))]
        public Task<List<StarchModel>> Starch()
        {
            var result = Task.FromResult(_dapper.GetAll<StarchModel>($"SELECT * from tblStarch", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(StarchById))]
        public async Task<StarchModel> StarchById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<StarchModel>($"SELECT * from tblStarch where StarchId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Protein
        [HttpGet(nameof(Protein))]
        public Task<List<ProteinModel>> Protein()
        {
            var result = Task.FromResult(_dapper.GetAll<ProteinModel>($"SELECT * from tblProtein", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(ProteinById))]
        public async Task<ProteinModel> ProteinById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<ProteinModel>($"SELECT * from tblProtein where ProteinId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Breakfast
        [HttpGet(nameof(Breakfast))]
        public Task<List<BreakfastModel>> Breakfast()
        {
            var result = Task.FromResult(_dapper.GetAll<BreakfastModel>($"SELECT * from tblBreakfast", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(BreakfastById))]
        public async Task<BreakfastModel> BreakfastById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<BreakfastModel>($"SELECT * from tblBreakfast where BreakfastId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Seasoning
        [HttpGet(nameof(Seasoning))]
        public Task<List<SeasoningModel>> Seasoning()
        {
            var result = Task.FromResult(_dapper.GetAll<SeasoningModel>($"SELECT * from tblSeasoning", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(SeasoningById))]
        public async Task<SeasoningModel> SeasoningById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<SeasoningModel>($"SELECT * from tblSeasoning where SeasoningId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get Fruit or Vegi
        [HttpGet(nameof(FruitOrVegitable))]
        public Task<List<FruitOrVegitableModel>> FruitOrVegitable()
        {
            var result = Task.FromResult(_dapper.GetAll<FruitOrVegitableModel>($"SELECT * from tblFruitOrVegitable", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(FruitOrVegitableById))]
        public async Task<FruitOrVegitableModel> FruitOrVegitableById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<FruitOrVegitableModel>($"SELECT * from tblFruitOrVegitable where Id={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Post School menu
        [HttpPost(nameof(CreateSchoolMenu))]
        public async Task<int> CreateSchoolMenu(SchoolMenuModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", data.Id, DbType.Int32);
            dbparams.Add("@Day", data.Day);
            dbparams.Add("@Menu", data.Menu);
            dbparams.Add("@Product", data.Product);
            dbparams.Add("@SchoolType", data.SchoolType);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_AddSchoolMenu]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        //Update School menu
        [HttpPost(nameof(UpdateSchoolMenu))]
        public async Task<int> UpdateSchoolMenu(SchoolMenuModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", data.Id, DbType.Int32);
            dbparams.Add("@Day", data.Day);
            dbparams.Add("@Menu", data.Menu);
            dbparams.Add("@Product", data.Product);
            dbparams.Add("@SchoolType", data.SchoolType);

            var result = await Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateSchoolMenu]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        //Get School Menu
        [HttpGet(nameof(GetSchoolMenu))]
        public Task<List<SchoolMenuModel>> GetSchoolMenu()
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolMenuModel>($"SELECT * from tblSchoolMenu", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetSchoolMenuById))]
        public async Task<SchoolMenuModel> GetSchoolMenuById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<SchoolMenuModel>($"SELECT * from tblSchoolMenu where Id={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get School Menu
        [HttpGet(nameof(GetSchoolType))]
        public Task<List<SchoolTypeModel>> GetSchoolType()
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolTypeModel>($"SELECT * from tblSchoolType", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetSchoolTypeById))]
        public async Task<SchoolTypeModel> GetSchoolTypeById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<SchoolTypeModel>($"SELECT * from tblSchoolType where Id={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Get School level by school
        [HttpGet(nameof(GetSchoolLevelBySchool))]
        public Task<List<SchoolRegionModel>> GetSchoolLevelBySchool(string Institute)
        {
            var result = Task.FromResult(_dapper.GetAll<SchoolRegionModel>($"SELECT Level from tblSchoolRegion where InstitutionName like '{Institute}'", null,
                    commandType: CommandType.Text));
            return result;
        }
    }
}
