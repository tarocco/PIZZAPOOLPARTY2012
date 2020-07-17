using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyTarget : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    private Rigidbody _Rigidbody;

    public Rigidbody Rigidbody
    {
        get { return _Rigidbody; }
        set { _Rigidbody = value; }
    }

    [SerializeField]
    private Transform _TargetTransform;
    public Transform TargetTransform
    {
        get { return _TargetTransform; }
        set { _TargetTransform = value; }
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Rigidbody.MovePosition(TargetTransform.position);
        Rigidbody.MoveRotation(TargetTransform.rotation);
    }

    public void OnBeforeSerialize()
    {
        if (Rigidbody == null)
            Rigidbody = GetComponent<Rigidbody>();
    }

    public void OnAfterDeserialize()
    {
    }
}
