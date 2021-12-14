using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    private AudioSource _source;

    public AudioClip _clip;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "SALE")
        {

            StartCoroutine(Son());
        }
    }


    private IEnumerator Son()
    {
        yield return null;

        if (!_source.isPlaying)
        {

            _source.PlayOneShot(_clip, 1f);
        }

    }
}
