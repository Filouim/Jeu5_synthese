using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTest : MonoBehaviour
{
    private List<List<Material>> biomesMats = new List<List<Material>>();
    // Start is called before the first frame update
    void Start()
    {
        int nbBiomes = 1;
        int nbVariant = 1;
        bool ResteDesMats = true;
        List<Material> tpBiome = new List<Material>();
        do
        {
            Object mot = Resources.Load("b"+nbBiomes+"_"+nbVariant);
            if(mot != null) //Tant que mot n'est pas null, qu'il y a un biome nomme comme celui-la
            {
                tpBiome.Add((Material)mot); //Ajoute mot  a la liste tpBiome
                Debug.Log(mot);
                nbVariant++; //Augmente nbVaraint de 1 pour voir si il y a d'autre dans ce biome
            } else {
                if(nbVariant == 1)
                {
                    ResteDesMats = false; //Si le nbVariant est encore egale a 1, cela veut dire qu'il n'y a pas d'autres biomes
                }
                else
                {
                    biomesMats.Add(tpBiome); //Ajoute tpBiome a biomeMats
                    tpBiome = new List<Material>(); //Reset tpBiome
                    nbVariant = 1; //Remet
                    nbBiomes++;
                }
            }
        } while (ResteDesMats);

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.GetComponent<Renderer>().material = biomesMats[0][1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
