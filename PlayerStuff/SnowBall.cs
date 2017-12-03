using UnityEngine;
using System.Collections;

public class SnowBall : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private ObjectPool m_Pool;

    private Rigidbody2D m_Rigidbody;

    private Direction m_Dir;

    public void Setup(Vector3 pos, Direction dir, ObjectPool pool)
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        transform.position = pos;
        if(dir == Direction.Left)
        {
            m_Rigidbody.velocity = Vector2.left * m_Speed;
        }

        if(dir == Direction.Right)
        {
            m_Rigidbody.velocity = Vector2.right * m_Speed;
        }
        m_Dir = dir;
        m_Pool = pool;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.tag.Contains("SnowBall"))
        {
            m_Pool.Add(this.gameObject);
            if (collision.collider.tag.Contains("Player"))
            {
                collision.collider.GetComponent<PlayerScript>().Heat -= 1.4f;
            }
            Vector3 rotation = Vector3.zero;
            if (m_Dir == Direction.Right)
            {
                rotation = Vector3.zero;
            }
            if(m_Dir == Direction.Left)
            {
                rotation = new Vector3(0, 0, 180);
            }
            Instantiate(Resources.Load("SnowEffect"), collision.contacts[0].point, Quaternion.Euler(rotation));
        }
    }
}
