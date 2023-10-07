using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DisplayScore");        
    }

    // 숫자에 맞춰 이미지 점수 생성
    public void setScore(int score)
    {

    }

    // Update is called once per frame

    IEnumerator DisplayScore()
    {
        // 이동 및 연해지기
        for (float a = 1; a >= 0; a -= 0.05f)
        {
            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }
}
