using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ReactInput : MonoBehaviour
{
    TextMeshProUGUI[] text_guis;
    byte r = 0;
    byte s = 1;
    byte f = 2;
    byte spc = 3;
    byte esc = 4;
    // Start is called before the first frame update
    void Start()
    {
        text_guis = new TextMeshProUGUI[5];
        for(int i=0; i<text_guis.Length; i++){
            text_guis[i] = transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
        }
    }

    // テキストの色を変える
    void ChangeColorRed(byte index){
        text_guis[index].color = Color.red;
    }
    void ChangeColorMagenta(byte index){
        text_guis[index].color = Color.magenta;
    }

    // 各inputのイベント
    // void OnReload_action(InputValue value){
    //     if((int)(value.Get<float>()) == 0){
    //         ChangeColorMagenta(r);
    //     }
    //     else{
    //         ChangeColorRed(r);
    //     }
    // }

    void OnStart_recording(InputValue value){
        if((int)(value.Get<float>()) == 0){
            ChangeColorMagenta(s);
        }
        else{
            ChangeColorRed(s);
        }
    }

    void OnStop_recording(InputValue value){
        if((int)(value.Get<float>()) == 0){
            ChangeColorMagenta(f);
        }
        else{
            ChangeColorRed(f);
        }
    }

    void OnSpace_pressed(InputValue value){
        if((int)(value.Get<float>()) == 0){
            ChangeColorMagenta(spc);
        }
        else{
            ChangeColorRed(spc);
        }
    }

    void OnEscape(){
        ChangeColorRed(esc);
    }
}
