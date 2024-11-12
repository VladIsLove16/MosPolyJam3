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
    [SerializeField]
    private DamageModificationSpawner damageModificationSpawner;
    [SerializeField]
    private ItemSpawner itemSpawner;
    [SerializeField]
    private EnemySpawner EnemySpawner;
    [SerializeField]
    private BulletParent BulletParent;
    [SerializeField]
    private ScoreManager ScoreManager;
       
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
        ServiceLocator.Current.Register(damageModificationSpawner);
        ServiceLocator.Current.Register(itemSpawner);
        ServiceLocator.Current.Register(EnemySpawner);
        ServiceLocator.Current.Register(BulletParent);
        ServiceLocator.Current.Register(ScoreManager);
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