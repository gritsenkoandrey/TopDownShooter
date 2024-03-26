using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SWinReward : SystemComponent<CWinReward>
    {
        private LevelModel _levelModel;
        private LootModel _lootModel;

        [Inject]
        private void Construct(LevelModel levelModel, LootModel lootModel)
        {
            _levelModel = levelModel;
            _lootModel = lootModel;
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
            int count = _levelModel.CalculateLevelStar();

            for (int i = 0; i < component.Stars.Length; i++)
            {
                component.Stars[i].gameObject.SetActive(count > i);
            }
        }

        private void CalculateLoot(CWinReward component)
        {
            component.Text.text = string.Format(FormatText.AddMoneyWin, 
                _lootModel.GenerateLevelLoot(_levelModel.Level).Trim());
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