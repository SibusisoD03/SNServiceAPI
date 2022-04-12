using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SNServiceAPI.Models;
using System.Data;
namespace SNServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnerController : ControllerBase
    {
        private readonly IDapper _dapper;
        public LearnerController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpGet(nameof(LearnerInfo))]
        public Task<List<LearnerModel>> LearnerInfo()
        {
            var result = Task.FromResult(_dapper.GetAll<LearnerModel>($"select * from [tblLearner_info]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetLeanerByAccessionNo))]
        public async Task<LearnerModel> GetLeanerByAccessionNo(string AccessionNo)
        {
            var result = await Task.FromResult(_dapper.Get<LearnerModel>($"Select * from [tblLearner_info] where AccessionNo = {AccessionNo}", null, commandType: CommandType.Text));
            return result;
        }

    }
}
