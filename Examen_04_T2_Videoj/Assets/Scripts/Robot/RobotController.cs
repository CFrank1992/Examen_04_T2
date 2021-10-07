using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController : MonoBehaviour
{
    //public properties
    public float velocityX = 12f;
    
    public float jumpForce = 40f;

    

    public GameObject balaDerecha1;
    public GameObject balaIzquierda1;

    //public GameObject balaDerecha2;
    //public GameObject balaIzquierda2;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    // Start is called before the first frame update


    private bool isJumping = false;

    //Constants

    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;
    private const int ANIMATION_SLIDE = 3;
    private const int ANIMATION_SHOOT = 4;
    private const int ANIMATION_RUNSHOOT = 5;
    private const int ANIMATION_DEAD = 6;

    //Tags

    private const string TAG_PISO = "Ground";
    private const string TAG_KEY = "Key";


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Quieto
        rb.velocity = new Vector2(0, rb.velocity.y);
        changeAnimation(ANIMATION_IDLE);

        //caminarDerecha
        if(Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            sr.flipX = false;
            changeAnimation(ANIMATION_RUN);

        }

        //caminarIzquierda
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
            sr.flipX = true;
            changeAnimation(ANIMATION_RUN);
            
        }

        //Saltar
        if(Input.GetKey(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            changeAnimation(ANIMATION_JUMP);
            isJumping= true;
        }

        //deslizar
        if(Input.GetKey(KeyCode.M))
        {
            changeAnimation(ANIMATION_SLIDE);
        }

        //DispararDerecha
        if(Input.GetKeyUp(KeyCode.X) && !sr.flipX)
        {
            //Crear el objeto
            //1. GameObject que debemos crear
            //2. Position donde va a aparecer
            //3. Rotación
            changeAnimation(ANIMATION_SHOOT);
            
            var position = new Vector2(transform.position.x,transform.position.y);
            var rotation = balaDerecha1.transform.rotation;
            Instantiate(balaDerecha1,position,rotation);

        }

        //DispararIzquierda
        if(Input.GetKeyUp(KeyCode.X) && sr.flipX)
        {
            //Crear el objeto
            //1. GameObject que debemos crear
            //2. Position donde va a aparecer
            //3. Rotación
            changeAnimation(ANIMATION_SHOOT);
            
            var position = new Vector2(transform.position.x,transform.position.y);
            var rotation = balaIzquierda1.transform.rotation;
            Instantiate(balaIzquierda1,position,rotation);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == TAG_PISO)
        {
            isJumping = false;
        }

        if(collision.gameObject.tag == TAG_KEY)
        {
            SceneManager.LoadScene("Scene 2");
        }

    }

    

    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
