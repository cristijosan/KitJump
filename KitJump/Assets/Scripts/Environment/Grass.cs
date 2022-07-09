using System;
using UnityEngine;

namespace Environment
{
    public class Grass : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Sword"))
                _animator.SetTrigger("Shake");
        }
    }
}
