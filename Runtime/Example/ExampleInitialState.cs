﻿using UnityEngine;

public class ExampleInitialState : IState
{
    private TeaTime _teaTime;
    private float _deltaTimeLocal;
    private MetaDataState _metaDataState;

    public ExampleInitialState()
    {
        _teaTime = new TeaTime(ServiceLocator.Instance.GetService<ITeaTimeManager>().GetMonoBehaviour());
        _metaDataState = new MetaDataState
        {
            id = "ExampleInitialState",
            nextStateId = "SecondState",
            isFirst = true
        };

        _teaTime.Pause().Add(2, () => { Debug.Log($"{nameof(ExampleInitialState)} Start state"); }).Loop(handler =>
        {
            _deltaTimeLocal += handler.deltaTime;
            if (_deltaTimeLocal > 5)
            {
                handler.Break();
                if (Mathf.Approximately(_deltaTimeLocal, 5))
                {
                    _metaDataState.nextStateId = "ThirdState";
                }
                else
                {
                    _metaDataState.nextStateId = "SecondState";
                }
            }

            Debug.Log($"{nameof(ExampleInitialState)} Example to delta time {_deltaTimeLocal} ");
        });
    }

    public void OnEnterState()
    {
        _teaTime.Play();
    }

    public void OnExitState()
    {
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