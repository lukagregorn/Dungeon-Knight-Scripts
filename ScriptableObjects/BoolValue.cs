using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolValue", menuName = "Rogue Knight/BoolValue", order = 1)]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver {
    public bool initialValue;
    public bool value;  // used in runtime
    public void OnBeforeSerialize() {
        value = initialValue;
    }
    public void OnAfterDeserialize() {
        value = initialValue;
    }
}
