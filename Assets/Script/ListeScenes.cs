using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeScenes : MonoBehaviour
{
    public List<string> historiqueScenes = new List<string>();

    private static ListeScenes _instance;
    public static ListeScenes instance => _instance;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public bool LoadPreviousScene()
    {
        bool returnValue = false;

        if (historiqueScenes.Count >= 2)
        {
            returnValue = true;
            historiqueScenes.RemoveAt(historiqueScenes.Count - 1);
            ChangerScene.instance.LoadScene(historiqueScenes[historiqueScenes.Count - 1]);
        }

        return returnValue;
    }
}
