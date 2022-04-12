using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using SNServiceAPI.Models;
using System.Data;
namespace SNServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Submission : ControllerBase
    {
        private readonly IDapper _dapper;
        public Submission(IDapper dapper)
        {
            _dapper = dapper;
        }

        [HttpGet(nameof(GetSubmissions))]
        public Task<List<SubmissionModel>> GetSubmissions()
        {
            var result = Task.FromResult(_dapper.GetAll<SubmissionModel>($"select * from [tblSubmissions]", null,
                    commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetSubmissionById))]
        public Task<SubmissionModel> GetSubmissionById(int Id)
        {
            var result = Task.FromResult(_dapper.Get<SubmissionModel>($"select * from [tblSubmissions] where Id = {Id}", null,
                    commandType: CommandType.Text));
            return result;
        }


    }
}
