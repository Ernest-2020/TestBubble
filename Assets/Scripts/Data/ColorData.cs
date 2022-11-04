using UnityEngine;

[CreateAssetMenu(fileName = "NewColorSettings", menuName = "Bubble/" + nameof(ColorData), order = 0)]
public class ColorData : ScriptableObject
{
    [SerializeField] private ColorSettings[] colorSettings;

    public void GetRandomColor(SpriteRenderer spriteRenderer, GameObject gameObject)
    {
        var setting = colorSettings[Random.Range(0, colorSettings.Length)];
        spriteRenderer.color = setting.Color;
        gameObject.layer = setting.IndexLayerMask;
    }
}
