using System;
using Logic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Control
{
    [Serializable]
    public enum ButtonType {Jump, Attack}

    public class ButtonControlKit : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Animator _animator;
        [SerializeField] private ButtonType currentType;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _animator.SetBool("IsClicked", true);
            KitControl.Behavior?.Invoke(currentType);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _animator.SetBool("IsClicked", false);
        }
    }
}
