using Logic;
using UnityEngine;

namespace AI
{
    public class GhostHp : MonoBehaviour
    {
        private int ghostHP = 2;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        // Damage
        public void TakeGhostDamage()
        {
            ghostHP -= 1;

            if (ghostHP < 0)
            {
                _animator.SetTrigger("Hit");
                _animator.SetBool("Died", true);
                GetComponent<BoxCollider2D>().enabled = false;
                Game.PlusScore?.Invoke();
                Game.SpawnOther?.Invoke();
            }
            else
                _animator.SetTrigger("Hit");
        }

        public void DestroyGo()
        {
            Destroy(gameObject);
        }
    }
}
