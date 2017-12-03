using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TemperatureIndicator : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_Sprites = new Sprite[11];

    private Image m_Indicator;

    [SerializeField][Range(1, 11)]
    private float m_Heat = 11f;

    private float m_MaxHeat = 11f;

    [SerializeField]
    private int m_PlayerID;
    // Use this for initialization
    void Start()
    {
        m_Indicator = GetComponent<Image>();
        Inventory.OnPlayerPicksUpSnowball += OnSnowballPickup;
        PlayerScript.OnSnowballThrow += OnSnowballThrow;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Heat > 11f)
        {
            m_Heat = 11f;
        }
        if (m_Heat < m_MaxHeat)
        {
            m_Heat += 0.7f * Time.deltaTime;
        }
        if (m_Heat > 1)
        {
            m_Indicator.sprite = m_Sprites[Mathf.RoundToInt(m_Heat - 1f)];
        }
    }

    public float Heat
    {
        get { return m_Heat; }
        set { m_Heat = value; }
    }

    public void ResetMaxHeat()
    {
        m_MaxHeat = 11f;
    }

    void OnSnowballPickup(int playerID)
    {
        if(playerID == m_PlayerID)
        {
            m_Heat -= 0.25f;
            m_MaxHeat -= 0.5f;
        }
    }


    void OnSnowballThrow(int playerID)
    {
        if (playerID == m_PlayerID)
        {
            m_MaxHeat += 0.5f;
        }
    }
}
