using System;
using System.Collections.Generic;
using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateMachine : StateMachine
    {
        public CharacterStateMachine(CCharacter character)
        {
            States = new Dictionary<Type, IState>
            {
                {typeof(CharacterStateIdle), new CharacterStateIdle(this, character)},
                {typeof(CharacterStateRun), new CharacterStateRun(this, character)},
                {typeof(CharacterStateFight), new CharacterStateFight(this, character)},
                {typeof(CharacterStateDeath), new CharacterStateDeath(this, character)},
                {typeof(CharacterStateNone), new CharacterStateNone(this, character)},
            };
        }
    }
}