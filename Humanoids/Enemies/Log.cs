using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (path != null) {
            if (!ReachedPathEnd()) {
                Vector2 direction = MoveTowardsTarget();
                AnimateSpriteDirection(direction);
            }
        }

    }


    // MOVEMENT
    private void AnimateSpriteDirection(Vector2 direction) {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            if (direction.x > 0) {
                animator.SetFloat("moveX", 1f);
                animator.SetFloat("moveY", 0f);
            } else {
                animator.SetFloat("moveX", -1f);
                animator.SetFloat("moveY", 0f);
            }

        } else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) {
            if (direction.y > 0) {
                animator.SetFloat("moveX", 0f);
                animator.SetFloat("moveY", 1f);
            } else {
                animator.SetFloat("moveX", 0f);
                animator.SetFloat("moveY", -1f);
            }
        }
    }


    // COMBAT
    private void OnTriggerEnter2D(Collider2D other) {
        // check if player
        if (other.gameObject.CompareTag("Player") && other.isTrigger) {

            // get humanoid object
            Humanoid playerHuman = other.GetComponent<Humanoid>();
            
            // get values
            float knockTime = GetKnockTime();
            float knockThrust = GetKnockThrust();
            int damage = GetDamage();

            // calculate knock vector
            Vector2 knockVector = other.GetComponent<Rigidbody2D>().transform.position - transform.position;
            knockVector = knockVector.normalized * knockThrust;

            // inflict knockback and damage
            playerHuman.TakeDamage(damage);
            playerHuman.Knockback(knockVector, knockTime);
        }
    }
    

}
