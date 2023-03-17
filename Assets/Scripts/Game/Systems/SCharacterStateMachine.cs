using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterStateMachine : SystemComponent<CCharacter>
    {
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();

            foreach (CCharacter character in Entities)
            {
                character.UpdateStateMachine.Execute();
            }
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            CharacterStateMachine stateMachine = new CharacterStateMachine(component);
            
            stateMachine.Init();

            component.UpdateStateMachine
                .Subscribe(_ => stateMachine.Tick())
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CCharacter component)
        {
            base.OnDisableComponent(component);
        }
    }
}