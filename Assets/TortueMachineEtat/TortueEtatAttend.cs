using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TortueEtatAttend : TortueEtatsBase
{
    public override void InitEtat(TortueEtatsManager tortue)
    {
        Debug.Log("Je t'attend'");
        tortue.StartCoroutine(SuitLePerso(tortue));
    }

    public override void UpdateEtat(TortueEtatsManager tortue)
    {
                
    }

    private IEnumerator SuitLePerso(TortueEtatsManager tortue)
    {
        do
        {
            tortue.agent.speed = 0;
        } while (tortue.agent.remainingDistance == tortue.distanceCible || tortue.agent.pathPending);
        yield return null;

        tortue.ChangerEtat(tortue.suit);
    }
}