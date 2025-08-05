using MediatR;
using Microsoft.AspNetCore.Mvc;
using WordFinder.Dtos;
using WordFinder.Features;

namespace WordFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordFinderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WordFinderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GetFoundWords([FromBody] GetFoundWordsDto dto, CancellationToken cancellationToken)
        {
            var request = new GetFoundWordsRequest(dto.Matrix, dto.Wordstream);

            return Ok(await _mediator.Send(request, cancellationToken));
        }
    }
}
