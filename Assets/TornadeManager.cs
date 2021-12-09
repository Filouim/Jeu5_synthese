using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class TornadeManager : MonoBehaviour
{
    public NavMeshAgent tornade;

    public GameObject player;

    [Range(0, 100)] public float speed;

    [Range(1, 500)] public float radius;

    private bool collided;

    private bool _in;

    public CharacterController controller;

    public Rigidbody rb;

    private GameManager _gameManager;

    void Start()
    {
        rb.isKinematic = true;
        _gameManager = GameManager.instance;

        tornade = GetComponent<NavMeshAgent>();
        if (tornade != null)
        {
            tornade.speed = speed;
            tornade.SetDestination(RandomLoc());
        }
    }

    void FixedUpdate()
    {
        if (tornade != null && tornade.remainingDistance <= tornade.stoppingDistance)
        {
            tornade.SetDestination(RandomLoc());
        }

        if (collided)
        {

            StartCoroutine(PersoRoutine());
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


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collided = true;
            _in = true;
            _gameManager.SubirDegats(20f);
        }


    }










    private IEnumerator PersoRoutine()
    {


        if (_in)
        {
            player.transform.position = new Vector3(transform.position.x, 9, transform.position.z);


        }
        controller.enabled = false;
        rb.isKinematic = false;

        player.transform.Rotate(0, 30, 0);

        yield return new WaitForSeconds(1f);

        _in = false;

        yield return new WaitForSeconds(0.1f);
        if (!_in)
        {

            rb.AddForce(transform.up * 0.6f, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(2f);
        rb.isKinematic = true;
        collided = false;
        controller.enabled = true;

        yield return new WaitForSeconds(0.1f);

    }




}
