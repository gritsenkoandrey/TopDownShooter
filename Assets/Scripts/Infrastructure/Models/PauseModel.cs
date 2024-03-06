using JetBrains.Annotations;
using UniRx;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class PauseModel
    {
        public IReactiveCommand<bool> OnPause { get; } = new ReactiveCommand<bool>();
    }
}