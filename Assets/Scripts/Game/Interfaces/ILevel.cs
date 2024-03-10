using UniRx;

namespace CodeBase.Game.Interfaces
{
    public interface ILevel
    {
        public IReactiveProperty<int> Time { get; }
        public void SpendTime();
    }
}