using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneriqueHandler : MonoBehaviour {
    public Transform subtitles;
    public Transform focus;
    public Camera subtitlesCam;
    public float maxDistanceToCamera = 1.0f;
    public float moveFactor = .5f;
  
    public Canvas Lecanvas;
    public Camera LaCamera;

    float velocity = 0;

    Vector3 trackPosition;
    Vector3 origFocus;

    public Image imagepanel;
    public Sprite Name;
    public Sprite Leave;

   public  CanvasGroup group;

    public GameObject Sound;


    public bool fadeIn = false;
    public bool fadeOut = false;


    void Awake()
    {
        trackPosition = subtitlesCam.transform.position;
        origFocus = focus.position;
     
        if (enabled && gameObject.activeSelf)
        {
            subtitlesCam.enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            subtitlesCam.enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // subtitlesCam.transform.position = Camera.main.transform.position;
        focus.position = subtitlesCam.transform.position + (origFocus - Camera.main.transform.position);
        subtitles.position = Vector3.Lerp(subtitles.position, trackPosition, moveFactor);
        subtitles.LookAt(focus);
        float distance = (trackPosition - subtitlesCam.transform.position).magnitude;
        if (distance > maxDistanceToCamera)
            trackPosition = subtitlesCam.transform.position;


        if (fadeIn)
        {

            group.alpha = group.alpha + Time.deltaTime;
            if (group.alpha >= 0.5F)
            {
                group.alpha = 0.5F;
                fadeIn = false;
            }
        }
        if (fadeOut)
        {

            group.alpha = group.alpha - Time.deltaTime;
            if (group.alpha <= 0)
            {
                group.alpha = 0;
                fadeOut = false;
            }

        }



    }


    /// Appelé lorsque le script ou l'objet est activé. Lance automatiquement la lecture vidéo des sous-titres
    void OnEnable()
    {
        subtitlesCam.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
      

        trackPosition = subtitlesCam.transform.position;
        focus.position = subtitlesCam.transform.position + (origFocus - Camera.main.transform.position);
        subtitles.position = trackPosition;
        subtitles.LookAt(focus);
    }

    /// Appelé lors de la désactivation de l'objet ou du script. Masque les sous-titres.
    void OnDisable()
    {
        subtitlesCam.enabled = false;
        this.gameObject.GetComponentInChildren<Canvas>().enabled = false;
     
    }


    public IEnumerator Gen()
    {

        yield return new WaitForSeconds(3);

        AkSoundEngine.PostEvent("generique", Sound);

            yield return new WaitForSeconds(8);
        fadeIn = true;
        yield return new WaitForSeconds(7);
        fadeOut = true;
        yield return new WaitForSeconds(3.5f);
        
        imagepanel.sprite = Name;

        fadeIn = true;
        yield return new WaitForSeconds(11f);
        fadeOut = true;
        yield return new WaitForSeconds(4f);
        imagepanel.sprite = Leave;

        fadeIn = true;

    }
}
