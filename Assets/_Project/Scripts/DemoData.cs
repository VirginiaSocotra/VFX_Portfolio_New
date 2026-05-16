using UnityEngine;

[CreateAssetMenu(menuName = "VFX Portfolio/Demo Data")]
public class DemoData : ScriptableObject
{
    [Header("Main Info")]
    public string title;

    [TextArea(3, 10)]
    public string description;

    [Header("Category")]
    public string category; // e.g., "Fire & Smoke", "Magic & Energy", "Impacts", "2D Effects"

    [Header("Tags")]
    public string[] tags;

    [Header("Demo Content")]
    public GameObject demoPrefab; 
    
    [Header("Environment Settings")]
    [Tooltip("Ambient lighting color for this demo")]
    public Color ambientColor = new Color(0.2f, 0.22f, 0.3f);
    
    [Tooltip("Directional light intensity")]
    [Range(0f, 3f)]
    public float directionalLightIntensity = 1.2f;
    
    [Tooltip("Custom skybox material for this demo (optional)")]
    public Material customSkybox;
    
    [Tooltip("Enable camera rotation around the effect")]
    public bool enableCameraRotation = true;
    
    [Header("Camera Settings")]
    [Tooltip("Camera distance from effect")]
    [Range(2f, 10f)]
    public float cameraDistance = 3.5f;
    
    [Tooltip("Camera height")]
    [Range(0f, 5f)]
    public float cameraHeight = 1.5f;
    
    [Tooltip("Camera field of view")]
    [Range(30f, 80f)]
    public float cameraFOV = 50f;
}
