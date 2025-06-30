using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchService;

namespace SearchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecificationDto>>> GetAsync([FromQuery] string query)
        {
            try
            {
                var client = new SearchServiceClient();
                var results = await client.SearchAsync(query);
                return Ok(results);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
