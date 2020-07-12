using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgramAddInController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _Exit = new UnityEvent();
    public event UnityAction Exit
    {
        add => _Exit.AddListener(value);
        remove => _Exit.RemoveListener(value);
    }

    private void OnExit()
    {
        _Exit.Invoke();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            OnExit();
    }
}
