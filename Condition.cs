using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Condition
{
    public static Vector3 pos = new Vector3(0, 0, 0);
    public static Vector3 angle = new Vector3(0, 90f, 0);

    public static List<string[]> csvDatas = new List<string[]>();

    public static string id;
    public static string str_agent;
    public static GameObject agent;
    public static float distance = 6.0f;
    public static byte tallForm;                   // 0: Agent, 1: Static, 2: HMD
    public static bool stuck = false;
    public static bool fade;
    public static bool atop = true;
    public static byte limitButtonTime = 255;      // limitation time of button push

    public static byte num_exp = 0;
    public static byte max_num_exp = 0;
}