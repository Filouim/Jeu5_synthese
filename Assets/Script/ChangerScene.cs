using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangerScene : MonoBehaviour
{
    public void LoadScene(string nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }
}
