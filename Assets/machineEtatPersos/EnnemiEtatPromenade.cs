using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiEtatPromenade : EnnemiEtatsBase
{
    public override void InitEtat(EnnemiEtatsManager ennemi)
    {
        ennemi.animator.SetBool("isWalking", true);
        ennemi.StartCoroutine(Anime(ennemi));
    }

        public override void TriggerEnterEtat(EnnemiEtatsManager ennemi, Collider other)
    {
        
    }

    private IEnumerator Anime(EnnemiEtatsManager ennemi)
    {
        // Vitesse de l'agent
        ennemi.agent.speed = ennemi.vitessePromenade;

        // Trouve la cible et la met en destination de l'agent
        ennemi.agent.destination = ennemi.origine.position;

        // Tant que l'agent est a plus de 2.5 unites de la cible
        // ou bien que le path n'est pas encore calcule
        while (ennemi.agent.remainingDistance > 2.5f || ennemi.agent.pathPending)
        {
            // Met a jour toutes les 0.2 secondes
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);
        
        ennemi.animator.SetBool("isWalking", false);
        ennemi.ChangerEtat(ennemi.repos);
    }
}
