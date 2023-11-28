using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{
    [SerializeField] private int _count;

    public void Interact(Creature creature)
    {
        var player = creature.GetComponent<Player>();

        player.AddFood(_count);
        Destroy(gameObject);
    }
}