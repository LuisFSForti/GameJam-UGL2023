using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Walk Properties")]
    [SerializeField] private float velocity;

    [Header("Jump Properties")]
    [SerializeField] private float jumpStrength;
    [SerializeField] float originalGravity;
    [SerializeField] private int maxQtdeJump;
    private bool isJumping = false;
    private int qtdeJump = 0;

    [Header("Dash Properties")]
    [SerializeField] private float dashForce = 24f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCoolDown = 1f;
    private bool canDash = true;
    private bool isDashing = false;

    [Header("Player Status")]
    [SerializeField] private float life; 
    [SerializeField] private float speedPercentage = 1f;


    [Header("Components")]
    //private Animator anim;
    private Rigidbody2D rig;
    //private TrailRenderer tr;

    #region "INITIAL SETTINGS / UPDATE"

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        originalGravity = rig.gravityScale;
    }

    private void Update()
    {
        if (!GameManager.instance.isPaused)
        {
            rig.gravityScale = originalGravity;

            if (!isDashing)
            {
                Move();
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.Z) && (canDash || !isJumping))
                StartCoroutine(Dash());
        }
        else
        {
            rig.velocity = Vector2.zero;
            rig.gravityScale = 0;
        }
            
    }

    #endregion

    #region "WALK"

    private void Move()
    {
        float dirX = Input.GetAxisRaw("Horizontal");

        rig.velocity = new Vector2(dirX * velocity * speedPercentage, rig.velocity.y);
    }

    #endregion

    #region "JUMP"

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && qtdeJump < maxQtdeJump)
        {
            rig.velocity = new Vector2(0, jumpStrength);
            qtdeJump++;

            /*
            if (qtdeJump > 1)
            {
                anim.SetBool("DoubleJump", true);
            }*/
        }

    }

    #endregion

    #region "Dash"

    private IEnumerator Dash()
    {
        float direction;
        //float originalGravity = rig.gravityScale;

        canDash = false;
        isDashing = true;
        rig.gravityScale = 0;

        if (rig.velocity.x > 0)
            direction = 1;
        else
            direction = -1;

        rig.velocity = new Vector2(direction * dashForce, rig.velocity.y);
        //tr.emitting = true;

        yield return new WaitForSeconds(dashDuration);
        //tr.emitting = false;
        rig.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    #endregion

    #region "COLLISIONS"

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            qtdeJump = 0;
            isJumping = false;
            //anim.SetBool("DoubleJump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = true;
        }
    }

    #endregion
}
