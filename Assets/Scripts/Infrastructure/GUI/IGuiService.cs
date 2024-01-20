using CodeBase.Infrastructure.Services;
using CodeBase.UI;

namespace CodeBase.Infrastructure.GUI
{
    public interface IGuiService : IService
    {
        public StaticCanvas StaticCanvas { get; }
        public float ScaleFactor { get; }
    }
}