namespace WordFinder.Service.Services
{
    public class WordFinderService : IWordFinderService
    {
        private List<string>? _rows;
        private List<string>? _columns;
        private int _maxRows;
        private int _maxCols;
        private bool _isMatrixInitialized = false;

        public void SetMatrix(IEnumerable<string> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            _rows = matrix.ToList();
            if (_rows.Count == 0 || _rows.Any(row => row.Length != _rows[0].Length))
                throw new ArgumentException("Matrix must be non-empty and all rows must be the same length.");

            _maxRows = _rows.Count;
            _maxCols = _rows[0].Length;
            _columns = BuildColumns(_rows, _maxRows, _maxCols);

            _isMatrixInitialized = true;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            if (!_isMatrixInitialized)
                throw new InvalidOperationException("Matrix has not been set. Call SetMatrix() before Find().");

            if (wordstream == null)
                throw new ArgumentNullException(nameof(wordstream));

            var uniqueWords = new HashSet<string>(wordstream.Where(w => !string.IsNullOrEmpty(w)));
            var foundWords = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var word in uniqueWords)
            {
                if (word.Length > _maxCols && word.Length > _maxRows)
                    continue;

                bool found = _rows!.Any(line => line.Contains(word, StringComparison.OrdinalIgnoreCase)) ||
                             _columns!.Any(line => line.Contains(word, StringComparison.OrdinalIgnoreCase));

                if (found)
                {
                    int count = wordstream.Count(w => string.Equals(w, word, StringComparison.OrdinalIgnoreCase));
                    foundWords[word] = count;
                }
            }

            return foundWords
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key)
                .Take(10)
                .Select(kvp => kvp.Key);
        }

        private List<string> BuildColumns(List<string> rows, int rowCount, int colCount)
        {
            var columns = new List<string>(colCount);

            for (int col = 0; col < colCount; col++)
            {
                var columnChars = new char[rowCount];
                for (int row = 0; row < rowCount; row++)
                {
                    columnChars[row] = rows[row][col];
                }
                columns.Add(new string(columnChars));
            }

            return columns;
        }

    }
}
