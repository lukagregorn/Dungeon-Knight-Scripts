using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HumanoidState {
    idle,
    walk,
    attack,
    stagger,
    interact,
    dead
}


public class Humanoid : MonoBehaviour
{
    public string humanoidName;
    protected HumanoidState currentState;
    protected Rigidbody2D myRigidbody;
    protected Animator animator;
    public IntValue initialHealth;
    protected int health;
    public FloatValue initialSpeed;
    protected float speed;
    public float thrust;
    public float knockTime;
    public IntValue initialDamage;
    protected int baseDamage;

    
    // AWAKE [make references to own objects and init variables]
    private void Awake() {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        health = initialHealth.initialValue;
        speed = initialSpeed.initialValue;
        baseDamage = initialDamage.initialValue;

        currentState = HumanoidState.idle;

    }


    // STATE
    protected void ChangeState(HumanoidState newState) {
        if (currentState != newState) {
            currentState = newState;
        }
    }

    public HumanoidState GetState() {
        return currentState;
    }


    // COMBAT
    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            ChangeState(HumanoidState.dead);
            gameObject.SetActive(false);
        }
    }

    public void Knockback(Vector2 knockVector, float recoverTime) {
        HumanoidState state = GetState();
        if (myRigidbody && state != HumanoidState.stagger && !IsDead()) {

            // set state
            ChangeState(HumanoidState.stagger);
            
            // add force
            myRigidbody.AddForce(knockVector, ForceMode2D.Impulse);

            // recover
            StartCoroutine(RecoverCoroutine(recoverTime));
        }
    }

    public IEnumerator RecoverCoroutine(float recoverTime) {
        if (myRigidbody) {

            yield return new WaitForSeconds(recoverTime);
            myRigidbody.velocity = Vector2.zero;

            // give idle state
            ChangeState(HumanoidState.idle);
        }
    }


    // COMMON CHECKS
    public bool IsDead() { return (GetState() == HumanoidState.dead); }


    // GETTERS
    public float GetThrust() { return thrust; }
    public float GetKnockTime() { return knockTime; }
    public int GetDamage() { return baseDamage; }
    public Rigidbody2D GetRigidbody() { return myRigidbody; }

}

