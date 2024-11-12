using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    Rigidbody2D Rigidbody2D;
    NavMeshAgent NavMeshAgent;
    public float StoppingDistance = 1f;
    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.updateRotation = false;
        NavMeshAgent.updateUpAxis = false;
        NavMeshAgent.stoppingDistance = StoppingDistance;
        HealthComponent healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnStun += OnStun;
    }
    float lastTimeStunned = 0f;
    bool stunned =false;
    private void Update()
    {
        if (lastTimeStunned + 0.5f < Time.time)
        {
            stunned = false;
        }
    }
    private void OnStun()
    {
        stunned = true;
    }

    
public void Move(Vector2 target, float speed)
    {
        if (stunned)
        {
            Rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            NavMeshAgent.isStopped = false;
            NavMeshAgent.SetDestination(target);
            NavMeshAgent.speed = speed;
            if (Rigidbody2D.velocity.magnitude > 0)
            {
                SoundManager.PlaySound(SoundManager.Sound.EnemyMove, transform.position);
            }
        }
       
    }
    public void Stop()
    {
        if(NavMeshAgent==null)
            NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.isStopped = true;
    }

}
