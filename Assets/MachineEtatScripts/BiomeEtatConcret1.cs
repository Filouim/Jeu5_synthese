using System.Collections;
using UnityEngine;

public class BiomesEtatConcret1 : BiomesEtatsBase
{

    public float positionFumee = 1f;
    private Vector3 _positionBiome;
    private GameObject _unDechet;
    private GameObject _leDechet;

    public override void InitEtat(BiomesEtatsManager biome)
    {
        switch (Random.Range(0, 30))
        {
            case 0:
                _unDechet = Resources.Load("Items/baril") as GameObject;
                break;
            case 1:
                _unDechet = Resources.Load("Items/colonneI") as GameObject;
                break; 

                    case 2 :
                _unDechet = Resources.Load("Items/bouteille") as GameObject;
                break;
                
             case 3 : 
                 _unDechet = Resources.Load("Items/prop") as GameObject;
                break;

                default: 
                break;
        }

        _positionBiome = biome.transform.position;

        if (_unDechet != null)
        {
            GameObject _leDechet = GameObject.Instantiate(_unDechet, new Vector3(_positionBiome.x, _positionBiome.y + 1f, _positionBiome.z), Quaternion.identity);
            _leDechet.transform.localScale = _leDechet.transform.localScale / 5;
            if (_unDechet.name == "colonneI")
            {
                _leDechet.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            }
            int rotRand = Random.Range(0, 4) * 90;
            int coucherRand = Random.Range(0, 4) * 90;
            _leDechet.transform.rotation = Quaternion.Euler(coucherRand, rotRand, -rotRand);
        }
    }

    public override void UpdateEtat(BiomesEtatsManager biome)
    {

    }

    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider other)
    {
        biome.StartCoroutine(changementBiome(biome)); //On demarre la Coroutine changementBiome des que biome (le cube) sent un contact
    }

    //THOMAS ST-PIERRE
    private IEnumerator changementBiome(BiomesEtatsManager biome)
    {
        //Fait apparaitre un dechet au hasard 
        var particule = Resources.Load<ParticleSystem>("particule/smoke") as ParticleSystem;
        ParticleSystem laFumee = GameObject.Instantiate(particule, new Vector3(_positionBiome.x, _positionBiome.y + positionFumee, _positionBiome.z), Quaternion.identity);
        laFumee.transform.parent = biome.transform;

        laFumee.Play(); //On demarre le systeme de poussiereBiohazard de biomes a l'instant ou le trigger est activer
        AudioSource nettoyage = biome.GetComponent<AudioSource>();
        nettoyage.Play();
        float t = 0.0f; //On cree une variable qui representera le temps en secondes et qui sera utiliser par une boucle while

        yield return new WaitForSeconds(1f); //On attend quelque secondes avant le de commencer la boucle
        do
        {
            t += Time.deltaTime; //On augmente la variable t, en temps réel

            if (t > 1.0f) //Lorsque t est plus grand que 1.0f (1 secondes)
            {
                biome.ChangerEtat(biome.etat2); //On change l'état du biome, ce qui va lui donnant une nouvelle texture, selon sa position

                //On change la rotation du cube biome pour qu'elle soit bien droite (c'est pour empecher d'avoir un terrain "accidentée")
                int randomRot = Random.Range(0, 4) * 90;
                biome.transform.rotation = Quaternion.Euler(0f, randomRot, 0f);

            }

            //On fait une rotation en live, en multipliant la variable tourne sur tout ses axes, en utilisant Quaternion.AngleAxis et en multipliant sa valeur t avec 360

        } while (t < 1.0f); //On continu la boucle, tant que t est plus petit que 1.0f
        yield return null;
    }
}
