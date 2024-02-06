using CodeBase.Game.Components;
using CodeBase.Game.StateMachine;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Game.StateMachine.Unit;
using CodeBase.Infrastructure.States;
using CodeBase.Utils;
using JetBrains.Annotations;
using VContainer;

namespace CodeBase.Infrastructure.Factories.StateMachine
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StateMachineFactory : IStateMachineFactory
    {
        private readonly IObjectResolver _objectResolver;

        public StateMachineFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        IGameStateMachine IStateMachineFactory.CreateGameStateMachine()
        {
            GameStateMachine gameStateService = new GameStateMachine();
            
            gameStateService.States.Values.Foreach(_objectResolver.Inject);
            
            return gameStateService;
        }

        IStateMachine IStateMachineFactory.CreateCharacterStateMachine(CCharacter character)
        {
            CharacterStateMachine characterStateMachine = new CharacterStateMachine(character);
            
            characterStateMachine.States.Values.Foreach(_objectResolver.Inject);
            
            return characterStateMachine;
        }

        IStateMachine IStateMachineFactory.CreateUnitStateMachine(CUnit unit)
        {
            UnitStateMachine unitStateMachine = new UnitStateMachine(unit);
            
            unitStateMachine.States.Values.Foreach(_objectResolver.Inject);
            
            return unitStateMachine;
        }
    }
}