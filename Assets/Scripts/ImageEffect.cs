using UnityEngine;

[ExecuteInEditMode]
public class ImageEffect : MonoBehaviour
{
    public Material processingImageMaterial;
     public bool triggerRandomness = true;

    void Update()
    {
        if (triggerRandomness)
            processingImageMaterial.SetFloat("_RandomNumber", Random.Range(0.0f, 1.0f));

    }
    void OnRenderImage(RenderTexture imageFromRenderedImage, RenderTexture imageDisplayedOnScreen)
    {
        if (processingImageMaterial != null)
            Graphics.Blit(imageFromRenderedImage, imageDisplayedOnScreen, processingImageMaterial);
    }
}