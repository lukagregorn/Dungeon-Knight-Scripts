using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntValue", menuName = "Rogue Knight/IntValue", order = 0)]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver {
    public int initialValue;
    public int value;  // used in runtime
    public void OnBeforeSerialize() {
        value = initialValue;
    }
    public void OnAfterDeserialize() {
        value = initialValue;
    }
}
