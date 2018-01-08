using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class Repondeur : VRTK_InteractableObject
{

    private VRTK_ControllerActions controllerActions;

    bool isUsed = false;
    bool alreadybeenused = false;
    bool HasFinished = false;

    // Use this for initialization
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isUsed && !AkSoundEngine.GetIsGameObjectActive(this.gameObject))
   
        {
            isUsed = false;
            this.gameObject.GetComponentInChildren<blinklight>().usage(isUsed);
        }




        // Debug.Log(isUsed);
      

    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        controllerActions = usingObject.GetComponent<VRTK.VRTK_ControllerActions>();

        
        
        if (!isUsed)
        {
            

           
            
            bool a = AkSoundEngine.GetIsGameObjectActive(this.gameObject);
            if (!alreadybeenused)
            {
                StartCoroutine(vibrations());
                AkSoundEngine.PostEvent("answering_machine", this.gameObject);

            }
            else
            {
                StartCoroutine(vibrations());
                AkSoundEngine.PostEvent("answering_machine_2", this.gameObject);

            }
          
            controllerActions.TriggerHapticPulse(0.63f, 0.2f, 0.01f);

            bool b  =  AkSoundEngine.GetIsGameObjectActive(this.gameObject);
            controllerActions.TriggerHapticPulse(0.63f, 0.2f, 0.01f);
            isUsed = true;
            alreadybeenused = true;
            this.gameObject.GetComponentInChildren<blinklight>().usage(isUsed);
        }

    }
    public IEnumerator vibrations()
    {
        yield return new WaitForSeconds(0.1f);
        controllerActions.TriggerHapticPulse(1f);
        yield return new WaitForSeconds(1f);


    }
}



