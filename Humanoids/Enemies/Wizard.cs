using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{

    public GameObject projectile;

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (path != null) {
            if (!ReachedPathEnd() && !IsInAttackRadius()) {
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
    public override IEnumerator AttackCoroutine() {
        while (!IsDead()) {
            yield return new WaitForSeconds(5f);
            if (IsInAttackRadius()) {
                
                // get values
                Vector3 tempVector = target.transform.position - transform.position;
                float knockTime = GetKnockTime();
                float knockThrust = GetKnockThrust();
                int damage = GetDamage();

                // instantiate projectile
                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                
                // fire towards player
                current.GetComponent<Projectile>().Launch(tempVector, damage, knockTime, knockThrust);

            }
        }
    }
    

}
