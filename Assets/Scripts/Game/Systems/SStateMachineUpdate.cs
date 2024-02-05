using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;

namespace CodeBase.Game.Systems
{
    public sealed class SStateMachineUpdate : SystemComponent<CStateMachine>
    {
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(UpdateStateMachine);
        }

        private void UpdateStateMachine(CStateMachine stateMachine)
        {
            if (stateMachine.IsCreated)
            {
                stateMachine.StateMachine.Tick();
            }
        }
    }
}