using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HumanoidState {
    idle,
    walk,
    attack,
    stagger,
    interact,
}


public class Humanoid : MonoBehaviour
{
    public HumanoidState currentState;
    public string humanoidName;
    public int health;
    public float speed;
    public float thrust;
    public float knockTime;
    public int baseAttack;

    public Rigidbody2D myRigidbody;


    // STATE
    public void ChangeState(HumanoidState newState) {
        if (currentState != newState) {
            currentState = newState;
        }
    }

    public HumanoidState GetState() {
        return currentState;
    }


    // COMBAT
    public virtual void Knockback(Vector2 knockVector, float recoverTime) {
        if (myRigidbody && currentState != HumanoidState.stagger) {

            // set state
            ChangeState(HumanoidState.stagger);
            
            // add force
            myRigidbody.AddForce(knockVector, ForceMode2D.Impulse);

            // recover
            StartCoroutine(RecoverCoroutine(recoverTime));
        }
    }

    public virtual IEnumerator RecoverCoroutine(float recoverTime) {
        if (myRigidbody) {

            yield return new WaitForSeconds(recoverTime);
            myRigidbody.velocity = Vector2.zero;

            // give idle state
            ChangeState(HumanoidState.idle);
        }
    }
}

