using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoodStorage : MonoBehaviour
{
    private int _foodCount;

    public UnityAction<int, int> FoodCountChanged;

    public void AddFood(int count)
    {
        if(count < 0)
        {
            ReduceFood(count);
            return;
        }

        int previousFoodCount = _foodCount;

        _foodCount += count;
        FoodCountChanged?.Invoke(previousFoodCount, _foodCount);
    }

    public void ReduceFood(int count)
    {
        if (count <= 0)
            return;

        if (count > _foodCount)
            return;

        int previousFoodCount = _foodCount;

        _foodCount -= count;
        FoodCountChanged?.Invoke(previousFoodCount, _foodCount);
    }
}