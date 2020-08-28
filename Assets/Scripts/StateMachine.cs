using System.Collections.Generic;
using System;
using UnityEngine;
public class StateMachine {
    private IState _currentState;
    private Dictionary<Type, List<Transition>> _transitionMap = new Dictionary<Type, List<Transition>>();
    private List<Transition> _currentTransitions = new List<Transition>();
    private List<Transition> _anyTransitions = new List<Transition>();
    private static List<Transition> EmptyTransitions = new List<Transition>(0);


    public void Tick()
    {
        Transition transition = GetTransition();
        if(transition != null) 
            SetState(transition.to);

        _currentState.Tick();
    }

    public void SetState(IState state) 
    {
        if (state == _currentState) return;
        Debug.Log("Changing state to: " + state.GetType());
        _currentState?.OnExit();
        _currentState = state;

        _transitionMap.TryGetValue(_currentState.GetType(), out _currentTransitions);
        if(_currentTransitions == null)
            _currentTransitions = EmptyTransitions;

        _currentState.OnEnter();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if(_transitionMap.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            _transitionMap[from.GetType()] = transitions;
        }
        
        transitions.Add(new Transition(to, predicate));
    }

    public void AddAnyTransition(IState to, Func<bool> predicate)
    {
        _anyTransitions.Add(new Transition(to, predicate));
    }

    private class Transition 
    {
        public Func<bool> condition { get; private set; }
        public IState to { get; private set; }

        public Transition (IState state, Func<bool> condition) {
            this.condition = condition;
            this.to = state;
        }
    }

    private Transition GetTransition()
    {
        foreach (Transition transition in _anyTransitions)
            if (transition.condition())
                return transition;

        foreach (Transition transition in _currentTransitions)
            if (transition.condition())
                return transition;

        return null;

    }
}