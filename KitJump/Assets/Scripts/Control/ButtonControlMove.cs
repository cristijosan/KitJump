using UnityEngine;

namespace Control
{
    public class ButtonControlMove : MonoBehaviour
    {
        private FloatingJoystick floatingJoystick;

        private void Start()
        {
            floatingJoystick = gameObject.GetComponent<FloatingJoystick>();
        }

        private void FixedUpdate()
        {
            KitControl.SetMovementDirection?.Invoke(floatingJoystick.Horizontal, floatingJoystick.Vertical);
        }
    }
}