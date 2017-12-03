using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnSnowball : MonoBehaviour {

    SpriteRenderer m_Renderer;

    bool m_Enebeld = false;

	// Use this for initialization
	void Start () {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Renderer.enabled = m_Enebeld;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("SnowBall"))
        {
            m_Enebeld = true;
            m_Renderer.enabled = m_Enebeld;
        }
    }

    public bool State
    {
        get { return m_Enebeld; }
    }
}
