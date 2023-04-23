using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Player player;

    [Header("Movement details")]

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float distanceToRun;
    private float maxDistance;
    public bool canMove;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float ceillingCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform groundForwardCheck;
    [SerializeField] private Transform wallCheck; // center of box for gizmos
    [SerializeField] private Vector2 wallCheckSize;
    private bool isGrounded;
    private bool groundForward;
    private bool wallDetected;
    private bool ceillingDetected; // kayarken idle pozisyonuna geçtiğinde takılıp kalmamasını önlemek için ilk bu şekilde değişken tanımladık
    public bool ledgeDetected;

    [Header("Ledge info")]
    [SerializeField] private Vector2 offset1; // offset for position before climb
    [SerializeField] private Vector2 offset2; // offset for position AFTER climb

    private Vector2 climbBegunPosition;
    private Vector2 climbOverPosition;

    private bool canGrabLedge = true;
    private bool canClimb;
    private bool justRespawned = true;
    private float defaultGravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameManager.instance.player;

        rb.velocity = new Vector2(10, 15); // ilk animasyon için konum verdik.
        defaultGravityScale = rb.gravityScale;
        rb.gravityScale = rb.gravityScale * .6f;

        maxDistance = transform.position.x + distanceToRun;



    }
    private void Update()
    {
        if (justRespawned)
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = defaultGravityScale * 2;
            }
            if (isGrounded)
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        CheckCollision();
        AnimatorControllerParameter();
        Movement();
        CheckForLedge();
        SpeedController();

        if (transform.position.x > maxDistance)
        {
            canMove = false;
            return;
        }



        if (!groundForward || wallDetected)
        {
            Jump();
        }


    }
    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }

    }
    private void SpeedController()
    {
        bool playerAhead = player.transform.position.x > transform.position.x;
        bool playerFarAway = Vector2.Distance(player.transform.position, transform.position) > 2.5f;
        if (playerAhead)
        {
            if (playerFarAway)
            {
                moveSpeed = 22;
            }
            else
            {
                moveSpeed = 17;
            }
        }
        else
        {
            if (playerFarAway) // player behind
            {
                moveSpeed = 11;

            }
            else
            {
                moveSpeed = 14;
            }

        }
    }


    private void Movement()
    {
        if (justRespawned)
        {
            return;
        }

        if (canMove)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }
    #region Ledge Climb Region

    private void CheckForLedge()
    {
        if (ledgeDetected && canGrabLedge)
        {
            canGrabLedge = false;
            rb.gravityScale = 0; //roll is not triggered after ledge climb

            Vector2 ledgePosition = GetComponentInChildren<LedgeDetection>().transform.position; // ledge classına ulaşıyoruz

            climbBegunPosition = ledgePosition + offset1;
            climbOverPosition = ledgePosition + offset2;

            canClimb = true; // we can no longer grabledge but we can climb
        }

        if (canClimb)
            transform.position = climbBegunPosition;
    }

    private void LedgeClimbOver()
    {

        canClimb = false;

        rb.gravityScale = 5;

        transform.position = climbOverPosition; // duvarı aşınca yukarı pozisyonda olmasını istiyoruz.

        Invoke("AllowLedgeGrab", .1f); // hafif bekletmeli çağırdık metodu
    }

    private void AllowLedgeGrab() => canGrabLedge = true;


    #endregion

    private void AnimatorControllerParameter()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("canClimb", canClimb);
        anim.SetBool("justRespawned", justRespawned);
    }
    private void AnimationTrigger()
    {
        rb.gravityScale = defaultGravityScale;
        justRespawned = false;
        canMove = true;
    }
    private void CheckCollision()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        groundForward = Physics2D.Raycast(groundForwardCheck.position, Vector2.down, groundCheckDistance, whatIsGround); // enemy için özel

        ceillingDetected = Physics2D.Raycast(transform.position, Vector2.up, ceillingCheckDistance, whatIsGround);

        wallDetected = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, 0, whatIsGround);
    }
    private void OnDrawGizmos() // visual debugging dont have a functionality
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance)); // playerden aşağıya doğru bir çizgi
        Gizmos.DrawLine(groundForwardCheck.position, new Vector2(groundForwardCheck.position.x, groundForwardCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + ceillingCheckDistance)); // playerden yukarı doğru bir çizgi
        Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
    }

}
