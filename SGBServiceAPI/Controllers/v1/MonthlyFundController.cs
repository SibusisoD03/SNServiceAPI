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
    public class MonthlyFundController : ControllerBase
    {
        private readonly IDapper _dapper;
        public MonthlyFundController(IDapper dapper)
        {
            _dapper = dapper;
        }

        //Post item
        [HttpPost(nameof(AddItem))]
        public async Task<int> AddItem(ItemModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@itemId", data.itemId, DbType.Int32);
            dbparams.Add("@itemName", data.itemName);
            dbparams.Add("@itemPrice", data.itemPrice, DbType.Int32);            

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddItem]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }
        //Update items
        [HttpPost(nameof(UpdateItem))]
        public async Task<int> UpdateItem(ItemModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@itemId", data.itemId, DbType.Int32);
            dbparams.Add("@itemName", data.itemName);
            dbparams.Add("@itemPrice", data.itemPrice, DbType.Int32);

            var result = await Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateItem]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }
        //Get
        [HttpGet(nameof(GetItems))]
        public Task<List<ItemModel>> GetItems()
        {
            var result = Task.FromResult(_dapper.GetAll<ItemModel>($"select * from [tblItem]", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetItemById))]
        public async Task<ItemModel> GetItemById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<ItemModel>($"select * from [tblItem] where itemId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Post monthly budget
        [HttpPost(nameof(AddMonthlyBudget))]
        public async Task<int> AddMonthlyBudget(MonthlyBudgetModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@id", data.id, DbType.Int32);
            dbparams.Add("@currentMonth", data.currentMonth, DbType.DateTime);
            dbparams.Add("@monthlyFund", data.monthlyFund, DbType.Int32);
            dbparams.Add("@balance", data.balance, DbType.Int32);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddMonthlyBudget]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }
        //Update monthly budget
        [HttpPost(nameof(UpdateMonthlyBudget))]
        public async Task<int> UpdateMonthlyBudget(MonthlyBudgetModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@id", data.id, DbType.Int32);
            dbparams.Add("@currentMonth", data.currentMonth, DbType.DateTime);
            dbparams.Add("@monthlyFund", data.monthlyFund, DbType.Int32);
            dbparams.Add("@balance", data.balance, DbType.Int32);

            var result = await Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateMonthlyBudget]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }
        //Get
        [HttpGet(nameof(GetMonthlyBudgets))]
        public Task<List<MonthlyBudgetModel>> GetMonthlyBudgets()
        {
            var result = Task.FromResult(_dapper.GetAll<MonthlyBudgetModel>($"select * from [tblMonthlyBudget]", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetMonthlyFundById))]
        public async Task<MonthlyBudgetModel> GetMonthlyFundById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<MonthlyBudgetModel>($"select * from [tblMonthlyBudget] where id={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }

        //Post invoice
        [HttpPost(nameof(AddInvoice))]
        public async Task<int> AddInvoice(InvoiceModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@invoiceId", data.invoiceId, DbType.Int32);
            dbparams.Add("@datePurchased", data.datePurchased, DbType.DateTime);
            dbparams.Add("@itemId", data.itemId, DbType.Int32);
            dbparams.Add("@supplier", data.supplier);
            dbparams.Add("@uploadInvoice", data.uploadInvoice);
            dbparams.Add("@subTotal", data.subTotal, DbType.Int32);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[spAddInvoice]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }
        //Update monthly budget
        [HttpPost(nameof(UpdateInvoice))]
        public async Task<int> UpdateInvoice(InvoiceModel data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@invoiceId", data.invoiceId, DbType.Int32);
            dbparams.Add("@datePurchased", data.datePurchased, DbType.DateTime);
            dbparams.Add("@itemId", data.itemId, DbType.Int32);
            dbparams.Add("@supplier", data.supplier);
            dbparams.Add("@uploadInvoice", data.uploadInvoice);
            dbparams.Add("@subTotal", data.subTotal, DbType.Int32);

            var result = await Task.FromResult(_dapper.Update<int>("[dbo].[spUpdateInvoice]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }
        //Get
        [HttpGet(nameof(GetInvoices))]
        public Task<List<InvoiceModel>> GetInvoices()
        {
            var result = Task.FromResult(_dapper.GetAll<InvoiceModel>($"select * from [tblInvoices]", null,
                    commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetInvoiceById))]
        public async Task<InvoiceModel> GetInvoiceById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<InvoiceModel>($"select * from [tblInvoices] where invoiceId={Id}", null,
                    commandType: CommandType.Text));
            return result;
        }
    }
}
