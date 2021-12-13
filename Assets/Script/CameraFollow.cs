using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update 

    [SerializeField] private float _vitesseRotCam = 5f;
    [SerializeField] private GameObject _player;

    private Vector3 _offset;

    public bool cameraPeutTourner = true;
    void Start()
    {
        _offset = transform.position - _player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Si au moins un des menus est actif, la camera ne bouge pas
        if (Menus._introActif || Menus._defaiteActif || Menus._victoireActif) cameraPeutTourner = false;
        else if (!Menus._introActif && !Menus._defaiteActif && !Menus._victoireActif) cameraPeutTourner = true;

        if(cameraPeutTourner)
        {
            Quaternion camTourne = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * _vitesseRotCam, Vector3.up);
            _offset = camTourne * _offset;
        }
        transform.position = _player.transform.position + _offset;

        if(cameraPeutTourner)
        {
            transform.LookAt(_player.transform);
        }
        // transform.RotateAround(Vector3.zero + _offset, transform.right, rotateVertical);
    }
}
