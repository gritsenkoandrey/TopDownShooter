using CodeBase.UI;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.GUI
{
    public interface IGuiService
    {
        public StaticCanvas StaticCanvas { get; }
        public float ScaleFactor { get; }
        public void Push(BaseScreen screen);
        public void Pop();
        public void CleanUp();
    }
}