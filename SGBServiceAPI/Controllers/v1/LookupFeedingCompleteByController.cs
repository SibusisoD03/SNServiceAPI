using Dapper;
using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Models;
using SNServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Controllers.v1
{

    [Route("api/[controller]")]
    [ApiController]
    public class LookupFeedingCompleteByController : ControllerBase
  
        {
            private readonly IDapper _dapper;
            public LookupFeedingCompleteByController(IDapper dapper)
            {
                _dapper = dapper;
            }


            [HttpGet(nameof(GetLookupFeedingCompleteBy))]
            public Task<List<LookupFeedingCompleteByModel>> GetLookupFeedingCompleteBy()
            {
                var LookupFeedingList = Task.FromResult(_dapper.GetAll<LookupFeedingCompleteByModel>($"select * from [dbo].[tblLookupFeedingCompleteBy]", null,
                        commandType: CommandType.Text));
                return LookupFeedingList;
            }
        }
    }

