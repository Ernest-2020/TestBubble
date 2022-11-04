using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public abstract class BaseThrowableBubble : Bubble
{
    public abstract void Move();
    public abstract void StopMove();
    public abstract void Rotation(Vector3 direction);
    public abstract void CheckCanShot(bool isClickOrTouch,bool isPointerOverGameObject);
    public abstract void Aim(Aim aim, Vector3 screenPosition, bool isMouseButtonDown, bool isPointerOverGameObject);
    public abstract void Shot(Aim aim, bool isMouseButtonUp,bool isPointerOverGameObject);
}
