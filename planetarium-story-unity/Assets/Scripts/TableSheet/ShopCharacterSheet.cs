namespace TableSheet
{
    public class ShopCharacterSheet : Sheet<int, ShopCharacterSheet.Row>
    {
        public class Row : SheetRow<int>
        {
            public override int Key => TotalCount;
            public int TotalCount { get; private set; }
            public int Cost { get; private set; }

            public override void Set(string[] fields)
            {
                TotalCount = ParseInt(fields[0]);
                Cost = ParseInt(fields[1]);
            }
        }
    }
}