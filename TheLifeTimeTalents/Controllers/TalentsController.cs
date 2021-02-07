using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLifeTimeTalents.Models;
namespace TheLifeTimeTalents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalentsController : ControllerBase
    {
        //static readonly TalentRepository repository = new TalentRepository();

        //[HttpGet]
        //public IEnumerable<Talent> GetAllTalents()
        //{
        //    return repository.GetAll();
        //}

        //[HttpGet("{id}")]
        //public Talent GetTalent(int id)
        //{
        //    Talent item = repository.Get(id);
        //    if (item == null)
        //    {
        //        throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
        //    }
        //    return item;
        //}

        public TalentsController(AppDb db)
        {
            Db = db;
        }

        // GET api/Talents
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new TalentQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/Talents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new TalentQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/Talents
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Talent body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/Talents/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromForm] Talent body)
        {
            await Db.Connection.OpenAsync();
            var query = new TalentQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Name = body.Name;
            result.ShortName = body.ShortName;
            result.Reknown = body.Reknown;
            result.Bio = body.Bio;
            result.Url = body.Url;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/Talents/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new TalentQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/Talents
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new TalentQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }

        public AppDb Db { get; }

    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using TalentsSearch.Models;
//using System.Web.Http.Cors;

//namespace TalentsSearch.Controllers
//{
//    public class TalentsController : ApiController
//    {
//        static readonly TalentRepository repository = new TalentRepository();
//        [EnableCors(origins: "*", headers: "*", methods: "*")]

//        [Route("api/talents")]
//        public IEnumerable<Talent> GetAllTalents()
//        {
//            return repository.GetAll();
//        }

//        [Route("api/talents/{id:int}")]
//        public Talent GetTalent(int id)
//        {
//            Talent item = repository.Get(id);
//            if (item == null)
//            {
//                throw new HttpResponseException(HttpStatusCode.NotFound);
//            }
//            return item;
//        }


//    }
//}