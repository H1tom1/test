using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocoAction : MonoBehaviour
{
    GameObject Manager; //マネージャー
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameController");
        Destroy(gameObject, 10.0f); //自身を10秒で撤去
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //自身の名前の6文字目から1文字を整数に変換してPriceに代入
            int Price = int.Parse(gameObject.name.Substring(5, 1));
            Manager.SendMessage("ChangeScore", Price,
            SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //自身を回す
        transform.Rotate(Vector3.up * Time.deltaTime * 90.0f);
    }
}
