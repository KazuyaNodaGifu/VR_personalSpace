using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class Pos_Text : MonoBehaviour
{
    //HMDの位置座標格納用
    public Vector3 HMDPosition;
    //HMDの回転座標格納用（クォータニオン）
    private Quaternion HMDRotationQ;
    //HMDの回転座標格納用（オイラー角）
    private Vector3 HMDRotation;

    //左コントローラの位置座標格納用
    private Vector3 LeftHandPosition;
    //左コントローラの回転座標格納用（クォータニオン）
    private Quaternion LeftHandRotationQ;
    //左コントローラの回転座標格納用
    private Vector3 LeftHandRotation;

    //右コントローラの位置座標格納用
    private Vector3 RightHandPosition;
    //右コントローラの回転座標格納用（クォータニオン）
    private Quaternion RightHandRotationQ;
    //右コントローラの回転座標格納用
    private Vector3 RightHandRotation;
    


///////////////////////////////////////////
    // public Text TextFrame;
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    [SerializeField]
    private TextMeshProUGUI AngleText;

    // public GameObject figure;
    public GameObject camera;

    public Vector3 posFigure;
    private Vector3 posCamera;

    public GameObject stop_figure;
    public GameObject walk_figure;

    private GameObject figure;

    

    private bool walk_flag = false;

    // public GameObject stop_figure;

    //1フレーム毎に呼び出されるUpdateメゾット
    void Update()
    {
        if(stop_figure.activeInHierarchy){
            figure = stop_figure;
            walk_flag = false;
        }
        else{
            figure = walk_figure;
            walk_flag = true;
        }

        /*InputTracking.GetLocalPosition(XRNode.機器名)で機器の位置や向きを呼び出せる*/

        //Head（ヘッドマウンドディスプレイ）の情報を一時保管-----------
        //位置座標を取得
        HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
        //回転座標をクォータニオンで値を受け取る
        HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
        //取得した値をクォータニオン → オイラー角に変換
        HMDRotation = HMDRotationQ.eulerAngles;
        //--------------------------------------------------------------


        //LeftHand（左コントローラ）の情報を一時保管--------------------
        //位置座標を取得
        LeftHandPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
        //回転座標をクォータニオンで値を受け取る
        LeftHandRotationQ = InputTracking.GetLocalRotation(XRNode.LeftHand);
        //取得した値をクォータニオン → オイラー角に変換
        LeftHandRotation = LeftHandRotationQ.eulerAngles;
        //--------------------------------------------------------------


        //RightHand（右コントローラ）の情報を一時保管--------------------
        //位置座標を取得
        RightHandPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
        //回転座標をクォータニオンで値を受け取る
        RightHandRotationQ = InputTracking.GetLocalRotation(XRNode.RightHand);
        //取得した値をクォータニオン → オイラー角に変換
        RightHandRotation = RightHandRotationQ.eulerAngles;
        //--------------------------------------------------------------


        //取得したデータを表示（HMDP：HMD位置，HMDR：HMD回転，LFHR：左コン位置，LFHR：左コン回転，RGHP：右コン位置，RGHR：右コン回転）
        // Debug.Log("HMDP:" + HMDPosition.x + ", " + HMDPosition.y + ", " + HMDPosition.z + "\n" +
        //             "HMDR:" + HMDRotation.x + ", " + HMDRotation.y + ", " + HMDRotation.z);
        // Debug.Log("LFHP:" + LeftHandPosition.x + ", " + LeftHandPosition.y + ", " + LeftHandPosition.z + "\n" +
        //             "LFHR:" + LeftHandRotation.x + ", " + LeftHandRotation.y + ", " + LeftHandRotation.z);
        // Debug.Log("RGHP:" + RightHandPosition.x + ", " + RightHandPosition.y + ", " + RightHandPosition.z + "\n" +
        //             "RGHR:" + RightHandRotation.x + ", " + RightHandRotation.y + ", " + RightHandRotation.z);

    ////////////////////////////////////////////

    // camera.transform.position = HMDPosition;

    if(walk_flag){
        posFigure = transform.TransformPoint(figure.transform.position);
        posFigure.z = -1*posFigure.z;
    }
    else{
        posFigure = figure.transform.position;
    }
    
    // posCamera = camera.transform.position;
    float dist_x = posFigure.x - HMDPosition.x;
    float dist_z = posFigure.z - (-HMDPosition.z);
    float distance = Mathf.Sqrt(dist_x * dist_x + dist_z * dist_z);

    // float dis = Vector3.Distance(posFigure,posCamera);
    
    // cardNameText.text = string.Format("HMD_X", HMDPosition.x);
    cardNameText.text = string.Format("{0}m:HMDPosition.x\n{1}m:HMDPosition.y\n{2}m:HMDPosition.z", HMDPosition.x, HMDPosition.y, HMDPosition.z);
    cardNameText.text += string.Format("\n{0}:figPosition", posFigure);

    AngleText.text = string.Format("HMDRotation.x:{0}\nHMDRotation.y:{1}\nHMDRotation.z:{2}\nRightHandPosition:{3}\nLeftHandPosition:{4}", HMDRotation.x, HMDRotation.y, HMDRotation.z,RightHandPosition,LeftHandPosition);
    // Debug.Log("HMDP:" + HMDPosition.x);

    Debug.Log("figure activeSelf : " + figure.activeInHierarchy);
    }
}