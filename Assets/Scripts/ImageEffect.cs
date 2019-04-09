using UnityEngine;
 
[ExecuteInEditMode]
public class ImageEffect : MonoBehaviour
{
    public Material processingImageMaterial;
 
    void OnRenderImage(RenderTexture imageFromRenderedImage, RenderTexture imageDisplayedOnScreen)
    {
        if (processingImageMaterial != null)
            Graphics.Blit(imageFromRenderedImage, imageDisplayedOnScreen, processingImageMaterial);
    }
}