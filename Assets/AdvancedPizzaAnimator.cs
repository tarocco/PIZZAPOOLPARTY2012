using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AdvancedPizzaAnimator : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    private Animator _Animator;

    public Animator Animator
    {
        get => _Animator;
        private set => _Animator = value;
    }

    [SerializeField, Range(0f, 5f)]
    private float _TransitionTime = 0.5f;

    public float TransitionTime
    {
        get { return _TransitionTime; }
        set { _TransitionTime = value; }
    }

    [SerializeField, Range(1f, 10f)]
    private float _LoopMinTime = 5f;

    public float LoopMinTime
    {
        get { return _LoopMinTime; }
        set { _LoopMinTime = value; }
    }

    [SerializeField, Range(1f, 10f)]
    private float _LoopRandomTime = 5f;

    public float LoopRandomTime
    {
        get { return _LoopRandomTime; }
        set { _LoopRandomTime = value; }
    }

    [SerializeField]
    private Vector2Int _NumLoopsRange = new Vector2Int(3, 5);

    public Vector2Int NumLoopsRange
    {
        get { return _NumLoopsRange; }
        set { _NumLoopsRange = value; }
    }

    [SerializeField]
    private List<AnimationClip> _Loops;

    public IReadOnlyList<AnimationClip> Loops
    {
        get { return _Loops; }
    }

    [SerializeField]
    private List<AnimationClip> _Sequences;

    public IReadOnlyList<AnimationClip> Sequences
    {
        get { return _Sequences; }
    }

    [SerializeField]
    private UnityEvent _Smear = new UnityEvent();
    public event UnityAction Smear
    {
        add => _Smear.AddListener(value);
        remove => _Smear.RemoveListener(value);
    }

    public void OnSmear()
    {
        _Smear.Invoke();
    }

    [SerializeField]
    private UnityEvent _UnSmear = new UnityEvent();
    public event UnityAction UnSmear
    {
        add => _UnSmear.AddListener(value);
        remove => _UnSmear.RemoveListener(value);
    }

    public void OnUnSmear()
    {
        _UnSmear.Invoke();
    }

    private float _AnimationTimer = 0f;
    private int _LoopCounter = 0;

    private static T GetRandom<T>(IReadOnlyList<T> list)
    {
        var idx = Random.Range(0, list.Count);
        return list[idx];
    }

    private void TransitionToClip(AnimationClip clip, float transition_time)
    {
        var state_name = clip.name; // Hacky
        Animator.CrossFadeInFixedTime(state_name, transition_time);
    }

    void Start()
    {
    }

    private void AdvanceLoop()
    {
        var clip = GetRandom(Loops);
        _AnimationTimer = Time.time + LoopMinTime + Random.Range(0f, LoopRandomTime);
        _LoopCounter++;
        TransitionToClip(clip, TransitionTime);
    }

    private void AdvanceSequence()
    {
        var clip = GetRandom(Sequences);
        _AnimationTimer = Time.time + clip.length;
        _LoopCounter = 0;
        TransitionToClip(clip, TransitionTime);
    }

    void Update()
    {
        if (_AnimationTimer < Time.time)
        {
            AnimationClip clip;
            if (_LoopCounter < NumLoopsRange[0])
                AdvanceLoop();
            else if (_LoopCounter >= NumLoopsRange[1])
                AdvanceSequence();
            else
            {
                var divisor = NumLoopsRange[1] - NumLoopsRange[0];
                var probability = 1f / divisor;
                if (Random.value > probability)
                    AdvanceLoop();
                else
                    AdvanceSequence();
            }
        }
    }

    public void OnBeforeSerialize()
    {
        if (Animator == null)
            Animator = GetComponent<Animator>();
    }

    public void OnAfterDeserialize()
    {
    }
}
