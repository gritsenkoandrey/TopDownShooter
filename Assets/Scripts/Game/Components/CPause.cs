using System;
using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;

namespace CodeBase.Game.Components
{
    public sealed class CPause : EntityComponent<CPause>
    {
        private IPause[] _pauses = Array.Empty<IPause>();

        protected override void OnEntityCreate()
        {
            base.OnEntityCreate();
        
            _pauses = GetComponentsInChildren<IPause>();
        }

        public void Pause(bool isPause)
        {
            if (_pauses.IsReadOnly) return;

            for (int i = 0; i < _pauses.Length; i++)
            {
                _pauses[i].Pause(isPause);
            }
        }
    }
}