using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime; //Strixの利用に必要

public class PlayerAction : StrixBehaviour
{
    public float rotSpeed = 180.0f; //回転速度
    public float fwdSpeed = 3.0f; //前進速度
    public float backSpeed = 0.8f; //後進速度
    GameObject DebugUI; //デバッグＵＩ
    Animator myAnim; //自身のアニメーター
    Transform MuzzlePos; //銃口の位置
    public float bulletSpeed = 30.0f; //弾の速度
    public float bulletLife = 2.0f; //弾の寿命
    public GameObject Bullet; //弾のプレハブ
    Collider myCol; //自身のコライダー
    Rigidbody myRB; //自身のリジッドボディ
    CombatManager myCM; //自身の戦闘管理者
    // Start is called before the first frame update
    void Start()
    {
        myCol = GetComponent<Collider>();
        myRB = GetComponent<Rigidbody>();
        myCM = GetComponent<CombatManager>();
        MuzzlePos = transform.Find("MuzzlePos"); //自身の配下から探す
        myAnim = GetComponent<Animator>(); //自身のアニメーターを取得
        //デバッグUIを取得する
        DebugUI = GameObject.Find("StrixDebugRoomInfoUI");
    }

    //死亡中の設定
    void WhileDeath()
    {
        //アタリ判定を無くして、死亡中に撃たれないようにする。
        myRB.useGravity = false;
        myRB.Sleep();
        myCol.enabled = false;
    }
    //リスポーン処理
    void Respawn()
    {
        //アタリ判定を復活させる。
        myRB.useGravity = true;
        myRB.WakeUp();
        myCol.enabled = true;
        //自身がローカルプレイヤーならリスポーン実行
        if (CompareTag("Player") && isLocal)
        {
            Vector3 SpawnPos = new Vector3(0, 2, 0);
            SpawnPos.x = Random.Range(-40.0f, 40.0f);
            SpawnPos.z = Random.Range(-40.0f, 40.0f);
            transform.position = SpawnPos;
        }
    }

    void FixedUpdate()
    {
        if (!isLocal)
        {
            return;
        }

        if (myCM != null && myCM.health <= 0)
        {
            return; //死亡中は操作を受け付けない制御
        }
        //ユーザーの操作を取得して進行方向を算出
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        myAnim.SetFloat("Speed", v); //アニメーターに前後進値を渡す
        Vector3 Dir = new Vector3(0, 0, v);
        //上下キー方向をプレイヤーの向きで前後方向に変換
        Dir = transform.TransformDirection(Dir);
        //プレイヤーの速度決定
        Dir *= (v > 0.1) ? fwdSpeed : backSpeed;
        //プレイヤーの移動
        transform.position += Dir * Time.fixedDeltaTime;
        //プレイヤーの回転
        transform.Rotate(0, h * rotSpeed * Time.fixedDeltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocal)
        {
            return;
        }

        if (myCM != null && myCM.health <= 0)
        {
            return; //死亡中は操作を受け付けない制御
        }

        //弾を発砲
        if (Input.GetButtonDown("Jump"))
        {
            //弾を生成する
            GameObject B = Instantiate(Bullet, MuzzlePos.position, MuzzlePos.rotation);
            //弾に速度を与える
            B.GetComponent<Rigidbody>().velocity = MuzzlePos.forward * bulletSpeed;
            //弾に寿命を与える
            Destroy(B, bulletLife);
        }
        //Esc押下で、デバッグUIの表示/非表示を切り替える
        if (Input.GetKeyDown(KeyCode.Escape) && DebugUI != null)
        {
            DebugUI.SetActive(!DebugUI.activeInHierarchy);
        }
    }
}
