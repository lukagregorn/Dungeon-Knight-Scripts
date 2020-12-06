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
    public IntValue maxHealth;
    public FloatValue speed;
    public IntValue damage;
    public FloatValue knockTime;
    public FloatValue knockThrust;
    
    // AWAKE [make references to own objects]
    private void Awake() {
        Debug.Log("I have awoken");
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        
        Debug.Log(myRigidbody);
        Debug.Log(animator);

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


    // COMBAT COMMON METHODS
    public virtual void TakeDamage(int damage) { return; }
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

    public virtual void OnDeath() {
        ChangeState(HumanoidState.dead);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    // COMMON CHECKS
    public bool IsDead() { return (GetState() == HumanoidState.dead); }


    // GETTERS
    public Rigidbody2D GetRigidbody() { return myRigidbody; }
    public float GetKnockThrust() { return knockThrust.value; }
    public float GetKnockTime() { return knockTime.value; }
    public int GetDamage() { return damage.value; }

}

