using System.ComponentModel;
using CodeBase.Infrastructure.Progress;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Infrastructure.CheatService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class CheatService : ICheatService
    {
        private readonly IProgressService _progressService;

        public CheatService(IProgressService progressService)
        {
            _progressService = progressService;
        }

        void ICheatService.Init()
        {
            if (Debug.isDebugBuild)
            {
                SRDebug.Init();
                SRDebug.Instance.AddOptionContainer(this);
            }
        }
        
        [Category("Currency")]
        [SROptions.Sort(0)]
        [DisplayName("Money Count")]
        public int MoneyCount { get; set; } = 1000;

        [Category("Currency")]
        [SROptions.Sort(1)]
        [DisplayName("Add Money")]
        public void AddMoney() => _progressService.MoneyData.Data.Value += MoneyCount;
    }
}