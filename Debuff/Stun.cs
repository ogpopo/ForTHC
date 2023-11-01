using UnityEngine;

public class Stun : Debuff
{
    private readonly StunDebuffConfig _stunDebuffConfig;

    private readonly int _stunKey = Animator.StringToHash("Stun");
    private readonly int _walkKey = Animator.StringToHash("Walk");

    private readonly float _startSpeed;

    private readonly ParticleSystem _stunFX;

    public Stun(HostileUnit debuffedHostileUnit, DebuffContext debuffContext, StunDebuffConfig config) : base(
        debuffedHostileUnit, debuffContext)
    {
        _stunDebuffConfig = config;
        _startSpeed = debuffedHostileUnit.MoveSpeed;

        debuffedHostileUnit.Animator.SetTrigger(_stunKey);
        debuffedHostileUnit.NavMeshAgent.speed = 0;

        _stunFX = GameObject.Instantiate(_stunDebuffConfig.StunFX, debuffedHostileUnit.transform);
    }

    protected override DebuffType GetDebuffType()
    {
        return DebuffType.Stun;
    }

    protected override void Tic()
    {
    }

    protected override void OnOverDebuff(Debuff debuff)
    {
        DebuffedHostileUnit.NavMeshAgent.speed = _startSpeed;

        _stunFX.Stop();
        DebuffedHostileUnit.Animator.SetTrigger(_walkKey);

        Ended -= OnOverDebuff;
    }
}