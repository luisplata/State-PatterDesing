using System.Collections;
using UnityEngine;

namespace BellsebossStudio.Plugins.State
{
    public class ExampleStateMachine : MonoBehaviour
    {
        private StateMachine _stateMachine;

        private void Start()
        {
            _stateMachine = new StateMachine();
            var firstState = new MetaDataState
            {
                id = "firstState",
                isFirst = true,
                nextStateId = "secondState",
            };
            _stateMachine.AddState(firstState, new ExampleInitialState());

            var secondState = new MetaDataState { id = "secondState" };
            _stateMachine.AddState(secondState, new ExampleInitialState());

            _stateMachine.Configure();
            StartCoroutine(StateMachine());
        }

        private IEnumerator StateMachine()
        {
            var currentState = _stateMachine.GetState();
            while (currentState is not null)
            {
                currentState.OnEnterState();
                //yield return null;
                //validate if teatime is finished with a boolean var
                Debug.Assert(currentState != null, nameof(currentState) + " != null");
                while (currentState.IsCompleted())
                {
                    yield return null;
                }
                currentState.OnExitState();
                currentState = _stateMachine.GetState(_stateMachine.GetNextState());
            }

            Debug.Log("State Finished");
        }
    }
}