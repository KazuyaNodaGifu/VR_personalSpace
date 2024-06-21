using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {

            public class EyeFocusScript : MonoBehaviour
            {
                //⓪取得呼び出し-----------------------------
                //呼び出したデータ格納用の関数
                EyeData eye;
                //-------------------------------------------

                //④焦点情報--------------------
                //両目の焦点格納変数
                //レイの始点と方向（多分③の内容と同じ）
                Ray CombineRay;
                /*レイがどこに焦点を合わせたかの情報．Vector3 point : 視線ベクトルと物体の衝突位置，float distance : 見ている物体までの距離，
                   Vector3 normal:見ている物体の面の法線ベクトル，Collider collider : 衝突したオブジェクトのCollider，Rigidbody rigidbody：衝突したオブジェクトのRigidbody，Transform transform：衝突したオブジェクトのTransform*/
                //焦点位置にオブジェクトを出すためにpublicにしています．
                public static FocusInfo CombineFocus;
                //レイの半径
                float CombineFocusradius;
                //レイの最大の長さ
                float CombineFocusmaxDistance;
                //オブジェクトを選択的に無視するために使用されるレイヤー ID
                int CombinefocusableLayer = 0;
                //------------------------------

                public GameObject focus_ball;
                GameObject FocusHit;


                //1フレーム毎に実行
                void Update()
                {
                    //おまけ------------------------------------
                    //エラー確認ViveSR.Error.がWORKなら正常に動いている．（フレームワークの方に内蔵済みだからいらないかも）
                    if (SRanipal_Eye_API.GetEyeData(ref eye) == ViveSR.Error.WORK)
                    {
                        //一応機器が正常に動いてる時の処理をここにかける
                    }
                    //-------------------------------------------


                    //⓪取得呼び出し-----------------------------
                    SRanipal_Eye_API.GetEyeData(ref eye);
                    //-------------------------------------------

                    

                    //④焦点情報--------------------
                    //radius, maxDistance，CombinefocusableLayerは省略可
                    if (SRanipal_Eye.Focus(GazeIndex.COMBINE, out CombineRay, out CombineFocus/*, CombineFocusradius, CombineFocusmaxDistance, CombinefocusableLayer*/))
                    {
                        //Debug.Log("Combine Focus Point" + CombineFocus.point.x + ", " + CombineFocus.point.y + ", " + CombineFocus.point.z);
                        focus_ball.transform.position = new Vector3(CombineFocus.point.x,CombineFocus.point.y,CombineFocus.point.z);
                        
                        
                    }

                    //------------------------------
                }
            }
        }
    }
}
