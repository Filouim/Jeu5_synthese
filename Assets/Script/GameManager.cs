using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider _slider; // Valeur de la barre d'oxygene
    [SerializeField] private Slider _sliderObj; //Valeur de l'objectif
    [SerializeField] private Text _txtPointage; // Champ texte du pointage
    [SerializeField] private float _maxOxygene = 100f; // Niveau d'oxygene maximum
    [SerializeField] private float _delaiPerteOxygene = 1f; // Frequence a laquelle le joueur perd de l'oxygene

    public GameObject _menuUIIntro;
    public GameObject _menuUIDefaite;
    public GameObject _menuUIVictoire;
    private float _oxygeneActuel; // Niveau d'oxygene actuel
    private int _points = 0; // Nbre de points du perso
    private int _objectif; //l'objectif du joueur
    private int _completion = 0; //le niveau de completion du joueur 
    public bool _invincible;
    public int laCibleDesRequins = 0;

    private bool _joueurDefaite = false;
    private bool _joueurVictoire = false;

    [SerializeField] private Image _HpContainer; 

    public Color red => Color.red;  

       public Color white => Color.white; 

    public Color norm => _HpContainer.color;

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
    void FixedUpdate()
    {
        PerdOxygene();
        if (_oxygeneActuel <= 0f) _joueurDefaite = true;

        if(_oxygeneActuel < 30){

        _HpContainer.color = LerpRed(5);
        }else{ 
            _HpContainer.color = white;
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_joueurVictoire) _menuUIVictoire.GetComponent<Menus>()._menuActif = true;
        else if (_joueurDefaite) _menuUIDefaite.GetComponent<Menus>()._menuActif = true;
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
        _objectif = (pourcentage * 60) / 100;
        _sliderObj.maxValue = _objectif;
    }

    public void ObjectifProgresse()
    {
        _completion += 1;
        SetObjectifBarre();
        
        if (_completion == _objectif) _joueurVictoire = true;
    }

    public void SetObjectifBarre()
    {
        _sliderObj.value = _completion;
    }

    /// <summary>
    /// Une fonctione qui permet de déterminer l'invincibilité.
    /// </summary>
    /// <param name="invin">Un booleen qui represente si le personnage est invincible ou non</param>
    public void GetInvinvibilite(bool invin)
    {
        _invincible = invin;
    }

    /// <summary>
    /// Permettra au requin de changer de cible
    /// </summary>
    /// <param name="laCible">Represente la cible des requins</param>
    public void ChangerLaCible(int laCible)
    {
        laCibleDesRequins = laCible;
    }

    public Color LerpRed(float speed)
    {
     return Color.Lerp(white,red,Mathf.Sin(Time.time *speed)); 
    }
}
