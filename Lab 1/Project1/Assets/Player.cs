using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    public Animator anim;
    public Rigidbody2D rb;

    private bool isGrounded;
    private bool isAirborne;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime * horizontal;
        if (horizontal > 0.1f)
        {
            transform.rotation =Quaternion.Euler(0,0,0);
        }
        else if (horizontal < -0.1f)
        {
            transform.rotation =Quaternion.Euler(0,180,0);
        }

        anim.SetBool("Idle",Mathf.Abs(horizontal) > 0.1f);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0,1)*jumpforce, ForceMode2D.Impulse);
            anim.SetBool(("Jump"), true);
        }
        else if(Input.GetButtonDown("Jump") && isAirborne)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 1) * jumpforce, ForceMode2D.Impulse);
            isAirborne = false;
            anim.SetBool(("Jump"), true);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        anim.SetBool(("Jump"), false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        isAirborne = true;
        
    }
}
