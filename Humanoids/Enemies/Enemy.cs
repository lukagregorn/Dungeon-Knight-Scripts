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
        } else {
            Debug.LogWarning("No player transform");
        }
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
