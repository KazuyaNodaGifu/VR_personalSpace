using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using Valve.VR;

public class variable_test : MonoBehaviour
{
    // UI Text指定用
    // public Text TextFrame;
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    // 表示する変数
    private int frame;

    //HMDの位置座標格納用
    private Vector3 HMDPosition;

    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Head（ヘッドマウンドディスプレイ）の情報を一時保管-----------
        //位置座標を取得
        HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);

        // cardNameText.text = string.Format("{0:00000} frame", frame);
        // frame++;
        cardNameText.text = "HMD_X"+HMDPosition.x;
    }
}
