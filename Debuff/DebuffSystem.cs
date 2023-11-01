using Kuhpik;
using Supyrb;
using System.Linq;

public class DebuffSystem : GameSystem
{
    public override void OnInit()
    {
        base.OnInit();

        Signals.Get<AddDebuffSignal>().AddListener(TryAddDebuff);
    }

    private void TryAddDebuff(HostileUnit target, DebuffContext debuffContext)
    {
        var debuffType = debuffContext.DebuffType;

        if (target.Debuffs.Where(x => x.Type == debuffType).ToList().Count != 0)
            return;

        if (debuffType == DebuffType.Freezing)
            target.AddDebuffs(new Freezing(target, (FreezingDebuffContext)debuffContext, (FreezingDebuffConfig)config.GameConfigCollection.DebuffDatas.FirstOrDefault(x => x.DebuffType == debuffType)));
        if (debuffType == DebuffType.Poison)
            target.AddDebuffs(new Poison(target, debuffContext, (PoisonDebuffConfig)config.GameConfigCollection.DebuffDatas.FirstOrDefault(x => x.DebuffType == debuffType)));
        if (debuffType == DebuffType.Stun)
            target.AddDebuffs(new Stun(target, debuffContext, (StunDebuffConfig)config.GameConfigCollection.DebuffDatas.FirstOrDefault(x => x.DebuffType == debuffType)));
    }
}