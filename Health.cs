using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int health =20;
    private int MAX_HEALTH = 20;

    // Update is called once per frame
    void Update()
    {
        
        
    if(Input.GetKeyDown(KeyCode.E)){
        //Damage(5);
      }
       /*if(Input.GetKeydown(KeyCode.K)){
        Health(10)
        }*/
    }

    private IEnumerator VisualIndicator(Color color){

        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
         GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Damage(int amount){
    if(amount <0){
    
        throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
            
        }
    
    this.health -= amount;

    StartCoroutine(VisualIndicator(Color.red));

    if(health <= 0){
        Die();

         }
    }

    public void Heal(int amount){
        if(amount <0){
    
        throw new System.ArgumentOutOfRangeException("Cannot have negative healing");
            
        }
        // for readability
        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;
     
        if(wouldBeOverMaxHealth){
            this.health = MAX_HEALTH;
        }else{
            this.health += amount;
        }
    }

    
    private void Die(){
        Debug.Log("man im dead!");
        Destroy(gameObject);

    }
}


