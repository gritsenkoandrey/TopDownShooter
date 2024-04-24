using CodeBase.ECSCore;
using CodeBase.Infrastructure.Pool;
using VContainer;

namespace CodeBase.Game.SystemsBase
{
    public sealed class SObjectPoolUpdate : SystemBase
    {
        private IObjectPoolService _objectPoolService;
        
        [Inject]
        private void Construct(IObjectPoolService objectPoolService)
        {
            _objectPoolService = objectPoolService;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            _objectPoolService.ReleaseAll();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            _objectPoolService.Log();
        }
    }
}