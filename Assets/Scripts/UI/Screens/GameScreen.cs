namespace CodeBase.UI.Screens
{
    public sealed class GameScreen : BaseScreen
    {
        private protected override void OnEnable()
        {
            base.OnEnable();
            
            FadeCanvas(0f, 1f, 1f);
        }
    }
}