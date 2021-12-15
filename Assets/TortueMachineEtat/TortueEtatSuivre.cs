using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TortueEtatSuivre : TortueEtatsBase
{

    private Coroutine suitPerso = null;
    private bool _controle = true;
    public override void InitEtat(TortueEtatsManager tortue)
    {
        suitPerso = tortue.StartCoroutine(Patience(tortue));
        _controle = false;
    }

    public override void UpdateEtat(TortueEtatsManager tortue)
    {

    }

    public IEnumerator Patience(TortueEtatsManager tortue)
    {
        //vitesse tortue    
        tortue.agent.speed = tortue.vitesse;

        //Trouve le perso et le suit
        tortue.agent.destination = tortue.cible.transform.position; 

        do
        {
            tortue.agent.destination = tortue.cible.transform.position;
            yield return new WaitForSeconds(0.2f);
        } while (tortue.agent.remainingDistance > tortue.distanceCible && !_controle);
        yield return new WaitForSeconds(0.2f);

        tortue.ChangerEtat(tortue.attend);
    }

    public void Arrete(TortueEtatsManager tortue)
    {
        if(suitPerso != null)
        {
            tortue.StopCoroutine(suitPerso);
            _controle = true;
            Debug.Log("STOOOOP");
        }
    }
}