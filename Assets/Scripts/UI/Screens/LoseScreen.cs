﻿using CodeBase.App;
using CodeBase.Infrastructure.States;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens
{
    public sealed class LoseScreen : BaseScreen
    {
        [SerializeField] private Button _button;
        [SerializeField] private Transform _splat;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _button
                .OnClickAsObservable()
                .DoOnSubscribe(ScreenAnimation)
                .First()
                .Subscribe(_ => RestartGame().Forget())
                .AddTo(this);

            FadeCanvas(0f, 1f, 0.25f);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private async UniTaskVoid RestartGame()
        {
            await _button.transform.PunchTransform().AsyncWaitForCompletion().AsUniTask();
            
            GameStateService.Enter<StatePreview, string>(SceneName.Lobby);
        }

        private void ScreenAnimation()
        {
            _button.gameObject.SetActive(false);
            _splat.localScale = Vector3.zero;
            _splat
                .DOScale(Vector3.one * 1.5f, 1f)
                .SetEase(Ease.OutBack)
                .OnComplete(ShowButton);
        }

        private void ShowButton()
        {
            _button.gameObject.SetActive(true);
            _button.transform.PunchTransform();
        }
    }
}