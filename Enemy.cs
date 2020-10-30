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
    public Vector3 MoveTowardsTarget() {
        if (currentState == HumanoidState.idle || currentState == HumanoidState.walk) {
            Vector3 tmp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            myRigidbody.MovePosition(tmp);

            ChangeState(HumanoidState.walk);

            // returns direction
            return tmp - transform.position;
        }

        return Vector3.zero;
    }

    public bool IsInChaseRadius() {
        return (Vector3.Distance(target.position, transform.position) <= chaseRadius);
    }


    public bool IsInAttackRadius() {    
        return (Vector3.Distance(target.position, transform.position) <= attackRadius);
    }



    // COMBAT

}
