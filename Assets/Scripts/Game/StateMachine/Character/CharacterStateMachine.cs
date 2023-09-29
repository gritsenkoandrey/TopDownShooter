using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Input;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateMachine
    {
        private readonly CCharacter _character;
        private readonly ICameraService _cameraService;
        private readonly IJoystickService _joystickService;
        private readonly IGameFactory _gameFactory;
        private IEnemy _target;

        private float _delay;
        private float _angle;
        private bool _isAttack;

        public CharacterStateMachine(CCharacter character, IGameFactory gameFactory, ICameraService cameraService, IJoystickService joystickService)
        {
            _character = character;
            _gameFactory = gameFactory;
            _cameraService = cameraService;
            _joystickService = joystickService;
        }

        public void Tick()
        {
            Move();
            Target();
            Rotate();
            Attack();
        }

        private void Move()
        {
            Vector3 move = Vector3.zero;

            if (_joystickService.GetAxis().sqrMagnitude > 0.1f)
            {
                _angle = Mathf.Atan2(_joystickService.GetAxis().x, _joystickService.GetAxis().y) * 
                    Mathf.Rad2Deg + _cameraService.Camera.transform.eulerAngles.y;

                move = Quaternion.Euler(0f, _angle, 0f) * Vector3.forward;

                Vector3 next = _character.Position + move * _character.Move.Speed * Time.deltaTime;
                        
                Ray ray = new Ray { origin = next, direction = Vector3.down };

                if (!Physics.Raycast(ray, 5f, Layers.Ground))
                {
                    return;
                }
            }

            move.y = _character.Move.IsGrounded ? 0f : Physics.gravity.y;

            _character.Move.CharacterController.Move(move * _character.Move.Speed * Time.deltaTime);
        }

        private void Target()
        {
            if (_gameFactory.Enemies.Count == 0)
            {
                _isAttack = false;
                
                return;
            }

            int index = -1;

            float attackDistance = _character.Weapon.AttackDistance * _character.Weapon.AttackDistance;
            float minDistance = attackDistance;

            for (int i = 0; i < _gameFactory.Enemies.Count; i++)
            {
                float distance = (_gameFactory.Enemies[i].Position - _character.Position).sqrMagnitude;

                if (distance < attackDistance)
                {
                    if (distance < minDistance)
                    {
                        index = i;
                        minDistance = distance;
                    }
                }
            }

            if (index >= 0)
            {
                _isAttack = true;
                _target = _gameFactory.Enemies[index];
            }
            else
            {
                _isAttack = false;
            }
        }

        private void Rotate()
        {
            if (_isAttack)
            {
                Quaternion lookRotation = Quaternion.LookRotation(_target.Position - _character.Position);
                _character.transform.rotation = Quaternion.Slerp(_character.Rotation, lookRotation, 0.75f);
            }
            else
            {
                float lerpAngle = Mathf.LerpAngle(_character.Angle, _angle, 0.25f);
                _character.transform.rotation = Quaternion.Euler(0f, lerpAngle, 0f);
            }
        }

        private void Attack()
        {
            if (_isAttack)
            {
                if (_delay < 0f)
                {
                    _character.Weapon.Shoot.Execute();
                    _delay = _character.Weapon.AttackRecharge;
                }
            }

            _delay -= Time.deltaTime;
        }
    }
}