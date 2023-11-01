public class DebuffContext
{
    public int Duration { get; private set; }
    public int DamageInTic { get; private set; }
    public DebuffType DebuffType { get; private set; }

    public void Init(int damage, int duration, DebuffType debuffType)
    {
        DamageInTic = damage;
        Duration = duration;
        DebuffType = debuffType;
    }
}