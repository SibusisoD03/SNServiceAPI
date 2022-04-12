using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using SNServiceAPI.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace SNServiceAPI.Controllers
{
    public class ParentController : ControllerBase
    {
        private readonly IDapper _dapper;
        public ParentController(IDapper dapper)
        {
            _dapper = dapper;
        }
        /// <summary>
        /// Return Parent Info
        /// </summary>
        /// <param name="IDNumber"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetParentInfo))]
        public Task<ParentModel> GetParentInfo(string IDNumber)
        {
            var result = Task.FromResult(_dapper.Get<ParentModel>($"Select * from [tblParent_info] where IDNumber = {IDNumber}", null,
                    commandType: CommandType.Text));
            return result;
        }

        /// <summary>
        /// Return All schooled for the parent
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetChildrenSchoolByParentId))]
        public async Task<List<LearnerModel>> GetChildrenSchoolByParentId(int ParentId)
        {
            var result = await Task.FromResult(_dapper.GetAll<LearnerModel>($"SELECT DISTINCT [EmisCode],[InstitutionName] FROM [dbo].[tblLearner_info] WHERE ParentID = {ParentId}", null, commandType: CommandType.Text));
            return result;
        }

        [Authorize]
        [HttpGet(nameof(GetLeanersByParent))]
        public async Task<List<LearnerModel>> GetLeanersByParent(string ParentId,string EmisCode)
        {
            var result = await Task.FromResult(_dapper.GetAll<LearnerModel>($"Select * from [tblLearner_info] where ParentID = {ParentId} and EmisCode = {EmisCode}", null, commandType: CommandType.Text));
            return result;
        }

    }
}
