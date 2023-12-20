using System.Collections.Generic;

namespace TableSheet
{
    public class GameConfigSheet : Sheet<string, GameConfigSheet.Row>
    {
        public class Row : SheetRow<string>
        {
            public override string Key => _key;

            private string _key;
            public string Value { get; private set; }

            public override void Set(string[] fields)
            {
                _key = fields[0];
                Value = fields[1];
            }
        }

        public int PerTime { get; private set; }
        public int GetCostTime { get; private set; }
        public int GetCostClick { get; private set; }
        public int PerClickCount { get; private set; }
        public int SpaceBonusMinimumCost { get; private set; }
        public int SpaceBonusMaximumCost { get; private set; }
        public int SpaceBonusPerTime { get; private set; }
        public int SpaceBonusProbability { get; private set; }
        public int PerTimeDialogue { get; private set; }
        public int PerTimeDialogueProbability { get; private set; }


        public void Update(List<Row> rowList)
        {
            foreach (var row in rowList)
            {
                switch (row.Key)
                {
                    case "PerTime":
                        PerTime = int.Parse(row.Value);
                        break;
                    case "GetCostTime":
                        GetCostTime = int.Parse(row.Value);
                        break;
                    case "GetCostClick":
                        GetCostClick = int.Parse(row.Value);
                        break;
                    case "PerClickCount": 
                        PerClickCount = int.Parse(row.Value);
                        break;
                    case "SpaceBonusMinimumCost":
                        SpaceBonusMinimumCost = int.Parse(row.Value);
                        break;
                    case "SpaceBonusMaximumCost":
                        SpaceBonusMaximumCost = int.Parse(row.Value);
                        break;
                    case "SpaceBonusPerTime":
                        SpaceBonusPerTime = int.Parse(row.Value);
                        break;
                    case "SpaceBonusProbability":
                        SpaceBonusProbability = int.Parse(row.Value);
                        break;
                    case "PerTimeDialogue":
                        PerTimeDialogue = int.Parse(row.Value);
                        break;
                    case "PerTimeDialogueProbability":
                        PerTimeDialogueProbability = int.Parse(row.Value);
                        break;
                }
            }
        }
    }
}