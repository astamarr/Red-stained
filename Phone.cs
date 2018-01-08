using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Phone : VRTK_InteractableObject {

    uint IDplaying;
   public GameObject soundemitter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Grabbed(GameObject currentGrabbingObject)
    {

        base.Grabbed(currentGrabbingObject);
        if(soundemitter)
        {

            IDplaying = AkSoundEngine.PostEvent("phone_busy", soundemitter);
        }
    }

    public override void Ungrabbed(GameObject previousGrabbingObject)
    {
        base.Ungrabbed(previousGrabbingObject);
        AkSoundEngine.StopPlayingID(IDplaying);
    }



}
