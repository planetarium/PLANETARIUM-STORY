namespace TableSheet
{
    public enum TeamType
    {
        Game,
        Publishing,
        Operations,
        BlockChain,
        Verse8,
        GameFi,
        Investment
    }
    
    public class CharacterSheet : Sheet<int, CharacterSheet.Row>
    {
        public class Row : SheetRow<int>
        {
            public override int Key => Id;
            public int Id { get; private set; }
            public string Name { get; private set; }
            public TeamType Team { get; private set; }

            public override void Set(string[] fields)
            {
                Id = ParseInt(fields[0]);
                Name = fields[1];
                Team = ParseEnum<TeamType>(fields[2]);
            }
        }
    }
}