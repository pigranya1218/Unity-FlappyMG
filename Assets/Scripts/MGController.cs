using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGController : MonoBehaviour
{
    private Collider2D m_collider;
    private Rigidbody2D m_rigidbody;

    public SpriteRenderer normalSprite;
    public SpriteRenderer sadSprite;

    public float jumpPower = 3f;

    private bool m_inputJump;
    private void Start()
    {
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();

        normalSprite.enabled = true;
        sadSprite.enabled = false;

        m_rigidbody.isKinematic = true;

        m_inputJump = false;
    }

    private void Update()
    {
        if (GameManager.instance.IsGamePlaying() == false) return;

        // 키 입력
        if ((Input.touchCount > 0) && (m_inputJump == false))
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                m_inputJump = true;
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    m_inputJump = true;
        //}
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.IsGamePlaying() == true)
        {
            if (m_inputJump == true)
            {
                m_inputJump = false;
                m_rigidbody.velocity = Vector2.zero;
                m_rigidbody.AddForce(Vector2.up * jumpPower);
            }
        }
        else if (GameManager.instance.IsGameFinish() == true)
        {
            m_rigidbody.MoveRotation(m_rigidbody.rotation + 5f);
        }
    }

    public void StartGame()
    {
        m_rigidbody.isKinematic = false;
        m_inputJump = false;

        normalSprite.enabled = true;
        sadSprite.enabled = false;
    }

    public void ResetGame()
    {
        gameObject.transform.position = new Vector2(-1.5f, 0.5f);
        m_rigidbody.rotation = 0f;

        normalSprite.enabled = true;
        sadSprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            m_rigidbody.velocity = Vector2.zero;
            m_rigidbody.isKinematic = true;
            
            normalSprite.enabled = false;
            sadSprite.enabled = true;

            GameManager.instance.SetGameFinish();
        }
        else if (collision.CompareTag("ScoreZone"))
        {
            GameManager.instance.AddScore();
        }
    }
}
