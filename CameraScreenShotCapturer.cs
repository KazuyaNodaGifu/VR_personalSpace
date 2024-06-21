using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// 指定されたカメラの内容をキャプチャするサンプル
/// </summary>
public class CameraScreenShotCapturer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    string folder_name = "LogFolder";
    string img_folder_name = "IMGLog";
    public int i = 0;
    public int flame_cnt = 0;
    bool recording;
    int frameCount;
    public int superSize;
    // private SynchronizationContext _mainContext;


    void Start(){

        // _mainContext = SynchronizationContext.Current;

        //フォルダ作成
        DateTime now = DateTime.Now;
        img_folder_name += "/"+now.ToString("yyyy-MM-dd-HH-mm-ss");
        if(!Directory.Exists(Application.persistentDataPath+"/"+folder_name+"/"+img_folder_name)){
            Directory.CreateDirectory(Application.persistentDataPath+"/"+folder_name+"/"+img_folder_name);
        }
        // StartCoroutine("StartRecord");

        // Task task = Task.Run(() => {
        //     DateTime now = DateTime.Now;
        //     if(flame_cnt%10==0){
        //         CaptureScreenShot(Application.persistentDataPath+ "/"+ folder_name+"/"+ img_folder_name +"/"+i+".png");
        //     }
        // }); 
    
    }

    private void Update()
    {
        
        DateTime now = DateTime.Now;
        if(flame_cnt%10==0){
            CaptureScreenShot(Application.persistentDataPath+ "/"+ folder_name+"/"+ img_folder_name +"/"+i+".png");
            i++;
        }
        flame_cnt++;
    }


    // カメラのスクリーンショットを保存する
    
    private void CaptureScreenShot(string filePath)
    {
        // Debug.Log ("Capture_Flame_cnt:"+flame_cnt);
        var rt = new RenderTexture(_camera.pixelWidth, _camera.pixelHeight, 24);
        var prev = _camera.targetTexture;
        _camera.targetTexture = rt;
        _camera.Render();
        _camera.targetTexture = prev;
        RenderTexture.active = rt;
        
        var screenShot = new Texture2D(
            _camera.pixelWidth,
            _camera.pixelHeight,
            TextureFormat.RGB24,
            false);
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply();
            
        var bytes = screenShot.EncodeToPNG();
        Destroy(screenShot);
            
        File.WriteAllBytes(filePath, bytes);

    }
    
}