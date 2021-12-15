using System.Collections;
using UnityEngine;

public class BiomesEtatConcret2 : BiomesEtatsBase
{
    private Vector3 _positionBiome;
    private GameObject _unObject;
    public override void InitEtat(BiomesEtatsManager biome)
    {
        GameManager.instance.ObjectifProgresse();
        Object premierVariant = Resources.Load("Mats/Biomes/b"+biome.biomeMateriel+"_1");
        int nbRandom = Random.Range(1,10);
        if(premierVariant.name == "b5_1"){
            _unObject = Resources.Load("Items/Plantes/plante"+nbRandom) as GameObject;
        } else {
            int faitApparaitreObjet = Random.Range(0, 20);

            //Fait apparaitre un plante seulement dans la situation donnée dans le switch case
            switch (faitApparaitreObjet)
            {
                case 0: 
                    _unObject = Resources.Load("Items/Plantes/plante"+nbRandom) as GameObject;
                    break;
                case 1: 
                    _unObject = Resources.Load("Items/coquilleI") as GameObject;
                    break;
                case 2:
                    _unObject = Resources.Load("Items/StarI") as GameObject;
                    break;
                default:
                    break;
            }
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
            GameObject objet = GameObject.Instantiate(_unObject, new Vector3(_positionBiome.x, _positionBiome.y + 1f, _positionBiome.z), Quaternion.identity);
            
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
        biome.ChangerEtat(biome.etat3);
        yield return null;
    }
}
