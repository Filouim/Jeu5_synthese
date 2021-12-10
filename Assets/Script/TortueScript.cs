using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class TortueScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    //La tortue ne fait que distraire les requins quand elle est controler par le joueur. Autrement, elle ne fait que suivre le joueur

    void Start()
    {
        Invoke("ActiverNavMesh", 2f);
    }

    void ActiverNavMesh()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        gameObject.GetComponent<TortueEtatsManager>().cible = _player;
        gameObject.GetComponent<TortueEtatsManager>().origine = _player.transform;
    }

}