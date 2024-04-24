using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Loot;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateDeath : UnitState, IState
    {
        private IEffectFactory _effectFactory;
        private ILootService _lootService;
        private LevelModel _levelModel;

        public UnitStateDeath(IStateMachine stateMachine, CUnit unit) : base(stateMachine, unit)
        {
        }

        [Inject]
        private void Construct(IEffectFactory effectFactory, ILootService lootService, LevelModel levelModel)
        {
            _effectFactory = effectFactory;
            _lootService = lootService;
            _levelModel = levelModel;
        }
        
        public void Enter()
        {
            Unit.Agent.Agent.ResetPath();
            Unit.Animator.OnDeath.Execute();
            Unit.Shadow.SetActive(false);
            Unit.CleanSubscribe();
            
            Unit.BodyMediator.SkinnedMeshes.Foreach(mesh => CreateDestroyMeshEffect(mesh).Forget());

            DeactivateUnit();

            _lootService.GenerateEnemyLoot(Unit);
            _levelModel.RemoveEnemy(Unit);
            _effectFactory.CreateEffect(EffectType.Blood, Unit.Position).Forget();
        }

        public void Exit() { }

        public void Tick() { }

        private async UniTaskVoid CreateDestroyMeshEffect(SkinnedMeshRenderer mesh)
        {
            GameObject effect = await _effectFactory.CreateEffect(EffectType.DestroyMesh, Unit.Position);
            effect.GetComponent<CDestroyMeshEffect>().OnInit.Value = mesh;
        }

        private void DeactivateUnit()
        {
            DOVirtual.DelayedCall(3f, () => Unit.SetActive(false)).SetLink(Unit.gameObject);
        }
    }
}