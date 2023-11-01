using System;
using System.Collections;
using UnityEngine;

public abstract class Debuff
{
    public Action<Debuff> Ended;

    public DebuffType Type { get; }

    protected int DamageInTic { get; private set; }
    protected HostileUnit DebuffedHostileUnit { get; private set; }

    private readonly int _duration;

    private Coroutine _ticCoroutine;

    protected Debuff(HostileUnit debuffedHostileUnit, DebuffContext context)
    {
        Type = GetDebuffType();

        DebuffedHostileUnit = debuffedHostileUnit;

        _duration = context.Duration;
        DamageInTic = context.DamageInTic;

        DebuffedHostileUnit.Died += OnDebuffedHostileUnitDie;

        _ticCoroutine = Coroutines.StartRoutine(Countdown());

        Ended += OnOverDebuff;
    }

    private IEnumerator Countdown()
    {
        var pastTenses = 0;
        
        yield return new WaitForSeconds(.01f);

        while (pastTenses <= _duration)
        {
            yield return new WaitForSeconds(1);

            Tic();
            pastTenses++;
        }

        Ended?.Invoke(this);
    }

    private void OnDebuffedHostileUnitDie()
    {
        Coroutines.StopRoutine(_ticCoroutine);
        _ticCoroutine = null;
        DebuffedHostileUnit.Died -= OnDebuffedHostileUnitDie;
    }

    protected abstract void Tic();
    protected abstract DebuffType GetDebuffType();
    protected abstract void OnOverDebuff(Debuff debuff);
}