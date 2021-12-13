using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [SerializeField] private GameObject _menuIntroUI;
    [SerializeField] private GameObject _menuDefaiteUI;
    [SerializeField] private GameObject _menuVictoireUI;
    [SerializeField] private GameObject _UIJeu;
    [SerializeField] private float _tempsApparitionIntro = 5f;

    public static bool _introActif = false;
    public static bool _defaiteActif = false;
    public static bool _victoireActif = false;

    private static Menus _instance;
    public static Menus instance => _instance;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        StartCoroutine(DelaiMenuIntro(_tempsApparitionIntro));
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!_introActif) Continuer(_menuIntroUI, _introActif);
        else Pause(_menuIntroUI, _introActif);

        if (!_defaiteActif) Continuer(_menuDefaiteUI, _defaiteActif);
        else Pause(_menuDefaiteUI, _defaiteActif);

        if (!_victoireActif) Continuer(_menuVictoireUI, _victoireActif);
        else Pause(_menuVictoireUI, _victoireActif);
    }

    public void DesactiverIntro()
    {
        _introActif = false;
    }

    private void Continuer(GameObject menu, bool actif)
    {
        menu.SetActive(false);
        _UIJeu.SetActive(true);
        Time.timeScale = 1f;
        actif = false;
    }

    private void Pause(GameObject menu, bool actif)
    {
        menu.SetActive(true);
        _UIJeu.SetActive(false);
        Time.timeScale = 0f;
        actif = true;
    }

    private IEnumerator DelaiMenuIntro(float delai)
    {
        yield return new WaitForSeconds(delai);

        _introActif = true;
    }
}
