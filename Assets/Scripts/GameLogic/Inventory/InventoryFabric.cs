using UnityEngine;

public class InventoryFabric : MonoBehaviour , IService
{
    Player Player;
    public Cottons Cottons;
    public Milk Milk;
    public Rat Rat;
    private void Awake()
    {
        Player = ServiceLocator.Current.Get<Player>();  
    }
    public InventoryItem Create(InventoryItemType itemType)
    {
        switch (itemType)
        {
            case InventoryItemType.cottons:
                {
                    
                    return Instantiate(Cottons.gameObject, Player.transform.position, Quaternion.identity).GetComponent<InventoryItem>();
                }

            case InventoryItemType.milk:
                {
                   return  Instantiate(Milk.gameObject, Player.transform.position, Quaternion.identity).gameObject.GetComponent<InventoryItem>();
                }
            case InventoryItemType.rat:
                {
                    return Instantiate(Rat.gameObject, Player.transform.position, Quaternion.identity).GetComponent <InventoryItem>();
                }
        }
        return null;
    }
}
