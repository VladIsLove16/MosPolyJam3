using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{

    NavMeshAgent NavMeshAgent;
    public float StoppingDistance = 1f;
    private void Start()
    {

        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.updateRotation = false;
        NavMeshAgent.updateUpAxis = false;
        NavMeshAgent.stoppingDistance = StoppingDistance;
    }

    public void Move(Vector2 target, float speed)
    {
        NavMeshAgent.isStopped = false; 
        NavMeshAgent.SetDestination(target);
        NavMeshAgent.speed = speed;
    }
    public void Stop()
    {
        NavMeshAgent.isStopped  = true;
    }

}
