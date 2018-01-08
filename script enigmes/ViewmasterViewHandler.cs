using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// TODO: rajouter icône indiquant la rotation
public class ViewmasterViewHandler : MonoBehaviour {
    [System.Serializable]
    public class ViewmasterPhotoClip {
        public string name;
        public Texture[] images;
    }
    
    public AspectRatioFitter imageFitter;
    public RawImage curPortrait;
    public Camera renderCam;
    public CanvasGroup portraitsGroup;
    public MeshRenderer blackoutMesh;
    public VRTK.VRTK_InteractableObject viewmasterObject;
    public Transform[] viewmasterObjectAnchors;
    public Transform portraitOrientation;
    
    public Transform leftImageTransform;
    public Transform rightImageTransform;
    public Transform leftImageTransformNext;
    public Transform rightImageTransformNext;
    public Transform leftImageTransformPrev;
    public Transform rightImageTransformPrev;
    
    public RawImage leftImage;
    public RawImage rightImage;
    public RawImage leftImageNext;
    public RawImage rightImageNext;
    public RawImage leftImagePrev;
    public RawImage rightImagePrev;
    
    public ViewmasterPhotoClip[] photoClips;
    public Transform viewmasterGrabbableClipRotation;
    
    public float minDistance = .17f;
    public float maxDistance = .3f;
    public float rotateSensitivity = .3f;
    
    public GameObject soundEmitter;
    public Animator triggerAnimator;
    public string switchPictureEvent = "viewmaster_switch";
    public VRTK.VRTK_BasicTeleport teleportHandler;
    
    int currentPictureIndex=0;
    
    float blackoutProgression = 1;
    float canvasProgression = 0;
    float lastRotation;
    bool pressingButton = false;
    float currentPortraitRotation = 0;
    float currentClipRotation = 0;
    ViewmasterPhotoClip currentClip;
    bool firstTimeUsing = true;
    Coroutine firstTimeUsingHapticCoroutine;
    VRTK.VRTK_ControllerActions lastControllerActions;
    bool ignoreTrigger = false;
    bool autoMoveToNextPicture = false;

    void Awake () {
        currentClip = photoClips[0];
        updateImage();
        firstTimeUsingHapticCoroutine = StartCoroutine(FirstTimeUsingHapticCoroutine());
    }
    
    void Update() {
        // calcul de la progression du fondu au noir
        float oldProgression = blackoutProgression;
        if (viewmasterObject.IsGrabbed())
        {
            float distance=999;
            foreach (Transform anchorPoint in viewmasterObjectAnchors)
                distance = Mathf.Min(distance, Vector3.Distance(anchorPoint.position, Camera.main.transform.position));
            float range = maxDistance - minDistance;
            blackoutProgression = (distance - minDistance) / range;
        }
        else
            blackoutProgression += Time.deltaTime;
        blackoutProgression = Mathf.Clamp(blackoutProgression, 0, 1);
        Color matCol = blackoutMesh.material.color;
        matCol.a = 1-blackoutProgression*.9f;
        blackoutMesh.material.color = matCol;
        
        renderCam.enabled = blackoutProgression <= 1;
        if (blackoutProgression < 1 && oldProgression >= 1)
        {
            // calcul de la position de l'image en fonction de la tête du joueur
            portraitOrientation.localPosition = renderCam.transform.localPosition + new Vector3(0,-.1f,0);
            portraitOrientation.localRotation = Quaternion.Euler(
                portraitOrientation.localEulerAngles.x,
                renderCam.transform.localEulerAngles.y,
                portraitOrientation.localEulerAngles.z);
            // désactivation TP
            teleportHandler.enabled = false;
        }
        else if (blackoutProgression >= 1 && oldProgression < 1)
            teleportHandler.enabled = true;
        
        // calcul de la progression du fondu de l'image
        if (blackoutProgression <= 0)
            canvasProgression += Time.deltaTime;
        else
            canvasProgression -= Time.deltaTime*7;
        
        canvasProgression = Mathf.Clamp(canvasProgression, 0, 1);
        portraitsGroup.alpha = canvasProgression;
        
        if (blackoutProgression <= 0)
        {
            float angleBetweenPictures = 30;
            if (currentClip != null)
                angleBetweenPictures = 180f / currentClip.images.Length;
            // float angleBetweenPictures = 30; // à n'utiliser que si l'autre n'est pas assez intuitif
            
            GameObject grabbingObj = viewmasterObject.GetGrabbingObject();
            if (grabbingObj != null)
            {
                VRTK.VRTK_ControllerEvents eventCtrl = grabbingObj.GetComponent<VRTK.VRTK_ControllerEvents>();
                lastControllerActions = grabbingObj.GetComponent<VRTK.VRTK_ControllerActions>();
                if (eventCtrl != null)
                {
                    // on est en train de visionner l'image, on récupère la manette en train de tenir le VM afin de récupérer les swipes sur le pavé tactile
                    eventCtrl.axisFidelity = 5;
                    bool wasPressing = pressingButton;
                    pressingButton = eventCtrl.IsButtonPressed(VRTK.VRTK_ControllerEvents.ButtonAlias.Touchpad_Touch);
                    if (pressingButton && !wasPressing)
                        lastRotation = eventCtrl.GetTouchpadAxisAngle();
                    float trigger = eventCtrl.GetTriggerAxis();
                    if (trigger != 1)
                        ignoreTrigger = false;
                    if (trigger == 1 && !ignoreTrigger)
                    {
                        ignoreTrigger = true;
                        autoMoveToNextPicture = true;
                        StartCoroutine(TriggerHapticCoroutine());
                    }
                    if (pressingButton && trigger == 0 && false)
                    {
                        float oldPortraitRotation = currentPortraitRotation;
                        // en train de faire un swipe
                        float currentPadRotation = eventCtrl.GetTouchpadAxisAngle();
                        if (lastRotation - currentPadRotation < 180 && lastRotation - currentPadRotation > -180)
                            currentPortraitRotation += (lastRotation - currentPadRotation) * rotateSensitivity;
                        if (lastControllerActions != null && ((int)(currentPadRotation)/35) != ((int)(lastRotation)/35))
                            lastControllerActions.TriggerHapticPulse(.05f);
                        if (currentPortraitRotation > angleBetweenPictures/2 && oldPortraitRotation < angleBetweenPictures/2)
                            StartCoroutine(TriggerHapticCoroutine());
                        if (currentPortraitRotation < -angleBetweenPictures/2 && oldPortraitRotation > -angleBetweenPictures/2)
                            StartCoroutine(TriggerHapticCoroutine());
                        lastRotation = currentPadRotation;
                    }
                }
            }
            
            if (!pressingButton || autoMoveToNextPicture)
            {
                if (autoMoveToNextPicture)
                    currentPortraitRotation += Time.deltaTime * (angleBetweenPictures+.01f-currentPortraitRotation) * 10;
                else
                {
                    // retour à l'image la plus proche
                    if (currentPortraitRotation > angleBetweenPictures/2)
                        currentPortraitRotation += Time.deltaTime * (angleBetweenPictures+.01f-currentPortraitRotation) * 10;
                    else if (currentPortraitRotation < -angleBetweenPictures/2)
                        currentPortraitRotation += Time.deltaTime * -(angleBetweenPictures+.01f+currentPortraitRotation) * 10;
                    else
                        currentPortraitRotation -= Time.deltaTime * currentPortraitRotation * 10;
                }
            }
            
            if (currentPortraitRotation > angleBetweenPictures)
            {
                currentPortraitRotation -= angleBetweenPictures;
                nextImage();
            }
            if (currentPortraitRotation < -angleBetweenPictures)
            {  
                currentPortraitRotation += angleBetweenPictures;
                previousImage();
            }
            
            // rotation de l'image
            leftImageTransform.transform.localRotation = Quaternion.Euler(0, 0, currentPortraitRotation);
            rightImageTransform.transform.localRotation = Quaternion.Euler(0, 0, currentPortraitRotation);
            leftImageTransformNext.transform.localRotation = Quaternion.Euler(0, 0, currentPortraitRotation+angleBetweenPictures);
            leftImageTransformPrev.transform.localRotation = Quaternion.Euler(0, 0, currentPortraitRotation-angleBetweenPictures);
            rightImageTransformNext.transform.localRotation = Quaternion.Euler(0, 0, currentPortraitRotation+angleBetweenPictures);
            rightImageTransformPrev.transform.localRotation = Quaternion.Euler(0, 0, currentPortraitRotation-angleBetweenPictures);
        }
        else
        {
            // rotation du disque du grabbable, pas de la vue.
            
            GameObject grabbingObj = viewmasterObject.GetGrabbingObject();
            if (grabbingObj != null)
            {
                VRTK.VRTK_ControllerEvents eventCtrl = grabbingObj.GetComponent<VRTK.VRTK_ControllerEvents>();
                lastControllerActions = grabbingObj.GetComponent<VRTK.VRTK_ControllerActions>();
                if (eventCtrl != null)
                {
                    // on est en train de visionner l'image, on récupère la manette en train de tenir le VM afin de récupérer les swipes sur le pavé tactile
                    eventCtrl.axisFidelity = 5;
                    bool wasPressing = pressingButton;
                    pressingButton = eventCtrl.IsButtonPressed(VRTK.VRTK_ControllerEvents.ButtonAlias.Touchpad_Touch);
                    if (pressingButton && !wasPressing)
                        lastRotation = eventCtrl.GetTouchpadAxisAngle();
                    if (pressingButton && false)
                    {
                        // en train de faire un swipe
                        float currentPadRotation = eventCtrl.GetTouchpadAxisAngle();
                        if (lastRotation - currentPadRotation < 180 && lastRotation - currentPadRotation > -180)
                            currentClipRotation += (lastRotation - currentPadRotation) * rotateSensitivity;
                        if (lastControllerActions != null && ((int)(currentPadRotation)/275) != ((int)(lastRotation)/275))
                        {

                            triggerAnimator.SetTrigger("Trigger");
                        }
                        lastRotation = currentPadRotation;
                        
                        viewmasterGrabbableClipRotation.transform.localRotation = Quaternion.Euler(currentClipRotation, 0, 0);
                    }
                }
            }
        }
    }
    
    void previousImage()
    {
        
        currentPictureIndex--;
        int numImages = 6;
        if (currentClip != null)
            numImages = currentClip.images.Length;
        currentPictureIndex = pythonMod(currentPictureIndex, numImages);
        firstTimeUsing = false;
    
        updateImage();
;
         
    }
    void nextImage()
    {
       
        currentPictureIndex++;
        int numImages = 6;
        if (currentClip != null)
            numImages = currentClip.images.Length;
        currentPictureIndex = pythonMod(currentPictureIndex, numImages);
        firstTimeUsing = false;
       
        updateImage();

      
    }
    
    void updateImage()
    {
        if (currentClip != null)
        {
            leftImage.texture = currentClip.images[currentPictureIndex];
            leftImageNext.texture = currentClip.images[pythonMod(currentPictureIndex-1, currentClip.images.Length)];
            leftImagePrev.texture = currentClip.images[pythonMod(currentPictureIndex+1, currentClip.images.Length)];
            rightImage.texture = currentClip.images[currentPictureIndex];
            rightImageNext.texture = currentClip.images[pythonMod(currentPictureIndex-1, currentClip.images.Length)];
            rightImagePrev.texture = currentClip.images[pythonMod(currentPictureIndex+1, currentClip.images.Length)];
        }
        else
        {
            leftImage.texture = leftImageNext.texture = leftImagePrev.texture =
            rightImage.texture = rightImageNext.texture = rightImagePrev.texture = null;
        }
        autoMoveToNextPicture = false;
    }
    
    // Envoi des vibrations dans la manette jusqu'à ce que le joueur se décide à tourner les photos
    IEnumerator FirstTimeUsingHapticCoroutine()
    {
        while (firstTimeUsing)
        {
            yield return new WaitForSeconds(1);
            if (lastControllerActions != null && blackoutProgression <= 0 && !pressingButton)
                lastControllerActions.TriggerHapticPulse(.1f);
        }
    }
    
    // grosses vibrations dans manette, synchro avec le son de gachette
    IEnumerator TriggerHapticCoroutine()
    {
        if (lastControllerActions == null)
            yield break;

        AkSoundEngine.PostEvent(switchPictureEvent, soundEmitter);
 
        lastControllerActions.TriggerHapticPulse(1f);
        yield return new WaitForSeconds(.4f);
        lastControllerActions.TriggerHapticPulse(1f);
    }
    
    int pythonMod(int a, int b)
    {
        if (a >= 0)
            return a%b;
        return b - Mathf.Abs(a) % b;
    }
    
    public void SetClip(int i)
    {
        currentPortraitRotation = currentPictureIndex = 0;
        if (i < 0)
            currentClip = null;
        else
            currentClip = photoClips[i];
        
        updateImage();
    }
}
