using System.Collections.Generic;
using UnityEngine;

public class ServiceLocatorLoader_Main : MonoBehaviour
{
    //[SerializeField] private bool _loadFromJSON;
        
    private EventBus _eventBus;
       
    //private List<IDisposable> _disposables = new List<IDisposable>();

    private void Awake()
    {
        _eventBus = new EventBus();
        RegisterServices();
        Init();
    }
        
    private void RegisterServices()
    {
        ServiceLocator.Initialize();

        ServiceLocator.Current.Register(_eventBus);
    }

    private void Init()
    {
        //var loaders = new List<ILoader>();
    }

    private void OnDestroy()
    {
        //foreach (var disposable in _disposables)
        //{
        //    disposable.Dispose();
        //}
    }
}