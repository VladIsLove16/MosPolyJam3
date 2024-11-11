using UnityEngine;

[ExecuteInEditMode]
public class RandomWhitePixelEffect : MonoBehaviour
{
    public Material effectMaterial;
    public float interval = 1.0f;
    private float lastChangeTime;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (effectMaterial != null)
        {
            if (Time.time - lastChangeTime >= interval)
            {
                effectMaterial.SetFloat("_Interval", Time.time);
                lastChangeTime = Time.time;
            }

            Graphics.Blit(src, dest, effectMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
