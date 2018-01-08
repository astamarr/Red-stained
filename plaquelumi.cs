using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plaquelumi : MonoBehaviour {

   public phase3 enigma;

   public Material clue;
	// Use this for initialization
	void Start () {

        StartCoroutine(trig());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator trig()
    {
       
        yield return new WaitForSeconds(195);
        if(!enigma.EnigmaWon)
        {

            this.gameObject.GetComponent<Renderer>().material = clue;
        }


    }
}
