using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    private Stack<GameObject> m_Objects;

    [SerializeField]
    private GameObject m_SnowBall;

    [SerializeField]
    private Transform m_ObjectPoolPosition;

    // Use this for initialization
    void Start()
    {
        m_Objects = new Stack<GameObject>(18);
        for(int i = 0; i < 18; i++)
        {
            GameObject snowBall = Instantiate(Resources.Load("SnowBall") as GameObject, transform.position + Vector3.left * i, Quaternion.identity);
            m_Objects.Push(snowBall);
        }
    }

    public void Add(GameObject obj)
    {
        obj.transform.position = m_ObjectPoolPosition.position;
        m_Objects.Push(obj);
    }

    public GameObject Get()
    {
        return m_Objects.Pop();
    }
}
