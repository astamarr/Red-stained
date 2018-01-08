using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEvent : MonoBehaviour
{

    public GameObject activatelights;
    public ProgressiveLight[] lights;
    public GameObject AmbientSoundSource;
    public GameObject AldoSoundSource;
    public float TimeBeforeStorm = 8;
    public float TimeBeforeLight = 6;
    public GameObject Player;

    public uint SoundIDpiano;
    public uint SoundIDscape;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(starting());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator starting()
    {
        yield return new WaitForSeconds(TimeBeforeStorm);
        activatelights.GetComponent<Lightnin>().Startthestorm = true;
        yield return new WaitForSeconds(TimeBeforeLight);
        /*
        AkSoundEngine.PostEvent("aldo_footsteps_beginning", AldoSoundSource);
       
        yield return new WaitForSeconds(5.5f);
        */
        //AkSoundEngine.SetRTPCValue()
        SoundIDpiano = AkSoundEngine.PostEvent("aldo_footsteps", AldoSoundSource);
        yield return new WaitForSeconds(1f);



        foreach ( ProgressiveLight l in lights)
        {
            l.Switchon = true;
        }

        yield return new WaitForSeconds(22f);



        SoundIDscape=  AkSoundEngine.PostEvent("soundscape_1", Player);


    }



}
