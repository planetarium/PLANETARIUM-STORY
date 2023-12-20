    using TableSheet;

    public class Character
    {
        public CharacterSheet.Row Row { get; private set; }
        public bool IsActivated { get; private set; }
        
        public Character(CharacterSheet.Row row, bool isActivated = false)
        {
            Row = row;
            IsActivated = isActivated;
        }
        
        public void Activate()
        {
            IsActivated = true;
        }
    }
