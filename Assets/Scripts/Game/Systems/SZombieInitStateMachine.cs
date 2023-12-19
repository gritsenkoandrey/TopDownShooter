using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Zombie;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieInitStateMachine : SystemComponent<CZombie>
    {
        protected override void OnEnableComponent(CZombie component)
        {
            base.OnEnableComponent(component);

            component.Target
                .SkipLatestValueOnSubscribe()
                .First()
                .Subscribe(_ => InitializeStateMachine(component))
                .AddTo(component.LifetimeDisposable);
        }

        private void InitializeStateMachine(CZombie component)
        {
            component.StateMachine.SetStateMachine(new ZombieStateMachine(component));

            component.StateMachine.UpdateStateMachine
                .Subscribe(_ => component.StateMachine.StateMachine.Tick())
                .AddTo(component.LifetimeDisposable);
        }
    }
}