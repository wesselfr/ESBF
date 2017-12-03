using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFollowCamera : MonoBehaviour {

    [SerializeField]
    Camera m_Camera;

    [SerializeField]
    private GameObject m_Player1, m_Player2;

    [SerializeField][Range(5, 70)]
    private float m_Range;

    [SerializeField]
    [Range(0, 30)]
    private float m_Min;

    [SerializeField][Range(0, 20)]
    private float m_MaxRange;
	// Use this for initialization
	void Start () {
        m_Camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        float zoom = Vector3.Distance(m_Player1.transform.position, m_Player2.transform.position) - m_Range;
        if(zoom < m_Min)
        {
            zoom = m_Min;
        }
        if(zoom > m_MaxRange)
        {
            zoom = m_MaxRange;
        }
        m_Camera.orthographicSize = zoom;

        Vector3 position = (m_Player1.transform.position + m_Player2.transform.position) / 2;
        position.y += 0.71f;
        position.z -= 10;

        m_Camera.transform.position = Vector3.Lerp(transform.position, position, 0.2f);
	}
}
