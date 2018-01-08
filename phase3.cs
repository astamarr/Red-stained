using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VRTK;

public class phase3 : MonoBehaviour
{

    public RevoxManager revox1;
    public RevoxManager revox2;
    public RevoxManager revox3;
    public RevoxManager revox4;
     VideoPlayer player;

    public VideoSubtitlesHandler Subtitles;

    public ProgressiveLight projo1;
    public ProgressiveLight projo2;
    public Light AmbientLit;




    public GameObject ProjectorGameObject;
    public GameObject ProjectorLightGameObject;

    private bool videolaunched = false;

    public bool EnigmaWon = false;
    public bool videohadfinished = false;

    private uint projectorsoundID;


    public FlickerLight[] FlickerPowerOutage;
    public blinklight[] emissiveblink;
    public Light[] LightPowerOutage;
    public Renderer[] emmisivesPowerOutage;
    public Renderer Redlightemissive;
    public Light redlight;


    public Collider Evier;
    public VRTK_InteractableObject Placard1;
    public VRTK_InteractableObject Placard2;



    public GameObject[] ToActivate;
    public Transform Placard;

    public GameObject aldosound;

    // Use this for initialization
    void Start()
    {





    }

    // Update is called once per frame
    void Update()
    {
        if (!EnigmaWon)
        {



            if (revox1.CurrentSongIsGoodSong && revox2.CurrentSongIsGoodSong && revox3.CurrentSongIsGoodSong && revox4.CurrentSongIsGoodSong)
            {


                StartCoroutine(FinishingRevox());

            }
        }
        
    }

















    public IEnumerator FinishingRevox()
    {


        EnigmaWon = true;

        revox1.fin();
        revox2.fin();
        revox3.fin();
        revox4.fin();

        yield return new WaitForSeconds(2f);
        projo1.Switchonoff(false);
        projo2.Switchonoff(false);
        AmbientLit.enabled = false;
        yield return new WaitForSeconds(8f);

        ProjectorGameObject.SetActive(true);
        player = ProjectorGameObject.GetComponent<VideoPlayer>();
        player.Prepare();


        ProgressiveLight[] lights = ProjectorGameObject.GetComponentsInChildren<ProgressiveLight>();
        projectorsoundID = AkSoundEngine.PostEvent("projector_launch", ProjectorGameObject);


        yield return new WaitForSeconds(1f);

        foreach (ProgressiveLight l in lights)
        {

            l.Switchonoff(true);
        }


        yield return new WaitForSeconds(3f);
        ProjectorLightGameObject.SetActive(true);
        yield return new WaitForSeconds(1f);


        player.Play();
        AkSoundEngine.PostEvent("video", ProjectorGameObject);
        Subtitles.enabled = true;
        videolaunched = true;


        
        yield return new WaitForSeconds(94f);

        AkSoundEngine.StopPlayingID(projectorsoundID);
        projectorsoundID = AkSoundEngine.PostEvent("projector_end", ProjectorGameObject);





        foreach (FlickerLight a in FlickerPowerOutage)
        {

            if (a)
            {

                a.enabled = true;
            }

        }

        foreach (blinklight a in  emissiveblink)
        {


            if(a)
            {

                a.enabled = true;
            }
        }

        ProjectorLightGameObject.GetComponent<FlickerLight>().Strength = 600;


        yield return new WaitForSeconds(5f);

        AkSoundEngine.StopPlayingID(projectorsoundID);
        videohadfinished = true;
        Subtitles.Lecanvas.enabled = false;


        Subtitles.subtitlesCam.enabled = false;
        AkSoundEngine.PostEvent("power_down", ProjectorGameObject);
        player.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        player.gameObject.GetComponent<Renderer>().enabled = false;
        ProjectorLightGameObject.SetActive(false);

        foreach (blinklight a in emissiveblink)
        {


            if (a)
            {
                a.enabled = false;
            }
        }

        foreach (Light a in LightPowerOutage)

        {

            if (a)
            {
                a.enabled = !a.isActiveAndEnabled;
            }
        }

        foreach(Renderer a in emmisivesPowerOutage)
        {
            if(a)
            {
               
                Color baseColor = Color.red; //Replace this with whatever you want for your base color at emission level '1'

                Color finalColor = baseColor * Mathf.LinearToGammaSpace(0);

                a.material.SetColor("_EmissionColor", finalColor);


            }


        }

     



        foreach (FlickerLight a in FlickerPowerOutage)
        {

            if (a)
            {

                a.enabled = false;
            }




        }
        yield return new WaitForSeconds(5f);

        AkSoundEngine.PostEvent("aldo_tg", aldosound);


       yield return new WaitForSeconds(3f);

        foreach (GameObject obj in ToActivate)
        {
            if (obj)
            {

                obj.SetActive(!obj.activeSelf);
            }



        }

       

        yield return new WaitForSeconds(3f);
        Placard.transform.Rotate(new Vector3(0, -20, 0));
        redlight.enabled = true;
        redlight.gameObject.GetComponent<NastyBlink>().enabled = true;
        AkSoundEngine.PostEvent("loupiote", redlight.gameObject);
        Color ah = Color.red * Mathf.LinearToGammaSpace(0.9f);

        Redlightemissive.material.SetColor("_EmissionColor", ah);

        //desactive collider de l'évier pour éviter de se stuck la tronche dedans
        Evier.enabled = false;
        Placard1.isGrabbable = true;
        Placard2.isGrabbable = true;

    }


   
}
