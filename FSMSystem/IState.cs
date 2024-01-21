using System;
using BaseSystems.FSMSystem;

namespace FSMSystem
{
    public interface IState<T>  where T : Enum
    {
        void InitState(FSM<T> fsm);
        T GetStateID();
        void EnterState();
        void ExitState();
    }
}