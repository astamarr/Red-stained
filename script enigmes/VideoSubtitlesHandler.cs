using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSubtitlesHandler : MonoBehaviour {
    public Transform subtitles;
    public Transform focus;
    public Camera subtitlesCam;
    public float maxDistanceToCamera = .3f;
    public float moveFactor = .05f;
    public VideoPlayer vidPlayer;
    public Canvas Lecanvas;
    public Camera LaCamera;
    
    float velocity = 0;
    
    Vector3 trackPosition;
    Vector3 origFocus;

    void Awake () {
        trackPosition = subtitlesCam.transform.position;
        origFocus = focus.position;
        vidPlayer.url = Application.streamingAssetsPath + "/soustitrage_1.mp4";
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
    
    void Update () {
        // subtitlesCam.transform.position = Camera.main.transform.position;
        focus.position = subtitlesCam.transform.position + (origFocus - Camera.main.transform.position);
        subtitles.position = Vector3.Lerp(subtitles.position, trackPosition, moveFactor);
        subtitles.LookAt(focus);
        float distance = (trackPosition - subtitlesCam.transform.position).magnitude;
        if (distance > maxDistanceToCamera)
            trackPosition = subtitlesCam.transform.position;
    }
    
    
    /// Appelé lorsque le script ou l'objet est activé. Lance automatiquement la lecture vidéo des sous-titres
    void OnEnable() {
        subtitlesCam.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        vidPlayer.Play();
        
        trackPosition = subtitlesCam.transform.position;
        focus.position = subtitlesCam.transform.position + (origFocus - Camera.main.transform.position);
        subtitles.position = trackPosition;
        subtitles.LookAt(focus);
    }
    
    /// Appelé lors de la désactivation de l'objet ou du script. Masque les sous-titres.
    void OnDisable() {
        subtitlesCam.enabled = false;
        this.gameObject.GetComponentInChildren<Canvas>().enabled = false;
        vidPlayer.Stop();
    }
    
}
