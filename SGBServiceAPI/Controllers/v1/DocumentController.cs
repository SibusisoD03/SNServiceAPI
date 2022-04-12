using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SNServiceAPI.Models;
using System.Data;

namespace SNServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDapper _dapper;
        public DocumentController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(DocumentModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Title", data.Title);
            dbparams.Add("@DocumentTypeId", data.DocumentTypeId);
            dbparams.Add("@DocumentPath", data.DocumentPath);
            dbparams.Add("@UploadedDate", System.DateTime.Now);
            dbparams.Add("@UpdloadedBy", data.UpdloadedBy);
            dbparams.Add("@Id", 0,DbType.Int32,ParameterDirection.Output);
            
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertDocument]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@Id");
            return retVal;
        }
        [HttpGet(nameof(DocumentList))]
        public Task<List<DocumentModel>> DocumentList()
        {
            var result = Task.FromResult(_dapper.GetAll<DocumentModel>($"select * from [tblDocuments]", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetById))]
        public async Task<DocumentModel> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<DocumentModel>($"Select * from [tblDocuments] where UserId = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var result = await Task.FromResult(_dapper.Execute($"Delete [tblDocuments] Where UserId = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(Count))]
        public Task<int> Count(string title)
        {
            var totalcount = Task.FromResult(_dapper.Get<int>($"select COUNT(*) from [tblDocuments] WHERE Title like '%{title}%'", null,
                    commandType: CommandType.Text));
            return totalcount;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(DocumentModel data)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("@Title", data.Title);
            dbPara.Add("@DocumentTypeId", data.DocumentTypeId);
            dbPara.Add("@DocumentPath", data.DocumentPath);
            dbPara.Add("@UploadedDate", data.UploadedDate);
            dbPara.Add("@UpdloadedBy", data.UpdloadedBy);
            dbPara.Add("@Id", data.Id);

            var updateDocument = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateDocument]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateDocument;
        }
    }
}
