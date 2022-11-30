using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rBody;
    private float horizontal;

    [SerializeField]private float speed = 3;
    [SerializeField]private float jumpForce = 10;
    [SerializeField]bool isGrounded;
    [SerializeField]Transform groundSensor;
    [SerializeField]float sensorRadius;
    [SerializeField]LayerMask sensorLayer;

    private Animator anim;

    
    // Start is called before the first frame update
    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if(horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("IsRunning", true);
        }

        else if(horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("IsRunning", true);
        }

        else if(horizontal == 0)
        {
            anim.SetBool("IsRunning", false);
        }

        isGrounded = Physics2D.OverlapCircle(groundSensor.position, sensorRadius, sensorLayer);

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("IsJumping", true);
        }

    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 3)
        {
            anim.SetBool("IsJumping", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Star")
        {
            GameManager.Instance.LoadLevel(0);
        }
        else if(other.gameObject.tag == "Coin")
        {
            GameManager.Instance.AddCoin(other.gameObject);
        }
    }
    

    void FixedUpdate()
    {
        rBody.velocity = new Vector2(horizontal * speed, rBody.velocity.y);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundSensor.position, sensorRadius);
    }
}
