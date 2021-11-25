using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float _nbOxygene = 100f;
    private static GameManager _instance;
    public static GameManager instance => _instance;
    public Slider slider; // Valeur de la barre d'oxygene
    public int points = 0;
    public Text txtPointage;

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
        SetMaxOxygene(_nbOxygene);
    }

    // Update is called once per frame
    void Update()
    {
        PerdOxygene();
        if(_nbOxygene < 0f)
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
    /// Ajuste le niveau d'oxygene maximum selon la valeur en parametre.
    /// </summary>
    /// <param name="oxygene">Valeur d'oxygene.</param>
    public void SetMaxOxygene(float oxygene)
    {
        slider.maxValue = oxygene;
        slider.value = oxygene;
    }

    /// <summary>
    /// Ajuste la barre d'oxygene selon la valeur en parametre.
    /// </summary>
    /// <param name="oxygene">Valeur d'oxygene.</param>
    public void SetOxygene(float oxygene)
    {
        slider.value = oxygene;
    }

    

    /// <summary>
    /// Ajoute un point au champ de texte du pointage.
    /// </summary>
    public void AjouterPoints(int montant)
    {
        points = points + montant;
        txtPointage.text = points.ToString();
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
        _nbOxygene -= Time.deltaTime * 1;
        SetOxygene(_nbOxygene);
    }

    public float BonusMalusOxygene(float montant)
    {  
        _nbOxygene += montant;
        //Limite la quantitee d'oxygene que le joueur peut avoir
        if(_nbOxygene > 100f)
        {
            _nbOxygene = 100f;
        }
        return _nbOxygene;
    }
}
