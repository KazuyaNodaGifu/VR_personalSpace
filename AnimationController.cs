using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator AnimationFigure = null;

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.W)) {
            // GetComponent<Animator>().enabled = false;
            AnimationFigure.SetTrigger("WalkTrigger");
        }

        else if (Input.GetKey (KeyCode.S)) {
            // GetComponent<Animator>().enabled = false;
            AnimationFigure.SetTrigger("StopTrigger");
        }
    }
}
