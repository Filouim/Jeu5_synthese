using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiEtatChasse : EnnemiEtatsBase
{
    public override void InitEtat(EnnemiEtatsManager ennemi)
    {
        ennemi.StartCoroutine(Anime(ennemi));
        ennemi.animator.SetBool("isRunning", true);
    }

    private IEnumerator Anime(EnnemiEtatsManager ennemi)
    {
        // Vitesse de l'agent
        ennemi.agent.speed = ennemi.vitesseChasse;

        // Trouve la cible et la met en destination de l'agent
        ennemi.agent.destination = ennemi.cible.transform.position;

        // Tant que l'agent est a plus de 2.5 unite de la cible
        // ou bien que le path n'est pas encore calcule
        while (ennemi.agent.remainingDistance > 2.5f || ennemi.agent.pathPending)
        {
            // Ajuste la destination sur la position de la cible
            ennemi.agent.destination = ennemi.cible.transform.position;

            // Met a jour toutes les 0.2 secondes
            yield return new WaitForSeconds(0.2f);
        }

        ennemi.animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(3f);

        ennemi.animator.SetBool("isRunning", false);
        ennemi.animator.SetBool("isAttacking", false);
        ennemi.ChangerEtat(ennemi.promenade);
    }
}
