using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCreator : MonoBehaviour
{
    [Header("Food level settings")]
    [SerializeField] private int _foodMaxValueOnLevel;
    [SerializeField] private int _foodCountOnLevel;
    [SerializeField] private float _spawnDelay = 2f;

    [Header("Food settings")]
    [SerializeField] private Food _foodPrefab;
    [SerializeField] private int _maxPoolCount;

    [Header("Food spawn range")]
    [SerializeField] private Vector2 _minSpawnRange;
    [SerializeField] private Vector2 _maxSpawnRange;

    private Queue<Food> _food = new Queue<Food>();

    private int _foodValueRemaining;
    private int _foodCountRemaining;

    private void Awake()
    {
        for (int i = 0; i < _maxPoolCount; i++)
            CreateFood();

        _foodValueRemaining = _foodMaxValueOnLevel;
        _foodCountRemaining = _foodCountOnLevel;
    }

    private void Start()
    {
        StartCoroutine(CreateFoodWithDelay());
    }

    private void Spawn()
    {
        if (_food.Count == 0)
            CreateFood();

        var food = _food.Dequeue();
        food.gameObject.SetActive(true);
        food.transform.position = CalculateSpawnPoint();
        food.SetCount(CalculateFoodCount());
    }

    private void CreateFood()
    {
        var food = Instantiate(_foodPrefab);
        food.gameObject.SetActive(false);
        food.SetFoodCreator(this);
        _food.Enqueue(food);
    }

    public void ReturnToPool(Food food)
    {
        food.gameObject.SetActive(false);
        _food.Enqueue(food);
    }

    private Vector2 CalculateSpawnPoint()
    {
        return new Vector2(Random.Range(_minSpawnRange.x, _maxSpawnRange.x), Random.Range(_minSpawnRange.y, _maxSpawnRange.y));
    }

    private int CalculateFoodCount()
    {
        int food = 0;

        if(_foodCountRemaining <= _foodCountOnLevel / 2)
            food = Random.Range(_foodValueRemaining / 2, _foodValueRemaining);
        else
            food = Random.Range(_foodValueRemaining/8, _foodValueRemaining / 4);

        if (_foodCountRemaining == 1)
            food = _foodValueRemaining;

        _foodValueRemaining -= food;
        _foodCountRemaining--;

        return food;
    }

    private IEnumerator CreateFoodWithDelay()
    {
        while(_foodCountRemaining > 0)
        {
            yield return new WaitForSeconds(_spawnDelay);
            Spawn();
        }
    }
}