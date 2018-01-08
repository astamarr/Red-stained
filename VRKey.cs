using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRKey : VRTK_InteractableObject
{
    public bool IsStuck = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Grabbed(GameObject currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);

        if (IsStuck)
        {

            StartCoroutine(ResetLock());
          



        }

    }

    public override void Ungrabbed(GameObject previousGrabbingObject)
    {
        base.Ungrabbed(previousGrabbingObject);
        if (IsStuck)
        {
            IsStuck = false;
            this.GetComponent<Rigidbody>().isKinematic = false;

        }

    }
    IEnumerator ResetLock()
    {
        yield return new WaitForSeconds(0.5f);
       
        if(this.GetComponent<Key>().LastSerrure)
        {
            this.GetComponent<Key>().LastSerrure.enabled = true;

            if(this.GetComponent<Key>().LastSerrure.GetComponent<DoorLock>().open == false )
            {
                this.GetComponent<Key>().LastSerrure.GetComponent<DoorLock>().currentKeyInLock = null;
                this.GetComponent<Key>().LastSerrure.GetComponent<DoorLock>().BigDoorColider.enabled = true;
            }
         


        }



    }
}
