using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TortueEtatControler : TortueEtatsBase
{
    public override void InitEtat(TortueEtatsManager tortue)
    {
        Debug.Log("Je suis sous ton controle");
        tortue.StopAllCoroutines();
    }

    public override void UpdateEtat(TortueEtatsManager tortue)
    {
        float moveAxis = Input.GetAxisRaw("Vertical");
        float turnAxis = Input.GetAxisRaw("Horizontal");

        if(tortue.controller.isGrounded && tortue.velocite.y < 0)
        {
            tortue.velocite.y = -2f;
        }

        tortue.transform.Rotate(0, turnAxis * 180 * Time.deltaTime, 0);

        Vector3 directionMouvement = new Vector3(0, 0, moveAxis);
        directionMouvement = tortue.transform.TransformDirection(directionMouvement);
        directionMouvement *= tortue.vitesse;

        tortue.controller.Move(directionMouvement * Time.deltaTime);
        tortue.velocite.y -= tortue.gravite * Time.deltaTime;
        tortue.controller.Move(tortue.velocite * Time.deltaTime);
    }
}
