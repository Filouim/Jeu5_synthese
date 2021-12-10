using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangerScene : MonoBehaviour
{
    [SerializeField] private float _delai = 0.25f;

    private static ChangerScene _instance;
    public static ChangerScene instance => _instance;

    public void LoadScene(string nomScene)
    {
        ListeScenes.instance.historiqueScenes.Add(SceneManager.GetActiveScene().name);
        StartCoroutine(ChangeSceneDelay(nomScene, _delai, false, false));
    }

    public void LoadPreviousScene()
    {
        StartCoroutine(ChangeSceneDelay("", _delai, true, false));
    }

    public void LoadNextScene()
    {
        ListeScenes.instance.historiqueScenes.Add(SceneManager.GetActiveScene().name);
        StartCoroutine(ChangeSceneDelay("", _delai, false, true));
    }

    public void QuitterJeu()
    {
        Application.Quit();
    }

    private IEnumerator ChangeSceneDelay(string nomScene, float delai, bool isPreviousScene, bool isNextScene)
    {
        yield return new WaitForSeconds(delai);

        if (isPreviousScene && !isNextScene && nomScene == "") ListeScenes.instance.LoadPreviousScene();
        else if (isNextScene && !isPreviousScene && nomScene == "") SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else SceneManager.LoadScene(nomScene);
    }
}
