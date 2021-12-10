using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class TortueScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private float _vitesse = 10f;
    private float gravite = 0.2f;
    private Vector3 velocite;
    CharacterController controller;

    //La tortue ne fait que distraire les requins quand elle est controler par le joueur. Autrement, elle ne fait que suivre le joueur

    void Start()
    {
        Invoke("ActiverNavMesh", 1f);
        controller = GetComponent<CharacterController>();
    }

    void ActiverNavMesh()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        gameObject.GetComponent<TortueEtatsManager>().cible = _player;
        gameObject.GetComponent<TortueEtatsManager>().origine = _player.transform;
        gameObject.GetComponent<TortueScript>().enabled = false;
    }

    void FixedUpdate()
    {
        float moveAxis = Input.GetAxisRaw("Vertical");
        float turnAxis = Input.GetAxisRaw("Horizontal");

        if(controller.isGrounded && velocite.y < 0)
        {
            velocite.y = -2f;
        }

        transform.Rotate(0, turnAxis * 180 * Time.deltaTime, 0);

        Vector3 directionMouvement = new Vector3(0, 0, moveAxis);
        directionMouvement = transform.TransformDirection(directionMouvement);
        directionMouvement *= _vitesse;

        controller.Move(directionMouvement * Time.deltaTime);
        velocite.y -= gravite * Time.deltaTime;
        controller.Move(velocite * Time.deltaTime);
    }

}