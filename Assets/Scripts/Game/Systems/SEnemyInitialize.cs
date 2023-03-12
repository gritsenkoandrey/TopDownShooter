using CodeBase.ECSCore;
using CodeBase.Game.Components;

namespace CodeBase.Game.Systems
{
    public sealed class SEnemyInitialize : SystemComponent<CEnemy>
    {
        private readonly CCharacter _character;

        public SEnemyInitialize(CCharacter character)
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

        protected override void OnEnableComponent(CEnemy component)
        {
            base.OnEnableComponent(component);
            
            _character.Enemies.Add(component);
        }

        protected override void OnDisableComponent(CEnemy component)
        {
            base.OnDisableComponent(component);

            _character.Enemies.Remove(component);
        }
    }
}