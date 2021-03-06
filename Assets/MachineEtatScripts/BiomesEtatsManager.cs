using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomesEtatsManager : MonoBehaviour
{
    private BiomesEtatsBase etatActuel;
    public BiomesEtatInitial initial = new BiomesEtatInitial();
    public BiomesEtatPlante plante = new BiomesEtatPlante();
    public BiomesEtatFinal final = new BiomesEtatFinal();
    public BiomesEtatDecor decor = new BiomesEtatDecor();

    public int biomeMateriel { get; set; }
    public GameObject perso { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        ChangerEtat(initial);
    }

    public void ChangerEtat(BiomesEtatsBase etat)
    {
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }

    // Update is called once per frame
    void Update()
    {
        etatActuel.UpdateEtat(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        etatActuel.TriggerEnterEtat(this, other);
    }
}
