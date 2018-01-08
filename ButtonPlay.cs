using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ButtonPlay : VRTK_InteractableObject
{

    bool Play;
    RevoxManager revox;
    VRTK_ControllerActions controllerActions;
    Material Emmisive;

    // Use this for initialization
    void Start () {
        revox = this.gameObject.GetComponentInParent<RevoxManager>();
        Play = revox.Isplaying;
        Emmisive = GetComponent<Renderer>().material;
        SetEmissive();


    }
	
	// Update is called once per frame
	void Update () {



		
	}

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        controllerActions = usingObject.GetComponent<VRTK.VRTK_ControllerActions>();



        revox.MarcheArret();
        Play = !Play;
        StartCoroutine(vibrations());
        SetEmissive();





        }

    


    private void SetEmissive()
    {
        Color baseColor;
        if (Play == false)
        {

            baseColor = Color.red; //Replace this with whatever you want for your base color at emission level '1'
        }
        else
        {
            baseColor = Color.green;
        }


        Color finalColor = baseColor * Mathf.LinearToGammaSpace(0.6f);

        Emmisive.SetColor("_EmissionColor", finalColor);



    }

    public IEnumerator vibrations()
    {
        yield return new WaitForSeconds(0.1f);
        controllerActions.TriggerHapticPulse(1f);
        yield return new WaitForSeconds(1f);


    }



}
