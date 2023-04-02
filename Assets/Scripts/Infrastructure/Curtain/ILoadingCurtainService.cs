using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Curtain
{
    public interface ILoadingCurtainService : IService
    {
        public void Show();
        public UniTask Hide();
    }
}