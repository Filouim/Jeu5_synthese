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
    private TortueEtatsManager managerTortue;
    private NavMeshAgent _navMesh;
    private CharacterController _tortueController;
    private CharacterController _persoController;

    // private EnnemiEtatsManager

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
        managerTortue = persoEtTortue[1].GetComponent<TortueEtatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(iChooseU == 0)
            {
                iChooseU = persoEtTortue.Count - 1;
                GameManager.instance.ChangerLaCible(1);
            }
            else
            {
                iChooseU = 0;
                GameManager.instance.ChangerLaCible(0);
            }
            Swap();
        }
    }

    private void Swap()
    {
        persoControllable = persoEtTortue[iChooseU];
        //Active et desactive les bons components
        if(persoControllable == persoEtTortue[0])
        {
            _scriptScaphandre.enabled = true;
            _persoController.enabled = true;
        }
        else
        {
            _tortueController.enabled = true;
            _scriptTortue.enabled = true;
            managerTortue.ChangerEtat(managerTortue.sousMonControle);
        }
        _instance.SwitchCamera();


        //Desactive les bons components
        if(persoControllable == persoEtTortue[0])
        {
            _tortueController.enabled = false;
            _scriptTortue.enabled = false;
            managerTortue.ChangerEtat(managerTortue.suit);
        }
        else
        {
            _persoController.enabled = false;
            _scriptScaphandre.enabled = false;
        }
    }
}
