using UnityEngine;
[RequireComponent (typeof(Collider2D))]

public class DamagePickup : SpawnObject
{
    [SerializeField]
    Sprite poison;
    [SerializeField]
    Sprite elect;
    [SerializeField]
    Sprite phyc;
    SpriteRenderer SpriteRenderer;
    private DamageModification damageModification; // Модификация урона, которую игрок может подобрать
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer> ();
    }
    // Метод для активации модификации при взаимодействии с игроком
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player= other.GetComponent<Player>();
        if (player != null)
        {
            DamageApplier damageApplier = player.GetDamageApplier();
            damageModification.ApplyModification(damageApplier);
            Taken?.Invoke();
            SoundManager.PlaySound(SoundManager.Sound.pickup);
            Destroy(gameObject);
        }

        // Удаляем объект модификации с карты (или деактивируем его)
    }
    public void SetDamageModification(DamageModification damageModification)
    {
        this.damageModification = damageModification;
        switch(damageModification.modifiers[0].damageType)
        {
            case DamageType.poison:
                    SpriteRenderer.sprite = poison;
                break;
            case DamageType.electic:
                SpriteRenderer.sprite = elect;
                break;
            case DamageType.physical:
                SpriteRenderer.sprite = phyc;
                break;
        }
        Debug.Log("Setted SetDamageModification" + damageModification.ToString());
    }
}
