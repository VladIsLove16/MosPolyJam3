using System.Collections.Generic;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public List<Sprite> heads = new List<Sprite>();
    public List<Sprite> bodys = new List<Sprite>();
    public List<Sprite> arms = new List<Sprite>();
    public List<Sprite> legs = new List<Sprite>();
    public GameObject headPrefab;
    public GameObject bodyPrefab;   
    public GameObject armsPrefab;
    public GameObject legsPrefab;
    [SerializeField]
    public GameObject parent;
    [ContextMenu("CreateCharacter")]
    public void CreateCharacter()
    {
        SpriteRenderer headspriteRenderer = headPrefab.GetComponent<SpriteRenderer>();
        SpriteRenderer bodyspriteRenderer = bodyPrefab.GetComponent<SpriteRenderer>();
        SpriteRenderer armsspriteRenderer = armsPrefab.GetComponent<SpriteRenderer>();
        SpriteRenderer legsspriteRenderer = legsPrefab.GetComponent<SpriteRenderer>();

        headspriteRenderer.sprite = GetRandomElementFromList(heads);
        bodyspriteRenderer.sprite = GetRandomElementFromList(bodys);
        armsspriteRenderer.sprite = GetRandomElementFromList(arms);
        legsspriteRenderer.sprite = GetRandomElementFromList(legs);
    }
    public GameObject GetNewCharacter()
    {
        CreateCharacter();
        return parent;
    }
    private T GetRandomElementFromList<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("Список пуст или не инициализирован.");
            return default;
        }

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}
