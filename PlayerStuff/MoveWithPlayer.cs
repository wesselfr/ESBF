using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlayer : MonoBehaviour {

    [SerializeField]
    private GameObject m_Player;

	// Update is called once per frame
	void Update () {
        transform.position = m_Player.transform.position;
	}
}
