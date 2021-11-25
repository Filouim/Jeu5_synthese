using System.Collections;
using UnityEngine;

public class BiomesEtatConcret3 : BiomesEtatsBase
{
    public override void InitEtat(BiomesEtatsManager biome)
    {
        Object deuxiemeVariant = Resources.Load("Mats/Biomes/b"+biome.biomeMateriel+"_"+2);

        if(deuxiemeVariant != null)
        {
            biome.GetComponent<Renderer>().material = (Material)deuxiemeVariant;
        }        
    }

    public override void UpdateEtat(BiomesEtatsManager biome)
    {
        
    }

    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider other)
    {
        biome.StartCoroutine(ApparitionOr(biome));
    }

    private IEnumerator ApparitionOr(BiomesEtatsManager biome)
    {
        yield return null;
    }
}
