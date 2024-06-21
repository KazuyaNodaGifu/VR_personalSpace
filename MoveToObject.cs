using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObject : MonoBehaviour
{   
    [SerializeField] private Camera Main_camera;
    [SerializeField] private Camera Sub_camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     Sub_camera.transform.position = Main_camera.transform.position;   
     Sub_camera.transform.rotation = Main_camera.transform.rotation;
    }
}
