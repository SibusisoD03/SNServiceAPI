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
    public class ProvinceController : ControllerBase
    {
        private readonly IDapper _dapper;
        public ProvinceController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpGet(nameof(GetProvinceList))]
        public Task<List<ProvinceModel>> GetProvinceList()
        {
            var ProvinceList = Task.FromResult(_dapper.GetAll<ProvinceModel>($"select * from [dbo].[tblProvince]", null,
                    commandType: CommandType.Text));
            return ProvinceList;
        }
        [HttpGet(nameof(GetProvinceByID))]
        public Task<List<ProvinceModel>> GetProvinceByID(int ID)
        {
            var Province = Task.FromResult(_dapper.GetAll<ProvinceModel>($"select [Province] from [dbo].[tblProvince] where [Id] = {ID}", null,
                    commandType: CommandType.Text));
            return Province;
        }
    }
}
