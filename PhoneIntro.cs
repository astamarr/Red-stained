using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PhoneIntro : VRTK_InteractableObject
{
    GameObject grabbedby;
   public  Light sun;
    bool hasstarted = false;
    bool IsActivable = false;
    public VRTK_HeadsetFade fading;
    bool Ending;
   
    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Ending)
        {
      
            fading.Fade(Color.black, 0.0f);


        }
    }
    
    public override void Grabbed(GameObject currentGrabbingObject)
    {
        grabbedby = currentGrabbingObject;
        base.Grabbed(currentGrabbingObject);
       
    }

    public override void Ungrabbed(GameObject previousGrabbingObject)
    {
        base.Ungrabbed(previousGrabbingObject);

        grabbedby = null;
    }

    public override void StartUsing(GameObject usingObject)
    {
        if ( !hasstarted)
        {


       
        if (grabbedby == usingObject)
        {

            

        }
        else
        {
                hasstarted = true;
            StartCoroutine(Launchscene());
       
        }
        }

    }


    IEnumerator Launchscene()
    {
        AkSoundEngine.PostEvent("call_emma", this.gameObject);
        yield return new WaitForSeconds(12.0f);
        fading.Fade(Color.black, 6.0f);
        yield return new WaitForSeconds(6.0f);
        sun.intensity = 0;
        sun.enabled = false;
        sun.gameObject.SetActive(false);


        yield return new WaitForSeconds(6.0f);
        Ending = true;


        yield return new WaitForSeconds(20.0f);
        SteamVR_LoadLevel.Begin("Main", false);

    }




}
