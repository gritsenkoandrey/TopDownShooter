﻿using System;
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

        private Dictionary<State, Action> _actions;
        
        private State _state;
        
        private Vector3 _patrolPosition;
        private float _maxDelay;
        private float _delay;
        private float _minDistance;
        private float _patrolRadius;

        public EnemyStateMachine(CEnemy enemy, CCharacter character)
        {
            _enemy = enemy;
            _character = character;
        }

        public void Init()
        {
            _state = State.Idle;
            _patrolPosition = _enemy.transform.position;
            _maxDelay = 1f;
            _delay = _maxDelay;
            _minDistance = 1f;
            _patrolRadius = 5f;

            _actions = new Dictionary<State, Action>
            {
                { State.Idle, Idle },
                { State.Patrol, Patrol },
                { State.Pursuit, Pursuit },
            };
        }

        public void Tick() => _actions[_state].Invoke();

        private void Idle()
        {
            if (_delay > 0f)
            {
                _delay -= Time.deltaTime;
            }
            else
            {
                _enemy.Agent.enabled = true;

                if (Distance() < _enemy.Radar.Radius)
                {
                    PursuitState();
                }
                else
                {
                    PatrolState();
                }
            }
        }

        private void Patrol()
        {
            if (Distance() < _enemy.Radar.Radius)
            {
                PursuitState();
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
            if (Distance() > _enemy.Radar.Radius)
            {
                PatrolState();
            }
            else
            {
                if (Distance() < _minDistance)
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
                Vector3 center = _patrolPosition + GenerateRandomPoint(_patrolRadius);

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

        private float Distance() => Vector3.Distance(_enemy.Position, _character.Position);

        private void PursuitState()
        {
            _enemy.Agent.ResetPath();
            _enemy.Radar.Clear.Execute();
            _state = State.Pursuit;
        }

        private void PatrolState()
        {
            _enemy.Agent.ResetPath();
            _enemy.Radar.Draw.Execute();
            _state = State.Patrol;
        }
    }
}