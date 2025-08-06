using FluentValidation;
using MediatR;

namespace WordFinder.Features
{
    public class GetFoundWordsRequest : IRequest<IEnumerable<string>>
    {
        public IEnumerable<string> Matrix { get; }
        public IEnumerable<string> WordStream { get; }

        public GetFoundWordsRequest(IEnumerable<string> matrix, IEnumerable<string> wordStream)
        {
            Matrix = matrix;
            WordStream = wordStream;
        }
    }

    public class GetFoundWordsRequestValidator : AbstractValidator<GetFoundWordsRequest>
    {
        private const int MaxMatrixSize = 64;
        public GetFoundWordsRequestValidator()
        {
            RuleFor(x => x.Matrix)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Matrix must not be null.")
                .NotEmpty().WithMessage("Matrix must contain at least one row.")
                .Must(HaveUniformRowLengths).WithMessage("All matrix rows must have the same length.")
                .Must(m => m.All(row => !string.IsNullOrWhiteSpace(row))).WithMessage("Matrix rows must not be empty or whitespace.")
                .Must(m => m.Count() <= MaxMatrixSize).WithMessage($"Matrix must not contain more than {MaxMatrixSize} rows.")
                .Must(m => m.First().Length <= MaxMatrixSize).WithMessage($"Matrix rows must not contain more than {MaxMatrixSize} columns.");

            RuleFor(x => x.WordStream)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("WordStream must not be null.")
                .NotEmpty().WithMessage("WordStream must contain at least one word.")
                .Must(ws => ws.All(w => !string.IsNullOrWhiteSpace(w))).WithMessage("WordStream must not contain empty or whitespace words.");
        }

        private static bool HaveUniformRowLengths(IEnumerable<string> rows)
        {
            if (rows is null || !rows.Any())
                return false;

            var firstLength = rows.First().Length;
            return rows.All(r => r.Length == firstLength);
        }
    }
}
