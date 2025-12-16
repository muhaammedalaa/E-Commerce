using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.APIs.Errors;
using Talabat.Repository.Data.Contexts;

namespace Talabat.APIs.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var thing = await _dbContext.products.FindAsync(-1);
            if (thing == null) return NotFound(new ApiResponse(404) );
            return Ok(thing);
        }
        [HttpGet("server-error")]
        public async Task<ActionResult> GetServerError()
        {
            var thing = await _dbContext.products.FindAsync(-1);
            var thingToReturn = thing.ToString();
            return Ok(thingToReturn);
        }
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("bad-request/{id}")]
        public ActionResult GetValidationError(int id)
        {
            return Ok();
        }
        [HttpGet("unauthorized")]
        public ActionResult GetUnauthorized()
        {
            return Unauthorized(new ApiResponse(401));
        }
    
       

    }
}
