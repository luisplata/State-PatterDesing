using System.Collections.Generic;
using System.Linq;


public class StateMachinePattern
{
    private Dictionary<MetaDataState, IState> _states;
    private MetaDataState _currentState;

    public StateMachinePattern()
    {
        _states = new Dictionary<MetaDataState, IState>();
    }

    public void AddState(IState state)
    {
        var metaData = state.GetMetaData();
        //if id is already in the dictionary, throw an exception
        if (_states.Any(teaTime => teaTime.Key.id == metaData.id))
        {
            throw new System.Exception($"State with id {metaData.id} already exists");
        }

        //validate id the id is null or empty
        if (string.IsNullOrEmpty(metaData.id))
        {
            throw new System.Exception("State id cannot be null or empty");
        }

        //if is the first state, set the current state
        if (metaData.isFirst)
        {
            _currentState = metaData;
        }

        _states.Add(metaData, state);
    }

    public IState GetState(string id)
    {
        //validate id the id is null or empty
        if (string.IsNullOrEmpty(id))
        {
            return null;
        }

        //validate if the state exists
        if (_states.All(teaTime => teaTime.Key.id != id))
        {
            throw new System.Exception($"State with id {id} not found");
        }

        return _states.FirstOrDefault(teaTime => teaTime.Key.id == id).Value;
    }

    public IState GetState()
    {
        return _states.FirstOrDefault(teaTime => teaTime.Key.isFirst).Value;
    }

    public void Configure()
    {
    }

    public string GetNextState()
    {
        var nextState = _currentState.nextStateId;
        _currentState = _states.FirstOrDefault(teaTime => teaTime.Key.id == nextState).Key;
        return nextState;
    }
}

public class MetaDataState
{
    public string id;
    public bool isFirst;
    public string nextStateId;
}