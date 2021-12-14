using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TortueEtatSuivre : TortueEtatsBase
{
            public override void InitEtat(TortueEtatsManager tortue)
            {
                        Debug.Log("Je te suis");
                        tortue.StartCoroutine(Patience(tortue));
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
                        } while (tortue.agent.remainingDistance > tortue.distanceCible);
                        yield return new WaitForSeconds(0.2f);

                        tortue.StopCoroutine(Patience(tortue));
                        tortue.ChangerEtat(tortue.attend);
            }
}