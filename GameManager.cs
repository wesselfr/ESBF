using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerScript m_Player1, m_Player2;

    [SerializeField]
    private Text m_P1Score, m_P2Score, m_WinText;

    [SerializeField]
    private int m_Player1Score, m_Player2Score;

    private Vector3 m_StartP1, m_StartP2;

    private float m_ResetTimer;

    bool m_Reset = false;

    // Use this for initialization
    void Start()
    {
        m_P1Score.text = "P1: " + m_Player1Score;
        m_P2Score.text = "P2: " + m_Player2Score;

        m_StartP1 = m_Player1.transform.position;
        m_StartP2 = m_Player2.transform.position;

        if(m_WinText != null)
        {
            m_WinText.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(m_Reset == true)
        {
            m_ResetTimer += Time.deltaTime;
            m_Player1.Sleep();
            m_Player2.Sleep();


            if(m_ResetTimer > 2f)
            {
                Reset();
                m_ResetTimer = 0;
            }
        }
        if (m_Reset == false)
        {
            if (m_Player1.Heat <= 0)
            {
                m_Player2Score++;
                m_P2Score.text = "P2: " + m_Player2Score;
                m_Player1.Sad();
                m_Player2.Happy();
                m_Reset = true;
                UpdateText();
            }
            if (m_Player2.Heat <= 0)
            {
                m_Player1Score++;
                m_P1Score.text = "P1: " + m_Player1Score;
                m_Player1.Happy();
                m_Player2.Sad();
                m_Reset = true;
                UpdateText();
            }
        }

    }

    private void UpdateText()
    {
        if (m_Player1Score == 5 || m_Player2Score == 5)
        {
            m_ResetTimer -= 2f;
            if (m_Player1Score == 5)
            {
                m_WinText.text = "Player 1 Wins!";
                m_WinText.enabled = true;
            }
            if (m_Player2Score == 5)
            {
                m_WinText.text = "Player 2 Wins!";
                m_WinText.enabled = true;
            }
        }
    }

    private void Reset()
    {
        //Start Again.
            m_Player1.Heat = 11f;
            m_Player2.Heat = 11f;

            m_Player1.ResetMaxHeat();
            m_Player2.ResetMaxHeat();

            m_Player1.Amount = 0;
            m_Player2.Amount = 0;

            m_Player1.transform.position = m_StartP1;
            m_Player2.transform.position = m_StartP2;

            m_Player1.WakeUp();
            m_Player2.WakeUp();

            m_Player1.ResetFace();
            m_Player2.ResetFace();
        
        if(m_Player1Score == 5 || m_Player2Score == 5)
        {
            SceneManager.LoadScene(0);
        }

        m_Reset = false;
        
    }
}
