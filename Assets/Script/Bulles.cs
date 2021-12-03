using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulles : MonoBehaviour
{
    [SerializeField] private float _vitesseBulle = 0.5f;
    [SerializeField] private ParticleSystem _particulesBulle;

    public AudioSource _bullesPrise;
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.instance;
        _bullesPrise.GetComponent<AudioSource>();
        _particulesBulle.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _vitesseBulle * Time.deltaTime, Space.World);
        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _particulesBulle.Play();
            _gameManager.AjouterOxygene(25f);
            _bullesPrise.Play();
            Debug.Log("He touched me");
            Destroy(gameObject, 1f);
        }
    }
}
