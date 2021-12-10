using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateurIles : MonoBehaviour
{
    public bool estExplorer;
    public int largeurIle = 10;
    public int profondeurIle = 10;
    public GameObject cube;
    public Renderer textureRenderer;
    public float attenuateur;
    public int coefAltitude = 6;
    public GameObject dauphin;
    public GameObject perso;
    public GameObject tortue; //#tim Thomas
    public float tailleEnnemiMin = 0.4f;
    public float tailleEnnemiMax = .75f;

    private List<List<Material>> biomesMats = new List<List<Material>>();

    public List<List<Material>> _biomesMats => biomesMats;
    private GameObject _unDauphin; //Pour que le dauphin suivent le joueur du regard
    private int apparaitreDauphin = 0; //Nous permettra de faire apparaitre le dauphin
    private int _objectif; //#tim Thomas Servira a calculer l'objectif du jeu


    public Material pasExplorer; //Permet de changer les texture de l'ile, quelle soit explorer ou non


    void Start()
    {
        GenererListeNaterielsBiomes();
        CreerMap();
        GetComponent<NavMeshSurface>().BuildNavMesh();
        // ApparaitreTortueEtIntelligence();
    }

    void Update()
    {
        _unDauphin.transform.LookAt(perso.transform.position);
    }

    // void ApparaitreTortueEtIntelligence()
    // {
    //     GameObject laTortue = Instantiate(tortue, new Vector3(0, 3f, 0), Quaternion.identity);
    //     Debug.Log(laTortue);
    //     laTortue.GetComponent<TortueEtatsManager>().cible = perso;
    //     laTortue.GetComponent<TortueEtatsManager>().origine = perso.transform;
    // }

    //THOMAS ST-PIERRE Genere une liste de biomes selon le dossier Resources
    void GenererListeNaterielsBiomes()
    {
        int nbBiomes = 1; //Sera utiliser pour aller chercher les biomes
        int nbVariant = 1; //Sera utiliser pour aller chercher tout les biomes et ses variantes, ainsi que de savoir si il reste des materiaux
        bool ResteDesMats = true; //Utiliser pour Savoirsi il reste des materiaux dans le dossier
        List<Material> tpBiome = new List<Material>(); //La liste qui contiendra les biomes
        do
        {
            Object mat = Resources.Load("Mats/Biomes/b" + nbBiomes + "_" + nbVariant);
            if (mat != null) //Tant que mot n'est pas null, qu'il y a un biome nomme comme celui-la
            {
                tpBiome.Add((Material)mat); //Ajoute mot  a la liste tpBiome
                nbVariant++; //Augmente nbVaraint de 1 pour voir si il y a d'autre dans ce biome
            }
            else
            {
                if (nbVariant == 1)
                {
                    ResteDesMats = false; //Si le nbVariant est encore egale a 1, cela veut dire qu'il n'y a pas d'autres biomes
                }
                else
                {
                    biomesMats.Add(tpBiome); //Ajoute tpBiome a biomeMats
                    tpBiome = new List<Material>(); //Reset tpBiome en créant une nouvellle liste
                    nbVariant = 1; //Remet nbVariant à 1
                    nbBiomes++; //Change de biomes en ajoutant 1 a nbBiomes
                }
            }
        } while (ResteDesMats); //Tant que ResteDesMats est a true, la boucle continuera
    }

    void CreerMap()
    {
        // GenererTerrain(profondeurIle, largeurIle, attenuateur);
        // float[,] map = GenererBordureEau(profondeurIle, largeurIle);
        float[,] map = GenererInnondationCirculaire(profondeurIle, largeurIle);
        float[,] ile = GenererTerrain(profondeurIle, largeurIle, attenuateur, map);
        // DessinerIle(ile);
        GenererIle(ile);
    }

    //Genere une ile circulaire avec l'eau
    //CREER PAR THOMAS ST-PIERRE - ça c'est pour que tu le trouve plus facilement ------------------------------------------------------
    private float[,] GenererInnondationCirculaire(int maxZ, int maxX)
    {
        float[,] ocean = new float[maxZ, maxX]; // le tableau qui contient les coordonnees de tout les cubes

        float cX = maxX / 2; // centre du cercle en X
        float cZ = maxZ / 2; // centre du cercle en Z
        float rayonEau = maxZ / 2; // le Rayon du cercle

        for (int z = 0; z < maxZ; z++)
        {
            for (int x = 0; x < maxX; x++)
            {
                float xDistance = cX - x; //Distance en x entre le centre et le point present
                float zDistance = cZ - z; //Distance en z entre le centre et le point present
                float val = Mathf.Sqrt(xDistance * xDistance + zDistance * zDistance); //Calcul la distance val, qui est la distance reel entre le centre et le point present
                float y = val / rayonEau; //Ramene val sous une valeur entre 0 et 1 pour qu'elle soit bien adapte pour la sigmoide
                ocean[z, x] = Sigmoid(y); //Prend l'ocean et donne la valeur y a la sigmoide pour creer une ile realiste
            }
        }

        return ocean;
    }

    //THOMAS ST-PIERRE Permet de faire une ile qui est realiste en faisant de tres petite inclinaisons
    float Sigmoid(float value)
    {
        float k = 15f; //Intensité de la variation
        float c = 0.7f; //Taille generale de l'ile, ici c'est 70% de l'ile
        return 1 / (1 + Mathf.Exp(-k * (value - c))); //Retourne sigmoide, une fonction en S
    }

    private float[,] GenererBordureEau(int maxZ, int maxX)
    {
        float[,] ocean = new float[maxZ, maxX]; //Les coordonnes de l'ocean
        float centreX = maxX / 2; //le centre de l'ile
        float centreZ = maxZ / 2; //le centre de l'ile
        for (int z = 0; z < maxX; z++)
        {
            for (int x = 0; x < maxZ; x++)
            {
                float yx = (Mathf.Abs(x - centreX) / centreX);
                float yz = (Mathf.Abs(z - centreZ) / centreZ);
                float y = Mathf.Max(yx, yz);
                ocean[z, x] = Sigmoid(y);
            }
        }
        return ocean;
    }

    //THOMAS ST-PIERRE Genere une ile rectangulaire
    private float[,] GenererTerrain(int maxZ, int maxX, float attenuateur, float[,] inondation)
    {
        int bruitAleatoire = Random.Range(0, 100000); //un nombre aleatoire qui permettra de toujours faire des iles aleatoire
        float[,] terrain = new float[maxZ, maxX]; //Un tableau qui contiendra la position des cubes
        for (int z = 0; z < maxX; z++)
        {
            for (int x = 0; x < maxZ; x++)
            {
                //bruitAleatoire s'arrange pour que la map generer soit TOUJOURS differentes
                float y = Mathf.PerlinNoise(x / attenuateur + bruitAleatoire, z / attenuateur + bruitAleatoire);

                float yflood = inondation[z, x]; //L'inondation, permet de savoir si l'ile sera circulaire ou non

                // Le terrain est calculer en soustrayant le perlinNoise ainsi que l'inondation, qui a été calculer dans GenererInondationCirculaire
                terrain[z, x] = Mathf.Clamp01(y - yflood);
            }
        }
        return terrain; //retourne le terrain lorsque la fonction est appeler
    }

    void GenererIle(float[,] map)
    {
        int larg = map.GetLength(0); //La map en largeur
        int prof = map.GetLength(1); //La map en profondeur

        int maximum = biomesMats.Count; //Le nombre de biomes Materiel qu'il y a
        for (int z = 0; z < prof; z++)
        {
            for (int x = 0; x < larg; x++)
            {
                float y = map[z, x]; //
                float intensiteAleatoire = Random.Range(0.75f, 1f);
                if (y > 0f)
                {
                    int quelBiome = Mathf.FloorToInt(map[z, x] * maximum);

                    quelBiome++; //Pour faire en sorte que les biomes soit placer au bons endroits

                    apparaitreDauphin++;

                    int rotRandom = Random.Range(0, 4) * 90;
                    GameObject unCube = Instantiate(cube, new Vector3(x - largeurIle / 2, y * coefAltitude, z - profondeurIle / 2), Quaternion.identity); //Instantie un cube a la position donnee

                    if (Random.Range(1, 2000) >= 1999)
                    {
                        float scaleRandomEnnemi = Random.Range(tailleEnnemiMin, tailleEnnemiMax);
                        GameObject unAgent = Instantiate((GameObject)Resources.Load("Ennemi/Ennemi"), new Vector3(unCube.transform.position.x, unCube.transform.position.y + 1f, unCube.transform.position.z), Quaternion.identity);
                        unAgent.GetComponent<EnnemiEtatsManager>().cible = perso;
                        Debug.Log(perso);
                        unAgent.GetComponent<EnnemiEtatsManager>().origine = unCube.transform;
                        unAgent.transform.localScale = new Vector3(scaleRandomEnnemi, scaleRandomEnnemi, scaleRandomEnnemi);
                    }

                    unCube.GetComponent<BiomesEtatsManager>().biomeMateriel = quelBiome;
                    unCube.GetComponent<BiomesEtatsManager>().perso = perso;

                    //Je ne comprend pas comment instancier le dauphin sur 1 biome sur 20, donc je vais les instancier comme ça, je m'en fous
                    if (apparaitreDauphin == 1600)
                    {
                        _unDauphin = Instantiate(dauphin, new Vector3(unCube.transform.position.x, unCube.transform.position.y + 2.0f, unCube.transform.position.z), Quaternion.identity);
                        apparaitreDauphin = 0;
                        _unDauphin.transform.LookAt(perso.transform.position);
                    }
                    unCube.AddComponent<BoxCollider>();
                    unCube.transform.rotation = Quaternion.Euler(0, rotRandom, 0);

                    unCube.transform.parent = gameObject.transform; //Centre l'ile sur son generateur

                    //CREATION DE L'OBJECTIF
                    _objectif++;
                    GameManager.instance.SetObjectif(_objectif);
                }
            }
        }
    }

    //Dessine l'ile sur la texture d'une plane
    void DessinerIle(float[,] map)
    {
        int larg = map.GetLength(0); //La map en largeur
        int prof = map.GetLength(1); //La map en profondeur

        Texture2D ileTexture = new Texture2D(larg, prof); //Cree un tableau qui contiendrait les informations de position d'une texture
        Color[] couleursTexture = new Color[larg * prof]; //

        for (int z = 0; z < prof; z++)
        {
            for (int x = 0; x < larg; x++)
            {
                float y = map[z, x];
                Color couleur = new Color(y, y, y, 1);
                // (Pour ligne du dessous) pour s'assurer que le tableau passera de 0 a 100 sans jamais sauter une position, 
                // on doit multiplier le max avec la largeur présente pour s'en assurer
                couleursTexture[z * larg + x] = couleur; //0*x: noir (parce qu'il n'y a rien)
            }
        }
        //Dessine ma texture
        ileTexture.SetPixels(couleursTexture);
        ileTexture.Apply();

        textureRenderer.sharedMaterial.mainTexture = ileTexture;
        textureRenderer.transform.localScale = new Vector3(prof, 1, larg);
    }
}
