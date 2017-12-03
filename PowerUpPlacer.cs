using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PowerUpEvent(Vector3 position);
public class PowerUpPlacer : MonoBehaviour {

    [SerializeField]
    private Transform[] m_Positions;

    [SerializeField]
    private GameObject[] m_PowerUps;

    [SerializeField]
    private int m_MaxAtSameTime = 2;

    [SerializeField]
    [Range(1,30)]
    private float m_MinWaitTime, m_MaxWaitTime;

    private int m_Amount;
    private float m_Timer;

    private bool m_Activated;

    [SerializeField]
    private List<Vector3> m_PositionsInUse;

	// Use this for initialization
	void Start () {
        SnowBallPowerUp.OnUse += OnPowerUpUse;
        HeatPowerUp.OnUse += OnPowerUpUse;
        m_Amount = 0;
        m_Timer = 0;
        m_Activated = true;

        m_PositionsInUse = new List<Vector3>();
	}   
	
	// Update is called once per frame
	void Update () {
        m_Timer -= Time.deltaTime;
        if (m_Timer <= 0)
        {
            m_Timer = 0;
            if (m_Activated)
            {
                SpawnPowerUp();
            }
        }

        if (m_Timer == 0)
        {
            if (m_Amount < m_MaxAtSameTime)
            {
                m_Activated = true;
                m_Timer = Random.Range(m_MinWaitTime, m_MaxWaitTime);
            }
        }

	}

    void SpawnPowerUp()
    {
        int Powerup = Random.Range(0, m_PowerUps.Length);
        int position = Random.Range(0, m_Positions.Length);

        if (m_PositionsInUse.Contains(m_Positions[position].position))
        {
            SpawnPowerUp();
        }

        Vector3 spawnPosition = m_Positions[position].position;

        if (m_Amount < m_MaxAtSameTime)
        {
            Instantiate(m_PowerUps[Powerup], spawnPosition, Quaternion.identity);
            m_PositionsInUse.Add(spawnPosition);
            m_Amount++;
        }
        m_Activated = false;
    }

    void OnPowerUpUse(Vector3 position)
    {
        //Spawned using system?
        if (m_PositionsInUse.Contains(position))
        {
            m_PositionsInUse.Remove(position);
        }
        m_Amount--;
    }
}
