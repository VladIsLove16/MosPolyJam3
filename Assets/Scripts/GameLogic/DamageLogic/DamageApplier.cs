using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using Unity.VisualScripting;
/// <summary>
/// Применяет урон компонентов к врагу.
/// </summary>
public class DamageApplier : MonoBehaviour
{
    public Dictionary<DamageType, DamageComponent> _damageComponents = new();
    public int _damageComponentsCount;
    [SerializeField]List<DamageComponent> _componentsListDebug = new List<DamageComponent>();
    [SerializeField]List<DamageComponent> StartedDamageComponents = new List<DamageComponent>();
    public Action<DamageComponent> DamageComponentUpdated;
    public List<DamageModification> StartedModificationList;
    [SerializeField]public ElectricDamageComponent electric;
    [SerializeField]public PoisonDamageComponent poison;
    [SerializeField] public PhysicalDamageComponent phys;
    private void Awake()
    {
        foreach(var damageComponent in StartedDamageComponents)
        {
            AddDamageComponent(damageComponent);
        }
        foreach(var a in StartedModificationList)
        {
            a.ApplyModification(this);
        }
    }
    private void Update()
    {
        _damageComponentsCount = _damageComponents.Count;
    }
    public void ApplyDamage(DamageParameters damageParameters, IDamageable damageable)
    {
        foreach (var damageComponent in _damageComponents.Values)
        {
            damageComponent.ApplyDamage(damageable,  damageParameters);
        }
    }
    public float CalculateDamage()
    {
        float total = 0;
        foreach (var damageComponent in _damageComponents.Values)
        {
             total += damageComponent.CalculateDamage();
        }
        return total;
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
            Debug.Log("(_damageComponents.CreateDamageComponent" + type.ToString());
            DamageComponent damageComponent = CreateDamageComponent(type);
            if(damageComponent==null)
            {
                Debug.Assert(false,"(_damageComponents.CreateDamageComponent NULL" + type.ToString());
            }
            AddDamageComponent(damageComponent);
            return damageComponent;
        }
    }

    private void AddDamageComponent(DamageComponent damageComponent)
    {
        _damageComponents.Add(damageComponent.damageType, damageComponent);
        DamageComponentUpdated?.Invoke(damageComponent);
        Debug.Log("(_damageComponents.added" + damageComponent.damageType.ToString());
        _componentsListDebug.Add(damageComponent);
    }
    private DamageComponent CreateDamageComponent(DamageType type)
    {
        DamageComponent damageComponent = new();
        switch (type)
        {
            case DamageType.electic:
                damageComponent = electric;
                break;
            case DamageType.poison:
            damageComponent = poison;
                break;
            case DamageType.physical:
            damageComponent = phys;
                break;
            default:
                damageComponent = phys;
                break ;
        }
        return damageComponent;
    }
}
