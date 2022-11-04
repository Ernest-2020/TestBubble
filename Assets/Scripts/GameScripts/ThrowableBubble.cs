using System;
using UnityEngine;

public class ThrowableBubble : BaseThrowableBubble
{
    public event Action StopedBubBle;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float minDirection;
    
    private Camera _camera;
    private Rigidbody2D _bubbleRigidbody;
    private CircleCollider2D _circleCollider2D;
    
    private Transform _bubbleTransform;
    private  Vector3 _endPosition;
    private bool _isTookAim;
    private bool _isClickOrTouch;
    public bool IsStopMove { get; private set; }


    private void Awake()
    {
        _camera = Camera.main;
        _bubbleRigidbody = GetComponent<Rigidbody2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _bubbleTransform = transform;
        

    }

    private void OnEnable()
    {
            SetColor(gameObject);
        
        _isTookAim = false;
        IsStopMove = false;
        _isClickOrTouch = false;
        _bubbleRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        _circleCollider2D.enabled = true;
    }

    private void OnDisable()
    {
        _bubbleRigidbody.simulated = false;
    }
    
    public override void Rotation(Vector3 direction)
    {
        if (direction == default)
        {
            direction = _endPosition;
        }
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _bubbleTransform.rotation = Quaternion.AngleAxis(angle,_bubbleTransform.forward);
    }

    public override void CheckCanShot(bool isClickOrTouch,bool isPointerOverGameObject)
    {
        if (isClickOrTouch&&!isPointerOverGameObject)
        {
            _isClickOrTouch = true;
        }
        
    }

    public override void Move()
    {
        if (_isTookAim&&!IsStopMove)
        {
            _bubbleRigidbody.velocity = _bubbleTransform.right*movementSpeed*Time.fixedDeltaTime;
        }
    }
    
    public override void Aim(Aim aim,Vector3 screenPosition,bool isMouseButtonDown,bool isPointerOverGameObject)
    {
        if (isPointerOverGameObject) return;
        if (isMouseButtonDown&&!IsStopMove)
        {
            if (_isTookAim) return;
            _bubbleRigidbody.simulated = false;
            var direction = screenPosition - _camera.WorldToScreenPoint(_bubbleTransform.position);
            if (direction.y <= minDirection)
            {
                aim.DisableAim();
                return;
            }

            Rotation(direction);
            aim.DrawLine(_bubbleTransform.position,_bubbleTransform.right);
        }

    }

    public override void Shot(Aim aim,bool isMouseButtonUp,bool isPointerOverGameObject)
    {
        if (!isMouseButtonUp||IsStopMove||isPointerOverGameObject||!_isClickOrTouch) return;
        aim.DisableAim();
        _isClickOrTouch = false;
        _isTookAim = true;
        _bubbleRigidbody.simulated = true;
        _bubbleRigidbody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void StopMove()
    {
        IsStopMove = true;
        _bubbleRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        StopedBubBle?.Invoke();
    }
    
}
