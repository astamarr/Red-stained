using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LuminolUse : VRTK_InteractableObject
{

    LuminolDetection detector;
    public GameObject lastroom;
    public GameObject pchitprefab;
    ParticleSystem pchit;
    public Transform pchittransform;
    private VRTK_ControllerActions controllerActions;
    // Use this for initialization
    void Start () {
        detector = this.gameObject.GetComponentInChildren<LuminolDetection>();
        pchit = pchitprefab.GetComponent<ParticleSystem>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Grabbed(GameObject currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);

        controllerActions = currentGrabbingObject.GetComponent<VRTK.VRTK_ControllerActions>();

        if (lastroom.activeSelf == false)
        {

            lastroom.SetActive(true);
        }



    }
    public override void StartUsing(GameObject usingObject)
    {
       
        AkSoundEngine.PostEvent("spray", this.gameObject);
        StartCoroutine(vibrations());
        StartCoroutine(particule());
        detector.Detect();
        base.StartUsing(usingObject);

    }

    public IEnumerator particule()
    {
        GameObject newPchit = Instantiate(pchitprefab);
        newPchit.transform.position = pchittransform.position;
        newPchit.transform.rotation = pchittransform.rotation;

        yield return new WaitForSeconds(pchit.main.duration * 3);
        Destroy(newPchit);
        


    }


    public IEnumerator vibrations()
    {
        yield return new WaitForSeconds(0.1f);
        controllerActions.TriggerHapticPulse(1f);
        yield return new WaitForSeconds(0.4f);
        controllerActions.TriggerHapticPulse(1f);


    }
}
