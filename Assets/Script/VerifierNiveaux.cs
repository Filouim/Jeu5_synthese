using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class VerifierNiveaux : MonoBehaviour
{
    [SerializeField] private ChangerScene _cs;

    private int _posScene;
    private UnityAction _action;

    // Start is called before the first frame update
    void Start()
    {
        _posScene = PlayerPrefs.GetInt("posScene");
        _action += ChargerNiveau;
        gameObject.GetComponent<Button>().onClick.AddListener(_action);
    }

    private void ChargerNiveau()
    {
        _cs.ChargerScene("Niveau " + _posScene);
    }
}
