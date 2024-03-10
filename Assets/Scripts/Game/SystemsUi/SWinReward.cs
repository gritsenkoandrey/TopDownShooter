using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SWinReward : SystemComponent<CWinReward>
    {
        private IProgressService _progressService;
        private LevelModel _levelModel;

        [Inject]
        private void Construct(IProgressService progressService, LevelModel levelModel)
        {
            _progressService = progressService;
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CWinReward component)
        {
            base.OnEnableComponent(component);

            int index = 1;

            if (CharacterDidNotTakeDamage())
            {
                index++;
            }

            if (IsLevelCompletedInTime())
            {
                index++;
            }

            for (int i = 0; i < component.Stars.Length; i++)
            {
                component.Stars[i].SetActive(index > i);
            }
            
            CalculateLoot(component, index);
            StarAnimation(component);
        }

        private bool CharacterDidNotTakeDamage() => 
            _levelModel.Character.Health.CurrentHealth.Value == _levelModel.Character.Health.MaxHealth;

        private bool IsLevelCompletedInTime() => 
            _levelModel.Level.Time >= _levelModel.Level.MaxTime / 2;

        private void CalculateLoot(CWinReward component, int index)
        {
            int loot = _levelModel.Level.Loot * index;
            component.Text.text = loot.Trim();
            _progressService.MoneyData.Data.Value += loot;
        }

        private void StarAnimation(CWinReward component)
        {
            int index = 1;

            foreach (GameObject element in component.Elements)
            {
                element.transform
                    .DOScale(Vector3.one, 0.25f)
                    .From(Vector3.zero)
                    .SetEase(Ease.OutBack)
                    .SetDelay(index * 0.15f)
                    .SetLink(element);

                index++;
            }
        }
    }
}