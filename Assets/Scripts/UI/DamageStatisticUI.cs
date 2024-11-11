using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageStatisticUI
{
    VisualElement root;
    public DamageStatisticUI(VisualElement root)
    {
        this.root = root;
    }
    List<VisualElement> damageStatisticContainers = new List<VisualElement>();
    Dictionary<DamageComponent, Label> damageComponentsDict = new();

    public void Init(List<DamageComponent> damageComponents)
    {
        damageStatisticContainers = root.Children().ToList();
        HideAllContainers();
        for (int i = 0; i < damageComponents.Count; i++)
        {
            VisualElement damageStatisticContainer = GetorAddContainer(i);
            DamageComponent damageComponent = damageComponents[i];
            Setup(damageComponent, damageStatisticContainer);
        }
    }

    private void HideAllContainers()
    {
        foreach (var container in damageStatisticContainers)
        {
            container.style.visibility = Visibility.Hidden;
        }
    }

    public void OnAddDamageComponent(DamageComponent damageComponent)
    {
        VisualElement visualElement = GetorAddContainer(damageComponentsDict.Count);
        Setup(damageComponent, visualElement);
    }

    private void Setup(DamageComponent damageComponent, VisualElement damageStatisticContainer)
    {
        damageStatisticContainer.Q("icon").style.backgroundImage = new StyleBackground(damageComponent.Sprite);
        Label totalDamage = damageStatisticContainer.Q<Label>("totalDamage");

        damageComponentsDict.Add(damageComponent, totalDamage);
        OnDamageComponentChanged(damageComponent);
        damageComponent.damageChanged += OnDamageComponentChanged;

    }

    private VisualElement GetorAddContainer(int index)
    {
        VisualElement visualElement;

        if (damageStatisticContainers.Count <= index)
        {
            visualElement = Create();
            damageStatisticContainers.Add(visualElement);  // Добавляем новый элемент в список
            root.Add(visualElement);  // Добавляем его в интерфейс
        }
        else
        {
            visualElement = damageStatisticContainers[index];
            visualElement.style.visibility = Visibility.Visible;
        }

        return visualElement;
    }

    private VisualElement Create()
    {
        var visualTree = Resources.Load<VisualTreeAsset>("Assets/UI Toolkit/DamageStatistic.uxml");
        VisualElement newElement = visualTree.CloneTree();
        return newElement;
    }

    private void OnDamageComponentChanged(DamageComponent damageComponent)
    {
        damageComponentsDict[damageComponent].text = damageComponent.GetDescription();
    }
}
