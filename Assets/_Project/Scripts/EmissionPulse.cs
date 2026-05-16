using UnityEngine;

/// <summary>
/// Пульсирующее свечение для материала
/// </summary>
public class EmissionPulse : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Материал с Emission")]
    [SerializeField] private Material emissionMaterial;
    
    [Tooltip("Минимальная интенсивность")]
    [SerializeField] private float minIntensity = 1.0f;
    
    [Tooltip("Максимальная интенсивность")]
    [SerializeField] private float maxIntensity = 3.0f;
    
    [Tooltip("Скорость пульсации")]
    [SerializeField] private float pulseSpeed = 2.0f;
    
    [Header("Color")]
    [Tooltip("Базовый цвет Emission")]
    [SerializeField] private Color emissionColor = new Color(0.3f, 0.8f, 2.5f);

    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private Material materialInstance;

    private void Start()
    {
        // Создать instance материала
        if (emissionMaterial != null)
        {
            var renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                materialInstance = new Material(emissionMaterial);
                renderer.material = materialInstance;
            }
        }
    }

    private void Update()
    {
        if (materialInstance == null) return;

        // Вычислить интенсивность (синусоида)
        float t = Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f;
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);

        // Установить Emission color
        Color finalColor = emissionColor * intensity;
        materialInstance.SetColor(EmissionColor, finalColor);
    }

    private void OnDestroy()
    {
        // Очистить instance
        if (materialInstance != null)
        {
            Destroy(materialInstance);
        }
    }
}
