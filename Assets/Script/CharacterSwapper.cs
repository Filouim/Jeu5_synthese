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

    private CameraFollow _instance;
    // Start is called before the first frame update
    void Start()
    {
        _instance = CameraFollow.instance;
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
        persoControllable.GetComponent<CharacterController>().enabled = true;
        persoControllable.GetComponent<MovePerso>().enabled = true;
        Debug.Log(persoControllable.GetComponent<NavMeshAgent>());
        if(persoControllable.GetComponent<NavMeshAgent>() != null)
        {
            persoControllable.GetComponent<NavMeshAgent>().enabled = false;
        }
        _instance.SwitchCamera();

        for(int i = 0; i < persoEtTortue.Count; i++)
        {
            if(persoEtTortue[i] != persoControllable)
            {
                Debug.Log(persoControllable);
                Debug.Log(persoEtTortue[i]);
                persoEtTortue[i].GetComponent<CharacterController>().enabled = false;
                persoEtTortue[i].GetComponent<MovePerso>().enabled = false;
                if(persoEtTortue[i].GetComponent<NavMeshAgent>() != null)
                {
                    persoEtTortue[i].GetComponent<NavMeshAgent>().enabled = true;
                }
            }
        }
    }
}
