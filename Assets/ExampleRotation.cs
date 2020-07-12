using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExampleRotation : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    private Rigidbody _Rigidbody;

    public Rigidbody Rigidbody
    {
        get { return _Rigidbody; }
        set { _Rigidbody = value; }
    }

    void Start()
    {
        
    }

    void Update()
    {
        var parent_rotation = transform.parent?.rotation ?? Quaternion.identity;
        var point = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        point = point - new Vector3(0.5f, 0.5f, 0f);
        var rotation = Quaternion.Euler(180f * point.y, -180f * point.x, 0f);
        Rigidbody.MoveRotation(rotation * parent_rotation);
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
