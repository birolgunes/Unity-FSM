using System;
using System.Collections.Generic;
using FSMSystem;
using UnityEngine;

namespace BaseSystems.FSMSystem
{
    public abstract class FSM<T> : MonoBehaviour
        where T : Enum
    {
        public IState<T> CurrentState { get; set; }

        public static Action<T> OnStateEntered;
        public static Action<T> OnStateExited;

        protected Dictionary<T, IState<T>> States;
        protected Dictionary<T, List<T>> Transitions;

        [SerializeField] private T _initStateID;
        [SerializeField] private bool _isDebugEnabled = false;

        private void Awake()
        {
            States = new Dictionary<T, IState<T>>();
            Transitions = new Dictionary<T, List<T>>();
            
            InitializeFSM();
            InitializeInitState();
        }

        public abstract void InitializeFSM();

        protected void AddState(T state, IState<T> stateObject)
        {
            States.Add(state, stateObject);
            stateObject.InitState(this);
        }

        protected void AddTransition(T from, List<T> to)
        {
            Transitions.Add(from, to);
        }
        
        private void InitializeInitState()
        {
            CurrentState = States[_initStateID];
            OnStateEntered?.Invoke(CurrentState.GetStateID());
            CurrentState.EnterState();

            if (_isDebugEnabled)
            {
                Debug.Log($"Enter {CurrentState.GetStateID()}");
            }
        }

        public void ChangeState(T nextState)
        {
            if (!Transitions[CurrentState.GetStateID()].Contains(nextState))
            {
                return;
            }

            if (_isDebugEnabled)
            {
                Debug.Log($"Exit {CurrentState.GetStateID()} - Enter {nextState}");
            }

            CurrentState.ExitState();
            OnStateExited?.Invoke(CurrentState.GetStateID());

            CurrentState = States[nextState];
            
            OnStateEntered?.Invoke(CurrentState.GetStateID());
            States[nextState].EnterState();
        }

        public T GetCurrentStateID()
        {
            return CurrentState.GetStateID();
        }
    }
}