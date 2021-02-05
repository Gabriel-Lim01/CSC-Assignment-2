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
        static readonly TalentRepository repository = new TalentRepository();

        [HttpGet]
        public IEnumerable<Talent> GetAllTalents()
        {
            return repository.GetAll();
        }

        [HttpGet("{id}")]
        public Talent GetTalent(int id)
        {
            Talent item = repository.Get(id);
            if (item == null)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
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