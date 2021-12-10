using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Permet de changer de perso;
public class CharacterSwapper : MonoBehaviour
{
    public Transform persoControllable;
    public List<Transform> persoEtTortue;
    public int iChooseU;

    private MovePerso _scriptScaphandre;
    private TortueScript _scriptTortue;
    private NavMeshAgent _navMesh;
    private CharacterController _tortueController;
    private CharacterController _persoController;

    private CameraFollow _instance;
    // Start is called before the first frame update
    void Start()
    {
        _instance = CameraFollow.instance;
        _scriptScaphandre = persoEtTortue[0].GetComponent<MovePerso>();
        _persoController = persoEtTortue[0].GetComponent<CharacterController>();
        _scriptTortue = persoEtTortue[1].GetComponent<TortueScript>();
        _navMesh = persoEtTortue[1].GetComponent<NavMeshAgent>();
        _tortueController = persoEtTortue[1].GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(iChooseU == 0)
            {
                iChooseU = persoEtTortue.Count - 1;
            }
            else
            {
                iChooseU = 0;
            }
            Swap();
        }
    }

    private void Swap()
    {
        persoControllable = persoEtTortue[iChooseU];
        //Active les bons components
        if(persoControllable == persoEtTortue[0])
        {
            _scriptScaphandre.enabled = true;
            _persoController.enabled = true;
        }
        else
        {
            _tortueController.enabled = true;
            _scriptTortue.enabled = true;
            _navMesh.enabled = false;
        }
        _instance.SwitchCamera();


        //Desactive les bons components
        if(persoControllable == persoEtTortue[0])
        {
            _tortueController.enabled = false;
            _scriptTortue.enabled = false;
            _navMesh.enabled = true;
        }
        else
        {
            _persoController.enabled = false;
            _scriptScaphandre.enabled = false;
        }
    }
}
