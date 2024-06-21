using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameClose : MonoBehaviour
{
    public void OnEscape(){
        StartCoroutine(quit_coroutine());
    }

    IEnumerator quit_coroutine(){
        GameObject.Find("Camera").GetComponent<Fade>().Fade_out();
        yield return new WaitForSeconds(1.5f);
        if(Condition.num_exp < Condition.max_num_exp){
            ChangeScene.Progress();
            SceneManager.LoadScene("ExperimentScene");
        }
        else{
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
            {}
        }
    }
}
