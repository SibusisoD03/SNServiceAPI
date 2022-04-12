using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SNServiceAPI.Models;
using System.Data;
using System;

namespace SNServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominationController : ControllerBase
    {
        private readonly IDapper _dapper;
        public NominationController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(NominationModel data)
        {
            var dbparams = new DynamicParameters();

            dbparams.Add("@ParentId", data.ParentId);
            dbparams.Add("@SecondedBy", data.SecondedBy);
            dbparams.Add("@EmisCode", data.EmisCode);

            dbparams.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertNomination]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@Id");
            return retVal;
        }
        //[HttpGet(nameof(MeetingList))]
        //public Task<List<MeetingModel>> MeetingList()
        //{
        //    var result = Task.FromResult(_dapper.GetAll<MeetingModel>($"Select * from [tblNominations]", null,
        //            commandType: CommandType.Text));
        //    return result;
        //}
        //[HttpGet(nameof(GetById))]
        //public async Task<MeetingModel> GetById(int Id)
        //{
        //    var result = await Task.FromResult(_dapper.Get<MeetingModel>($"Select * from [tblNominations] where Id = {Id}", null, commandType: CommandType.Text));
        //    return result;
        //}
        /// <summary>
        /// Check if school is scheduled for nominations based on the current date
        /// </summary>
        /// <param name="EmisCode"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetScheduledNominationByEmisCode))]
        public async Task<NominationModel> GetScheduledNominationByEmisCode(string EmisCode, DateTime currentDate)
        {
            var result = await Task.FromResult(_dapper.Get<NominationModel>($"SELECT * from tblSchedule WHERE '{currentDate}' <= EndDate AND '{currentDate}' >= ScheduleDate AND ScheduleTypeId = 1 AND EmisCode = {EmisCode} ORDER BY [ScheduleDate] ASC", null, commandType: CommandType.Text));
            return result;
        }
        /// <summary>
        /// Returned schedule based on school. If end date is less than the current date then high to the parent that the nominations are closed
        /// </summary>
        /// <param name="EmisCode"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetScheduledInfoByEmisCode))]
        public async Task<NominationModel> GetScheduledInfoByEmisCode(int EmisCode)
        {
            var result = await Task.FromResult(_dapper.Get<NominationModel>($"SELECT * from tblSchedule WHERE EmisCode = {EmisCode} AND ScheduleTypeId = 1 ORDER BY [ScheduleDate] ASC", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetProposedNominationsByEmisCode))]
        public async Task<List<ParentModel>> GetProposedNominationsByEmisCode(int EmisCode)
        {
            var result = await Task.FromResult(_dapper.GetAll<ParentModel>($"SELECT EmisCode,Fname FirstName,Sname LastName,InstitutionName,ParentID,IsNominated,IsSeconded,NominatedBy,Count FROM [dbo].[tblParent_info] WHERE EmisCode = {EmisCode} AND IsNominated IS NULL", null, commandType: CommandType.Text));
            return result;
        }
        /// <summary>
        /// get all the parent who are nominated to be seconded. You cant nominate and second the same parent
        /// </summary>
        /// <param name="EmisCode">School</param>
        /// <param name="ParentId">Logged in parent</param>
        /// <returns></returns>
        [HttpGet(nameof(GetNominationsToBeSecondedByEmisCode))]
        public async Task<List<ParentModel>> GetNominationsToBeSecondedByEmisCode(int EmisCode, int ParentId)
        {
            var result = await Task.FromResult(_dapper.GetAll<ParentModel>($"SELECT EmisCode,Fname FirstName,Sname LastName,InstitutionName,ParentID,IsNominated,IsSeconded,NominatedBy,Count FROM [dbo].[tblParent_info] WHERE EmisCode = {EmisCode} AND IsNominated = 1 AND NominatedBy <> {ParentId}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var result = await Task.FromResult(_dapper.Execute($"Delete [tblNominations] Where Id = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(Count))]
        public Task<int> Count(string EmisCode, int ParentId)
        {
            var totalcount = Task.FromResult(_dapper.Get<int>($"Select COUNT(*) from [tblNominations] WHERE EmisCode = {EmisCode} AND ParentId = {ParentId}", null,
                    commandType: CommandType.Text));
            return totalcount;
        }
        //[HttpPatch(nameof(Update))]
        //public Task<int> Update(NominationModel data)
        //{
        //    var dbparams = new DynamicParameters();
        //    dbparams.Add("@ParentId", data.ParentId);
        //    dbparams.Add("@IsNominated", data.IsNominated);
        //    dbparams.Add("@NominatedBy", data.NominatedBy);
        //    dbparams.Add("@IsSeconded", data.IsSeconded);
        //    dbparams.Add("@SecondedCount", data.SecondedCount);
        //    dbparams.Add("@Id", data.Id);

        //    var updateMeeting = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateNomination]",
        //                    dbparams,
        //                    commandType: CommandType.StoredProcedure));
        //    return updateMeeting;
        //}
    }
}
