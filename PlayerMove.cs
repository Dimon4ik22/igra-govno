using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public bool isGrounded;
    Animator anim;
    Rigidbody rb;
    ParticleSystem ps;
    public float speed, jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.Mouse0) && isGrounded == true)
        {
            Vector3 vec = new Vector3(0, jumpForce, 0);
            rb.AddForce(vec, ForceMode.Impulse);
			anim.Play("PlayerFrontFlip");
			ps.Stop();
            isGrounded = false;
        }
        if(rb.position.y < -1)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

        //if (Input.GetKeyDown("escape"))
        //    Cursor.lockState = CursorLockMode.None;
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Ground" && isGrounded == false)
        {
            isGrounded = true;
            ps.Play();
        }
    }
}
