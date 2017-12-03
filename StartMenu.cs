using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    [SerializeField]
    private ShowOnSnowball m_P1Ready, m_P2Ready;

    [SerializeField]
    private float m_Delay;

    private float m_Timer;
	// Update is called once per frame
	void Update () {
		if(m_P1Ready.State && m_P2Ready.State)
        {
            m_Timer += Time.deltaTime;
        }
        if(m_Timer >= m_Delay)
        {
            SceneManager.LoadScene(1);
        }
	}
}
