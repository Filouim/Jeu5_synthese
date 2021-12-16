using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//THOMAS ST-PIERRE
public class TortueEtatsManager : MonoBehaviour
{
    private TortueEtatsBase etatActuel;
    public TortueEtatSuivre suit = new TortueEtatSuivre();
    public TortueEtatAttend attend = new TortueEtatAttend();
    public TortueEtatControler sousMonControle = new TortueEtatControler();


    public float vitesse = 15f;
    public float distanceCible = 25f;
    
    
    
    public CharacterController controller;
    public float gravite = 0.2f;
    public Vector3 velocite;

    public GameObject cible { get; set; }
    public Transform origine { get; set; }
    public NavMeshAgent agent { get; set; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangerEtat(attend);
    }

    void Update()
    {
        etatActuel.UpdateEtat(this);
    }

    public void ChangerEtat(TortueEtatsBase etat)
    {
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }
}