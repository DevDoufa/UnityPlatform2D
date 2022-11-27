using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header ("Basic infos")]
    public int speed;
    public Transform foot;
    public LayerMask ground;
    private bool doubleJump = false;
    public int jumpForce;
    private bool isTouchingWall;

    [Header ("WallSlide")]
    public Transform wallCheck;
    public float wallCheckDistance;
    private bool isWallSliding;
    public int wallSlideSpeed;
    private int facing = 1;

    private bool canMove = true;

    public static PlayerBehaviour inst;

    [SerializeField] private float acceleration;
    public float accelerationSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inst = this;

    }

    void Update()
    {
        
        WallSlide();
        WallJump();
        if (canMove)
        {
            Movement();
            Jump();
        }
        
        OnFloor();
    }


    void Movement() //Movimentação lateral
    {
        float moving = Input.GetAxisRaw("Horizontal"); //Checa se os botoes horizontais estão sendo pressionados
        //rb.velocity = new Vector2(moving * speed * acceleration, rb.velocity.y); //Define a velocidade horizontal
        if (moving != 0)
        {
            acceleration += accelerationSpeed;
            if (acceleration > 1)
            {
                acceleration = 1;
            }
        }
        else
        {
            acceleration -= accelerationSpeed;
            if (acceleration < 0)
            {
                acceleration = 0;
            }
        }
        
        switch (moving)
        {
            case > 0:
                if (anim.GetBool("onFloor"))
                {
                    anim.SetBool("run", true);
                }
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                facing = 1;
                break;
            case < 0:
                if (anim.GetBool("onFloor"))
                {
                    anim.SetBool("run", true);
                }
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                facing = -1;
                break;
            default:
                anim.SetBool("run", false);
                break;
        }
        rb.velocity = new Vector2(facing * speed * acceleration, rb.velocity.y);
    
    }   

    void OnFloor() //Checagem do chão
    {
        if (Physics2D.OverlapCircle(foot.position,0.3f, ground)) //Se colidir com o chão
        {
            doubleJump = false;
            anim.ResetTrigger("doubleJump");
            anim.SetBool("onFloor", true); //Define que o player está no chão
            
        }
        else
        {
            anim.SetBool("onFloor", false);
        }
    }

    void WallSlide() //WallSlide
    {
        isTouchingWall = Physics2D.Raycast(wallCheck.position, new Vector2(facing, 0f), wallCheckDistance, ground);
    
        isWallSliding = (isTouchingWall && !anim.GetBool("onFloor") && rb.velocity.y < 0) ? true : false;

        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
            anim.SetBool("wallSlide", true);
        }
        else
        {
            anim.SetBool("wallSlide", false);
        }
    
    }

    void WallJump()
    {
        if (isWallSliding)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(-facing * jumpForce, jumpForce), ForceMode2D.Impulse);
                StartCoroutine("StopMove");
                transform.Rotate(new Vector3(0f,180f,0f));
                facing *= -1;
            }
        }
    }
    void Jump() //Pulo 
    {
        if (Input.GetButtonDown("Jump")) //Se eu apertar pra pular
        {
            if (anim.GetBool("onFloor")) //Checa se estou no chão
            {
                rb.velocity = Vector2.up * jumpForce; //Da um impulso pra cima
            }
            else
            {
                if (!doubleJump && !isWallSliding) //Se ainda não deu doubleJump
                {
                    rb.velocity = Vector2.up * jumpForce; //Define a velocidade vertical pra 0
                    doubleJump = true; //Define que ja deu Double Jump
                    anim.SetTrigger("doubleJump"); //Chama a animação de doubleJump
                }
            }

            
            
        }

        if (rb.velocity.y < 0f) //Se estiver caindo
        {
            anim.SetBool("falling", true);
        }
        else
        {
            anim.SetBool("falling", false); 
        }
    }

    public void Hit()
    {
        
        anim.SetTrigger("hit");
        StartCoroutine("StopMove");
        GameController.inst.GameOver();
        Destroy(gameObject);
    }
    
    IEnumerator StopMove()
    {
        canMove = false;
        yield return new WaitForSeconds(.3f);
        canMove = true;
    }
    void OnDrawGizmos() //Desenha o Raycast
    {
        Gizmos.color = Color.white;
        if (facing == 1)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        }
        else
        {
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x - wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        }
    }
    


}
