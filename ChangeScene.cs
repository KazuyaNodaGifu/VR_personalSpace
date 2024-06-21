using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ChangeScene : MonoBehaviour
{
    // Inspector
    [SerializeField] TMP_Dropdown dropdownFigureComponent;
    [SerializeField] TMP_Dropdown dropdownExperimentComponent;
    [SerializeField] TMP_Dropdown dropdownLimitationComponent;
    [SerializeField] TMP_Dropdown dropdownTallComponent;
    [SerializeField] TMP_Dropdown dropdownStuckComponent;
    [SerializeField] TMP_InputField inputD;
    [SerializeField] TMP_InputField inputFileName;
    [SerializeField] TMP_InputField inputId;
    [SerializeField] Toggle toggle_fade;
    string Id; 
    string agent;
    bool atop;
    bool stuck;
    byte form_tall;
    bool bool_fade;
    byte limitButtonTime;
    float inD;

    void OnClick()
    {
        if(inputFileName.text == ""){
            Id = "manual";
            agent = GetValueFigure();
            atop = GetValueExperiment();
            limitButtonTime = GetValueLimitation();
            inD = float.Parse(inputD.text);
        }
        else{
            Id = inputId.text;
            try{
                TextAsset csvFile = Resources.Load(inputFileName.text) as TextAsset; // Resouces下のCSV読み込み
                StringReader reader = new StringReader(csvFile.text);

                // , で分割しつつ一行ずつ読み込み
                // リストに追加していく
                while (reader.Peek() != -1) // reader.Peaekが-1になるまで
                {
                    string line = reader.ReadLine(); // 一行ずつ読み込み
                    Condition.csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
                }
                Condition.max_num_exp = (byte)(Condition.csvDatas.Count);
                Progress();
            }
            catch(FileNotFoundException e){
                Debug.Log(e);
            }
        }
        stuck = false;
        form_tall = 1;
        bool_fade = false;
        // stuck = GetValueStuck();
        // form_tall = GetValueTall();
        // bool_fade = GetValueFade();
        SetData();
        SceneManager.LoadScene("ExperimentScene");
    }

    // Condition.csに実験条件をセット
    void SetData(){
        Condition.id = Id;
        Condition.str_agent = agent;
        Condition.distance = inD;
        Condition.tallForm = form_tall;
        Condition.stuck = stuck;
        Condition.fade = bool_fade;
        Condition.atop = atop;
        Condition.limitButtonTime = limitButtonTime;
    }

        // Figureドロップダウンの値を取得する
    string GetValueFigure()
    {
        return dropdownFigureComponent.options[dropdownFigureComponent.value].text;
    }

        // Experimentドロップダウンの値を取得する
    bool GetValueExperiment()
    {
        string txt = dropdownExperimentComponent.options[dropdownExperimentComponent.value].text;
        if(txt == "Agent -> Participant"){
            return true;
        }
        else{
            return false;
        }
    }

        // Stuckドロップダウンの値を取得する
    bool GetValueStuck()
    {
        string txt = dropdownStuckComponent.options[dropdownStuckComponent.value].text;
        if(txt == "true"){
            return true;
        }
        else{
            return false;
        }
    }

        // Limitationドロップダウンの値を取得する
    byte GetValueLimitation()
    {
        string txt = dropdownLimitationComponent.options[dropdownLimitationComponent.value].text;
        if(txt == "once"){
            return 1;
        }
        else{
            return 255;
        }
    }

    // tall の調整方法
    byte GetValueTall()
    {
        string txt = dropdownTallComponent.options[dropdownTallComponent.value].text;
        if(txt == "Agent"){
            return 0;
        }
        else if(txt == "Static"){
            return 1;
        }
        else{
            return 2;
        }
    }

    // fade のon off
    bool GetValueFade()
    {
        return toggle_fade.isOn;
    }
}
