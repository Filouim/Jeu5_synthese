using System.Collections;
using UnityEngine;

public class BiomesEtatDecor : BiomesEtatsBase
{

    private Vector3 _positionBiome;
    private GameObject _unObject;
    public override void InitEtat(BiomesEtatsManager biome)
    {
        int faitApparaitreObjet = Random.Range(0, 40);
        int typePlante = Random.Range(1, 10);

            //Fait apparaitre un plante seulement dans la situation donnée dans le switch case
        switch (faitApparaitreObjet)
        {
            case 0: 
                _unObject = Resources.Load("Items/coquilleI") as GameObject;
                break;
            case 1:
                _unObject = Resources.Load("Items/StarI") as GameObject;
                break;
            case 2:
                _unObject = Resources.Load("Items/Plantes/plante"+typePlante) as GameObject;
                break;
            default:
                break;
        }  

        biome.StartCoroutine(GrandirDecor(biome));
    }

    public override void UpdateEtat(BiomesEtatsManager biome)
    {
        
    }

    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider other)
    {

    }

    private IEnumerator GrandirDecor(BiomesEtatsManager biome)
    {
        _positionBiome = biome.transform.position;
        if(_unObject != null)
        {
            //Instancie le decor
            GameObject decor = GameObject.Instantiate(_unObject, new Vector3(_positionBiome.x, _positionBiome.y + .5f, _positionBiome.z), Quaternion.identity);            
            float t = 0.0f;
            //Boucle do while
            do
            {
                t += Time.deltaTime;
                if(t >= 0)
                {
                    if(decor.name == "coquille1(Clone)") decor.transform.localScale = new Vector3(3f * .5f * t, 3f * .5f * t, 3f * .5f * t);
                    else if (decor.name == "StarI(Clone)")
                    {
                        decor.transform.localScale = new Vector3
                            (6f * t, 6f * t, 6f * t);
                        decor.transform.eulerAngles = new Vector3(-90f, 0f, Random.Range(0f, 180f) * t);
                    }
                    else 
                    {
                        decor.transform.localScale = new Vector3(6f * .5f * t, 6f * .5f * t, 6f * .5f * t);
                    }
                }

                yield return null;
            } while (t <2.0f);
        }
        biome.ChangerEtat(biome.final);
        yield return null;
    }

}
