using Kuhpik;
using TMPro;
using UnityEngine;

public class MoneyScreenUI : UIScreen
{
    public TMP_Text MoneyCount => moneyCount;

    [SerializeField] private TMP_Text moneyCount;
}