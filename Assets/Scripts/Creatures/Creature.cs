using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Creature : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float MaxSpeed = 10f;
    [SerializeField] protected float Acceleration = 52f;
    [SerializeField] protected float Decceleration = 52f;
    [SerializeField] protected float TurnSpeed = 80f;

    [Space]
    [SerializeField] private GameObject _spritePlacement;
    [SerializeField] private int _startLevel;
    [SerializeField] private float _spriteSize = 0.16f;

    protected Vector2 InputVector;
    private Vector2 _desiredVelocity;
    private Vector2 _velocity;

    protected int CurrentLevel;

    private float _speedLimiter = 0.7f;
    private float _maxSpeedChange;

    private bool _isMoving;
    protected bool NormalSprite = true;

    protected Rigidbody2D Rigidbody;

    public int Level => CurrentLevel;

    public UnityAction FishAted;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        CurrentLevel = _startLevel;
        UpdateScale();
    }

    protected virtual void Update()
    {
        ApplySpeedLimiter();

        _desiredVelocity = new Vector2(InputVector.x, InputVector.y) * Mathf.Max(MaxSpeed, 0f);
        PressingKeyCheck();
    }

    protected virtual void FixedUpdate()
    {
        _velocity = Rigidbody.velocity;
        Move();
    }

    protected void UpdateScale()
    {
        float value = 1 + Level * _spriteSize / 4;

        transform.localScale = new Vector3(value, value);
    }

    public virtual void Die()
    {
    }

    public void AddLevel(int value)
    {
        CurrentLevel += value;
        UpdateScale();
    }

    public void TryToEatOtherCreature(Creature creature)
    {
        Creature winingCreature = Level > creature.Level ? this : creature;
        Creature losingCreature = winingCreature == this ? creature : this;

        winingCreature.AddLevel(losingCreature.Level);
        winingCreature.FishAted?.Invoke();
        losingCreature.Die();
    }

    protected void ChangeSpriteDirection()
    {
        if (InputVector.x != 0)
        {
            if (InputVector.x > 0)
            {
                _spritePlacement.transform.localScale = Vector3.one;
                NormalSprite = true;
            }
            else
            {
                _spritePlacement.transform.localScale = new Vector3(-1, 1, 1);
                NormalSprite = false;
            }
        }
    }

    private void Move()
    {
        if (_isMoving)
        {
            if (Mathf.Sign(InputVector.x) != Mathf.Sign(_velocity.x) || Mathf.Sign(InputVector.y) != Mathf.Sign(_velocity.y))
                _maxSpeedChange = TurnSpeed * Time.deltaTime;
            else
                _maxSpeedChange = Acceleration * Time.deltaTime;
        }
        else
            _maxSpeedChange = Decceleration * Time.deltaTime;

        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);
        _velocity.y = Mathf.MoveTowards(_velocity.y, _desiredVelocity.y, _maxSpeedChange);

        Rigidbody.velocity = _velocity;
        ChangeSpriteDirection();
    }

    private void PressingKeyCheck()
    {
        if (InputVector.x != 0 || InputVector.y != 0)
            _isMoving = true;
        else
            _isMoving = false;
    }

    private void ApplySpeedLimiter()
    {
        if (IsDiagonalMovement())
        {
            InputVector.x *= _speedLimiter;
            InputVector.y *= _speedLimiter;
        }
    }

    private bool IsDiagonalMovement()
    {
        return InputVector.x != 0 && InputVector.y != 0;
    }
}