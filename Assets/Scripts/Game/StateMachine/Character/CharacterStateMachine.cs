using System;
using System.Collections.Generic;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Input;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateMachine : StateMachine
    {
        public CharacterStateMachine(ICharacter character, ICameraService cameraService, IJoystickService joystickService, IGameFactory gameFactory)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(CharacterStateIdle)] = new CharacterStateIdle(this, character, cameraService, joystickService, gameFactory),
                [typeof(CharacterStateRun)] = new CharacterStateRun(this, character, cameraService, joystickService, gameFactory),
                [typeof(CharacterStateFight)] = new CharacterStateFight(this, character, cameraService, joystickService, gameFactory),
                [typeof(CharacterStateDeath)] = new CharacterStateDeath(this, character, cameraService, joystickService, gameFactory),
                [typeof(CharacterStateNone)] = new CharacterStateNone(this, character, cameraService, joystickService, gameFactory),
            };
        }
    }
}