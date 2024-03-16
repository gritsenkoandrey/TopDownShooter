using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Screens
{
    public sealed class GameScreen : BaseScreen
    {
        private protected override void OnEnable()
        {
            base.OnEnable();
            
            Show().Forget();
        }
    }
}