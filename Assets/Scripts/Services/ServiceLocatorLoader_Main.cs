using System.Collections.Generic;
using UnityEngine;

public class ServiceLocatorLoader_Main : MonoBehaviour
{
    //[SerializeField] private bool _loadFromJSON;
        
    private EventBus _eventBus;
    private Inventory inventory;
    [SerializeField]
    private Player player;
    [SerializeField]
    private InventoryFabric InventoryFabric;
    [SerializeField]
    private GameAssets gameAssets;
       
    //private List<IDisposable> _disposables = new List<IDisposable>();

    private void Awake()
    {
      
        Init();
        RegisterServices();

    }

    private void RegisterServices()
    {
        ServiceLocator.Initialize();
        //c#
        //порядок важен пизда
        ServiceLocator.Current.Register(InventoryFabric);
        ServiceLocator.Current.Register(inventory);

        //monobehs
        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register(player);
        ServiceLocator.Current.Register(gameAssets);
    }

    private void Init()
    {
        _eventBus = new EventBus();
        inventory = new Inventory();
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