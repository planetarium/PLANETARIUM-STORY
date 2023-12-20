namespace TableSheet
{
    public class ShopSpaceSheet : Sheet<int, ShopSpaceSheet.Row>
    {
        public class Row : SheetRow<int>
        {
            public override int Key => Step;
            public int Step { get; private set; }
            public int Cost { get; private set; }
            public float BonusSpeed { get; private set; }

            public override void Set(string[] fields)
            {
                Step = ParseInt(fields[0]);
                Cost = ParseInt(fields[1]);
                BonusSpeed = ParseFloat(fields[2]);
            }
        }
        
    }
}