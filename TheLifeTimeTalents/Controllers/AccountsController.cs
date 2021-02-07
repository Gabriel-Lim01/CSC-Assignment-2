using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLifeTimeTalents.Models;

namespace TheLifeTimeTalents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public AccountsController(AppDb db)
        {
            Db = db;
        }

        //// GET api/blog
        //[HttpGet]
        //public async Task<IActionResult> GetLatest()
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new AccountQuery(Db);
        //    var result = await query.LatestPostsAsync();
        //    return new OkObjectResult(result);
        //}

        //// GET api/blog/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOne(int id)
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new AccountQuery(Db);
        //    var result = await query.FindOneAsync(id);
        //    if (result is null)
        //        return new NotFoundResult();
        //    return new OkObjectResult(result);
        //}

        // GET api/Accounts/login/{email}
        [HttpGet("login/{email}")]
        public async Task<IActionResult> GetAccount(string email)
        {
            await Db.Connection.OpenAsync();
            var query = new AccountQuery(Db);
            var result = await query.FindOneAsyncViaEmail(email);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/Accounts/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post([FromForm] Account body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        //// PUT api/blog/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOne(int id, [FromForm] Login body)
        //{
        //    await Db.Connection.OpenAsync();
        //    var query = new AccountQuery(Db);
        //    var result = await query.FindOneAsync(id);
        //    if (result is null)
        //        return new NotFoundResult();
        //    result.Email = body.Email;
        //    result.Username = body.Username;
        //    result.Password = body.Password;
        //    await result.UpdateAsync();
        //    return new OkObjectResult(result);
        //}

        public AppDb Db { get; }
    }
}
