using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GlitchEffect : MonoBehaviour
{
    public Material glitchMaterial;  // �������� � ��������
    public float glitchAmount = 0.2f; // ����������� ��������� ����� ��������
    public float glitchDuration = 0.5f; // ������������ ������ (����� "�����")
    public float timeBetweenGlitches = 2f; // ����� ����� "�������" ������

    private float glitchTimer = 0f;
    private float glitchCooldown = 0f;

    private void Start()
    {
        if (glitchMaterial != null)
        {
            glitchMaterial.SetFloat("_GlitchAmount", 0f); // ���������� ���� ��������
        }
    }

    private void Update()
    {
        glitchCooldown -= Time.deltaTime;
        if (glitchCooldown <= 0f)
        {
            // ��������� ����� "����"
            glitchCooldown = timeBetweenGlitches;
            glitchTimer = glitchDuration;

            // ���������� ����
            glitchMaterial.SetFloat("_GlitchAmount", glitchAmount);
        }

        // ��������� ������������� ������� �����
        if (glitchTimer > 0f)
        {
            glitchTimer -= Time.deltaTime;
        }
        else
        {
            // ����� ���� �������������, ����������
            glitchMaterial.SetFloat("_GlitchAmount", 0f);
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (glitchMaterial != null)
        {
            Graphics.Blit(source, destination, glitchMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
