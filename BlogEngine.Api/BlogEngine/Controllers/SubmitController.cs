using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Intefaces.Data.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Controllers
{
    public class SubmitController : BaseController
    {
        private readonly ISubmitService _submitService;

        public SubmitController(ISubmitService submitService)
        {
            _submitService = submitService;
        }

        [Attributes.Authorize(Role.Editor)]
        [HttpPut("update-submit-post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(SubmitRequest request)
        {
            if (request == null) return BadRequest();

            await _submitService.UpdateAsync(request);

            return await ResponseResult(true);
        }
    }
}
