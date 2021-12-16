using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _UIJeu;
    [SerializeField] private float _tempsApparitionIntro = 5f;

    public bool _menuActif = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        StartCoroutine(DelaiMenuIntro(_tempsApparitionIntro));
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!_menuActif) Continuer(_menuUI, _menuActif);
        else Pause(_menuUI, _menuActif);
    }

    public void DesactiverIntro()
    {
        _menuActif = false;
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

        _menuActif = true;
    }
}
