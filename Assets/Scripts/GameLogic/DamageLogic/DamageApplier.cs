using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class DamageApplier : MonoBehaviour
{
    public Dictionary<DamageType, DamageComponent> _damageComponents = new();
    public int _damageComponentsCount;
    [SerializeField ]
    List<DamageComponent> _componentsListDebug = new List<DamageComponent>();
    [SerializeField ]
    List<DamageComponent> StartedDamageComponents = new List<DamageComponent>();
    public Action<DamageComponent> DamageComponentUpdate;
    private void Awake()
    {
        foreach(var damageComponent in StartedDamageComponents)
        {
            AddDamageComponent(damageComponent);
        }
    }
    private void Update()
    {
        _damageComponentsCount = _damageComponents.Count;
    }
    public void ApplyDamage(IDamageable damageable)
    {
        foreach (var damageComponent in _damageComponents.Values)
        {
            damageComponent.ApplyDamage(damageable);
        }
    }

    // Метод для получения компонента DamageComponent по типу
    public DamageComponent GetDamageComponent(DamageType type)
    {
        DamageComponent damageComponent = GetOrAddDamageComponent(type);
        return damageComponent;
    }
    public List<DamageComponent> GetDamageComponentstList()
    {
        return _damageComponents.Values.ToList();
    }
    private DamageComponent GetOrAddDamageComponent(DamageType type)
    {
        if (_damageComponents.ContainsKey(type))
        {
            Debug.Log("(_damageComponents.ContainsKey(type)" + type.ToString());
            return _damageComponents[type];

        }
        else
        {
            DamageComponent damageComponent = CreateDamageComponent(type);
            AddDamageComponent(damageComponent);
            return damageComponent;
        }
    }

    private void AddDamageComponent(DamageComponent damageComponent)
    {
        _damageComponents.Add(damageComponent.damageType, damageComponent);
        DamageComponentUpdate?.Invoke(damageComponent);
        Debug.Log("(_damageComponents.added" + damageComponent.damageType.ToString());
        _componentsListDebug.Add(damageComponent);
    }
    private DamageComponent CreateDamageComponent(DamageType type)
    {
        DamageComponent damageComponent = new();
        switch (type)
        {
            case DamageType.electic:
            damageComponent = new ElectricDamageComponent();
                break;
            case DamageType.poison:
            damageComponent = new PoisonDamageComponent();
                break;
            case DamageType.physical:
            damageComponent = new DamageComponent();
                break;
            default:
                damageComponent = new DamageComponent();
                break ;
        }
        return damageComponent;
    }
}
