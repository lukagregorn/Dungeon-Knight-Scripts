using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatValue", menuName = "Rogue Knight/FloatValue", order = 0)]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver {
    public float initialValue;

    [HideInInspector]
    public float value;  // used in runtime
    public void OnBeforeSerialize() {
        value = initialValue;
    }
    public void OnAfterDeserialize() {}
}
