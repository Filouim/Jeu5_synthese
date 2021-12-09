using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Permet de changer de perso;
public class CharacterSwapper : MonoBehaviour
{
    public Transform persoControllable;
    public List<Transform> persoEtTortue;
    public int iChooseU;
    // Start is called before the first frame update
    void Start()
    {
        
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
                iChooseU = 1;
            }
            Swap();
        }
    }

    private void Swap()
    {
        persoControllable = persoEtTortue[iChooseU];
        persoControllable.GetComponent<CharacterController>().enabled = true;

        for(int i = 0; i < persoEtTortue.Count; i++)
        {
            if(persoEtTortue[i] != persoControllable)
            {
                persoEtTortue[i].GetComponent<CharacterController>().enabled = false;
                Debug.Log(persoControllable);
            }
        }
    }
}
