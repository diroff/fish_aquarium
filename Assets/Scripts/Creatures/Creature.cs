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
    [SerializeField] private Animator _animator;

    [SerializeField] private int _startLevel;
    [SerializeField] private float _spriteSize = 0.16f;

    protected Vector2 InputVector;
    private Vector2 _desiredVelocity;
    private Vector2 _velocity;

    private int _currentExperience;

    protected int CurrentLevel;
    protected int StartLevel => _startLevel;

    private float _speedLimiter = 0.7f;
    private float _maxSpeedChange;
    private float _baseScale;

    private float _currentMaxSpeed;

    private bool _isMoving;
    protected bool NormalSprite = true;

    protected bool IsImmortal = false;

    private bool _isFrozen = false;

    protected Rigidbody2D Rigidbody;

    public int Level => CurrentLevel;
    public GameObject SpritePlacement => _spritePlacement;

    public UnityAction FishAted;
    public UnityAction<int, int> LevelChanged;
    public UnityAction<Creature> Died;
    public UnityAction<Vector2> WasAtedOnPosition;
    public UnityAction<int, int> ExperienceWasChanged;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _baseScale = transform.localScale.x;
    }

    protected virtual void Start()
    {
        _currentMaxSpeed = MaxSpeed;
    }

    protected virtual void Update()
    {
        ApplySpeedLimiter();

        _desiredVelocity = new Vector2(InputVector.x, InputVector.y) * Mathf.Max(_currentMaxSpeed, 0f);
        PressingKeyCheck();
    }

    protected virtual void FixedUpdate()
    {
        _velocity = Rigidbody.velocity;
        Move();
    }

    protected void UpdateScale()
    {
        float value = _baseScale + Level * _spriteSize / 4;

        transform.localScale = new Vector3(value, value);
    }

    public virtual void Die()
    {
        Died?.Invoke(this);
    }

    public void AddLevel(int value)
    {
        CurrentLevel += value;
        UpdateExperience();

        UpdateScale();

        LevelChanged?.Invoke(CurrentLevel - 1, CurrentLevel);
    }

    protected void SetLevel(int value)
    {
        CurrentLevel = value;
        UpdateExperience();

        UpdateScale();

        LevelChanged?.Invoke(CurrentLevel - 1, CurrentLevel);
    }

    private void UpdateExperience()
    {
        _currentExperience = (CurrentLevel * (CurrentLevel + 1)) / 2;
    }

    public void UpdateLevel()
    {
        int level = 0;
        int requiredExperience = 0;
        int experienceForPreviousLevels = 0;

        while (requiredExperience <= _currentExperience)
        {
            level++;
            experienceForPreviousLevels = requiredExperience;
            requiredExperience += level;
        }

        CurrentLevel = level;

        UpdateScale();

        LevelChanged?.Invoke(CurrentLevel - 1, CurrentLevel);

        int currentExperienceForLevel = _currentExperience - experienceForPreviousLevels;
        int experienceNeededForNextLevel = requiredExperience - experienceForPreviousLevels;

        ExperienceWasChanged?.Invoke(currentExperienceForLevel, experienceNeededForNextLevel);
    }

    private void AddExperience(int value)
    {
        _currentExperience += value;
        UpdateLevel();
    }

    public void EatOtherCreature(Creature winingCreature, Creature losingCreature)
    {
        int previousLevel = winingCreature.CurrentLevel;

        winingCreature.AddExperience(losingCreature.Level);
        winingCreature.FishAted?.Invoke();

        losingCreature.WasAtedOnPosition?.Invoke(transform.position);
        losingCreature.Die();
    }

    public void TryToEatOtherCreature(Creature creature)
    {
        Creature winingCreature = Level > creature.Level ? this : creature;
        Creature losingCreature = winingCreature == this ? creature : this;

        if (winingCreature.IsImmortal || losingCreature.IsImmortal)
            return;

        EatOtherCreature(winingCreature, losingCreature);
    }

    protected virtual void EnableImmortality()
    {
        IsImmortal = true;
    }

    protected virtual void DisableImmortality()
    {
        IsImmortal = false;
    }

    protected void ChangeSpriteDirection()
    {
        if (InputVector.x != 0)
        {
            _animator.SetBool("Moving", true);

            if (InputVector.x > 0)
            {
                _spritePlacement.transform.localScale = new Vector3(-1, 1, 1);
                NormalSprite = true;
            }
            else
            {
                _spritePlacement.transform.localScale = Vector3.one;
                NormalSprite = false;
            }
        }
        else
            _animator.SetBool("Moving", false);
    }

    private void Move()
    {
        if (_isFrozen)
            return;

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

    public void FreezeMoving()
    {
        _isFrozen = true;
        StopMove();
    }

    public void UnfreezeMoving()
    {
        _isFrozen = false;
    }

    private void StopMove()
    {
        Rigidbody.velocity = Vector2.zero;
        _desiredVelocity = Vector2.zero;
        _animator.SetBool("Moving", false);
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

    public void AddMaxSpeed(float value)
    {
        _currentMaxSpeed += value;
    }

    private bool IsDiagonalMovement()
    {
        return InputVector.x != 0 && InputVector.y != 0;
    }
}