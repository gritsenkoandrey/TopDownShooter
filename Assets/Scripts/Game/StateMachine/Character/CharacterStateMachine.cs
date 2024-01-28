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
        public CharacterStateMachine(CCharacter character, ICameraService cameraService, IJoystickService joystickService, LevelModel levelModel)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(CharacterStateIdle)] = new CharacterStateIdle(this, character, cameraService, joystickService, levelModel),
                [typeof(CharacterStateRun)] = new CharacterStateRun(this, character, cameraService, joystickService, levelModel),
                [typeof(CharacterStateFight)] = new CharacterStateFight(this, character, cameraService, joystickService, levelModel),
                [typeof(CharacterStateDeath)] = new CharacterStateDeath(this, character, cameraService, joystickService, levelModel),
                [typeof(CharacterStateNone)] = new CharacterStateNone(this, character, cameraService, joystickService, levelModel),
            };
        }
    }
}