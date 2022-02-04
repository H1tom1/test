using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //uGUIを用いるのに必要
using UnityEngine.SceneManagement; //シーン切り替えに必要

public class RuleAction : MonoBehaviour
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
        txtNavigate.text = (Elapsed < 0.8f) ? "Push LMB to PLAY" : "";
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
