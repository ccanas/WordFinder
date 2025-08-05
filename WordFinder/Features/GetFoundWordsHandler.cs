using MediatR;
using WordFinder.Features;
using WordFinder.Service.Services;

namespace WordFinder.Application.Features
{
    public class GetFoundWordsHandler : IRequestHandler<GetFoundWordsRequest, IEnumerable<string>>
    {
        private readonly IWordFinderService _wordFinderClient;

        public GetFoundWordsHandler(IWordFinderService wordFinderClient)
        {
            _wordFinderClient = wordFinderClient;
        }

        public Task<IEnumerable<string>> Handle(GetFoundWordsRequest request, CancellationToken cancellationToken)
        {
            _wordFinderClient.SetMatrix(request.Matrix);
            var result = _wordFinderClient.Find(request.WordStream);
            return Task.FromResult(result);
        }
    }
}
