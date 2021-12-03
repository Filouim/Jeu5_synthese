using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TortueEtatsManager : MonoBehaviour
{
            private TortueEtatsBase etatActuel;
            public TortueEtatSuivre suit = new TortueEtatSuivre();
            public TortueEtatAttend attend = new TortueEtatAttend();

            public float vitesse = 15f;
            public float distanceCible = 5f;

            public GameObject cible { get; set; }
            public Transform origine { get; set; }
            public NavMeshAgent agent { get; set; }

            void Start()
            {
                        agent = GetComponent<NavMeshAgent>();
                        ChangerEtat(attend);
            }

            public void ChangerEtat(TortueEtatsBase etat)
            {
                        etatActuel = etat;
                        etatActuel.InitEtat(this);
            }
}