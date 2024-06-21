using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using TMPro;
using Valve.VR; 
using UnityEngine.XR;
using UnityEngine.UI;
using static Condition;


namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {
            public class File_Write : MonoBehaviour
            {
                //⓪取得呼び出し-----------------------------
                //呼び出したデータ格納用の関数
                EyeData eye;
                //-------------------------------------------

                string csv_path;
                string folder_name = "LogFolder";
                string csv_folder_name = "CSVLog";
                string img_folder_name = "IMGLog";

                public SteamVR_Action_Boolean TriggerClick;
                private SteamVR_Input_Sources inputSource;

                Ray CombineRay;

                //HMDの位置座標格納用
                public Vector3 HMDPosition;
                //焦点座標
                public static FocusInfo CombineFocus;

                float Focus_X;
                float Focus_Y;
                float Focus_Z;

                //④瞳孔の直径-------------------------------
                //呼び出したデータ格納用の関数
                float LeftPupiltDiameter;
                float RightPupiltDiameter;
                //-------------------------------------------

                //エージェントが歩いているかどうか-----------
                public GameObject walking_trigger;
                string str_walking;
                string str_pressed;
                //------------------------------------------

                //エージェントと座標------------------------
                public GameObject agent;
                Vector3 agent_position;
                //------------------------------------------

                //①どのぐらいまぶたを開いてるか-----------------
                //呼び出したデータ格納用の関数
                float LeftOpenness;
                float RightOpenness;
                //-------------------------------------------

                //②視線の起点の座標(角膜の中心）mm単位------
                //呼び出したデータ格納用の関数
                Vector3 LeftGazeOrigin;
                Vector3 RightGazeOrigin;
                //-------------------------------------------

                //③瞳孔の位置-------------------------------
                //呼び出したデータ格納用の関数
                Vector2 LeftPupilPosition;
                Vector2 RightPupilPosition;
                //-------------------------------------------

                //焦点位置にあるオブジェクト
                GameObject FocusHit;
                string ObjectPath;

                // 実験の条件
                string Id;
                string str_num;
                string sceneName;
                string experimentDesign;
                string limitation;
                string distance;

                TextMeshProUGUI txt;

                // 記録できるかどうかを表す
                bool recordable = true;
                
                // Start is called before the first frame update
                void Start()
                {
                    //  // Debug.Log("t or f " + Directory.Exists(Application.dataPath+"/"+folder_name));
                    //  // Debug.Log("folder " + Application.dataPath+"/"+folder_name);
                    // csv_path = Application.persistentDataPath + "/test.txt";
                    
                    // https://qiita.com/w_yang/items/8458cd790607d14b1b36
                    //C:/Users/xxxx/AppData/LocalLow/CompanyName/ProductName
                    //フォルダ作成
                    if(!Directory.Exists(Application.persistentDataPath+"/"+folder_name)){
                        Directory.CreateDirectory(Application.persistentDataPath+"/"+folder_name);
                    }

                    if(!Directory.Exists(Application.persistentDataPath+"/"+folder_name+"/"+csv_folder_name)){
                        Directory.CreateDirectory(Application.persistentDataPath+"/"+folder_name+"/"+csv_folder_name);
                    }

                    // if(!Directory.Exists(Application.persistentDataPath+"/"+folder_name+"/"+img_folder_name)){
                    //     Directory.CreateDirectory(Application.persistentDataPath+"/"+folder_name+"/"+img_folder_name);
                    // }

                    // Debug.Log("t or f " + Directory.Exists(Application.dataPath+"/"+folder_name));

                    DateTime now = DateTime.Now;
                    string file_name = "";
                    if(SceneManager.GetActiveScene().name == "Genesis8Woman_Walk"){
                        file_name += "G";
                    } 
                    file_name += now.ToString("yyyy-MM-dd-HH-mm-ss")+".csv";
                    // string file_distant_name = now.ToString("yyyy-MM-dd-HH-mm-ss")+"distant.csv";
                    // string img_folder_now = now.ToString("yyyy-MM-dd-HH-mm-ss");
                    // img_folder_name += "/"+now.ToString("yyyy-MM-dd-HH-mm-ss");

                    // if(!Directory.Exists(Application.persistentDataPath+"/"+folder_name+"/"+img_folder_name)){
                    //     Directory.CreateDirectory(Application.persistentDataPath+"/"+folder_name+"/"+img_folder_name);
                    // }


                    csv_path = Application.persistentDataPath+ "/"+ folder_name +"/"+csv_folder_name +"/"+ file_name;
                    // Debug.Log("FilerPath " + csv_path);

                    
                    // 実験の条件
                    Id = Condition.id;
                    str_num = Condition.num_exp.ToString();
                    sceneName = SceneManager.GetActiveScene().name;
                    if (Condition.atop)   experimentDesign = "AtoP";
                    else                    experimentDesign = "PtoA";
                    distance = Condition.distance.ToString();
                    limitation = Condition.limitButtonTime.ToString();
                    File.WriteAllText(csv_path,"ID,number_of_experiment,demographic,approacher,distance,stoplimitation,TimeStamp,HMD_X,HMD_Y,HMD_Z,Focus_X,Focus_Y,Focus_Z,LeftPupiltDiameter[mm],RightPupiltDiameter[mm],Trigger,walking,pressed,agent_X,agent_Y,agent_Z,LeftOpenness,RightOpenness,LeftGazeOrigin_X,LeftGazeOrigin_Y,LeftGazeOrigin_Z,RightGazeOrigin_X,RightGazeOrigin_Y,RightGazeOrigin_Z,LeftPupilPosition_X,LeftPupilPosition_Y,RightPupilPosition_X,RightPupilPosition_Y,ObjectPath\n");
                    //File.WriteAllText(csv_distant_path, headline);

                    txt = GameObject.Find("text_recording").GetComponent<TextMeshProUGUI>();
                    txt.enabled = true;
                }

                // Update is called once per frame
                void Update()
                {
                    if(!recordable){
                        return;
                    }
                    SRanipal_Eye_API.GetEyeData(ref eye);

                    Focus_X=10000;
                    Focus_Y=10000;
                    Focus_Z=10000;
                    LeftPupiltDiameter=10000;
                    RightPupiltDiameter=10000;

                    
                    //HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
                    HMDPosition = GameObject.Find("Camera").transform.position;
                    DateTime time_stamp = DateTime.Now;
                    if (SRanipal_Eye.Focus(GazeIndex.COMBINE, out CombineRay, out CombineFocus/*, CombineFocusradius, CombineFocusmaxDistance, CombinefocusableLayer*/)){
                        // focus_ball.transform.position = new Vector3(CombineFocus.point.x,CombineFocus.point.y,CombineFocus.point.z);
                        Focus_X = CombineFocus.point.x;
                        Focus_Y = CombineFocus.point.y;
                        Focus_Z = CombineFocus.point.z;
                        //焦点の位置にあるオブジェクトを取得
                        FocusHit = CombineFocus.collider.gameObject;
                        if(FocusHit.transform.parent){
                            // Debug.Log("FocusCollidrer：" + FocusHit.transform.parent.gameObject.name);
                            ObjectPath = $"{FocusHit.transform.parent.gameObject.name}/{FocusHit.name}";
                            if(FocusHit.transform.parent.parent){
                                ObjectPath = $"{FocusHit.transform.parent.parent.gameObject.name}/{FocusHit.transform.parent.gameObject.name}/{FocusHit.name}";
                            }
                        }
                        else{
                            // Debug.Log("FocusCollidrer：" + FocusHit.name);
                            ObjectPath = $"{FocusHit.name}";
                        }
                    }

                     //④瞳孔の直径-------------------------------
                    //左目の瞳孔の直径が妥当ならば取得　目をつぶるとFalse 判定精度はまあまあ
                    if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_DIAMETER_VALIDITY))
                    {
                        LeftPupiltDiameter = eye.verbose_data.left.pupil_diameter_mm;
                        // Debug.Log("Left Pupilt Diameter：" + LeftPupiltDiameter);
                    }

                    ////右目の瞳孔の直径が妥当ならば取得　目をつぶるとFalse　判定精度はまあまあ
                    if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_DIAMETER_VALIDITY))
                    {
                        RightPupiltDiameter = eye.verbose_data.right.pupil_diameter_mm;
                        // Debug.Log("Right Pupilt Diameter：" + RightPupiltDiameter);
                    }




                    /////////////////////////////////////////////////////////////////////////////////////////
                    //①どのぐらいまぶたを開いてるか-----------------
                    //左目を開いてるかが妥当ならば取得　なぜかHMD付けてなくてもTrueがでる，謎．
                    if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_EYE_OPENNESS_VALIDITY))
                    {
                        LeftOpenness = eye.verbose_data.left.eye_openness;
                        // Debug.Log("Left Openness：" + LeftOpenness);
                    }

                    //右目を開いてるかが妥当ならば取得　なぜかHMD付けてなくてもTrueがでる，謎．
                    if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_EYE_OPENNESS_VALIDITY))
                    {
                        RightOpenness = eye.verbose_data.right.eye_openness;
                        // Debug.Log("Right Openness：" + RightOpenness);
                    }
                    //-------------------------------------------


                    //②視線の起点の座標(角膜の中心）mm単位------ -
                    //左目の眼球データ（視線原点）が妥当ならば取得　目をつぶるとFalse　判定精度はまあまあ
                    if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY))
                    {
                        LeftGazeOrigin = eye.verbose_data.left.gaze_origin_mm;
                        // Debug.Log("Left GazeOrigin：" + LeftGazeOrigin.x + ", " + LeftGazeOrigin.y + ", " + LeftGazeOrigin.z);
                    }

                    ////右目の眼球データ（視線原点）が妥当ならば取得　目をつぶるとFalse　判定精度はまあまあ
                    if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY))
                    {
                        RightGazeOrigin = eye.verbose_data.right.gaze_origin_mm;
                        // Debug.Log("Right GazeOrigin：" + RightGazeOrigin.x + ", " + RightGazeOrigin.y + ", " + RightGazeOrigin.z);
                    }
                    //-------------------------------------------


                    //③瞳孔の位置-------------------------------
                    //左目の瞳孔の正規化位置が妥当ならば取得　目をつぶるとFalse 判定精度は微妙
                    if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_POSITION_IN_SENSOR_AREA_VALIDITY))
                    {
                        LeftPupilPosition = eye.verbose_data.left.pupil_position_in_sensor_area;
                        // Debug.Log("Left Pupil Position：" + LeftPupilPosition.x + ", " + LeftPupilPosition.y);
                    }

                    ////右目の瞳孔の正規化位置が妥当ならば取得　目をつぶるとFalse　判定精度は微妙
                    if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_POSITION_IN_SENSOR_AREA_VALIDITY))
                    {
                        RightPupilPosition = eye.verbose_data.right.pupil_position_in_sensor_area;
                        // Debug.Log("Right GazeOrigin：" + RightPupilPosition.x + ", " + RightPupilPosition.y);
                    }
                    //-------------------------------------------

                    
                    if (walking_trigger.GetComponent<InputEnterWalk>().stop){
                        str_walking = "stopping";
                    }
                    else{
                        str_walking = "walking";
                    }
                    
                    if (walking_trigger.GetComponent<InputEnterWalk>().pressed){
                        str_pressed = "pressed";
                        walking_trigger.GetComponent<InputEnterWalk>().pressed = false;
                    }
                    else{
                        str_pressed = "";
                    }

                    agent_position = agent.transform.position;

                    // File.AppendAllText(csv_path, time_stamp.ToString("HH:mm:ss")+","+HMDPosition.x+","+HMDPosition.y+","+HMDPosition.z+","+Focus_X+","+Focus_Y+","+Focus_Z+","+LeftPupiltDiameter+","+RightPupiltDiameter+",0"+"\n");
                    File.AppendAllText(csv_path, Id+","+str_num+","+sceneName+","+experimentDesign+","+distance+","+limitation+","
                                        +time_stamp.ToString("HH:mm:ss")+","+HMDPosition.x+","+HMDPosition.y+","+HMDPosition.z+","
                                        +Focus_X+","+Focus_Y+","+Focus_Z+","+LeftPupiltDiameter+","+RightPupiltDiameter+",0,"+str_walking+","+str_pressed+","
                                        +agent_position.x+","+agent_position.y+","+agent_position.z+","+LeftOpenness+","+RightOpenness+","
                                        +LeftGazeOrigin.x+","+LeftGazeOrigin.y+","+LeftGazeOrigin.z+","
                                        +RightGazeOrigin.x+","+RightGazeOrigin.y+","+RightGazeOrigin.z+","
                                        +LeftPupilPosition.x+","+LeftPupilPosition.y+","+RightPupilPosition.x+","+RightPupilPosition.y+","+ObjectPath+"\n");
                    
                }


                //コントローラ用
                private void OnEnable(){TriggerClick.AddOnStateDownListener(Press, inputSource);}
            
                private void OnDisable(){TriggerClick.RemoveOnStateDownListener(Press, inputSource);}


                //Pressした時の挙動(ファイル書き込み，スクリーンショット)
                private void Press(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
                {
                    //位置座標を取得
                    // HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
                    // // Debug.Log("HMD_Position " + HMDPosition);
                    // Debug.Log("HMDPosition " + HMDPosition);
                    // DateTime time_stamp = DateTime.Now;
                    // File.AppendAllText(csv_path, time_stamp.ToString("HH:mm:ss")+","+HMDPosition.x+","+HMDPosition.y+","+HMDPosition.z+"\n");
                    // Debug.Log("Save at " + csv_path);
                    Focus_X=10000;
                    Focus_Y=10000;
                    Focus_Z=10000;
                    LeftPupiltDiameter=10000;
                    RightPupiltDiameter=10000;

                    DateTime now = DateTime.Now;

                    
                    HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
                    DateTime time_stamp = DateTime.Now;
                    if (SRanipal_Eye.Focus(GazeIndex.COMBINE, out CombineRay, out CombineFocus/*, CombineFocusradius, CombineFocusmaxDistance, CombinefocusableLayer*/)){
                        // focus_ball.transform.position = new Vector3(CombineFocus.point.x,CombineFocus.point.y,CombineFocus.point.z);
                        Focus_X = CombineFocus.point.x;
                        Focus_Y = CombineFocus.point.y;
                        Focus_Z = CombineFocus.point.z;

                        //焦点の位置にあるオブジェクトを取得
                        FocusHit = CombineFocus.collider.gameObject;
                        if(FocusHit.transform.parent){
                            // Debug.Log("FocusCollidrer：" + FocusHit.transform.parent.gameObject.name);
                            ObjectPath = $"{FocusHit.transform.parent.gameObject.name}/{FocusHit.name}";
                        }
                        else{
                            // Debug.Log("FocusCollidrer：" + FocusHit.name);
                            ObjectPath = $"{FocusHit.name}";
                        }
                        
                    }

                     //④瞳孔の直径-------------------------------
                    //左目の瞳孔の直径が妥当ならば取得　目をつぶるとFalse 判定精度はまあまあ
                    if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_DIAMETER_VALIDITY))
                    {
                        LeftPupiltDiameter = eye.verbose_data.left.pupil_diameter_mm;
                        // Debug.Log("Left Pupilt Diameter：" + LeftPupiltDiameter);
                    }

                    ////右目の瞳孔の直径が妥当ならば取得　目をつぶるとFalse　判定精度はまあまあ
                    if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_DIAMETER_VALIDITY))
                    {
                        RightPupiltDiameter = eye.verbose_data.right.pupil_diameter_mm;
                        // Debug.Log("Right Pupilt Diameter：" + RightPupiltDiameter);
                    }



                    /////////////////////////////////////////////////////////////////////////////////////////
                    //①どのぐらいまぶたを開いてるか-----------------
                    //左目を開いてるかが妥当ならば取得　なぜかHMD付けてなくてもTrueがでる，謎．
                    if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_EYE_OPENNESS_VALIDITY))
                    {
                        LeftOpenness = eye.verbose_data.left.eye_openness;
                        // Debug.Log("Left Openness：" + LeftOpenness);
                    }

                    //右目を開いてるかが妥当ならば取得　なぜかHMD付けてなくてもTrueがでる，謎．
                    if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_EYE_OPENNESS_VALIDITY))
                    {
                        RightOpenness = eye.verbose_data.right.eye_openness;
                        // Debug.Log("Right Openness：" + RightOpenness);
                    }
                    //-------------------------------------------


                    //②視線の起点の座標(角膜の中心）mm単位------ -
                    //左目の眼球データ（視線原点）が妥当ならば取得　目をつぶるとFalse　判定精度はまあまあ
                    if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY))
                    {
                        LeftGazeOrigin = eye.verbose_data.left.gaze_origin_mm;
                        // Debug.Log("Left GazeOrigin：" + LeftGazeOrigin.x + ", " + LeftGazeOrigin.y + ", " + LeftGazeOrigin.z);
                    }

                    ////右目の眼球データ（視線原点）が妥当ならば取得　目をつぶるとFalse　判定精度はまあまあ
                    if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY))
                    {
                        RightGazeOrigin = eye.verbose_data.right.gaze_origin_mm;
                        // Debug.Log("Right GazeOrigin：" + RightGazeOrigin.x + ", " + RightGazeOrigin.y + ", " + RightGazeOrigin.z);
                    }
                    //-------------------------------------------


                    //③瞳孔の位置-------------------------------
                    //左目の瞳孔の正規化位置が妥当ならば取得　目をつぶるとFalse 判定精度は微妙
                    if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_POSITION_IN_SENSOR_AREA_VALIDITY))
                    {
                        LeftPupilPosition = eye.verbose_data.left.pupil_position_in_sensor_area;
                        // Debug.Log("Left Pupil Position：" + LeftPupilPosition.x + ", " + LeftPupilPosition.y);
                    }

                    ////右目の瞳孔の正規化位置が妥当ならば取得　目をつぶるとFalse　判定精度は微妙
                    if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_POSITION_IN_SENSOR_AREA_VALIDITY))
                    {
                        RightPupilPosition = eye.verbose_data.right.pupil_position_in_sensor_area;
                        // Debug.Log("Right GazeOrigin：" + RightPupilPosition.x + ", " + RightPupilPosition.y);
                    }
                    //-------------------------------------------


                    // スクリーンショットを保存
                    // CaptureScreenShot(Application.persistentDataPath+ "/"+ folder_name+"/"+ img_folder_name +"/"+now.ToString("yyyy-MM-dd-HH-mm-ss")+".png");

                    // File.AppendAllText(csv_path, time_stamp.ToString("HH:mm:ss")+","+HMDPosition.x+","+HMDPosition.y+","+HMDPosition.z+","+Focus_X+","+Focus_Y+","+Focus_Z+","+LeftPupiltDiameter+","+RightPupiltDiameter+",1"+"\n");
                    File.AppendAllText(csv_path, Id+","+str_num+","+sceneName+","+experimentDesign+","+distance+","+limitation+","
                                        +time_stamp.ToString("HH:mm:ss")+","+HMDPosition.x+","+HMDPosition.y+","+HMDPosition.z+","
                                        +Focus_X+","+Focus_Y+","+Focus_Z+","+LeftPupiltDiameter+","+RightPupiltDiameter+",0,"+str_walking+","+str_pressed+","
                                        +agent_position.x+","+agent_position.y+","+agent_position.z+","+LeftOpenness+","+RightOpenness+","
                                        +LeftGazeOrigin.x+","+LeftGazeOrigin.y+","+LeftGazeOrigin.z+","
                                        +RightGazeOrigin.x+","+RightGazeOrigin.y+","+RightGazeOrigin.z+","
                                        +LeftPupilPosition.x+","+LeftPupilPosition.y+","+RightPupilPosition.x+","+RightPupilPosition.y+","+ObjectPath+"\n");
                }

                // 画面全体のスクリーンショットを保存する
                private void CaptureScreenShot(string filePath)
                {
                    ScreenCapture.CaptureScreenshot(filePath);
                }

                // // s キーがおされたとき
                // void OnStart_recording(){
                //     recordable = true;
                //     txt.enabled = true;
                // }

                // f キーがおされたとき
                void OnStop_recording(){
                    recordable = false;
                    txt.enabled = false;
                }
            }
        }
    }
}
