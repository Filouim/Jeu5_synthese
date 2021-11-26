using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dauphin : MonoBehaviour
{

    private float _vitDeplacement = 1f;
    private Renderer _leDauphin;
    private bool _activeDauphin = false;
    private Vector3 _positionDeBase;
    // Start is called before the first frame update
    void Start()
    {
        _leDauphin = GetComponentInChildren<SkinnedMeshRenderer>();
        _positionDeBase = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(_leDauphin.isVisible)
        {
            _activeDauphin = true;
            StartCoroutine(DauphinBouge());
        } else 
        {
            _activeDauphin = false;
            StopCoroutine(DauphinBouge());
        }
    }

    private IEnumerator DauphinBouge()
    {
        float t = 0.0f;
        do
        {
            t += Time.deltaTime * _vitDeplacement;

            float x = (Mathf.Cos(t) * 5) + _positionDeBase.x;
            float z = (Mathf.Sin(t) * 5) + _positionDeBase.z;

            transform.position = new Vector3(x , _positionDeBase.y + 3f, z);

            Quaternion rotation = Quaternion.LookRotation(_positionDeBase);
            transform.rotation = rotation;
            yield return null;
        } while (_activeDauphin);
        yield return null;
    }
}
