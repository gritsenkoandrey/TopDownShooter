using CodeBase.ECSCore;
using CodeBase.Infrastructure.Pool;

namespace CodeBase.Game.SystemsBase
{
    public sealed class SObjectPoolLog : SystemBase
    {
        private readonly IObjectPoolService _objectPoolService;
        public SObjectPoolLog(IObjectPoolService objectPoolService)
        {
            _objectPoolService = objectPoolService;
        }
        
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            _objectPoolService.Log();
        }
    }
}