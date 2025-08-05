namespace WordFinder.Service.Services
{
    public interface IWordFinderService
    {
        void SetMatrix(IEnumerable<string> matrix);
        IEnumerable<string> Find(IEnumerable<string> wordstream);
    }
}
