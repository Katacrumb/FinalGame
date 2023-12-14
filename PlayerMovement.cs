using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    float vertical;
    float horizontal;

    public float moveSpeed;
    public float speedlimit = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


    }

    void FixedUpdate(){
    if(horizontal != 0 && vertical != 0){
            horizontal *= speedlimit;
            vertical *= speedlimit;
        }
        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);



    }
}

