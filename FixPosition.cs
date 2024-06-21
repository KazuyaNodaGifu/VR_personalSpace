using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Condition;

public class FixPosition : MonoBehaviour
{
    [SerializeField] Transform agent;
    [SerializeField] Transform eye_l;
    [SerializeField] Transform eye_r;
    float agent_y;
    
    void Start(){
        StartCoroutine(Set_position());
    }

    IEnumerator Set_position(){
        yield return new WaitForSeconds(0.1f);
        agent_y = (eye_l.position.y + eye_r.position.y) / 2.0f;
        switch(Condition.tallForm){
        case 0:
            Set_0();
            break;
        case 1:
            Set_1();
            break;
        case 2:
            Set_2();
            break;
        default:
            break;
        }
        // Debug.Log(eye_l.position.y);
        // Debug.Log(transform.GetChild(2).position.y);
    }

    // agentのサイズ変更
    void Set_0(){
        Vector3 pos_hmd = transform.GetChild(2).position;
        float ratio = pos_hmd.y / agent_y;
        Vector3 scale = agent.localScale;
        scale.x *= ratio;
        scale.y *= ratio;
        scale.z *= ratio;
        agent.localScale = scale;
    }

    // 男性は174.2cm, 女性は160.65に固定
    void Set_1(){
        double y_scale = 0;
        string name_scene = SceneManager.GetActiveScene().name;
        if (name_scene == "Genesis8Woman_WalkStop"){
            y_scale = 160.65 / 180.03;
        }
        else if(name_scene == "asian_f"){
            y_scale = 160.65 / 171.43;
        }
        else if(name_scene == "asian_m"){
            y_scale = 174.2 / 173.47;
        }
        else if(name_scene == "white_f"){
            y_scale = 160.65 / 179.51;
        }
        else if(name_scene == "white_m"){
            y_scale = 174.2 / 183.45;
        }
        agent.localScale = new Vector3(1f, (float)(y_scale), 1f);
    }

    // hmdの位置を調整
    void Set_2(){
        Vector3 pos_hmd = transform.GetChild(2).position;
        float offset = agent_y - pos_hmd.y;
        Vector3 pos_steal = transform.position;
        pos_steal.y += offset;
        transform.position = pos_steal;
    }
}
