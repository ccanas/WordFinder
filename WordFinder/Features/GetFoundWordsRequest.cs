using FluentValidation;
using MediatR;

namespace WordFinder.Features
{
    public class GetFoundWordsRequest : IRequest<IEnumerable<string>>
    {
        public IEnumerable<string> Matrix { get; set; }
        public IEnumerable<string> WordStream { get; set; }

        
        public GetFoundWordsRequest(IEnumerable<string> matrix, IEnumerable<string> wordStream) 
        {
            Matrix = matrix;
            WordStream = wordStream;
            var validator = new GetFoundWordsRequestValidator();
            var validationResult = validator.Validate(this);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }


    public class GetFoundWordsRequestValidator : AbstractValidator<GetFoundWordsRequest>
    {
        public GetFoundWordsRequestValidator()
        {
            RuleFor(x => x.Matrix)
                .NotNull()
                .WithMessage("Matrix must not be null.")
                .NotEmpty()
                .WithMessage("Matrix must contain at least one row.")
                .Must(HaveUniformRowLengths)
                .WithMessage("All matrix rows must have the same length.")
                .Must(m => m.All(row => !string.IsNullOrWhiteSpace(row)))
                .WithMessage("Matrix rows must not be empty or whitespace.");

            RuleFor(x => x.WordStream)
                .NotNull()
                .WithMessage("WordStream must not be null.")
                .NotEmpty()
                .WithMessage("WordStream must contain at least one word.")
                .Must(ws => ws.All(w => !string.IsNullOrWhiteSpace(w)))
                .WithMessage("WordStream must not contain empty or whitespace words.");
        }

        private bool HaveUniformRowLengths(IEnumerable<string> rows)
        {
            if (rows == null || !rows.Any())
                return false;

            int length = rows.First().Length;
            return rows.All(r => r.Length == length);
        }
    }
}
