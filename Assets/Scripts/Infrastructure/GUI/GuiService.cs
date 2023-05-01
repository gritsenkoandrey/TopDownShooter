using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.GUI
{
    public sealed class GuiService : MonoBehaviour, IGuiService
    {
        [SerializeField] private StaticCanvas _staticCanvas;

        StaticCanvas IGuiService.StaticCanvas => _staticCanvas;
    }
}