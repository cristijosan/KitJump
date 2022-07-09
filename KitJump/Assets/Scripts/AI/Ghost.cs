using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class Ghost : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public float playerDistanceRun = 2.0f;
        public GameObject player;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void Update()
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);
            
            if (!(distance < playerDistanceRun))
                return;
            
            var dirToPlayer = transform.position - player.transform.position;
            var newPos = transform.position + dirToPlayer;
            _agent.SetDestination(newPos);
        }
    }
}
