using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{
    [SerializeField] private int _startCount;
    [SerializeField] private float _sizeModificator;

    private FoodCreator _creator;
    private int _count;

    private void Awake()
    {
        _count = _startCount;
    }

    public void Interact(Creature creature)
    {
        var player = creature.GetComponent<Player>();

        player.AddFood(_count);
        ReturnToPool();
    }

    public void SetFoodCreator(FoodCreator creator)
    {
        _creator = creator;
    }

    public void SetCount(int count)
    {
        _count = count;
    }

    public void ReturnToPool()
    {
        _creator.ReturnToPool(this);
    }
}