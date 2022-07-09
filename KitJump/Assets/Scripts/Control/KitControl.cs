using System;
using System.Collections;
using System.Threading.Tasks;
using Logic;
using UnityEngine;
using AI;

namespace Control
{
    public class KitControl : MonoBehaviour
    {
        public static Action<float, float> SetMovementDirection;
        public static Action TakeDamage;
        public static Action<ButtonType> Behavior;
        
        [Header("Movement")]
        public float runSpeed = 5.0f;
        
        [Header("Attack")]
        private int kitHP = 1;
        public Transform attackPos;
        public LayerMask enemy;
        public float attackRange;
        
        // Components
        private Rigidbody2D _rb;
        private Animator _animator;
        // Other
        private float _horizontal;
        private float _vertical;
        private bool _isJumping;

        private void Awake()
        {
            SetMovementDirection += SetDirection;
            TakeDamage += TakeKitDamage;
            Behavior += KitBehavior;
        }

        private void OnDestroy()
        {
            SetMovementDirection -= SetDirection;
            TakeDamage -= TakeKitDamage;
            Behavior -= KitBehavior;
        }

        private void Start ()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        // Move
        private void SetDirection(float horizontal, float vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }

        private void Move()
        {
            _rb.velocity = new Vector2(_horizontal * runSpeed, _vertical * runSpeed);
            _animator.SetFloat("Speed", _rb.velocity.magnitude);

            if (_horizontal > 0)
                transform.localScale = new Vector3(1, 1, 1);
            if (_horizontal < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        // Jump
        private void Jump()
        {
            if (_isJumping)
                return;

            _isJumping = true;
            _animator.SetTrigger("Jump");
            StartCoroutine(RestoreAfterJump());
        }

        private IEnumerator RestoreAfterJump()
        {
            yield return new WaitForSeconds(.5f);
            _isJumping = false;
        }
        
        // Attack
        private void Attack()
        {
            _animator.SetTrigger("Attack");
            var enemiesDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
            foreach (var t in enemiesDamage)
                if (t.GetComponent<GhostHp>() != null)
                    t.GetComponent<GhostHp>().TakeGhostDamage();
        }
        
        // Damage
        private async void TakeKitDamage()
        {
            if (_isJumping)
                return;;
            
            kitHP -= 1;

            if (kitHP < 0)
            {
                _animator.SetTrigger("Hit");
                _animator.SetBool("Died", true);
                await Task.Delay(1000);
                Game.OnLose?.Invoke();
            }
            else
                _animator.SetTrigger("Hit");
        }

        // Behavior
        private void KitBehavior(ButtonType type)
        {
            switch(type)
            {
                case ButtonType.Jump:
                    Jump();
                    break;
                case ButtonType.Attack:
                    Attack();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }
    }
}
