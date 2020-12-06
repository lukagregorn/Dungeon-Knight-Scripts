using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Enemy : Humanoid
{

    public Transform home;
    public Transform target;
    public FloatValue chaseRadius;
    public FloatValue attackRadius;
    public UnityEvent DiedEvent;
    protected int health;
    

    private void Start() {
        health = maxHealth.value;
        GameObject p = GameObject.FindWithTag("Player");
        if (p) {
            target = p.transform;
            Debug.Log("target transform");
            Debug.Log(target.position);
            Debug.Log(maxHealth.value);
            Debug.Log(maxHealth.initialValue);
            Debug.Log(health);
        } else {
            Debug.Log("No player transform");
        }
    }


    // MOVEMENT
    protected Vector3 MoveTowardsTarget() {
        HumanoidState state = GetState();
        Debug.Log(state);
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
        if (target)
            return (Vector3.Distance(target.position, transform.position) <= chaseRadius.value);
        else
            return false;
    }


    protected bool IsInAttackRadius() {
        if (target)
            return (Vector3.Distance(target.position, transform.position) <= attackRadius.value);
        else
            return false;
    }


    // COMBAT
    public override void TakeDamage(int damage) {
        Debug.Log(damage);
        Debug.Log(health);
        health -= damage;
        if (health <= 0) {
            OnDeath();
        }
    }


    // DIED EVENT FOR SPAWNERS
    private void OnDestroy() {
        DiedEvent.Invoke();
    }

}
