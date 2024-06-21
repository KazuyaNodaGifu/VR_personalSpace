using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class rotation_ui : MonoBehaviour
{
    [SerializeField] GameObject vr;
    [SerializeField] GameObject tmp_input;
    public void OnClick()
    {
        var inputR = tmp_input.GetComponent<TMP_InputField>();
        float inR = float.Parse(inputR.text);
        Vector3 rotate = new Vector3(0, inR, 0);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        vr.transform.eulerAngles = rotate;
    }
}
