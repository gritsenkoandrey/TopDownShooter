using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
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

            ActivateStar(component);
            CalculateLoot(component);
            ShowAnimation(component);
        }

        private void ActivateStar(CWinReward component)
        {
            int index = 0;

            if (CharacterHaseFullHealth())
            {
                index++;
            }

            if (CharacterHasHalfHealth())
            {
                index++;
            }

            if (LevelCompletedHalfTime())
            {
                index++;
            }

            for (int i = 0; i < component.Stars.Length; i++)
            {
                component.Stars[i].gameObject.SetActive(index > i);
            }
        }

        private bool CharacterHaseFullHealth() => 
            _levelModel.Character.Health.CurrentHealth.Value == _levelModel.Character.Health.MaxHealth;
        
        private bool CharacterHasHalfHealth() => 
            _levelModel.Character.Health.CurrentHealth.Value >= _levelModel.Character.Health.MaxHealth / 2;

        private bool LevelCompletedHalfTime() => 
            _levelModel.Level.Time >= _levelModel.Level.MaxTime / 2;

        private void CalculateLoot(CWinReward component)
        {
            int loot = _levelModel.Level.Loot;
            component.Text.text = loot.Trim();
            _progressService.MoneyData.Data.Value += loot;
        }

        private void ShowAnimation(CWinReward component)
        {
            int i = 1;

            foreach (Image star in component.Stars)
            {
                DOTween.Sequence()
                    .AppendInterval(i * 0.2f)
                    .Append(star.transform
                        .DOScale(Vector3.one, 0.5f)
                        .From(Vector3.one * 1.65f)
                        .SetEase(Ease.InCubic))
                    .Join(star
                        .DOFade(1f, 0.4f)
                        .From(0f)
                        .SetEase(Ease.Linear))
                    .Append(star.transform
                        .DOPunchScale(Vector3.one * 0.1f, 0.2f, 2, 0.5f))
                    .SetLink(star.gameObject);

                i++;
            }
        }
    }
}