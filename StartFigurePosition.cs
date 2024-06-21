using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Condition;

public class StartFigurePosition : MonoBehaviour
{
    [SerializeField] GameObject vr;
    [SerializeField] public GameObject ListenerEnter;

    public bool debug = false;
    // Start is called before the first frame update
    void Start()
    {
        if(debug)   return;

        // 実験条件の読み込み
        Progress();

        // 実験条件の反映
        Condition.agent = Agents.agents[Condition.str_agent];
        Vector3 pos = Condition.agent.transform.position;
        Condition.agent.transform.position = new Vector3(pos.x, pos.y, Condition.distance);
        vr.transform.position = Condition.pos;
        vr.transform.eulerAngles = Condition.angle;
        SetExperimentText();
    }

    // csvの行を読み込む
    public void Progress(){
        Condition.num_exp += 1;
        int index = Condition.num_exp;
        Condition.str_agent = Condition.csvDatas[index][0];
        if(Condition.csvDatas[index][1] == "atop"){
            atop = true;
        }
        else{
            atop = false;
        }
        inD = byte.Parse(Condition.csvDatas[index][2]);
        limitButtonTime = byte.Parse(Condition.csvDatas[index][3]);
        Debug.Log(agent);
        Debug.Log(atop);
        Debug.Log(inD);
        Debug.Log(limitButtonTime);
        SetData();
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

    // Set experiment info at canvas
    void SetExperimentText(){
        TextMeshProUGUI text_box = GameObject.Find("text_experiment").GetComponent<TextMeshProUGUI>();
        string s_atop = "";
        if(Condition.atop){
            s_atop = "AtoP";
        }
        else{
            s_atop = "PtoA";
        }
        text_box.text = "experiment: No." + Condition.num_exp.ToString() + ", " + s_atop + ", " + Condition.str_agent;
    }
}