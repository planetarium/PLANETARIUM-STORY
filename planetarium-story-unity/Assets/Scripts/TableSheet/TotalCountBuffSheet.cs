﻿namespace TableSheet
{
    public class TotalCountBuffSheet : Sheet<int, TotalCountBuffSheet.Row>
    {
        public class Row : SheetRow<int>
        {
            public override int Key => TotalCount;
            public int TotalCount { get; private set; }
            public int ClickCostBonus { get; private set; }

            public override void Set(string[] fields)
            {
                TotalCount = ParseInt(fields[0]);
                ClickCostBonus = ParseInt(fields[1]);
            }
        }
    }
}