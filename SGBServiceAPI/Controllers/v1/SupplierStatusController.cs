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
    public class SupplierStatusController : ControllerBase
    {
        private readonly IDapper _dapper;
        public SupplierStatusController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(CreateStatus))]
        public async Task<int> CreateStatus(SupplierStatusModel data)
        {
            var dataBaseParams = new DynamicParameters();
            dataBaseParams.Add("@Id", data.Id, DbType.Int32);
            dataBaseParams.Add("@Status", data.Status);

            var Output = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddSupplierStatus]"
                , dataBaseParams,
                commandType: CommandType.StoredProcedure));
            var Status = dataBaseParams.Get<int>("Id");
            return Status;
        }
        [HttpGet(nameof(GetStatusList))]
        public Task<List<SupplierStatusModel>> GetStatusList()
        {
            var StatusList = Task.FromResult(_dapper.GetAll<SupplierStatusModel>($"select * from [dbo].[tblSupplierStatus]", null,
                    commandType: CommandType.Text));
            return StatusList;
        }
        [HttpGet(nameof(GetStatusByID))]
        public Task<List<SupplierStatusModel>> GetStatusByID(int ID)
        {
            var Status = Task.FromResult(_dapper.GetAll<SupplierStatusModel>($"select [Status] from [dbo].[tblSupplierStatus] where [Id] = {ID}", null,
                    commandType: CommandType.Text));
            return Status;
        }
    }
}
