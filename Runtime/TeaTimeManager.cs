using UnityEngine;

public class TeaTimeManager : MonoBehaviour, ITeaTimeManager
{
    private void Awake()
    {
        //search if Exists a TeaTimeManager
        if (FindObjectsOfType<TeaTimeManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        if (ServiceLocator.Instance.RegisterService<ITeaTimeManager>(this))
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public MonoBehaviour GetMonoBehaviour()
    {
        return this;
    }
}

public interface ITeaTimeManager
{
    MonoBehaviour GetMonoBehaviour();
}