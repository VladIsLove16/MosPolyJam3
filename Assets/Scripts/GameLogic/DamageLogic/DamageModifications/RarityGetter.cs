using System;

public static class RarityGetter
{
    public static Rarity GetRandomRarity()
    {

        int total = 0;
        int max = (int) Enum.GetValues(typeof(Rarity)).GetValue(Enum.GetValues(typeof(Rarity)).Length-1);
        int chance = UnityEngine.Random.Range(0, 100);
        foreach(Rarity a in Enum.GetValues(typeof(Rarity)))
        {
            if (chance < (int)a)
                return a;
        }
        return (Rarity)max;
    }
}