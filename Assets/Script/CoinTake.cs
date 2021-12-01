using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTake : MonoBehaviour
{
    private bool _courtApres = false;
    public AudioSource _coin;
    public GameObject perso;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DonneCollider", 2f);
        _gameManager = GameManager.instance;
        _coin.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_courtApres)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, perso.transform.position, 12 * Time.deltaTime);
            if(Vector3.Distance(transform.position, perso.transform.position) < 1f )
            {
                //Appeler une fonction du gameManager pour les points
                Destroy(gameObject);
                // Debug.Log(_manager);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _courtApres = true;
        _coin.Play();
        _gameManager.AjouterPoints(1);
    }

    private void DonneCollider()
    {
        MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        collider.convex = true;
        collider.isTrigger = true;
    }
}
