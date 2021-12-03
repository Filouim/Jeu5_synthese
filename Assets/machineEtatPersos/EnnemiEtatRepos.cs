using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiEtatRepos : EnnemiEtatsBase
{
    public override void InitEtat(EnnemiEtatsManager ennemi)
    {
        ennemi.StartCoroutine(Anime(ennemi));
    }
    public override void TriggerEnterEtat(EnnemiEtatsManager ennemi, Collider other)
    {
        
    }

    private IEnumerator Anime(EnnemiEtatsManager ennemi)
    {
        float impatience = Random.Range(2f, 8f);

        yield return new WaitForSeconds(impatience);
        
        ennemi.ChangerEtat(ennemi.chasse);
    }
}
