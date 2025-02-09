using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Player _player;
    [SerializeField] private float _movementThreshold = 1f;

    private Camera _camera;

    private Vector2 _movementDirection;
    private Vector3 _targetPosition;

    private bool _isMoving;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!_isMoving)
            return;

        _player.SetInput(_movementDirection);

        if (Vector2.Distance(_player.transform.position, _targetPosition) > _movementThreshold)
            return;

        StopMovement();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateInputDirection(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopMovement();
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateInputDirection(eventData);
    }

    private void UpdateInputDirection(PointerEventData eventData)
    {
        _targetPosition = _camera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, _camera.nearClipPlane));
        _targetPosition.z = 0f;

        _movementDirection = ((Vector2)_targetPosition - (Vector2)_player.transform.position).normalized;
        _isMoving = true;
    }

    private void StopMovement()
    {
        _isMoving = false;
        _player.SetInput(Vector2.zero);
    }
}