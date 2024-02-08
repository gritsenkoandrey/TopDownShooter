using CodeBase.UI;

namespace CodeBase.Infrastructure.GUI
{
    public interface IGuiService
    {
        public StaticCanvas StaticCanvas { get; }
        public float ScaleFactor { get; }
    }
}