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
            _stateMachinePattern.AddState(new ExampleInitialState());
            
            _stateMachinePattern.AddState(new SecondState());

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