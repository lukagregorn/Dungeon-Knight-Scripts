using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Humanoid
{

    public Transform home;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;


    // MOVEMENT
    public void MoveTowardsTarget() {
        if (currentState == HumanoidState.idle || currentState == HumanoidState.walk) {
            Vector3 tmp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            myRigidbody.MovePosition(tmp);

            ChangeState(HumanoidState.walk);
        }
    }

    public bool IsInChaseRadius() {
        return (Vector3.Distance(target.position, transform.position) <= chaseRadius);
    }


    public bool IsInAttackRadius() {    
        return (Vector3.Distance(target.position, transform.position) <= attackRadius);
    }



    // COMBAT

}
