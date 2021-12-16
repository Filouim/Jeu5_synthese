using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangerScene : MonoBehaviour
{
    [SerializeField] private float _delai = 0f;

    private int _previousScene;

    private static ChangerScene _instance;
    public static ChangerScene instance => _instance;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _previousScene = PlayerPrefs.GetInt("posScene");
    }

    public void ChargerScene(string nomScene)
    {
        StartCoroutine(ChangeSceneDelai(nomScene, _delai));
    }

    public void RecommencerNiveau()
    {
        SceneManager.LoadScene(_previousScene);
    }

    public void ProchainNiveau()
    {
        SceneManager.LoadScene(_previousScene + 1);
    }

    public void QuitterJeu()
    {
        Application.Quit();
    }

    private IEnumerator ChangeSceneDelai(string nomScene, float delai)
    {
        yield return new WaitForSeconds(delai);

        SceneManager.LoadScene(nomScene);
    }
}
