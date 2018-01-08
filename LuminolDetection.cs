using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuminolDetection : MonoBehaviour {


    List<GameObject> spotted;

	// Use this for initialization
	void Start () {
        spotted = new List<GameObject>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Detect()
    {

        foreach(GameObject n in spotted)
        {
            if(n.GetComponent<RevealLight>().ActivatedByLuminol == true)
            {
                n.GetComponent<RevealLight>().enabled = true;

            }
          


        }






    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<RevealLight>())
        {
            spotted.Add(other.gameObject);
         
        }
        
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<RevealLight>())
        {
            spotted.Remove(other.gameObject);
          
        }


    }
}
