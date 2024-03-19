namespace CodeBase.Game.Behaviours.AnimationStateBehaviour
{
    public interface IAnimationStateReader
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
        void UpdateState(int stateHash);
    }
}