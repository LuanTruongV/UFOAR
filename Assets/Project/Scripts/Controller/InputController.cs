using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private MobileInput _input;

    private InputAction _directionInputAction;
    private InputAction _shelveInputAction;
    private Vector2 _direction;
    public Vector2 Direction => _direction;
    private Vector2 _shelve;
    public Vector2 Shelve => _shelve;

    private float _stickyCollective;
    public float StickyCollective=>_stickyCollective;
    
    private void Awake()
    {
        _input = new MobileInput();
    }

    private void OnEnable()
    {
        EnableInput();
    }

    private void EnableInput()
    {
        _directionInputAction = _input.Game.Direction;
        _shelveInputAction = _input.Game.Shelve;
        _directionInputAction.Enable();
        _shelveInputAction.Enable();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    private void DisableInput()
    {
        _directionInputAction.Disable();
        _shelveInputAction.Disable();
    }

    private void Update()
    {
        HandleInput();
        ClampInput();
        HandleStickyCollective();
    }

    

    private void HandleInput()
    {
        _direction = _directionInputAction.ReadValue<Vector2>();
        _shelve = _shelveInputAction.ReadValue<Vector2>();
    }
    private void ClampInput()
    {
        _direction = Vector2.ClampMagnitude(_direction, 1f);
        _shelve = Vector2.ClampMagnitude(_shelve, 1f);
    }
    private void HandleStickyCollective()
    {
        _stickyCollective += _shelve.y * Time.deltaTime;
        _stickyCollective = Mathf.Clamp01(_stickyCollective);
    }
}
