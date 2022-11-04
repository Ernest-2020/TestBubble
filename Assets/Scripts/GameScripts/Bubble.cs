using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Bubble : MonoBehaviour
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private SpriteRenderer bubbleSpriteRenderer;
    
    protected void SetColor(GameObject bubbleGameObject)
    {
        colorData.GetRandomColor(bubbleSpriteRenderer,bubbleGameObject);
    }
}
