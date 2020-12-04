using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Sprite contextClue;

    protected Collider2D playerInRange;
    protected bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange != null) {
            if (!isActive)
                OnEngage();
            else
                OnDisengage();

            // change active flag
            isActive = !isActive;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {

            playerInRange = other;
            Player p = playerInRange.GetComponent<Player>();

            p.SetContextClue(contextClue, true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {

            Player p = other.GetComponent<Player>();
            p.SetContextClue(null, false);

            playerInRange = null;

            // also disengage if we leave the area
            if (isActive) {
                OnDisengage();
                isActive = false;
            }
        }
    }


    // virtual methods
    protected virtual void OnEngage() {
        Debug.Log("Player engaged");
    }

    protected virtual void OnDisengage() {
        Debug.Log("Player engaged");
    }
}
