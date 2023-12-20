using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterPreviewChanger : SystemComponent<CCharacterPreviewChanger>
    {
        protected override void OnEnableComponent(CCharacterPreviewChanger component)
        {
            base.OnEnableComponent(component);

            component.UpButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.UpButton.transform.PunchTransform();
                    component.CCharacterPreviewModel.PressUp.Execute();
                })
                .AddTo(component.LifetimeDisposable);
            
            component.DownButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.DownButton.transform.PunchTransform();
                    component.CCharacterPreviewModel.PressDown.Execute();
                })
                .AddTo(component.LifetimeDisposable);
            
            component.LeftButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.LeftButton.transform.PunchTransform();
                    component.CCharacterPreviewModel.PressLeft.Execute();
                })
                .AddTo(component.LifetimeDisposable);
            
            component.RightButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.RightButton.transform.PunchTransform();
                    component.CCharacterPreviewModel.PressRight.Execute();
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}