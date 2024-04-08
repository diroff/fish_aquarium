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
        _creator.ReturnToPool(this);
    }

    public void SetFoodCreator(FoodCreator creator)
    {
        _creator = creator;
    }

    public void SetCount(int count)
    {
        _count = count;
        ResizeScale();
    }

    private void ResizeScale()
    {
        transform.localScale = Vector2.one * (_sizeModificator * _count);
    }
}