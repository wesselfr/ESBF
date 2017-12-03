using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Image[] m_SnowBalls;

    private int m_Amount;

    [SerializeField]
    private int m_PlayerID;

    public static PlayerEvent OnPlayerPicksUpSnowball;

    // Use this for initialization
    void Start()
    {
        PlayerScript.OnSnowballThrow += OnSnowballThrow;
        foreach(Image placeHolder in m_SnowBalls)
        {
            placeHolder.color = new Color(0, 0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Amount > 9)
        {
            m_Amount = 9;
        }
        Color unused = new Color(0,0,0,0);
        Color used = Color.white;
        for (int i = 0; i < m_SnowBalls.Length; i++)
        {
            if(i < m_Amount)
            {
                m_SnowBalls[i].color = used;
            }
            else
            {
                m_SnowBalls[i].color = unused;
            }
        }
    }

    public int Amount
    {
        get { return m_Amount; }
        set { m_Amount = value; }
    }

    public void AddSnowball()
    {
        if (m_Amount < 9)
        {
            m_Amount++;
            if(OnPlayerPicksUpSnowball != null)
            {
                OnPlayerPicksUpSnowball(m_PlayerID);
            }
        }
    }

    public void OnSnowballThrow(int ID)
    {
        if(m_PlayerID == ID)
        {
            m_Amount--;
        }
    }
}
