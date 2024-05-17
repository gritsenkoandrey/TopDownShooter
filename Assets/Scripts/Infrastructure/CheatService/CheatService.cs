using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CodeBase.Infrastructure.DailyTasks;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Infrastructure.CheatService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class CheatService : ICheatService
    {
        private readonly IProgressService _progressService;
        private readonly IDailyTaskService _dailyTaskService;
        private readonly LevelModel _levelModel;

        public CheatService(IProgressService progressService, IDailyTaskService dailyTaskService, LevelModel levelModel)
        {
            _progressService = progressService;
            _dailyTaskService = dailyTaskService;
            _levelModel = levelModel;
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

        [Category("Enemy")]
        [SROptions.Sort(0)]
        [DisplayName("Kill All Enemy")]
        public void KillAllEnemy() => _levelModel.Enemies.ToList().Foreach(enemy => enemy.Health.CurrentHealth.Value = 0);
        
        [Category("Daily Task")]
        [SROptions.Sort(0)]
        [DisplayName("Complete All Tasks")]
        public void CompleteAllTasks()
        {
            List<DailyTaskType> tasks = EnumExtension.GenerateEnumList<DailyTaskType>();
            tasks.Foreach(type => _dailyTaskService.Update.Execute((type, int.MaxValue)));
        }
    }
}