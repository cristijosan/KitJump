using System.Collections;
using Control;
using UnityEngine;

namespace Logic
{
    public class SwordPhysics : MonoBehaviour
    {
        public float speed = 2f;
        [SerializeField] private float interval = 30;
        [SerializeField] private Vector3 startPos, endPos;
        private Rigidbody2D _rb;
 
        private Vector3 targetPos;

        private void Awake() {
            targetPos = endPos;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            StartCoroutine(SpawnTimer());
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                KitControl.TakeDamage?.Invoke();
        }

        private void FixedUpdate() {
            var currentPos = transform.position;
 
            if(currentPos.y >= endPos.y)
                targetPos = startPos;
            if (currentPos.y <= startPos.y)
                targetPos = endPos;
 
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        
        private IEnumerator SpawnTimer() {
         
            while (true) {
                yield return new WaitForSeconds(interval);
                speed++;
            }
        }
    }
}
