using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //uGUIを利用するのに必要

public class GameManager : MonoBehaviour
{
    public enum STS
    {
        PLAY,
        CLEAR
    }
    static public STS GameStatus; //ゲームの状態
    GameObject Player; //プレイヤー
    float Elapsed; //経過時間
    int myScore; //スコア
    public float LimitTime = 60.0f; // 制限時間の初期値
    public Text txtScore;
    public Text txtTime;
    public Text txtMessage;
    public GameObject Choco1Prefab;
    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーを探しておく
        Player = GameObject.FindGameObjectWithTag("Player");
        GameStart();
        //チョコ生成処理を起動
        StartCoroutine("ChocoSpawner");
    }

    //チョコ生成処理
    IEnumerator ChocoSpawner()
    {
        while (true)
        {
            //ランダム時間を待機
            yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));
            if (GameStatus == STS.PLAY)
            {
                float RanX = Random.Range(-15.0f, 15.0f);
                float RanZ = Random.Range(-10.0f, 10.0f);
                Vector3 pos = new Vector3(RanX, 1.0f, RanZ); //生成位置決定
                Instantiate(Choco1Prefab, pos, Quaternion.identity);
            }
        }
    }
    //スコア変動処理
    void ChangeScore(int Point)
    {
        myScore += Point;
        txtScore.text = "SCORE : " + myScore.ToString().PadLeft(0, '0');
    }

    void ChangeScore2(int Point)
    {
        myScore -= Point;
        txtScore.text = "SCORE : " + myScore.ToString().PadLeft(0, '5');
    }

    void GameStart()
    {
        GameStatus = STS.PLAY;
        txtMessage.text = "";
        txtTime.text = "";
        txtScore.text = "";
        txtScore.text = "SCORE : 000";
        Elapsed = 0.0f;
    }

    void TimeUp()
    {
        GameStatus = STS.CLEAR;
        txtMessage.text = "TIME UP!";
        txtTime.text = "0.00s";
        Elapsed = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        switch (GameStatus)
        {
            case STS.PLAY:
                Elapsed += Time.deltaTime;
                if (Elapsed >= LimitTime)
                {
                    TimeUp();
                }
                else
                {
                    txtTime.text = (LimitTime - Elapsed).ToString("f2") + "s";
                }
                break;
            default:
                break;
        }
    }
}