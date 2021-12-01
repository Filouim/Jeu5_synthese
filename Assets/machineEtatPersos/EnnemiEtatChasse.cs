using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiEtatChasse : EnnemiEtatsBase
{
    private MovePerso perso;
    private Coroutine _stop;

    public override void InitEtat(EnnemiEtatsManager ennemi)
    {
        _stop = ennemi.StartCoroutine(Anime(ennemi));
        ennemi.animator.SetBool("isRunning", true);
    }

    public override void TriggerEnterEtat(EnnemiEtatsManager ennemi, Collider other)
    {
        if(other.tag == "Attaque")
        {
            ennemi.StopCoroutine(_stop);
            ennemi.ChangerEtat(ennemi.promenade);
        }
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

            // Met a jour toutes les 0.2 secondes
            yield return new WaitForSeconds(0.2f);
        }

        ennemi.animator.SetBool("isAttacking", true);
        // GameManager.instance.SubirDegats(25f);
        yield return new WaitForSeconds(3f);

        ennemi.animator.SetBool("isRunning", false);
        ennemi.animator.SetBool("isAttacking", false);
        ennemi.ChangerEtat(ennemi.promenade);
    }


}
