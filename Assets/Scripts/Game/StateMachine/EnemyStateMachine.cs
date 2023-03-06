using System;
using System.Collections.Generic;
using AndreyGritsenko.Game.Components;
using UnityEngine;
using UnityEngine.AI;

namespace AndreyGritsenko.Game.StateMachine
{
    public sealed class EnemyStateMachine
    {
        private readonly CEnemy _enemy;
        private readonly CCharacter _character;
        private readonly CRadar _radar;

        private Dictionary<State, Action> _actions;
        private State _state = State.None;
        private Vector3 _patrolPosition;
        private float _maxDelay;
        private float _delay;

        public EnemyStateMachine(CEnemy enemy, CRadar radar, CCharacter character)
        {
            _enemy = enemy;
            _radar = radar;
            _character = character;
        }

        public void Init()
        {
            _patrolPosition = _enemy.transform.position;
            _state = State.Idle;
            _maxDelay = 1f;
            _delay = 0f;

            _actions = new Dictionary<State, Action>()
            {
                { State.Idle, Idle },
                { State.Patrol, Patrol },
                { State.Pursuit, Pursuit },
            };
        }

        public void Execute() => _actions[_state].Invoke();

        private void Idle()
        {
            if (DistanceTo(_character.Position) < _radar.Radius)
            {
                _enemy.Agent.ResetPath();
                _radar.Clear.Execute();
                _state = State.Pursuit;
            }
            else if (DistanceTo(_patrolPosition) > _enemy.Agent.stoppingDistance)
            {
                _enemy.Agent.ResetPath();
                _radar.Draw.Execute();
                _state = State.Patrol;
            }
            else
            {
                if (_enemy.Agent.hasPath)
                {
                    return;
                }
                
                if (_delay > 0f)
                {
                    _delay -= Time.deltaTime;
                }
                else
                {
                    _delay = _maxDelay;
                    _enemy.Agent.SetDestination(_patrolPosition);
                }
            }
        }

        private void Patrol()
        {
            if (DistanceTo(_character.Position) < _radar.Radius)
            {
                _enemy.Agent.ResetPath();
                _radar.Clear.Execute();
                _state = State.Pursuit;
            }
            else
            {
                if (_enemy.Agent.hasPath)
                {
                    return;
                }
                
                if (_delay > 0f)
                {
                    _delay -= Time.deltaTime;
                }
                else
                {
                    _delay = _maxDelay;
                    _enemy.Agent.SetDestination(GeneratePointOnNavmesh());
                }
            }
        }

        private void Pursuit()
        {
            if (DistanceTo(_character.Position) > _radar.Radius)
            {
                _enemy.Agent.ResetPath();
                _radar.Draw.Execute();
                _state = State.Idle;
            }
            else
            {
                if (DistanceTo(_character.Position) < _enemy.Agent.stoppingDistance)
                {
                    if (_enemy.Agent.hasPath)
                    {
                        _enemy.Agent.ResetPath();
                    }
                }
                else
                {
                    _enemy.Agent.SetDestination(_character.Position);
                }
            }
        }
        
        private Vector3 GeneratePointOnNavmesh()
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 center = _patrolPosition + GenerateRandomPoint(5f);

                if (NavMesh.SamplePosition(center, out NavMeshHit hit, 1f, 1))
                {
                    return hit.position;
                }
            }
            
            return Vector3.zero;
        }
        
        private Vector3 GenerateRandomPoint(float radius)
        {
            float angle = UnityEngine.Random.Range(0f, 1f) * (2f * Mathf.PI) - Mathf.PI;
                    
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            return new Vector3(x, 0f, z);
        }

        private float DistanceTo(Vector3 position) => Vector3.Distance(_enemy.Position, position);
    }
}