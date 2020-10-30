using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = HumanoidState.idle;
        
        target = GameObject.FindWithTag("Player").transform;

        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsInChaseRadius()) {
            if (!IsInAttackRadius()) {
                MoveTowardsTarget();
            } else {
                Debug.Log("Attack");
            }
        }
    }


    // MOVEMENT


    // COMBAT
    private void OnTriggerEnter2D(Collider2D other) {
        // check if player
        if (other.gameObject.CompareTag("Player")) {
            
            Vector2 knockVector = other.GetComponent<Rigidbody2D>().transform.position - transform.position;
            knockVector = knockVector.normalized * thrust;

            other.GetComponent<Humanoid>().Knockback(knockVector, knockTime);
        }
    }
    

}
