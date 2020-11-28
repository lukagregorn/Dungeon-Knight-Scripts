using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool dialogActive;


    // engage function
    protected override void OnEngage() {
        dialogText.text = dialog;
        dialogBox.SetActive(true);
    }

    // disengage function
    protected override void OnDisengage() {
        dialogBox.SetActive(false);
    }

}
