using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right
}

public delegate void PlayerEvent(int id);
public class PlayerScript : MonoBehaviour {

    Rigidbody2D m_Rigidbody;

    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private int m_PlayerID;

    [SerializeField]
    private TemperatureIndicator m_HeatIndicator;

    [SerializeField]
    private Transform m_LeftShooting, m_RightShooting;

    [SerializeField]
    private Inventory m_Inventory;

    [SerializeField]
    private ObjectPool m_SnowBalls;

    [SerializeField]
    private Direction m_Direciton;

    private bool m_CanJump;
    private bool m_Jump;
    private float m_JumpTimer;
    private Vector2 m_JumpPosition;
    private Vector2 m_JumpStart;

    [SerializeField]
    private Sprite m_Left, m_Right, m_LeftHappy, m_RightHappy, m_LeftSad, m_RightSad;

    private SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    private AnimationCurve m_JumpCurve;

    private bool m_Happy;
    private bool m_Sad;

    private bool m_Frozen;

    private Collider2D m_Collider;

    [SerializeField]
    private LayerMask m_Player;

    //Events
    public static PlayerEvent OnSnowballThrow;

	// Use this for initialization
	void Start () {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<Collider2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if (!m_Frozen)
        {
            float horizontal = Input.GetAxis("Horizontal" + m_PlayerID);

            m_Rigidbody.velocity = Vector3.down * 10f;
            m_Rigidbody.velocity += Vector2.right * horizontal * m_Speed;

            Debug.Log(horizontal);

            if (horizontal != 0)
            {
                switch ((int)Mathf.Sign(horizontal))
                {

                    case -1:
                        m_Direciton = Direction.Left;
                        UpdateSprite();
                        break;

                    case 1:
                        m_Direciton = Direction.Right;
                        UpdateSprite();
                        break;
                }
            }

            if (m_CanJump && !m_Jump)
            {
                if (Input.GetButtonDown("Vertical" + m_PlayerID))
                {
                    m_Jump = true;
                    transform.position.Set(transform.position.x, transform.position.y + 0.50f, transform.position.z);
                    //m_JumpPosition = (transform.position) + Vector3.up * 5f;
                    //m_JumpStart = transform.position;
                    m_CanJump = false;
                    StartCoroutine(Jump());
                }
            }

            //if (m_Jump)
            //{
            //    if (m_JumpTimer < 0.35f)
            //    {
            //        m_JumpTimer += Time.deltaTime;
            //        transform.position += new Vector3(0, 30f * Time.deltaTime);
            //        //m_Jump = !RoofCheck();
            //    }
            //    else if (m_JumpTimer >= 0.35f) { m_JumpTimer = 0f; m_Jump = false; }
            //    //m_JumpPosition = m_JumpPosition + new Vector2(horizontal,0);
            //    //transform.position = Vector3.Lerp(transform.position, m_JumpPosition, m_JumpCurve.Evaluate(m_JumpTimer));
            //}

            if (!m_Jump)
            {
                m_CanJump = GroundCheck();
            }


            if (Input.GetButtonDown("Use" + m_PlayerID))
            {
                if (m_CanJump)
                {
                    m_Inventory.AddSnowball();
                }
            }

            if (Input.GetButtonDown("Fire" + m_PlayerID))
            {
                if (m_Inventory.Amount > 0)
                {
                    SnowBall snowball = m_SnowBalls.Get().GetComponent<SnowBall>();
                    if (m_Direciton == Direction.Left)
                    {
                        snowball.Setup(m_LeftShooting.position, Direction.Left, m_SnowBalls);
                    }
                    if (m_Direciton == Direction.Right)
                    {
                        snowball.Setup(m_RightShooting.position, Direction.Right, m_SnowBalls);
                    }

                    if (OnSnowballThrow != null)
                    {
                        OnSnowballThrow(m_PlayerID);
                    }
                }
            }
        }
    }

    public void LateUpdate()
    {
        //if (m_Jump)
        //{
        //    m_JumpTimer += Time.deltaTime;
        //    if (m_JumpTimer < 0.35f)
        //    {
        //        transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.up * 0.5f, 1.2f);
        //        m_Jump = !RoofCheck();
        //    }
        //    if (m_JumpTimer >= 0.35f) { m_JumpTimer = 0f; m_Jump = false; }
        //    //m_JumpPosition = m_JumpPosition + new Vector2(horizontal,0);
        //    //transform.position = Vector3.Lerp(transform.position, m_JumpPosition, m_JumpCurve.Evaluate(m_JumpTimer));
        //}

        //if (!m_Jump)
        //{
        //    m_CanJump = GroundCheck();
        //}
    }
    public void UpdateSprite()
    {
        switch (m_Direciton)
        {
            case Direction.Left:
                m_SpriteRenderer.sprite = m_Left;
                if (m_Happy)
                {
                    m_SpriteRenderer.sprite = m_LeftHappy;
                }
                if (m_Sad)
                {
                    m_SpriteRenderer.sprite = m_LeftSad;
                }
                break;
            case Direction.Right:
                m_SpriteRenderer.sprite = m_Right;
                if (m_Happy)
                {
                    m_SpriteRenderer.sprite = m_RightHappy;
                }
                if (m_Sad)
                {
                    m_SpriteRenderer.sprite = m_RightSad;
                }
                break;
        }
    }

    IEnumerator Jump()
    {
        if (m_Jump)
        {
            for (float i = 0; i < 23; i++)
            {
                transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.up * 0.5f, 1.2f);
                m_Jump = !RoofCheck();
                if (!m_Jump)
                {
                    break;
                }
                yield return null;
            }
            m_Jump = false;
            yield return null;

        }


    }

    /// <summary>
    /// Get the heat of the player
    /// </summary>
    /// <returns>Heat</returns>
    public float Heat
    {
        get { return m_HeatIndicator.Heat; }
        set { m_HeatIndicator.Heat = value; }
    }

    public int Amount
    {
        get { return m_Inventory.Amount; }
        set { m_Inventory.Amount = value; }
    }

    public void ResetMaxHeat()
    {
        m_HeatIndicator.ResetMaxHeat();
    }

    public void Happy()
    {
        m_Happy = true;
        m_Sad = false;
        UpdateSprite();
    }

    public void Sad()
    {
        m_Happy = false;
        m_Sad = true;
        UpdateSprite();
    }

    public void ResetFace()
    {
        m_Happy = false;
        m_Sad = false;
        UpdateSprite();
    }

    public void Sleep()
    {
        m_Rigidbody.Sleep();
        m_Frozen = true;
    }

    public void WakeUp()
    {
        m_Rigidbody.WakeUp();
        m_Frozen = false;
    }

    public bool GroundCheck()
    {
        Vector2 checkpos1 = new Vector2(m_Collider.bounds.min.x, m_Collider.bounds.min.y);
        Vector2 checkpos2 = new Vector2(m_Collider.bounds.max.x, m_Collider.bounds.min.y);

        checkpos1.x += 0.5f;
        checkpos2.x -= 0.5f;

        Debug.DrawRay(checkpos1, Vector3.down);
        Debug.DrawRay(checkpos2, Vector3.down);

        bool grounded = false;

        if (Physics2D.Raycast(checkpos1, Vector2.down, 0.1f, m_Player))
        {
            grounded = true;
        }
        if (Physics2D.Raycast(checkpos2, Vector2.down, 0.1f, m_Player))
        {
            grounded = true;
        }
        return grounded;
    }

    public bool RoofCheck()
    {
        Vector2 checkpos1 = new Vector2(m_Collider.bounds.min.x, m_Collider.bounds.max.y);
        Vector2 checkpos2 = new Vector2(m_Collider.bounds.max.x, m_Collider.bounds.max.y);

        checkpos1.x += 0.5f;
        checkpos2.x -= 0.5f;

        Debug.DrawRay(checkpos1, Vector3.up);
        Debug.DrawRay(checkpos2, Vector3.up);

        bool cancelJump = false;

        if (Physics2D.Raycast(checkpos1, Vector2.up, 0.1f, m_Player))
        {
            cancelJump = true;
        }
        if (Physics2D.Raycast(checkpos2, Vector2.up, 0.1f, m_Player))
        {
            cancelJump = true;
        }
        return cancelJump;
    }

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    m_CanJump = true;
    //    m_Jump = false;
    //}

    //public void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.collider.tag.Contains("Ground"))
    //    {
    //        m_CanJump = true;

    //    }
    //}

    //public void OnCollisionExit2D(Collision2D collision)
    //{
    //    m_CanJump = false;
    //}
}
