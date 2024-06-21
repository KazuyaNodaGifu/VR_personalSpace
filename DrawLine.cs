using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
public class DrawLine : MonoBehaviour
{
    Color c1 = Color.black;
    private Vector3 HMDPosition;

    public GameObject figure;
    private Vector3 posFigure;
    

   private LineRenderer lineRenderer;

    void Start()
    {
    lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    void Update()
    {
        Pos_Text pos_text = GetComponent<Pos_Text>();
        // var lineRenderer = gameObject.AddComponent<LineRenderer>();

        

        // var positions = new Vector3[]{
        // new Vector3(pos_text.HMDPosition.x, 0, pos_text.HMDPosition.z),               // 開始点
        // new Vector3(pos_text.posFigure.x, 0, pos_text.posFigure.z),               // 終了点
        // };
        var test =new Vector3(pos_text.HMDPosition.x, 0, pos_text.HMDPosition.z);
var positions = new Vector3[]{
        new Vector3(pos_text.HMDPosition.x, 0, pos_text.HMDPosition.z),               // 開始点
        new Vector3(pos_text.posFigure.x, 0, pos_text.posFigure.z),               // 終了点
        };

        Debug.Log(test);

// 線を引く場所を指定する
    // lineRenderer.SetPositions(positions);
    lineRenderer.SetPosition(0,test);
    lineRenderer.SetPosition(1,new Vector3(pos_text.posFigure.x, 0, pos_text.posFigure.z));

        lineRenderer.startWidth = 0.1f;
    lineRenderer.endWidth = 0.1f;
    lineRenderer.SetColors(c1,c1);

    }

}