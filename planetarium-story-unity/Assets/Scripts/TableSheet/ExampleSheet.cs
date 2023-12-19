namespace TableSheet
{
    public class ExampleSheet : Sheet<int, ExampleSheet.Row>
    {
        public class Row : SheetRow<int>
        {
            public override int Key => Id;
            public int Id { get; private set; }
            public string Name { get; private set; }
            
            public override void Set(string[] fields)
            {
                Id = int.Parse(fields[0]);
                Name = fields[1];
            }
        }
    }
}