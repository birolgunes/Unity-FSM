using System;
using FSMSystem;

namespace BaseSystems.FSMSystem
{
    public abstract class BaseState<T> : IState<T> where T : Enum
    {
        protected FSM<T> FSM { get; private set; }

        public void InitState(FSM<T> fsm)
        {
            FSM = fsm;
        }

        public abstract T GetStateID();
        public abstract void EnterState();
        public abstract void ExitState();
    }
}