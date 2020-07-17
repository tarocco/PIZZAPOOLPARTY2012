using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgramSelectInput : MonoBehaviour
{
    [Serializable]
    public struct Entry
    {
        public KeyCode KeyCode;
        public string ProgramId;
    }

    [SerializeField]
    private List<Entry> _Entries;

    public IReadOnlyList<Entry> Entries
    {
        get { return _Entries; }
    }

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

    void Start()
    {
        
    }

    void Update()
    {
        foreach(var entry in Entries)
        {
            if (Input.GetKeyDown(entry.KeyCode))
                OnProgramSelected(entry.ProgramId);
        }
    }
}
