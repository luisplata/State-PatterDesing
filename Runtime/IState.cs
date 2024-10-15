public interface IState
{
    void OnEnterState();
    void OnExitState();
    bool IsCompleted();
    MetaDataState GetMetaData();
}