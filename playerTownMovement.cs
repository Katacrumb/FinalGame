using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTownMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    private float horizontal;

    public float speed;
    public float jumpPower;
    bool facingRight = true;
   





    // Update is called once per frame
    void Update()
    {
        
        
        if (DialogueManager.GetInstance().dialogueIsPlaying){
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump")){
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

        }

        if(Input.GetButtonDown("Jump") && rb.velocity.y >0f){
            rb.velocity = new Vector2(rb.velocity.x , rb.velocity.y * 0.5f);
        }


        if (horizontal > 0 && !facingRight){
            Flip();
        }
        if (horizontal < 0 && facingRight){
            Flip();
        }
        

        
        
    }

    void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
    private void FixedUpdate(){

         rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    }


// this just flips the character to the other direction


    // When they enter water player slows down 
    void OnTriggerStay2D(Collider2D collider){


         if (collider.gameObject.tag == "Water"){
            
        speed = 7f;
        
        

        }


    }

    // when they exit water player goes to normal
    void OnTriggerExit2D(Collider2D collider){

        if (collider.gameObject.tag != "Water"){
            
        speed = 10f;
        
        

        }


    }


       
        

 }



