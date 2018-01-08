using UnityEngine;
using VRTK;

public class ViewMasterGrabAction : VRTK.SecondaryControllerGrabActions.VRTK_BaseGrabAction
{
    public ViewmasterClipHandler clipHandler;
    
    protected virtual void Awake()
    {
        isActionable = true;
        isSwappable = false;
    }
    
    public override void Initialise(VRTK_InteractableObject currentGrabbdObject, VRTK_InteractGrab currentPrimaryGrabbingObject, VRTK_InteractGrab currentSecondaryGrabbingObject, Transform primaryGrabPoint, Transform secondaryGrabPoint)
    {
        Debug.Log("ViewMasterGrabAction: Initialise");
        base.Initialise(currentGrabbdObject, currentPrimaryGrabbingObject, currentSecondaryGrabbingObject, primaryGrabPoint, secondaryGrabPoint);
        if (!clipHandler.IsEmpty())
            clipHandler.ClipGrabEject(currentSecondaryGrabbingObject.gameObject);
    }
}
