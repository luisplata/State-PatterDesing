namespace BellsebossStudio.Plugins.State
{
    public interface IState
    {
        void OnEnterState();
        void OnExitState();
        bool IsCompleted();
    }
}