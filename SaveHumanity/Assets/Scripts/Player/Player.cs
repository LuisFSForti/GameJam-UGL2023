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
    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private float dashForce = 24f;
    [SerializeField] private float dashCoolDown = 1f;
    [SerializeField] private float dashDuration = 0.2f;

    [Header("Bounce Effect")]
    [SerializeField] private float bounceFactor;

    [Header("Player Status")]
    [SerializeField] private float fullLife; 
    [SerializeField] private float currentLife; 
    [SerializeField] private float speedPercentage = 1f;
    [SerializeField] private float immortalCoolDown = 1f;

    private bool immortal;

    [Header("Shot Properties")]
    [SerializeField] private bool canShoot = true;
    [SerializeField] private float coolDown = 0.5f;
    [SerializeField] private GameObject shotPrefab;


    [Header("Components")]
    private Animator anim;
    private SpriteRenderer sprite;
    private Rigidbody2D rig;

    #region "INITIAL SETTINGS / UPDATE"

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        originalGravity = rig.gravityScale;
        currentLife = fullLife;
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

            if (Input.GetKeyDown(KeyCode.X) && (canShoot))
                StartCoroutine(Shot());
        }
        else
        {
            rig.velocity = Vector2.zero;
            rig.gravityScale = 0;
        }
        
        if(currentLife <= 0f)
        {
            Debug.Break();
        }
    }

    #endregion

    #region "WALK"

    private void Move()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
       
        if (dirX > 0)
            sprite.flipX = false;
        else if (dirX < 0)
            sprite.flipX = true;

        rig.velocity = new Vector2(dirX * velocity * speedPercentage, rig.velocity.y);

        if (rig.velocity.x != 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

    }

    #endregion
    
    #region "JUMP"

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && qtdeJump < maxQtdeJump)
        {
            rig.velocity = new Vector2(0, jumpStrength);
            qtdeJump++;
        }

    }

    #endregion
    
    #region "DASH AND DASH"

    private IEnumerator Dash()
    {
        float direction;

        canDash = false;
        isDashing = true;
        rig.gravityScale = 0;

        direction = sprite.flipX ? -1 : 1;
        rig.velocity = new Vector2(direction * dashForce, rig.velocity.y);
        //tr.emitting = true;

        yield return new WaitForSeconds(dashDuration);
        //tr.emitting = false;
        rig.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    private IEnumerator Bounce(Vector2 dir)
    {
        isDashing = true;
        rig.gravityScale = 0;

        rig.velocity = new Vector2(dir.x * bounceFactor, dir.y * bounceFactor);

        yield return new WaitForSeconds(0.5f);

        rig.gravityScale = originalGravity;
        isDashing = false;
    }

    #endregion
    
    #region "COLLISIONS"

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            qtdeJump = 0;
            isJumping = false;
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            //ADD BOUNCE

            float dirX = collision.GetContact(0).point.x - transform.position.x;
            float dirY = collision.GetContact(0).point.y - transform.position.y;

            Vector2 dir = - new Vector2(dirX, dirY).normalized;

            StartCoroutine(Bounce(dir));
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

    #region "STATUS"

    public void Engordar(float calorias)
    {
        transform.localScale += new Vector3(calorias, 0, 0);
        speedPercentage -= calorias;
        dashForce -= calorias * 20;
        currentLife--;
    }
    
    public void ReceberDano(float dano)
    {
        if (immortal)
            return;

        currentLife -= dano;
        StartCoroutine(Damage());
        
    }

    private IEnumerator Damage()
    {
        immortal = true;
        anim.SetBool("Damage", true);

        yield return new WaitForSeconds(immortalCoolDown);

        anim.SetBool("Damage", true);
        immortal = false;
    }
  
    public void ChangeSpeed(float speedFactor)
    {
        speedPercentage = speedFactor;
    }
    public void RestoreStatus()
    {
        currentLife = fullLife;
        speedPercentage = 1f;
    }

    #endregion

    #region "SHOT"

    private IEnumerator Shot()
    {
        canShoot = false;

        GameObject shot = Instantiate(shotPrefab);
        shot.transform.position = transform.position;

        int dir = sprite.flipX ? -1 : 1;
        shot.GetComponent<FireBall>().direction = dir;

        yield return new WaitForSeconds(coolDown);

        canShoot = true;
    }

    #endregion

}
