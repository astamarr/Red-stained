using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewmasterClipHandler : MonoBehaviour {
    public ViewmasterViewHandler viewHandler;
    public Animator clipChangeAnimator;
    public Transform newClipParent;
    public Transform currentClipParent;
    public Collider[] clips;
    public GameObject currentClip;
    public bool onlyChangeClipIfViewmasterEmpty = true;
    public GameObject soundEmitter;
    public string insertClipEvent = "viewmaster_in";
    public string removeClipEvent = "viewmaster_out";
    GameObject oldClip;
    Vector3 originalPosition;
    Quaternion originalRotation;
    bool doHack = false;
    bool isEmpty = true;
    
    void Awake () {
        MeshCollider viewmasterCollider = GetComponent<MeshCollider>();
        foreach (Collider clipCol in clips)
            Physics.IgnoreCollision(viewmasterCollider, clipCol, true);
        GameObject clip = currentClip;
        currentClip = null; // hack pour forcer le chargement du premier clip
        SetClip(clip, clip.transform.GetChild(0).GetComponent<Collider>(), true);
    }
    
    void Update () {
        if (currentClip && doHack)
        {
            // merci le VRTK useless qui passe son temps à supprimer le lien de parenté...
            currentClip.GetComponent<Rigidbody>().isKinematic = true;
            currentClip.transform.SetParent(newClipParent);
            currentClip.transform.localPosition = Vector3.zero;
            currentClip.transform.localRotation = Quaternion.identity;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        int i=0;
        foreach (Collider clipCol in clips)
        {
            if (other == clipCol)
            {
                GameObject clipObj = other.transform.parent.gameObject;
                if (currentClip == clipObj || oldClip == clipObj)
                    continue;
                VRTK.VRTK_InteractableObject interaction = clipObj.GetComponent<VRTK.VRTK_InteractableObject>();
                if (!interaction.IsGrabbed())
                    continue;
                if (!isEmpty && onlyChangeClipIfViewmasterEmpty)
                    continue;
                viewHandler.SetClip(i);
                SetClip(clipObj, other);
                break;
            }
            i++;
        }
    }
    
    void SetClip(GameObject clipObj, Collider other, bool fast=false)
    {
        if (isEmpty)
        {
            isEmpty = false;
            if (!fast)
                doHack = true;
            Collider collider = other;
            VRTK.VRTK_InteractableObject interaction = clipObj.GetComponent<VRTK.VRTK_InteractableObject>();
            Rigidbody body = clipObj.GetComponent<Rigidbody>();
            interaction.ForceStopInteracting();
            interaction.isGrabbable = false;
            // interaction.enabled = false;
            collider.enabled = false;
            originalPosition = clipObj.transform.localPosition;
            originalRotation = clipObj.transform.localRotation;
            if (!fast)
                clipObj.transform.SetParent(newClipParent);
            else
                clipObj.transform.SetParent(currentClipParent);
            clipObj.transform.localPosition = Vector3.zero;
            clipObj.transform.localRotation = Quaternion.identity;
            
            if (!fast)
                clipChangeAnimator.SetTrigger("PutIn");
            body.isKinematic = true;
            oldClip = currentClip;
            currentClip = clipObj;
          //  AkSoundEngine.PostEvent(removeClipEvent, soundEmitter);
            
            if (!fast)
                StartCoroutine(OnClipChangeComplete());
        }
        else
        {
            isEmpty = false;
            doHack = true;
            Collider collider = other;
            VRTK.VRTK_InteractableObject interaction = clipObj.GetComponent<VRTK.VRTK_InteractableObject>();
            Rigidbody body = clipObj.GetComponent<Rigidbody>();
            interaction.ForceStopInteracting();
            interaction.isGrabbable = false;
            // interaction.enabled = false;
            collider.enabled = false;
            originalPosition = clipObj.transform.localPosition;
            originalRotation = clipObj.transform.localRotation;
            clipObj.transform.SetParent(newClipParent);
            clipObj.transform.localPosition = Vector3.zero;
            clipObj.transform.localRotation = Quaternion.identity;
            
            clipChangeAnimator.SetTrigger("Change");
            body.isKinematic = true;
            oldClip = currentClip;
            currentClip = clipObj;
          //  AkSoundEngine.PostEvent(removeClipEvent, soundEmitter);
            
            StartCoroutine(OnClipChangeComplete());
        }
    }
    
    // Appelé par l'AnimationEvent marquant la fin de l'anim de changement de disque
    // UPDATE: merci Unity d'être useless, les animationevents fonctionnent pas. Yeah. On remplace la fonction par une coroutine...
    IEnumerator OnClipChangeComplete()
    {
        yield return new WaitForSeconds(1.9f);
        if (oldClip != null)
            AkSoundEngine.PostEvent(insertClipEvent, soundEmitter);
        yield return new WaitForSeconds(0.57f);
        doHack = false;
        currentClip.transform.SetParent(currentClipParent);
        currentClip.transform.localPosition = Vector3.zero;
        currentClip.transform.localRotation = Quaternion.identity;
        clipChangeAnimator.SetTrigger("Return");
        
        if (oldClip != null)
        {
            oldClip.transform.SetParent(null);
            oldClip.GetComponent<Rigidbody>().velocity = Vector3.zero;
            oldClip.GetComponent<Rigidbody>().isKinematic = false;
            oldClip.GetComponent<VRTK.VRTK_InteractableObject>().isGrabbable = true;
            oldClip.transform.GetChild(0).GetComponent<Collider>().enabled = true;
            yield return new WaitForSeconds(2);
            oldClip = null;
        }
    }
    
    public void ClipGrabEject(GameObject newClipGrabController)
    {
        viewHandler.SetClip(-1);
        currentClip.transform.SetParent(null);
        currentClip.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currentClip.GetComponent<Rigidbody>().isKinematic = false;
        currentClip.transform.GetChild(0).GetComponent<Collider>().enabled = true;
        currentClip.GetComponent<VRTK.VRTK_InteractableObject>().enabled = false;
        oldClip = currentClip;
        currentClip = null;
        StartCoroutine(SetOldClipToNull(newClipGrabController));
        isEmpty = true;
    }
    
    IEnumerator SetOldClipToNull(GameObject newClipGrabController)
    {
        yield return null;
        yield return null;
        oldClip.GetComponent<VRTK.VRTK_InteractableObject>().enabled = true;
        oldClip.GetComponent<VRTK.VRTK_InteractableObject>().isGrabbable = true;
        // oldClip.GetComponent<VRTK.VRTK_InteractableObject>().Grabbed(newClipGrabController);
        yield return new WaitForSeconds(2);
        oldClip = null;
    }
    
    public bool IsEmpty()
    {
        return isEmpty;
    }
}
