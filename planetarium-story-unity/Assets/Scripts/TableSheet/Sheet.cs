using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TableSheet
{
    public class Sheet<TKey, TValue> 
        where TKey : notnull 
        where TValue : SheetRow<TKey>, new()
    {
        private const string SplitRe = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        private const string LineSplitRe = @"\r\n|\n\r|\n|\r";

        public static List<TValue> Read(string csvData)
        {
            var data = new List<TValue>();

            var lines = Regex.Split(csvData, LineSplitRe);
            if (lines.Length <= 1)
            {
                return data;
            }

            // var header = Regex.Split(lines[0], SplitRe);
            for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], SplitRe);
                if (values.Length == 0 || values[0] == "")
                {
                    continue;
                }

                var row = new TValue();
                row.Set(values);

                data.Add(row);
            }

            return data;
        }
    }
    
    public abstract class SheetRow<T>
    {
        public abstract T Key { get; }

        public abstract void Set(string[] fields);
        
        protected static int ParseInt(string value)
        {
            return int.Parse(value);
        }
        
        protected static float ParseFloat(string value)
        {
            return float.Parse(value);
        }
        
        protected static TEnum ParseEnum<TEnum>(string value)
        {
            return (TEnum)System.Enum.Parse(typeof(TEnum), value);
        }
    }
}