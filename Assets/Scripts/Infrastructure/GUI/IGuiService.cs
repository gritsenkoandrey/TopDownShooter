using CodeBase.UI;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.GUI
{
    public interface IGuiService
    {
        StaticCanvas StaticCanvas { get; }
        float ScaleFactor { get; }
        void Push(BaseScreen screen);
        void Pop();
        void CleanUp();
    }
}