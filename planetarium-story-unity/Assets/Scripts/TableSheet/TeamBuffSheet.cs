namespace TableSheet
{
    public class TeamBuffSheet : Sheet<TeamType, TeamBuffSheet.Row>
    {
        public class Row : SheetRow<TeamType>
        {
            public override TeamType Key => TeamType;
            public TeamType TeamType { get; private set; }
            public long TimeCostBonus { get; private set; }

            public override void Set(string[] fields)
            {
                TeamType = ParseEnum<TeamType>(fields[0]);
                TimeCostBonus = ParseLong(fields[1]);
            }
        }
    }
}