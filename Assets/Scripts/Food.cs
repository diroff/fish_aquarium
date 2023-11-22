using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{
    public void Interact(Creature creature)
    {
        Destroy(gameObject);
    }
}