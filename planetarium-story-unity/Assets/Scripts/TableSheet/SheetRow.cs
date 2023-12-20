namespace TableSheet
{
    public abstract class SheetRow<T>
    {
        public abstract T Key { get; }
        
        public abstract void Set(string[] fields);

        #region Equals

        private bool Equals(SheetRow<T> other)
        {
            return Key.Equals(other.Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            
            if (obj.GetType() != GetType()) return false;
            return Equals((SheetRow<T>) obj);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        #endregion
        
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