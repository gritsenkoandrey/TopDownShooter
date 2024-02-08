using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Curtain
{
    public interface ILoadingCurtainService
    {
        public void Show();
        public UniTask Hide();
    }
}