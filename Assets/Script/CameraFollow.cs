using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update 

    [SerializeField] private float _vitesseRotCam = 5f;
    [SerializeField] private List<GameObject> _player = new List<GameObject>();

    private Vector3 _offset;

    public bool cameraPeutTourner = true;

    public int _persoSousControle = 0;
    private static CameraFollow _instance;
    public static CameraFollow instance => _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        _offset = transform.position - _player[_persoSousControle].transform.position;
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
        transform.position = _player[_persoSousControle].transform.position + _offset;

        if(cameraPeutTourner)
        {
            transform.LookAt(_player[_persoSousControle].transform);
        }
        // transform.RotateAround(Vector3.zero + _offset, transform.right, rotateVertical);
    }

    public void SwitchCamera()
    {
        if(_persoSousControle == 0)
        {
            _persoSousControle++;
        }
        else
        {
            _persoSousControle--;
        }
    }
}
