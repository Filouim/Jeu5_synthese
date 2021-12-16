using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Progression : MonoBehaviour
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Intro") PlayerPrefs.DeleteKey("posScene");
        else PlayerPrefs.SetInt("posScene", SceneManager.GetActiveScene().buildIndex);
        Debug.Log(PlayerPrefs.GetInt("posScene"));
        PlayerPrefs.Save();
    }
}
