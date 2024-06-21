using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
 
 //コントローラの入力管理
public class SteamVRInputTest: MonoBehaviour
{
    public SteamVR_Action_Boolean TriggerClick;
    private SteamVR_Input_Sources inputSource;

    public GameObject stop_figure;
    public GameObject walk_figure;
    private bool stop_figure_flag = true;
    private bool walk_figure_flag = false;
 
    private void Start() {
        stop_figure.SetActive(true);
        walk_figure.SetActive(false);
    } //Monobehaviours without a Start function cannot be disabled in Editor, just FYI
 
    private void OnEnable()
    {
        TriggerClick.AddOnStateDownListener(Press, inputSource);
    }
 
    private void OnDisable()
    {
        TriggerClick.RemoveOnStateDownListener(Press, inputSource);
    }
 
    private void Press(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //put your stuff here
        // print("Success");

        if(stop_figure_flag){
            stop_figure.SetActive(false);
            walk_figure.SetActive(true);
            stop_figure_flag = false;
        }
        else{
            stop_figure.SetActive(true);
            walk_figure.SetActive(false);
            stop_figure_flag = true;
        }
        Debug.Log("stop_figure_flag"+stop_figure_flag);
    }
}