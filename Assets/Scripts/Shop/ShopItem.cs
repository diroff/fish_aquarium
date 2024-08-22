using UnityEngine.Events;

public class ShopItem
{
    public BonusData BonusData { get; private set; }

    public UnityAction ItemWasUpdated;

    public ShopItem(BonusData data)
    {
        BonusData = data;
        ItemWasUpdated?.Invoke();
    }
}
