using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THOMAS ST-PIERRE
public abstract class TortueEtatsBase
{
    public abstract void InitEtat(TortueEtatsManager tortue);
    public abstract void UpdateEtat(TortueEtatsManager tortue);
}
