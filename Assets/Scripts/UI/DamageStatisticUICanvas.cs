using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageStatisticUICanvas : MonoBehaviour
{
    [SerializeField] private GameObject damageComponentUIPrefab;
    [SerializeField] private Transform containerParent;

    [SerializeField] private List<GameObject> damageStatisticContainers = new List<GameObject>();
    private Dictionary<DamageComponent, TextMeshProUGUI> damageComponentsDict = new();

    public void Init(List<DamageComponent> damageComponents)
    {
        HideAllContainers();
        for (int i = 0; i < damageComponents.Count; i++)
        {
            GameObject damageStatisticContainer = GetOrAddContainer(i);
            DamageComponent damageComponent = damageComponents[i];
            Setup(damageComponent, damageStatisticContainer);
        }
    }

    private void HideAllContainers()
    {
        foreach (var container in damageStatisticContainers)
        {
            container.SetActive(false);
        }
    }

    public void OnAddDamageComponent(DamageComponent damageComponent)
    {
        GameObject visualElement = GetOrAddContainer(damageComponentsDict.Count);
        Setup(damageComponent, visualElement);
    }

    private void Setup(DamageComponent damageComponent, GameObject damageStatisticContainer)
    {
        Image icon = damageStatisticContainer.transform.Find("icon").GetComponent<Image>();
        TextMeshProUGUI totalDamage = damageStatisticContainer.transform.Find("totalDamage").GetComponent<TextMeshProUGUI>();

        icon.sprite = damageComponent.Sprite;
        damageComponentsDict.Add(damageComponent, totalDamage);
        OnDamageComponentChanged(damageComponent);
        damageComponent.damageChanged += OnDamageComponentChanged;
    }

    private GameObject GetOrAddContainer(int index)
    {
        GameObject visualElement;
        if (damageStatisticContainers.Count <= index)
        {
            visualElement = Create();
            damageStatisticContainers.Add(visualElement);
        }
        else
        {
            visualElement = damageStatisticContainers[index];
            visualElement.SetActive(true);
        }
        return visualElement;
    }

    private GameObject Create()
    {
        return Instantiate(damageComponentUIPrefab, Vector3.zero, Quaternion.identity, containerParent);
    }

    private void OnDamageComponentChanged(DamageComponent damageComponent)
    {
        damageComponentsDict[damageComponent].text = damageComponent.GetDescription();
    }
}
