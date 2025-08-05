using FluentValidation;
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
            try
            {
                var request = new GetFoundWordsRequest(dto.Matrix, dto.Wordstream);
                var result = await _mediator.Send(request, cancellationToken);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }
        }
    }
}
