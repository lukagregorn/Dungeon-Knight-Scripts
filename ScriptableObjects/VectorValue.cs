using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "VectorValue", menuName = "Rogue Knight/VectorValue", order = 0)]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver {
    public Vector2 initialValue;

    [HideInInspector]
    public Vector2 value;  // used in runtime
    public void OnBeforeSerialize() {
        value = initialValue;
    }
    public void OnAfterDeserialize() {}
}
