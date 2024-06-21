using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR;
using static Condition;

public class Reload_location : MonoBehaviour
{
    [SerializeField] Transform vr;
    Transform camera;
    double convert = Math.PI / 180;
    float delay = 1.0f;

    void Start(){
        camera = vr.transform.GetChild(2);
        //StartCoroutine(Initial_reset());
    }

    IEnumerator Initial_reset(){
        yield return new WaitForSeconds(delay);
        Reset_location();
    }

    void OnReload_action(InputValue value){
        // ボタンを離すときは反応なし
        if(value.Get<float>() != 1) return;
        Reset_location();
    }

    // HMDの位置を調整する関数
    void Reset_location(){
        vr.position = new Vector3(0, 0, 0);

        // 向き調整
        float angle = camera.localEulerAngles.y;
        Condition.angle = new Vector3(0, -angle, 0);
        vr.eulerAngles = Condition.angle;

        // 位置調整
        Vector3 loc = camera.localPosition;
        double sin = Math.Sin(angle * convert);
        double cos = Math.Cos(angle * convert);
        float x = (float)(loc.x * cos - loc.z * sin);
        float z = (float)(loc.z * cos + loc.x * sin);
        Vector3 pos = vr.position;
        Condition.pos = new Vector3(pos.x-x, 0, pos.z-z);
        vr.position = Condition.pos;
    }
}