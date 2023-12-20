using System;
using System.Collections.Generic;
using System.Linq;

namespace TableSheet
{
    public abstract class Sheet<TKey, TValue> : Dictionary<TKey, TValue>, ISheet
        where TValue : SheetRow<TKey>, new()
        where TKey : notnull
    {
        private readonly List<int> _invalidColumnIndexes = new List<int>();

        public void Set(string csv)
        {
            if (string.IsNullOrEmpty(csv))
            {
                throw new ArgumentNullException(nameof(csv));
            }

            var lines = csv.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var columnNames = lines[0].Trim().Split(',');
            for (var i = 0; i < columnNames.Length; i++)
            {
                if (columnNames[i].StartsWith("_"))
                {
                    _invalidColumnIndexes.Add(i);
                }
            }

            var linesWithoutColumnName = lines.Skip(1);
            foreach (var line in linesWithoutColumnName)
            {
                if (string.IsNullOrEmpty(line) || line.StartsWith(",") || line.StartsWith("_"))
                {
                    continue;
                }
                
                var fields = line.Trim().Split(',')
                    .Where((column, index) => !_invalidColumnIndexes.Contains(index))
                    .ToArray();
                var row = new TValue();
                row.Set(fields);

                // Todo : check row is valid

                AddRow(row.Key, row);
            }
        }

        public void Set<T>(Sheet<TKey, T> sheet, bool executePostSet = true) where T : TValue, new()
        {
            foreach (var sheetRow in sheet)
            {
                AddRow(sheetRow.Key, sheetRow.Value);
            }
        }

        protected virtual void AddRow(TKey key, TValue value)
        {
            Add(key, value);
        }
    }
}