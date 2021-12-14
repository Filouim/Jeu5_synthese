using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiEtatsManager : MonoBehaviour
{
    private EnnemiEtatsBase etatActuel;
    public EnnemiEtatRepos repos = new EnnemiEtatRepos();
    public EnnemiEtatPromenade promenade = new EnnemiEtatPromenade();
    public EnnemiEtatChasse chasse = new EnnemiEtatChasse();
    public float vitesseChasse = 7f;
    public float vitessePromenade = 5f;
    public float distanceCible = 5f;

    public List<GameObject> cible { get; set; }
    public Transform origine { get; set; }
    public NavMeshAgent agent { get; set; }
    public Animator animator { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ChangerEtat(repos);
    }

    public void ChangerEtat(EnnemiEtatsBase etat)
    {
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        etatActuel.TriggerEnterEtat(this, other);
    }
}
