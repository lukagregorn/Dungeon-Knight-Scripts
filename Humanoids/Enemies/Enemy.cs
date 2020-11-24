using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Humanoid
{

    public Transform home;
    public Transform target;
    public FloatValue chaseRadius;
    public FloatValue attackRadius;
    protected int health;
    

    private void Start() {
        health = maxHealth.value;
        target = GameObject.FindWithTag("Player").transform;
    }


    // MOVEMENT
    protected Vector3 MoveTowardsTarget() {
        HumanoidState state = GetState();

        if (state == HumanoidState.idle || state == HumanoidState.walk) {
            Vector3 tmp = Vector3.MoveTowards(transform.position, target.position, speed.value * Time.deltaTime);
            myRigidbody.MovePosition(tmp);

            ChangeState(HumanoidState.walk);

            // returns direction
            return tmp - transform.position;
        }

        return Vector3.zero;
    }

    protected bool IsInChaseRadius() {
        return (Vector3.Distance(target.position, transform.position) <= chaseRadius.value);
    }


    protected bool IsInAttackRadius() {    
        return (Vector3.Distance(target.position, transform.position) <= attackRadius.value);
    }


    // COMBAT
    public override void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            OnDeath();
        }
    }

}
