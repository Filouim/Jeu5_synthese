using UnityEngine;
using System.Collections;

public class TortueScript : MonoBehaviour
{

    [SerializeField] private Vector3 _offset;

    private float distance = 6f;

    [SerializeField] private Transform _player;

    void LateUpdate()
    {
        transform.position = _player.position + _offset;


    }

}