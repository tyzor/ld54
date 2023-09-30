using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
    private Material objectMaterial;
    private Color originalEmissionColor;
    public Color highlightEmissionColor = new Color(1f, 1f, 1f);

    void Start()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        if (objectRenderer)
        {
            objectMaterial = objectRenderer.material;
            originalEmissionColor = objectMaterial.GetColor("_EmissionColor");
        }
    }

    public void Highlight()
    {
        if (objectMaterial)
        {
            objectMaterial.SetColor("_EmissionColor", highlightEmissionColor);
        }
    }

    public void RemoveHighlight()
    {
        if (objectMaterial)
        {
            objectMaterial.SetColor("_EmissionColor", originalEmissionColor);
        }
    }
}
