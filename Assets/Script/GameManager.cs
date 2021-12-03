using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider _slider; // Valeur de la barre d'oxygene
   
    [SerializeField] private FlashImage _flashImage = null;
    [SerializeField] private Slider _sliderObj; //Valeur de l'objectif
    [SerializeField] private Text _txtPointage; // Champ texte du pointage
    [SerializeField] private float _maxOxygene = 100f; // Niveau d'oxygene maximum
    [SerializeField] private float _delaiPerteOxygene = 1f; // Frequence a laquelle le joueur perd de l'oxygene

    public static float _oxygeneActuel; // Niveau d'oxygene actuel
    private int _points = 0; // Nbre de points du perso
    private int _objectif; //l'objectif du joueur
    private int _completion = 0; //le niveau de completion du joueur

    private static GameManager _instance;
    public static GameManager instance => _instance;

    // Singleton
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        // _displayPieces = GetComponent<TextMeshPro>();
        // SetMaxOxygene(_maxOxygene);
        _oxygeneActuel = _maxOxygene;
    }

    // Update is called once per frame
    void Update()
    {
        PerdOxygene();

        if (_oxygeneActuel <= 0f)
        {
            GetComponent<ChangerScene>().LoadScene("Defaite");
        }
    }

    // public int DonnePieces()
    // {
    //     _nbPieces++;
    //     AffichePieces();
    //     return _nbPieces;
    // }

    /// <summary>
    /// Ajuste la barre d'oxygene selon la valeur en parametre.
    /// </summary>
    /// <param name="oxygene">Valeur d'oxygene.</param>
    public void SetOxygene(float oxygene)
    {
        _slider.value = oxygene;
    }

    /// <summary>
    /// Ajoute un point au champ de texte du pointage.
    /// </summary>
    public void AjouterPoints(int montant)
    {
        _points = _points + montant;
        _txtPointage.text = _points.ToString();
    }

    /// <summary>
    /// Ajoute un point au champ de texte du pointage.
    /// </summary>
    // public void RetirerPoints(int montant)
    // {
    //     points = points - montant;
    //     txtPointage.text = points.ToString();
    // }

    /// <summary>
    /// #Tim Thomas Perd constamment de l'oxygene
    /// </summary>
    private void PerdOxygene()
    {
        _oxygeneActuel -= Time.deltaTime * _delaiPerteOxygene;

        SetOxygene(_oxygeneActuel);
    }

    /// <summary>
    /// Inflige des degats au joueur.
    /// </summary>
    /// <param name="degats"></param>
    public void SubirDegats(float degats)
    {
        _oxygeneActuel -= degats;

        SetOxygene(_oxygeneActuel);
    }

    /// <summary>
    /// Donne de l'oxygene au joueur.
    /// </summary>
    /// <param name="ajout"></param>
    public void AjouterOxygene(float ajout)
    {
        _oxygeneActuel += ajout;

        if (_oxygeneActuel > _maxOxygene) _oxygeneActuel = _maxOxygene;

        SetOxygene(_oxygeneActuel);
    }

    public void SetObjectif(int pourcentage)
    {
        _objectif = (pourcentage * 80) / 100;
        _sliderObj.maxValue = _objectif;
    }

    public void ObjectifProgresse()
    {
        _completion += 1;
        SetObjectifBarre();
    }

    public void SetObjectifBarre()
    {
        _sliderObj.value = _completion;
    }
}
