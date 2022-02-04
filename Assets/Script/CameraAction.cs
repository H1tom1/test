using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{
    GameObject P;
    Transform LookPos; //注視位置
    Transform CamPos; //カメラ位置
    // Start is called before the first frame update
    void Start()
    {
        LookPos = GameObject.Find("LookPos").transform; //注視位置を取得
        CamPos = GameObject.Find("CamPos").transform; //カメラ位置を取得
        P = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!P)
        {
            return; //プレイヤー不在なら動かさない。
        }
        //注視点からカメラへのRayを作成
        Vector3 CamDir = CamPos.position - LookPos.position;
        Ray CamRay = new Ray(LookPos.position, CamDir);
        RaycastHit hitInfo; //注視点からカメラまでの途中に何か当たるか検査
        if (Physics.Raycast(CamRay, out hitInfo, CamDir.magnitude))
        {
            //当たった点より少しキャラクター寄りにカメラ設置
            transform.position = hitInfo.point - CamDir.normalized * 0.2f;
        }
        else
        {
            //何にも当たらなければ、通常のCamPosがカメラ位置
            transform.position = CamPos.position;
        }
        //CamPosの正面方向と同調する回転指示
        transform.forward = CamPos.forward;
    }
}
