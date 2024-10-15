public class BaseState : IState
{
    protected TeaTime _teaTime;
    protected MetaDataState _metaDataState;

    protected BaseState()
    {
        _teaTime = new TeaTime(ServiceLocator.Instance.GetService<ITeaTimeManager>().GetMonoBehaviour());
    }

    public virtual void OnEnterState()
    {
        _teaTime.Play();
    }

    public virtual void OnExitState()
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