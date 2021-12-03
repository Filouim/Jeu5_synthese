using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlashImage : MonoBehaviour
{
    
    Image _image = null;
    Coroutine _currentFlashRoutine = null;
       // Start is called before the first frame update
        
        private float _oxy = GameManager._oxygeneActuel;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update(){
     StartFlash(0,0);
    }

    private void StartFlash(float secondsForOneFlash, float maxAlpha)
    {

        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);

        if(_oxy > 80){
            StopCoroutine(_currentFlashRoutine);
        }
           
        _currentFlashRoutine = StartCoroutine(flash(secondsForOneFlash, maxAlpha));
    }

    IEnumerator flash(float secondsForOneFlash, float maxAlpha)
    {

        float flashInDuration = secondsForOneFlash / 2;
        for (float t = 0; t <= flashInDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAlpha, t / flashInDuration);
            _image.color = colorThisFrame;

            yield return null;
        }

        float flashOutDuration = secondsForOneFlash / 2;
        for (float t = 0; t <= flashOutDuration; t+= Time.deltaTime)
        {

            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0, t / flashOutDuration);
            _image.color = colorThisFrame;
            yield return null;

        }

        _image.color = new Color32(0, 0, 0, 0);
    }

}


