using DG.Tweening;
using Kuhpik;
using Supyrb;
using UnityEngine;

public class MonetarySystem : GameSystemWithScreen<MoneyScreenUI>
{
    private Tween _moneyTween;

    [SerializeField] private float changeMoneyEffectTime;

    public override void OnInit()
    {
        Signals.Get<ChangedMoneySignal>().AddListener(ChangedMoney);

        screen.MoneyCount.text = game.PlayerField.Tower.TowerPersonalData.Money.ToString();
    }

    private void ChangedMoney(int value)
    {
        game.PlayerField.Tower.TowerPersonalData.Money += value;
        UpdateUI();

        Bootstrap.Instance.SaveGame();
    }

    public void UpdateUI()
    {
        ChangeMoneyEffect(game.PlayerField.Tower.TowerPersonalData.Money, changeMoneyEffectTime);
    }

    private void ChangeMoneyEffect(int to, float time)
    {
        if (_moneyTween != null)
            DOTween.Kill(_moneyTween);

        var variableValue = int.Parse(screen.MoneyCount.text);
        _moneyTween = DOTween.To(() => variableValue, x => variableValue = x, to, time).OnUpdate(() =>
        {
            screen.MoneyCount.text = variableValue.ToString();
        });
    }
}