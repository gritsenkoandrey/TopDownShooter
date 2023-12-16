using CodeBase.ECSCore;
using CodeBase.Game.Components;

namespace CodeBase.Game.Systems
{
    public sealed class SStateMachineUpdate : SystemComponent<CStateMachine>
    {
        protected override void OnUpdate()
        {
            base.OnUpdate();

            foreach (CStateMachine stateMachine in Entities)
            {
                stateMachine.UpdateStateMachine.Execute();
            }
        }
    }
}