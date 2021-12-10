using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dechet : MonoBehaviour
{
    [SerializeField] private int _rotationMax;
    [SerializeField] private int _rotationMin; 


     private GameObject _tornade;


    private int _vitesseRotation;
    private bool _peutPartir = false;

        private bool _peutPartirTor = false;
    private int _destruction;
    private float _vitesseDepart;
    // Start is called before the first frame update
    void Start()
    { 

        _tornade = GameObject.Find("tornade");
        _vitesseRotation = Random.Range(_rotationMin, _rotationMax + 1);
        _destruction = Random.Range(3, 10);
        _vitesseDepart = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(_peutPartir)
        {
            transform.Translate(Vector3.up * _vitesseDepart * Time.deltaTime, Space.World); //La vitesse du dechet 
            transform.Rotate(Vector3.forward * _vitesseRotation * Time.deltaTime, Space.Self); //La vitesse de rotation du dechet
        } 

         if(_peutPartirTor)
        {
             transform.Translate(Vector3.up * _vitesseDepart * Time.deltaTime, Space.World);
            transform.RotateAround(_tornade.transform.position,Vector3.up, 1000 * Time.deltaTime); //La vitesse de rotation du dechet 
          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "champDeForce"){
            _peutPartir = true;
            Destroy(gameObject, _destruction);
        } 

         if(other.gameObject.tag == "champTornade"){
            _peutPartirTor = true;
            Destroy(gameObject, _destruction);
        }
    }
}
