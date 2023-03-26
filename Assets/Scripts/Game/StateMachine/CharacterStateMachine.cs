using CodeBase.Game.Components;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.StateMachine
{
    public sealed class CharacterStateMachine
    {
        private readonly CCharacter _character;
        private CZombie _target;
        private Camera _camera;
        private float _delay;
        private float _gravity;
        private float _velocity;
        private float _angle;
        private bool _isAttack;

        public CharacterStateMachine(CCharacter character)
        {
            _character = character;
        }

        public void Init()
        {
            _camera = Camera.main;
            _delay = _character.Weapon.AttackRecharge;
            _gravity = Physics.gravity.y * 10f;
        }

        public void Tick()
        {
            Input();
            Move();
            Target();
            Rotate();
            Attack();
        }

        private void Input()
        {
            if (_character.Move.Input.sqrMagnitude > 0.5f)
            {
                _angle = Mathf.Atan2(_character.Move.Input.x, _character.Move.Input.y) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            }
        }

        private void Move()
        {
            Vector3 move = Vector3.zero;

            if (_character.Move.Input.sqrMagnitude > 0.5f)
            {
                move = Quaternion.Euler(0f, _angle, 0f) * Vector3.forward;

                Vector3 next = _character.transform.position + move * _character.Move.Speed * Time.deltaTime;
                        
                Ray ray = new Ray { origin = next, direction = Vector3.down };

                if (!Physics.Raycast(ray, 5f, Layers.Ground))
                {
                    return;
                }
            }

            move.y = _character.Move.CharacterController.isGrounded ? 0f : _gravity;

            _character.Move.CharacterController.Move(move * _character.Move.Speed * Time.deltaTime);
        }

        private void Target()
        {
            if (_character.Enemies.Count == 0)
            {
                _isAttack = false;
                
                return;
            }
            
            foreach (CZombie enemy in _character.Enemies)
            {
                if (Vector3.Distance(enemy.Position, _character.Position) < _character.Weapon.AttackDistance)
                {
                    _target = enemy;
                    
                    _isAttack = true;

                    break;
                }

                _isAttack = false;
            }
        }

        private void Rotate()
        {
            if (_isAttack)
            {
                Quaternion lookRotation = Quaternion.LookRotation(_target.Position - _character.Position);
                
                _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, lookRotation, 0.75f);
            }
            else
            {
                float smoothAngle = Mathf.SmoothDampAngle(_character.transform.eulerAngles.y, _angle, ref _velocity, 0.05f);

                _character.transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
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