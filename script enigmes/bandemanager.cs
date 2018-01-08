using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bandemanager : MonoBehaviour {

    public bool arret = true;
    public bool backward = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateRotationBandes(arret, backward);


    }

    void UpdateRotationBandes(bool active,bool alenvers)
    {

        bandesmagnetiques[] a = GetComponentsInChildren<bandesmagnetiques>();

        foreach(bandesmagnetiques bande in a)
        {

            bande.IsPlaying = active;
            bande.Backward = alenvers;





        }

    }





}
