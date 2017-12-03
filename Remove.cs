using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour {

    [SerializeField]
    private float m_TimeToRemove;

    private float m_Time;
	// Update is called once per frame
	void Update () {
        m_Time += Time.deltaTime;
        if(m_Time > m_TimeToRemove)
        {
            Destroy(this.gameObject);
        }
	}
}
