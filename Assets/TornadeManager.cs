using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class TornadeManager : MonoBehaviour
{
    public NavMeshAgent tornade;

    [Range(0, 100)] public float speed;

    [Range(1, 500)] public float radius;

    void Start()
    {
        tornade = GetComponent<NavMeshAgent>();
        if (tornade != null)
        {
            tornade.speed = speed;
            tornade.SetDestination(RandomLoc());
        }
    }

    void Update()
    {
        if (tornade != null && tornade.remainingDistance <= tornade.stoppingDistance)
        {
            tornade.SetDestination(RandomLoc());
        }
    }

    public Vector3 RandomLoc()
    {
        Vector3 final = Vector3.zero;
        Vector3 randomPos = Random.insideUnitSphere * radius;
        randomPos += transform.position;

        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, radius, 1))
        {
            final = hit.position;
        }

        return final;

    }




}
