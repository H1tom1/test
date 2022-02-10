using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //uGUIを利用するのに必要
using UnityEngine.SceneManagement; //シーンの移動に必要

public class TitleAction : MonoBehaviour
{
    float Elapsed = 0.0f;
    public Text txtNavigate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Elapsed += Time.deltaTime;
        Elapsed %= 1.0f;
        txtNavigate.text = (Elapsed < 0.8f) ? "Push LMB to Rule1" : "";
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Rule");
        }
    }
}
