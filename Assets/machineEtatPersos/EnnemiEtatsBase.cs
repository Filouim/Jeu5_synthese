using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnnemiEtatsBase
{
    public abstract void InitEtat(EnnemiEtatsManager ennemi);
    public abstract void TriggerEnterEtat(EnnemiEtatsManager ennemi, Collider other);
}
