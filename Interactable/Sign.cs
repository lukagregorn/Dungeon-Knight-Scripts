using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    public GameObject dialogBox;
    public Text dialogText;

    public IntValue bestWave;

    private string GetText() {
        return "Dungeon ahead . . . \nBEST WAVE: " + bestWave.initialValue.ToString();
    }

    // engage function
    protected override void OnEngage() {
        dialogText.text = GetText();;
        dialogBox.SetActive(true);
    }

    // disengage function
    protected override void OnDisengage() {
        dialogBox.SetActive(false);
    }

}
