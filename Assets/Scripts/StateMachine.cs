using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStuff
{
    public class StateMachine<T>
    {
        public state<T> currentState{get; private set;}
        public T owner;

        public StateMachine(T o)
        {
            owner = o;
            currentState = null;
        }

        public void changeState(state<T> newState)
        {
            if(currentState != null)
                currentState.ExitState(owner);

            currentState = newState;
            currentState.EnterState(owner);
        }

        public void Update()
        {
            if(currentState != null)
                currentState.UpdateState(owner);
        }

        public void FixedUpdate()
        {
            if(currentState != null)
                currentState.FixedUpdateState(owner);
        }
    }

    public abstract class state<T>
    {
        public abstract void EnterState(T owner);
        public abstract void ExitState(T owner);
        public abstract void UpdateState(T owner);
        public abstract void FixedUpdateState(T owner);
    }
}

