using System.Collections;
using UnityEngine;

namespace BellsebossStudio.Plugins.State
{
    public class ExampleStateMachine : MonoBehaviour
    {
        private StateMachinePattern _stateMachinePattern;

        private void Start()
        {
            _stateMachinePattern = new StateMachinePattern();
            var firstState = new MetaDataState
            {
                id = "firstState",
                isFirst = true,
                nextStateId = "secondState",
            };
            _stateMachinePattern.AddState(firstState, new ExampleInitialState());

            var secondState = new MetaDataState { id = "secondState" };
            _stateMachinePattern.AddState(secondState, new ExampleInitialState());

            _stateMachinePattern.Configure();
            StartCoroutine(StateMachine());
        }

        private IEnumerator StateMachine()
        {
            var currentState = _stateMachinePattern.GetState();
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
                currentState = _stateMachinePattern.GetState(_stateMachinePattern.GetNextState());
            }

            Debug.Log("State Finished");
        }
    }
}