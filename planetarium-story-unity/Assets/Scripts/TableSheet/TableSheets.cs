using System;
using System.Collections.Generic;

namespace TableSheet
{
    public class TableSheets
    {
        /// <summary>
        /// Initialize TableSheets with reflection
        /// </summary>
        /// <param name="sheets">pairs of sheet's name and csv</param>
        /// <exception cref="Exception"></exception>
        public TableSheets(Dictionary<string, string> sheets)
        {
            var type = typeof(TableSheets);
            foreach (var pair in sheets)
            {
                // Get Sheet property in TableSheets class
                var sheetPropertyInfo = type.GetProperty(pair.Key);
                if (sheetPropertyInfo is null)
                {
                    throw new Exception($"[{nameof(TableSheets)}] / ({pair.Key}, csv) / failed to get property");
                }

                var sheetObject = Activator.CreateInstance(sheetPropertyInfo.PropertyType);
                var iSheet = (ISheet)sheetObject;
                if (iSheet is null)
                {
                    throw new Exception($"[{nameof(TableSheets)}] / ({pair.Key}, csv) / failed to cast to {nameof(ISheet)}");
                }

                if (pair.Value is not null)
                {
                    iSheet.Set(pair.Value);
                }

                sheetPropertyInfo.SetValue(this, sheetObject);
            }
        }
        
        public CharacterSheet CharacterSheet { get; private set; }
        public GameConfigSheet GameConfigSheet { get; private set; }
        public ShopCharacterSheet ShopCharacterSheet { get; private set; }
        public ShopSpaceSheet ShopSpaceSheet { get; private set; }
        public TeamBuffSheet TeamBuffSheet { get; private set; }
        public TotalCountBuffSheet TotalCountBuffSheet { get; private set; }
    }
}