using CodeBase.ECSCore;

namespace CodeBase.Infrastructure.Factories.Systems
{
    public interface ISystemFactory
    {
        SystemBase[] CreateGameSystems();
        SystemBase[] CreatePreviewSystems();
    }
}