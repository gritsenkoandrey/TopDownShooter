using CodeBase.ECSCore;
using CodeBase.Game.Components;

namespace CodeBase.Game.Systems
{
    public sealed class SVirtualCamera : SystemComponent<CVirtualCamera>
    {
        private readonly CCharacter _character;

        public SVirtualCamera(CCharacter character)
        {
            _character = character;
        }
        
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
        }

        protected override void OnEnableComponent(CVirtualCamera component)
        {
            base.OnEnableComponent(component);
            
            component.SetTarget(_character.transform);
        }

        protected override void OnDisableComponent(CVirtualCamera component)
        {
            base.OnDisableComponent(component);
        }
    }
}