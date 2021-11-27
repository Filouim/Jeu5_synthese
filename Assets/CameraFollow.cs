using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update 

    [SerializeField] private GameObject _player;

    private Vector3 _offset;
    void Start()
    {
        _offset = transform.position - _player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}
