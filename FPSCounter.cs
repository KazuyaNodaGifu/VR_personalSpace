using UnityEngine;
using UnityEngine.Profiling;
using System.IO;
using System;

/// <summary>
/// For debugging: FPS Counter
/// デバッグ用: FPS カウンタ
/// </summary>
public class FPSCounter : MonoBehaviour
{
  /// <summary>
  /// Reflect measurement results every 'EveryCalcurationTime' seconds.
  /// EveryCalcurationTime 秒ごとに計測結果を反映する
  /// </summary>
  [SerializeField, Range(0.1f, 1.0f)]
  float EveryCalcurationTime = 0.5f;

  string csv_path;
  string folder_name = "LogFolder";
  string fps_folder_name = "FPSLog";
  
  /// <summary>
  /// FPS value
  /// </summary>
  public float Fps
  {
    get; private set;
  }
  
  int frameCount;
  float prevTime;
  
  void Start()
  {
    if(!Directory.Exists(Application.persistentDataPath+"/"+folder_name)){
      Directory.CreateDirectory(Application.persistentDataPath+"/"+folder_name);
    }

    if(!Directory.Exists(Application.persistentDataPath+"/"+folder_name+"/"+fps_folder_name)){
      Directory.CreateDirectory(Application.persistentDataPath+"/"+folder_name+"/"+fps_folder_name);
    }

    DateTime now = DateTime.Now;
    string file_name = now.ToString("yyyy-MM-dd-HH-mm-ss")+".csv";
    csv_path = Application.persistentDataPath+ "/"+ folder_name +"/"+fps_folder_name +"/"+ file_name;
    File.WriteAllText(csv_path, "time,FPS\n");

    frameCount = 0;
    prevTime = 0.0f;
    Fps = 0.0f;
  }
  void Update()
  {
    frameCount++;
    float time = Time.realtimeSinceStartup - prevTime;
    
    // n秒ごとに計測
    if (time >= EveryCalcurationTime)
    {
      Fps = frameCount / time;
      
      frameCount = 0;
      prevTime = Time.realtimeSinceStartup;
      //Debug.Log("FPS："+Fps);
    DateTime time_stamp = DateTime.Now;
    File.AppendAllText(csv_path, time_stamp.ToString("HH:mm:ss")+","+Fps+"\n");
    }
    
  }
}