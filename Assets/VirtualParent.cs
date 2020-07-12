using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualParent : MonoBehaviour
{
    [SerializeField]
    private Transform _VirtualParentTransform;
    public Transform VirtualParentTransform
    {
        get { return _VirtualParentTransform; }
        set { _VirtualParentTransform = value; }
    }

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = VirtualParentTransform.position;
        transform.rotation = VirtualParentTransform.rotation;
        transform.localScale = VirtualParentTransform.localScale;
    }
}
