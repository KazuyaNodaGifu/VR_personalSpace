using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Fade : MonoBehaviour
{
    public bool active;
    float time = 1.0f;
    Color trans = new Color(0f, 0f, 0f, 0f);
    void Start()
    {
        active = Condition.fade;
        if(active)  StartCoroutine(init());
    }

    IEnumerator init(){
        SteamVR_Fade.Start(Color.black, 0f);
        yield return new WaitForSeconds(1.0f);
        Fade_in();
    }

    public void Fade_in(){
        if(active)  SteamVR_Fade.Start(trans, time);
    }

    public void Fade_out(){
        if(active)  SteamVR_Fade.Start(Color.black, time);
    }
}
