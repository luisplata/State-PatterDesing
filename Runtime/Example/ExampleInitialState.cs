using BellsebossStudio.Plugins.ServiceLocatorPattern;
using UnityEngine;

namespace BellsebossStudio.Plugins.State
{
    public class ExampleInitialState : IState
    {
        private TeaTime _teaTime;
        private float _deltaTimeLocal;

        public ExampleInitialState()
        {
            _teaTime = new TeaTime(ServiceLocator.Instance.GetService<ITeaTimeManager>().GetMonoBehaviour());

            _teaTime.Pause().Add(2, () => { Debug.Log("ExampleTo initial State"); }).Loop(handler =>
            {
                _deltaTimeLocal += handler.deltaTime;
                if (_deltaTimeLocal > 5)
                {
                    handler.Break();
                }

                Debug.Log($"Example to delta time {_deltaTimeLocal}");
            });
        }

        public void OnEnterState()
        {
            _teaTime.Play();
            Debug.Log("StartTeaTime");
        }

        public void OnExitState()
        {
            _teaTime.Stop();
        }

        public bool IsCompleted()
        {
            return _teaTime.IsPlaying;
        }
    }
}