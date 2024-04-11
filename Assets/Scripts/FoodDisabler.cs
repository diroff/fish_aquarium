using UnityEngine;

public class FoodDisabler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food food = collision.GetComponent<Food>();

        if (food == null)
            return;

        food.ReturnToPool();
    }
}