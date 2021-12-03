using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateurOxygene : MonoBehaviour
{
    private GameObject _bulles;
    // Start is called before the first frame update
    void Start()
    {
        _bulles = Resources.Load("Items/bulles/bulleBase") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        int chancesBulles = Random.Range(0, 1000);
        if(chancesBulles >= 999)
        {
            Instantiate(_bulles, transform.position, Quaternion.identity);
        }
        _bulles.transform.Translate(Vector3.up * 10 * Time.deltaTime, Space.World);
    }
}
