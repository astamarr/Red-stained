using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRMembres : VRTK_InteractableObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Grabbed(GameObject currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);
        this.gameObject.GetComponent<Collider>().isTrigger = false;

    }
    override public void Ungrabbed(GameObject previousGrabbingObject)
    {
        base.Ungrabbed(previousGrabbingObject);
        /* Set grabbing, only if the item is not nailed up */
     
    }
}
