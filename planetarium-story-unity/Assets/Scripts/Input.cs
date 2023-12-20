using UniRx;

namespace PlanetariumStory
{
    public class Input
    {
        public Subject<Unit> OnClickScreen { get; } = new();
        public Subject<int> OnClickUnlockSpace { get; } = new();
        public Subject<int> OnClickCurrencyItem { get; } = new();
        
        // zoom
        // drag
    }
}