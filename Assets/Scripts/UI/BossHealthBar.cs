using UnityEngine;
using UnityEngine.UIElements;

public class BossHealthBar : HealthBar
{
    [SerializeField]
    float topPercent = 0.1f;
    [SerializeField]
    float leftPercent = 0.1f;

    [SerializeField]
    BossAI bossAI;
    private bool FightStarted;
    protected override void Awake()
    {
        base.Awake();
        bossAI.FightStarted += () => FightStarted = true;
    }
    protected override void UpdateHealthBarVisibility()
    {
        if ((FightStarted))
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    protected override void UpdatehealthBarWidth()
    {
        
    }
    protected override void UpdateHealthBarPosition()
    {
        //root.style.position = Position.Absolute;
        //root.style.alignSelf = Align.Center; 
        //root.style.top = new Length(topPercent, LengthUnit.Percent);
        //root.style.left = new Length(leftPercent, LengthUnit.Percent);
        //root.style.width = new Length(100, LengthUnit.Percent);
        //root.style.maxWidth = new Length(100, LengthUnit.Percent);
        //root.style.flexBasis= new Length(100, LengthUnit.Percent);
    }
}
