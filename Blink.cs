using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Condition;

public class Blink : MonoBehaviour
{
    [SerializeField] float blinkInterval = 5.0f;
    Animator anim;
    bool proceed = true;

    void Start()
    {
        StartCoroutine(BlinkCoroutine());
        anim = Condition.agent.GetComponent<Animator>();
    }

    // blinkInterval秒ごとに瞬き
    IEnumerator BlinkCoroutine(){
        while(proceed){
            yield return new WaitForSeconds(blinkInterval);
            anim.SetTrigger("BlinkTrigger");
        }
    }

    // 瞬きの速さを変更
    void SetBlinkSpeed(float speed){
        anim.SetFloat("BlinkSpeed", speed);
    }

    // 瞬きの間隔を変更
    void SetBlinkInterval(float interval){
        blinkInterval = interval;
    }
}
