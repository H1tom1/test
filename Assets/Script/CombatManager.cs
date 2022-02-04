using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime; //Strixの利用に必要

public class CombatManager : StrixBehaviour
{
    float deadTime = 0; //死亡時刻
    public float recoverTime = 2.5f; //リカバー時間
    Animator myAnim; //自身のアニメーター
    GameObject Manager;
    public int maxHealth = 100; //ヘルスの最大値
    [StrixSyncField] //変数をネットワーク同期
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>(); //自身のアニメーターを取得
    }

    [StrixRpc] //RPCは自身で実行できない
               //弾からのOnHitメッセージ受信で動作する被弾処理
    public void OnHit(int Damage)
    {
        //新ヘルス値を算出
        int newHealth = health - Damage;
        //範囲内に収める
        Mathf.Clamp(newHealth, 0, maxHealth);
        //全てのユーザー画面の自分に、新ヘルス値を伝達
        RpcToAll("SetHealth", newHealth);
    }
    [StrixRpc] //RPCは自身で実行できない
               //被弾者からネットワーク経由で動作するヘルス変動処理
    public void SetHealth(int newHealth)
    {
        //アニメーターに新ヘルス値を伝達
        myAnim.SetInteger("Health", newHealth);
        //新ヘルス値が減少傾向なら、どちらかのモーションを行う
        if (newHealth < health)
        {
            myAnim.SetTrigger((newHealth <= 0) ? "Dead" : "Damage");
        }
        //ヘルスがゼロ以下で死亡なのに、新ヘルスが正の値なら生き返る。リスポーン実施
        if (health <= 0 && newHealth > 0)
        {
            SendMessage("Respawn", SendMessageOptions.DontRequireReceiver);
        }
        if (health != newHealth && newHealth <= 0)
        {
            Manager.SendMessage("ChangeScore2");
            //Debug.Log("Detected Death !"); //自身の死亡を検出
            //リカバー時間経過後に復帰すべく、死亡時刻を記録
            deadTime = Time.time;
            //リカバー期間はアタリ判定を無くす為にメッセージ送信
            SendMessage("WhileDeath", SendMessageOptions.DontRequireReceiver);
        }
        health = newHealth; //新ヘルス値を現在ヘルスに反映
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocal)
        {
            return; //生き返る処理はローカルプレイヤーのみ
        }
        //ヘルスがゼロ以下で死んでて、リカバー時間が経過したらヘルスを最大にする
        if (health <= 0 && Time.time >= deadTime + recoverTime)
        {
            RpcToAll("SetHealth", maxHealth);
        }
    }
}
