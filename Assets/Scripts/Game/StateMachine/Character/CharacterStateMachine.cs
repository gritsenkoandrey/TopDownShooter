using System;
using System.Collections.Generic;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateMachine : StateMachine
    {
        public CharacterStateMachine(CCharacter character, IJoystickService joystickService, ICameraService cameraService, LevelModel levelModel)
        {
            States = new Dictionary<Type, IState>
            {
                {typeof(CharacterStateIdle), new CharacterStateIdle(this, character, joystickService, levelModel)},
                {typeof(CharacterStateRun), new CharacterStateRun(this, character, joystickService, cameraService)},
                {typeof(CharacterStateFight), new CharacterStateFight(this, character, joystickService, levelModel)},
                {typeof(CharacterStateDeath), new CharacterStateDeath(this, character)},
                {typeof(CharacterStateNone), new CharacterStateNone(this, character)},
            };
        }
    }
}