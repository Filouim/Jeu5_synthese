using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A integrer a l'interieur de la classe GameManager.
/// </summary>
public class TestDegats : MonoBehaviour
{
    public float maxOxygene = 100f; // Niveau d'oxygene maximum
    public float oxygeneActuel; // Niveau d'oxygene actuel
    public float oxygenePerdu = 5f; // Niveau d'oxygene que le joueur pert a chaque coup
    public float delaiPerteOxygene = 1f; // Delai de la perte d'oxygene en secondes

    public NiveauOxygene barreOxygene; // Reference a la barre d'oxygene

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        oxygeneActuel = maxOxygene;
        barreOxygene.SetMaxOxygene(maxOxygene);

        StartCoroutine(Delai(oxygenePerdu, delaiPerteOxygene));
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) SubirDegats(20f);
        if (Input.GetKeyDown(KeyCode.I)) AjouterOxygene(20f);
    }

    /// <summary>
    /// Inflige des degats au joueur.
    /// </summary>
    /// <param name="degats"></param>
    private void SubirDegats(float degats)
    {
        oxygeneActuel -= degats;

        if (oxygeneActuel < 0) oxygeneActuel = 0;

        barreOxygene.SetOxygene(oxygeneActuel);
    }

    /// <summary>
    /// Donne de l'oxygene au joueur.
    /// </summary>
    /// <param name="ajout"></param>
    private void AjouterOxygene(float ajout)
    {
        oxygeneActuel += ajout;

        if (oxygeneActuel > maxOxygene) oxygeneActuel = maxOxygene;

        barreOxygene.SetOxygene(oxygeneActuel);
    }

    /// <summary>
    /// Appelle la fonction SubirDegats en boucle avec un delai.
    /// </summary>
    /// <param name="degats"></param>
    /// <param name="delai"></param>
    /// <returns></returns>
    private IEnumerator Delai(float degats, float delai)
    {
        while (true)
        {
            yield return new WaitForSeconds(delai);

            SubirDegats(degats);
        }
    }
}
