using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SWallInitialize : SystemComponent<CWall>
    {
        private LevelModel _levelModel;

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CWall component)
        {
            base.OnEnableComponent(component);
            
            _levelModel.AddObstacle(component);
        }
    }
}