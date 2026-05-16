using System.Collections.Generic;
using UnityEngine;

public class DemoListController : MonoBehaviour
{
    [Header("List")]
    [SerializeField] private Transform contentRoot;

    [Header("Preview")]
    [SerializeField] private DemoPreviewPanel previewPanel;

    [Header("Play")]
    [SerializeField] private Transform demoSpawnPoint;
    [SerializeField] private GameObject demoEnvironment; // Пол, свет, камера для демо
    [SerializeField] private Animator cameraAnimator; // Animator для вращения камеры
    
    [Header("Cameras")]
    [SerializeField] private Camera menuCamera; // Main Camera для меню
    [SerializeField] private Camera demoCamera; // Demo Camera для эффектов
    
    [Header("Lighting")]
    [SerializeField] private Light directionalLight; // Directional Light в сцене

    [Header("Panels")]
    [SerializeField] private GameObject portfolioPanel; // Hub_Root

    private readonly List<DemoItemUI> items = new();
    private DemoItemUI selected;
    private GameObject currentInstance;
    private Camera currentDemoCamera; // Камера из текущего демо-префаба
    
    // Сохраняем дефолтные значения для восстановления
    private Color defaultAmbientColor;
    private float defaultDirectionalLightIntensity;
    private Material defaultSkybox;

    private void Awake()
    {
        Debug.Log("=== DemoListController Awake START ===");
        Debug.Log($"contentRoot: {(contentRoot != null ? contentRoot.name : "NULL")}");
        Debug.Log($"previewPanel: {(previewPanel != null ? previewPanel.name : "NULL")}");
        Debug.Log($"demoSpawnPoint: {(demoSpawnPoint != null ? demoSpawnPoint.name : "NULL")}");
        
        // Сохранить дефолтные настройки
        defaultAmbientColor = RenderSettings.ambientLight;
        defaultSkybox = RenderSettings.skybox;
        if (directionalLight != null) defaultDirectionalLightIntensity = directionalLight.intensity;
        
        items.Clear();
        contentRoot.GetComponentsInChildren(true, items);
        Debug.Log($"Found {items.Count} DemoItemUI components");
        
        items.RemoveAll(x => x.transform.parent != contentRoot);
        Debug.Log($"After filtering: {items.Count} DemoItemUI items");

        foreach (var it in items)
        {
            var local = it;
            if (local.Data != null)
            {
                Debug.Log($"  - {local.Data.title} (prefab: {(local.Data.demoPrefab != null ? local.Data.demoPrefab.name : "NULL")})");
            }
            else
            {
                Debug.LogWarning($"  - Item has NO DATA!");
            }
            local.SetClickHandler(() => Select(local));
        }

        if (items.Count > 0)
        {
            Debug.Log($"Auto-selecting first item: {items[0].Data?.title ?? "NO DATA"}");
            Select(items[0]);
        }
        else
        {
            Debug.LogError("No demo items found!");
        }
        
        Debug.Log("=== DemoListController Awake END ===");
    }

    public void Select(DemoItemUI item)
    {
        if (item == null) return;

        if (selected != null) selected.SetSelected(false);
        selected = item;
        selected.SetSelected(true);

        if (selected.Data != null)
        {
            Debug.Log($"[{gameObject.name}] Selected demo: {selected.Data.title} (Category: {selected.Data.category})");
        }
        else
        {
            Debug.LogError("Selected item has no Data!");
        }

        if (previewPanel != null && selected.Data != null)
            previewPanel.Show(selected.Data);
    }

    // ✅ ВОТ ЭТО нажимает кнопка Play
    public void PlaySelected()
    {
        Debug.Log($"[{gameObject.name}] PlaySelected() called!");
        Debug.Log($"[{gameObject.name}] Current selected: {(selected != null && selected.Data != null ? selected.Data.title : "NULL")}");
        
        if (selected == null || selected.Data == null)
        {
            Debug.LogError("PlaySelected: No demo selected or data is null!");
            return;
        }
        
        Debug.Log($"[{gameObject.name}] PlaySelected: {selected.Data.title} (Category: {selected.Data.category})");
        
        var prefab = selected.Data.demoPrefab;
        if (prefab == null)
        {
            Debug.LogError($"PlaySelected: {selected.Data.title} has no prefab assigned!");
            return;
        }
        
        if (demoSpawnPoint == null)
        {
            Debug.LogError($"[{gameObject.name}] PlaySelected: demoSpawnPoint is null!");
            return;
        }

        Debug.Log($"[{gameObject.name}] Creating prefab: {prefab.name} at position {demoSpawnPoint.position}");

        if (currentInstance != null)
            Destroy(currentInstance);

        currentInstance = Instantiate(prefab, demoSpawnPoint);
        currentInstance.transform.localPosition = Vector3.zero;
        currentInstance.transform.localRotation = Quaternion.identity;
        currentInstance.transform.localScale = Vector3.one;

        Debug.Log($"[{gameObject.name}] Instance created: {currentInstance.name}");

        // Найти камеру в префабе
        currentDemoCamera = currentInstance.GetComponentInChildren<Camera>(true);
        if (currentDemoCamera == null)
        {
            Debug.LogWarning($"[{gameObject.name}] No camera found in prefab {prefab.name}! Using fallback demo camera.");
            currentDemoCamera = demoCamera; // Fallback на общую камеру
        }
        else
        {
            Debug.Log($"[{gameObject.name}] Found camera in prefab: {currentDemoCamera.name}");
        }

        // Применить настройки окружения из DemoData
        ApplyDemoEnvironmentSettings(selected.Data);

        // Переключить на Demo View
        ShowDemoView();
    }
    
    // ✅ Показать Demo View (полноэкранный режим)
    private void ShowDemoView()
    {
        if (portfolioPanel != null) portfolioPanel.SetActive(false);
        if (demoEnvironment != null) demoEnvironment.SetActive(true); // Включить пол, освещение и UI
        
        // Камера rotation управляется через DemoData настройки
        
        // Переключить камеры
        if (menuCamera != null) menuCamera.enabled = false;
        if (currentDemoCamera != null) currentDemoCamera.enabled = true;
    }
    
    /// <summary>
    /// Применить настройки окружения из DemoData
    /// </summary>
    private void ApplyDemoEnvironmentSettings(DemoData data)
    {
        if (data == null) return;
        
        // Ambient lighting
        RenderSettings.ambientLight = data.ambientColor;
        Debug.Log($"[DemoListController] Set ambient color: {data.ambientColor}");
        
        // Custom Skybox
        if (data.customSkybox != null)
        {
            RenderSettings.skybox = data.customSkybox;
            DynamicGI.UpdateEnvironment();
            Debug.Log($"[DemoListController] Applied custom skybox: {data.customSkybox.name}");
        }
        
        // Directional light
        if (directionalLight != null)
        {
            directionalLight.intensity = data.directionalLightIntensity;
            Debug.Log($"[DemoListController] Set directional light intensity: {data.directionalLightIntensity}");
        }
        
        // Camera settings - используем прямые значения из DemoData (без адаптации)
        if (currentDemoCamera != null)
        {
            currentDemoCamera.fieldOfView = data.cameraFOV;
            
            // Установить позицию камеры относительно родителя
            Transform cameraTransform = currentDemoCamera.transform;
            Vector3 newPos = new Vector3(0, data.cameraHeight, -data.cameraDistance);
            cameraTransform.localPosition = newPos;
            
            Debug.Log($"[DemoListController] Set camera FOV: {data.cameraFOV}, Distance: {data.cameraDistance}, Height: {data.cameraHeight}");
        }
        
        // Camera rotation
        if (cameraAnimator != null)
        {
            cameraAnimator.enabled = data.enableCameraRotation;
            Debug.Log($"[DemoListController] Camera rotation: {data.enableCameraRotation}");
        }
    }
    
    /// <summary>
    /// Восстановить дефолтные настройки окружения
    /// </summary>
    private void RestoreDefaultEnvironmentSettings()
    {
        RenderSettings.ambientLight = defaultAmbientColor;
        RenderSettings.skybox = defaultSkybox;
        DynamicGI.UpdateEnvironment();
        
        if (directionalLight != null)
            directionalLight.intensity = defaultDirectionalLightIntensity;
        
        Debug.Log("[DemoListController] Restored default environment settings");
    }

    // ✅ Закрыть Demo View и вернуться к портфолио
    public void CloseDemo()
    {
        // Уничтожить текущий эффект
        if (currentInstance != null)
        {
            Destroy(currentInstance);
            currentInstance = null;
        }
        
        // Восстановить дефолтные настройки окружения
        RestoreDefaultEnvironmentSettings();

        // Переключить обратно на Portfolio
        if (portfolioPanel != null) portfolioPanel.SetActive(true);
        if (demoEnvironment != null) demoEnvironment.SetActive(false); // Выключить пол, освещение и UI
        if (cameraAnimator != null) cameraAnimator.enabled = false; // Остановить анимацию камеры
        
        // Переключить камеры обратно
        if (currentDemoCamera != null) currentDemoCamera.enabled = false;
        currentDemoCamera = null; // Очистить ссылку на камеру из префаба
        if (menuCamera != null) menuCamera.enabled = true;
    }
}
