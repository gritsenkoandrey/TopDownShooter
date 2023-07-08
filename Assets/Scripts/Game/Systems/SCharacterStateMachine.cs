using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Character;
using UniRx;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterStateMachine : SystemComponent<CCharacter>
    {
        private readonly IObjectResolver _objectResolver;

        public SCharacterStateMachine(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            foreach (CCharacter character in Entities)
            {
                character.UpdateStateMachine.Execute();
            }
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            InitializeStateMachine(component);
        }

        private void InitializeStateMachine(CCharacter component)
        {
            CharacterStateMachine stateMachine = new CharacterStateMachine(component);
            
            _objectResolver.Inject(stateMachine);

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