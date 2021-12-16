using System.Collections;
using UnityEngine;

public class BiomesEtatPlante : BiomesEtatsBase
{
    private Vector3 _positionBiome;
    private GameObject _unObject;
    public override void InitEtat(BiomesEtatsManager biome)
    {
        GameManager.instance.ObjectifProgresse();
        Object premierVariant = Resources.Load("Mats/Biomes/b"+biome.biomeMateriel+"_1");
        //fait pousser des plantes seulement dand le sable et les eponges
        if(premierVariant.name == "b5_1" || premierVariant.name == "b1_1"){
            //Pour reduire la quantite de plantes qui produise de l'oxygene
            switch(Random.Range(0, 6))
            {
                case 0: case 1:
                    _unObject = Resources.Load("Items/Plantes/plante"+Random.Range(1, 10)) as GameObject;
                    break;
                default:
                    biome.ChangerEtat(biome.final);
                    break;
            }
        }
        else
        {
            biome.ChangerEtat(biome.decor);
        }
        biome.GetComponent<Renderer>().material = (Material)premierVariant;
    }

    public override void UpdateEtat(BiomesEtatsManager biome)
    {

    }

    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider other)
    {
        biome.StartCoroutine(SwitcheMat(biome));
    }

    private IEnumerator SwitcheMat(BiomesEtatsManager biome)
    {
        _positionBiome = biome.transform.position;
        if(_unObject != null)
        {
            GameObject objet = GameObject.Instantiate(_unObject, new Vector3(_positionBiome.x, _positionBiome.y + .5f, _positionBiome.z), Quaternion.identity);
            
            //Change la taille des coraux
            int longueurRandom = Random.Range(4, 8);
            int hauteurRandom = Random.Range(5, 10);
            int profondeurRandom = Random.Range(4, 8);
            float t = 0.0f;
            do
            {
                t += Time.deltaTime;
                if(t >= 0f)
                {
                    objet.transform.localScale = new Vector3(longueurRandom * .5f * t, hauteurRandom * .5f * t, profondeurRandom * .5f * t);
                    Transform positionCube = biome.transform;
                    biome.ChangerEtat(biome.final);
                }
                yield return null;

            } while (t < 2.0f);

            int unePlante = objet.name.IndexOf("plante", 0, 6);

            if(unePlante == 0)
            {
                objet.AddComponent<generateurOxygene>();
            }
            // algues.transform.position = positionCube.position;
        }
        
        yield return null;
    }
}
