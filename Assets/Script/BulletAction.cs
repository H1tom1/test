using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public GameObject hitEffect; //着弾時のエフェクト
    public int Damage = 10; //弾の攻撃威力
    void OnTriggerEnter(Collider other)
    {
        //被弾者が戦闘管理者でローカルなら、攻撃威力を伝達
        CombatManager CM = other.gameObject.GetComponent<CombatManager>();
        if (CM != null && CM.isLocal)
        {
            CM.RpcToRoomOwner("OnHit", Damage);
        }
        //着弾エフェクトを生成
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        //自身（弾）を撤去
        Destroy(gameObject, 0.05f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
