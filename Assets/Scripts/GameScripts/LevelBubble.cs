using UnityEngine;

public class LevelBubble : Bubble
{
    [SerializeField] private bool isRandomColor;
    [SerializeField] private CircleCollider2D levelBubbleCollider;
    private void OnEnable()
    {
        levelBubbleCollider.enabled = true;
        if (!isRandomColor) return;
        SetColor(gameObject);
    }
}
