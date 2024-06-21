using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agents : MonoBehaviour
{
    [SerializeField] List<GameObject> models;
    [SerializeField] List<string> keys;
    public static Dictionary<string, GameObject> agents;

    void Start(){
        for(int i=0; i<models.Count; i++){
            agents.Add(keys[i], models[i]);
        }
    }

}
