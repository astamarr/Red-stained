using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlaneTicket : VRTK_InteractableObject
{
    public ToDoList liste;
    bool HasbeenUsed = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public override void Grabbed(GameObject currentGrabbingObject)
    {


        base.Grabbed(currentGrabbingObject);

        if (!HasbeenUsed)
        {
            liste.TicketFound();
            HasbeenUsed = true;
    

        }




    }
}
