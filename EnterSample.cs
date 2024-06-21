using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class EnterSample : MonoBehaviour{
    bool stopping = true;
    [SerializeField] Animator AnimationFigure = null;
    // when [enter] pressed
    public void OnPress(InputAction.CallbackContext context){
        if(stopping){
            stopping = false;
            AnimationFigure.SetTrigger("WalkTrigger");
            Debug.Log("kitayo");
        }
        else{
            stopping = true;
            AnimationFigure.SetTrigger("StopTrigger");
        }
    }
}
