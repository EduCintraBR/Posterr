using Microsoft.AspNetCore.Mvc;
using Posterr.Application.Models;
using Posterr.Application.Models.Requests;
using Posterr.Application.Services;
using Posterr.Domain.Entities;

namespace Posterr.WebApplication.Controllers {
    [ApiController]
    [Route("api")]
    public class ContentController : ControllerBase {

        private readonly ContentService _contentService;
        public ContentController(ContentService contentService) {
            _contentService = contentService;
        }

        [HttpGet]
        [Route("v1/content/latest")]
        public ActionResult<IEnumerable<Content>> ListByDateDescending([FromQuery] int offset, [FromQuery] int limit) {

            var result = _contentService.ListByDateDescending(offset, limit);

            return Ok(result);

        }

        [HttpGet]
        [Route("v1/content/trending")]
        public ActionResult<IEnumerable<Content>> ListByRepostsDescending([FromQuery] int offset, [FromQuery] int limit) {

            var result = _contentService.ListByRepostsDescending(offset, limit);

            return Ok(result);

        }

        [HttpGet]
        [Route("v1/content/search")]
        public ActionResult<IEnumerable<Content>> ListSearchByKeyword([FromQuery] int offset, [FromQuery] int limit, [FromQuery] string keyword) {

            var result = _contentService.ListSearchByKeyword(offset, limit, keyword);
            
            return Ok(result);

        }

        [HttpPost]
        [Route("v1/content/post")]
        public ActionResult<string> AddPost([FromBody] PostAddRequest request)
        {

            var result = _contentService.AddPost(request);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction("AddPost", result);

        }

        [HttpPost]
        [Route("v1/content/repost")]
        public ActionResult<string> Repost([FromBody] PostRepostRequest request)
        {

            var result = _contentService.Repost(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result.Success);

        }

    }
}
