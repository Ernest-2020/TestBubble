using System.Collections.Generic;
using UnityEngine;

public class BubbleView : MonoBehaviour
{
    [SerializeField] private ThrowableBubble throwableBubble;
    [SerializeField] private float rayDistance;
    [SerializeField] private  Vector2[] checkDirections;
    
    private CheckBubble _checkBubble;
    private Transform _transformBubble;
    private int _layerMask;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<Area>())
        {
            CollisionArea(other);
        }

        if (other.collider.GetComponent<Bubble>()&&!throwableBubble.IsStopMove)
        {
            CollisionBubble();
        }
    }
    
    private void OnEnable()
    {
        var bubbleGameObject = gameObject;
        _transformBubble = transform;
        _layerMask = bubbleGameObject.layer;
        _checkBubble = new CheckBubble();
    }

    private void CollisionArea(Collision2D collision2D)
    {
        var direction = transform.right.normalized;
        direction = Vector3.Reflect(direction, collision2D.transform.position.normalized);
        throwableBubble.Rotation(direction);
    }

    private void CollisionBubble()
    {
        throwableBubble.StopMove();
        _checkBubble.FindMatch(gameObject, _transformBubble.position,checkDirections,rayDistance,_layerMask);
    }
}
