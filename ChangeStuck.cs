using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ChangeStuck : MonoBehaviour
{
    bool pressed = false;
    public void OnPress(InputAction.CallbackContext context){
        var driver = GetComponent<UnityEngine.SpatialTracking.TrackedPoseDriver>();
        if (pressed){
            driver.trackingType = UnityEngine.SpatialTracking.TrackedPoseDriver.TrackingType.RotationAndPosition;
            pressed = false;
        }
        else{
            driver.trackingType = UnityEngine.SpatialTracking.TrackedPoseDriver.TrackingType.RotationOnly;
            pressed = true;
        }
    }
}
