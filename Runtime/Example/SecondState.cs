using UnityEngine;

public class SecondState : IState
{
    private TeaTime _teaTime;
    private float _deltaTimeLocal;
    private MetaDataState _metaDataState;

    public SecondState()
    {
        _teaTime = new TeaTime(ServiceLocator.Instance.GetService<ITeaTimeManager>().GetMonoBehaviour());
        _metaDataState = new MetaDataState
        {
            id = "SecondState"
        };
        _teaTime.Pause().Add(2, () => { Debug.Log($"{nameof(SecondState)} Start state"); }).Loop(handler =>
        {
            _deltaTimeLocal += handler.deltaTime;
            if (_deltaTimeLocal > 5)
            {
                handler.Break();
                if (Mathf.Approximately(_deltaTimeLocal, 5))
                {
                    _metaDataState.nextStateId = "ExampleInitialState";
                }
                else
                {
                    _metaDataState.nextStateId = ""; //finisher loop
                }
            }

            Debug.Log($"{nameof(SecondState)} Example to delta time {_deltaTimeLocal}");
        });
    }

    public void OnEnterState()
    {
        _teaTime.Play();
    }

    public void OnExitState()
    {
        _teaTime.Stop();
    }

    public bool IsCompleted()
    {
        return _teaTime.IsPlaying;
    }

    public MetaDataState GetMetaData()
    {
        return _metaDataState;
    }
}