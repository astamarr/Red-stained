using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinManager : MonoBehaviour {


    public MannequinMembreCollider head;
    public MannequinMembreCollider RightArm;
    public MannequinMembreCollider LeftArm;



    public Transform goodright;
    public Transform wrongright;
    public Transform GoodLeft;
    public Transform Wrongleft;
    public Transform GoodHead;
    public Transform WrongHead;


    public bool IsMainMannequin;

    public bool EnigmaCompleted = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (IsMainMannequin)
        {
            if (head.GoodEnigma && RightArm.GoodEnigma && LeftArm.GoodEnigma)
            {

                EnigmaCompleted = true;

            }


        }

    }
}
