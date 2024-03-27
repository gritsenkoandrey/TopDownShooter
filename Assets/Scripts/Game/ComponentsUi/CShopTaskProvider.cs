using System;
using System.Collections.Generic;
using CodeBase.ECSCore;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CShopTaskProvider : EntityComponent<CShopTaskProvider>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _content;
        [SerializeField] private TextMeshProUGUI _text;

        public CanvasGroup CanvasGroup => _canvasGroup;
        public Transform Content => _content;
        public TextMeshProUGUI Text => _text;

        public IList<CTask> Tasks = Array.Empty<CTask>();
    }
}