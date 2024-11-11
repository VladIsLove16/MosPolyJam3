using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenDistortionEffect : MonoBehaviour
{
    public Material distortionMaterial;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (distortionMaterial != null)
        {
            Graphics.Blit(src, dest, distortionMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
