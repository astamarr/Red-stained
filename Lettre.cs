using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Lettre : VRTK_InteractableObject
{
   public GameObject Sound_outside;
    bool HasbeenUsed = false;
    bool triggerlights = false;
    public Animator Volets;
    float light = 0;
    float t = 0.0f;

    float intensityMin = 0.0f;
    float intensityMax= 0.7f;
    float durationLerp = 7f;
    float durationCurrent = 0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (triggerlights)
        {
            durationCurrent += Time.deltaTime;

            if(durationCurrent >= durationLerp)
            {
                triggerlights = false;
                SetIntensity(intensityMax);

                Debug.Log("Lights stopped");
            }
            else
            {
                float alpha = durationCurrent / durationLerp;
                float mappedIntensity = Mathf.Lerp(intensityMin, intensityMax, alpha);
                Debug.Log("Alpha equal " + alpha +"  Intensity equals " + mappedIntensity);
                SetIntensity(mappedIntensity);
            }


            /*

            light = Mathf.Lerp(0, 1.0f, t);

            t += Time.deltaTime / 5;

            RenderSettings.ambientIntensity = 0.1f;
            RenderSettings.reflectionIntensity = 0.1f;

            if ( light >=1)
            {
                triggerlights = false;

            }
            */
   
        }

    }

    public void SetIntensity(float intensity)
    {
        RenderSettings.ambientIntensity = intensity;
        RenderSettings.reflectionIntensity = intensity;
    }


    public override void Grabbed(GameObject currentGrabbingObject)
    {


        base.Grabbed(currentGrabbingObject);

        if (!HasbeenUsed)
        {
            HasbeenUsed = true;
            AkSoundEngine.PostEvent("letter", this.gameObject);
            StartCoroutine(trigger());

        }




    }




    IEnumerator trigger()
    {
        yield return new WaitForSeconds(4);



     
        yield return new WaitForSeconds(2);
        Volets.enabled = true;

        triggerlights = true;
        durationCurrent = 0f;
        RenderSettings.ambientIntensity = intensityMin;
        RenderSettings.reflectionIntensity = intensityMin;

        Debug.Log("Lights triggered");

        AkSoundEngine.PostEvent("volet_roulant_intro", Volets.gameObject);

        /*
        RenderSettings.ambientIntensity = 0.05f;
        RenderSettings.reflectionIntensity = 0.05f;
        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.1f;
        RenderSettings.reflectionIntensity = 0.1f;
        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.15f;
        RenderSettings.reflectionIntensity = 0.15f;
        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.2f;
        RenderSettings.reflectionIntensity = 0.2f;
        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.25f;
        RenderSettings.reflectionIntensity = 0.25f;
        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.3f;
        RenderSettings.reflectionIntensity = 0.3f;
        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.35f;
        RenderSettings.reflectionIntensity = 0.35f;
        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.4f;
        RenderSettings.reflectionIntensity = 0.4f;
        yield return new WaitForSeconds(0.5f);

        RenderSettings.ambientIntensity = 0.45f;
        RenderSettings.reflectionIntensity = 0.45f;

        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.5f;
        RenderSettings.reflectionIntensity = 0.5f;
        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.55f;
        RenderSettings.reflectionIntensity = 0.55f;
        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.6f;
        RenderSettings.reflectionIntensity = 0.6f;
        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.65f;
        RenderSettings.reflectionIntensity = 0.65f;

        yield return new WaitForSeconds(0.5f);
        RenderSettings.ambientIntensity = 0.7f;
        RenderSettings.reflectionIntensity = 0.7f;

    */
        yield return new WaitForSeconds(7f);
        AkSoundEngine.PostEvent("volent_roulant_intro_end", Volets.gameObject);
        AkSoundEngine.PostEvent("ambiance_ext_intro", Sound_outside);

    }
}
