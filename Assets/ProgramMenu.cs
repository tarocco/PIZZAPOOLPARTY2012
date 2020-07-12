using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgramMenu : MonoBehaviour
{

    [Serializable]
    public class ProgramSelectEvent : UnityEvent<string> { };

    [SerializeField]
    private ProgramSelectEvent _ProgramSelected = new ProgramSelectEvent();

    public event UnityAction<string> ProgramSelected
    {
        add { _ProgramSelected.AddListener(value); }
        remove { _ProgramSelected.RemoveListener(value); }
    }

    private void OnProgramSelected(string id)
    {
        _ProgramSelected.Invoke(id);
    }


    public void SelectProgram(string id)
    {
        OnProgramSelected(id);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
