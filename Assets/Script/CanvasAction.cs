using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //uGUIの利用に必要

public class CanvasAction : MonoBehaviour
{
    public CombatManager myCM; //自身の戦闘管理者
    Image imgHealth; //ヘルスバー
    Text txtHealth; //ヘルス表示
    Text txtName; //ルームメンバー名
    // Start is called before the first frame update
    void Start()
    {
        imgHealth = transform.Find("imgHealth").GetComponent<Image>();
        txtHealth = transform.Find("txtHealth").GetComponent<Text>();
        txtName = transform.Find("txtName").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //自身のルームメンバー名を表示
        if (txtName != null && myCM.strixReplicator.roomMember != null)
        {
            txtName.text = myCM.strixReplicator.roomMember.GetName();
        }
        //ヘルスバーを増減して色を決定
        imgHealth.fillAmount = myCM.health / (float)myCM.maxHealth;
        if (imgHealth.fillAmount > 0.5f)
        {
            imgHealth.color = Color.green;
        }
        else if (imgHealth.fillAmount > 0.2f)
        {
            imgHealth.color = Color.yellow;
        }
        else
        {
            imgHealth.color = Color.red;
        }
        //ヘルス値を表示
        txtHealth.text = myCM.health.ToString("f0") + "/" + myCM.maxHealth.ToString("f0");
        //常にこのCanvasをカメラに向ける
        transform.forward = Camera.main.transform.forward;

    }
}
