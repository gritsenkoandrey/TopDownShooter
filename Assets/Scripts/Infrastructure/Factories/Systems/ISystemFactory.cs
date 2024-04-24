using CodeBase.ECSCore;

namespace CodeBase.Infrastructure.Factories.Systems
{
    public interface ISystemFactory
    {
        ISystem[] CreateGameSystems();
        ISystem[] CreatePreviewSystems();
    }
}