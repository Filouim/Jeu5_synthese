using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulles : MonoBehaviour
{
    public AudioSource _bullesPrise;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.instance;
        _bullesPrise.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * 2 * Time.deltaTime, Space.World);
        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _gameManager.AjouterOxygene(25f);
            _bullesPrise.Play();
            Debug.Log("He touched me");
            Destroy(gameObject, 1f);
        }
    }
}
