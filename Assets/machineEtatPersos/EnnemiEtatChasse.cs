using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiEtatChasse : EnnemiEtatsBase
{
    public override void InitEtat(EnnemiEtatsManager ennemi)
    {
        ennemi.animator.SetBool("isRunning", true);
        ennemi.StartCoroutine(Anime(ennemi));
    }

    public override void TriggerEnterEtat(EnnemiEtatsManager ennemi, Collider other)
    {
        
    }

    private IEnumerator Anime(EnnemiEtatsManager ennemi)
    {
        // Vitesse de l'agent
        ennemi.agent.speed = ennemi.vitesseChasse;

        // Trouve la cible et la met en destination de l'agent
        ennemi.agent.destination = ennemi.cible.transform.position;

        // Tant que l'agent est a une certaine distance de la cible
        // ou bien que le path n'est pas encore calcule
        while (ennemi.agent.remainingDistance > ennemi.distanceCible || ennemi.agent.pathPending)
        {
            // Ajuste la destination sur la position de la cible
            ennemi.agent.destination = ennemi.cible.transform.position;

            if (MovePerso.instance.estInvincible)
            {
                ennemi.animator.SetBool("isRunning", false);
                ennemi.ChangerEtat(ennemi.promenade);
            }

            // Met a jour toutes les 0.2 secondes
            yield return new WaitForSeconds(0.2f);
        }

        ennemi.animator.SetBool("isAttacking", true);
        ennemi.animator.SetBool("isRunning", false);

        yield return new WaitForSeconds(1f);

        ennemi.animator.SetBool("isAttacking", false);
        ennemi.ChangerEtat(ennemi.promenade);
    }
}
