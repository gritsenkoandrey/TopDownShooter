using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.DailyTasks;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using DG.Tweening;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsBase
{
    public sealed class SDailyTaskUpdate : SystemBase
    {
        private IProgressService _progressService;
        private IDailyTaskService _dailyTaskService;
        private LevelModel _levelModel;
        private InventoryModel _inventoryModel;

        private Tween _playMinutesTween;

        [Inject]
        private void Construct(IProgressService progressService, IDailyTaskService dailyTaskService, 
            LevelModel levelModel, InventoryModel inventoryModel)
        {
            _progressService = progressService;
            _dailyTaskService = dailyTaskService;
            _levelModel = levelModel;
            _inventoryModel = inventoryModel;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            SubscribeOnUpdateTask();
            
            SubscribeOnEnterGame();
            SubscribeOnChangeMoney();
            SubscribeOnKillEnemy();
            SubscribeOnPlayMinutes();
            SubscribeOnCompleteLevel();
            SubscribeOnDealDamage();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            _playMinutesTween?.Kill();
        }

        private void SubscribeOnUpdateTask()
        {
            _dailyTaskService.Update
                .Subscribe(value =>
                {
                    (DailyTaskType type, int score) = value;

                    _progressService.DailyTaskData.Data.Value.Update(type, score);
                })
                .AddTo(LifetimeDisposable);
        }

        private void SubscribeOnChangeMoney()
        {
            _progressService.MoneyData.Data
                .Pairwise()
                .Subscribe(money =>
                {
                    _dailyTaskService.Update.Execute(money.Current > money.Previous
                        ? (DailyTaskType.EarnMoney, money.Current - money.Previous)
                        : (DailyTaskType.SpendMoney, money.Previous - money.Current));
                })
                .AddTo(LifetimeDisposable);
        }

        private void SubscribeOnKillEnemy()
        {
            _levelModel.Enemies
                .ObserveRemove()
                .Subscribe(_ =>
                {
                    _dailyTaskService.Update.Execute(_inventoryModel.SelectedWeapon.Value == WeaponType.Knife
                        ? (DailyTaskType.KillEnemyMeleeWeapon, 1)
                        : (DailyTaskType.KillEnemyRangeWeapon, 1));
                })
                .AddTo(LifetimeDisposable);
        }

        private void SubscribeOnPlayMinutes()
        {
            _playMinutesTween = DOVirtual
                .DelayedCall(1f, () => _dailyTaskService.Update.Execute((DailyTaskType.PlayMinutes, 1)))
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }

        private void SubscribeOnCompleteLevel()
        {
            _levelModel.CompleteLevel
                .Subscribe(value =>
                {
                    if (value >= 3)
                    {
                        _dailyTaskService.Update.Execute((DailyTaskType.CompleteLevelThreeStar, 1));
                    }

                    _dailyTaskService.Update.Execute((DailyTaskType.CompleteLevel, 1));
                })
                .AddTo(LifetimeDisposable);
        }

        private void SubscribeOnEnterGame()
        {
            _dailyTaskService.Update.Execute((DailyTaskType.EnterGame, 1));
        }

        private void SubscribeOnDealDamage()
        {
            _levelModel.Enemies
                .ObserveAdd()
                .Subscribe(enemy =>
                {
                    enemy.Value.Health.CurrentHealth
                        .Pairwise()
                        .Subscribe(health =>
                        {
                            if (health.Current < health.Previous)
                            {
                                _dailyTaskService.Update.Execute((DailyTaskType.DealDamage, health.Previous - health.Current));
                            }
                        })
                        .AddTo(LifetimeDisposable);
                })
                .AddTo(LifetimeDisposable);
        }
    }
}