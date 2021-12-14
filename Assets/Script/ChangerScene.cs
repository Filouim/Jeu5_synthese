using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangerScene : MonoBehaviour
{
    [SerializeField] private float _delai = 0.25f;

    private static ChangerScene _instance;
    public static ChangerScene instance => _instance;

    public void ChargerScene(string nomScene)
    {
        StartCoroutine(ChangeSceneDelai(nomScene, _delai));
    }

    public void RechargerNiveau()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ProchainNiveau()
    {
        StartCoroutine(ChangeSceneDelai("", _delai));
    }

    public void QuitterJeu()
    {
        Application.Quit();
    }

    private IEnumerator ChangeSceneDelai(string nomScene, float delai)
    {
        yield return new WaitForSeconds(delai);

        if (nomScene == "") SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else SceneManager.LoadScene(nomScene);
    }
}
