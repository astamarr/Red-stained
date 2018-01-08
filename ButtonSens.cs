using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ButtonSens : VRTK_InteractableObject
{

    bool Sens;
    RevoxManager revox;
    VRTK_ControllerActions controllerActions;
    Material Emmisive;
    bool PlayingState = false;

    // Use this for initialization
    void Start()
    {
        revox = this.gameObject.GetComponentInParent<RevoxManager>();
        Sens = revox.Wrong;
        Emmisive = GetComponent<Renderer>().material;
        PlayingState = revox.Isplaying;
        SetEmissive();
     


    }

    // Update is called once per frame
    void Update()
    {
        if(revox.Isplaying != PlayingState)
        {

            if (revox.Isplaying)
            {
                Color baseColor;
                PlayingState = true;

                 if (Sens == false)
                {

                    baseColor = Color.magenta; //Replace this with whatever you want for your base color at emission level '1'
                }
                else
                {
                    baseColor = Color.cyan;
                }


                Color finalColor = baseColor * Mathf.LinearToGammaSpace(0.6f);

                Emmisive.SetColor("_EmissionColor", finalColor);




            }
            else
            {


              
                PlayingState = false;

               


                Color finalColor = Color.black * Mathf.LinearToGammaSpace(0);

                Emmisive.SetColor("_EmissionColor", finalColor);








            }




        }
           





    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        controllerActions = usingObject.GetComponent<VRTK.VRTK_ControllerActions>();

        if(revox.Isplaying)
        {

            StartCoroutine(vibrations());
            revox.ChangerPiste();
            Sens = !Sens;

            SetEmissive();
        }
        

       




    }




    private void SetEmissive()
    {
        Color baseColor;
        float toplay = 1;
        if(PlayingState)
        {


       
        if (Sens == false)
        {

            baseColor = Color.magenta; //Replace this with whatever you want for your base color at emission level '1'
        }
        else
        {
            baseColor = Color.cyan;
        }

        }
        else
        {
            toplay = 0;
            baseColor = Color.black;
        }



        Color finalColor = baseColor * Mathf.LinearToGammaSpace(toplay);

        Emmisive.SetColor("_EmissionColor", finalColor);



    }
    public IEnumerator vibrations()
    {
        yield return new WaitForSeconds(0.1f);
        controllerActions.TriggerHapticPulse(1f);
        yield return new WaitForSeconds(1f);


    }


}
