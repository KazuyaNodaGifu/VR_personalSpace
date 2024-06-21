using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure_position : MonoBehaviour
{
    public GameObject figure1;
    public GameObject figure2;
    public GameObject position_ball1;
    public GameObject position_ball2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position_ball1.transform.position= new Vector3(figure1.transform.position.x, 0, figure1.transform.position.z);
        position_ball2.transform.position= new Vector3(figure2.transform.position.x, 0, figure2.transform.position.z);
    }
}
