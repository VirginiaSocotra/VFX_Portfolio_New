using UnityEngine;

/// <summary>
/// Управляет запуском эффектов (Particle System и VFX Graph) по кнопке
/// </summary>
public class VFXEffectController : MonoBehaviour
{
    [System.Serializable]
    public class EffectData
    {
        [Tooltip("Название эффекта (для UI)")]
        public string effectName;
        
        [Tooltip("GameObject с Particle System или Visual Effect")]
        public GameObject effectObject;
        
        [Tooltip("Автозапуск при старте сцены")]
        public bool playOnStart = true;
        
        [Tooltip("Автоматически повторять эффект")]
        public bool autoLoop = false;
        
        [Tooltip("Задержка перед повтором (секунды)")]
        public float loopDelay = 3f;
        
        [HideInInspector] public float nextPlayTime;
    }
    
    [Header("Effects")]
    [SerializeField] private EffectData[] effects;
    
    void Start()
    {
        // Автозапуск эффектов при старте
        foreach (var effect in effects)
        {
            if (effect.effectObject != null && effect.playOnStart)
            {
                PlayEffectInternal(effect.effectObject);
                
                // Установить время следующего повтора
                if (effect.autoLoop)
                {
                    float duration = GetEffectDuration(effect.effectObject);
                    effect.nextPlayTime = Time.time + duration + effect.loopDelay;
                }
            }
        }
    }
    
    void Update()
    {
        // Проверить автоповтор для каждого эффекта
        foreach (var effect in effects)
        {
            if (effect.autoLoop && effect.effectObject != null)
            {
                if (Time.time >= effect.nextPlayTime)
                {
                    PlayEffectInternal(effect.effectObject);
                    
                    // Установить следующее время повтора
                    float duration = GetEffectDuration(effect.effectObject);
                    effect.nextPlayTime = Time.time + duration + effect.loopDelay;
                }
            }
        }
    }
    
    /// <summary>
    /// Запустить эффект по индексу
    /// </summary>
    public void PlayEffect(int index)
    {
        if (index < 0 || index >= effects.Length)
        {
            Debug.LogWarning($"[VFXEffectController] Invalid effect index: {index}");
            return;
        }
        
        var effect = effects[index];
        
        if (effect.effectObject == null)
        {
            Debug.LogWarning($"[VFXEffectController] Effect object is null for: {effect.effectName}");
            return;
        }
        
        PlayEffectInternal(effect.effectObject);
        Debug.Log($"[VFXEffectController] Playing effect: {effect.effectName}");
    }
    
    /// <summary>
    /// Получить длительность эффекта
    /// </summary>
    private float GetEffectDuration(GameObject effectObj)
    {
        float maxDuration = 0f;
        
        // Проверить Particle Systems
        ParticleSystem[] particleSystems = effectObj.GetComponentsInChildren<ParticleSystem>();
        foreach (var ps in particleSystems)
        {
            float psDuration = ps.main.duration + ps.main.startLifetime.constantMax;
            if (psDuration > maxDuration)
                maxDuration = psDuration;
        }
        
        // Если не нашли длительность, используем дефолт 2 секунды
        if (maxDuration == 0f)
            maxDuration = 2f;
        
        return maxDuration;
    }
    
    /// <summary>
    /// Внутренний метод для запуска эффекта (работает с ParticleSystem и VisualEffect)
    /// </summary>
    private void PlayEffectInternal(GameObject effectObj)
    {
        // Попробовать найти Particle Systems
        ParticleSystem[] particleSystems = effectObj.GetComponentsInChildren<ParticleSystem>();
        if (particleSystems.Length > 0)
        {
            foreach (var ps in particleSystems)
            {
                ps.Stop();
                ps.Clear();
                ps.Play();
            }
            return;
        }
        
        // Попробовать найти VFX Graph
#if UNITY_VFX_GRAPH
        UnityEngine.VFX.VisualEffect vfx = effectObj.GetComponentInChildren<UnityEngine.VFX.VisualEffect>();
        if (vfx != null)
        {
            vfx.Stop();
            vfx.Play();
            return;
        }
#endif
        
        Debug.LogWarning($"[VFXEffectController] No Particle System or Visual Effect found in: {effectObj.name}");
    }
    
    /// <summary>
    /// Запустить эффект по имени
    /// </summary>
    public void PlayEffect(string effectName)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i].effectName == effectName)
            {
                PlayEffect(i);
                return;
            }
        }
        
        Debug.LogWarning($"[VFXEffectController] Effect not found: {effectName}");
    }
    
    /// <summary>
    /// Остановить все эффекты
    /// </summary>
    public void StopAllEffects()
    {
        foreach (var effect in effects)
        {
            if (effect.effectObject != null)
            {
                StopEffectInternal(effect.effectObject);
            }
        }
    }
    
    /// <summary>
    /// Остановить эффект
    /// </summary>
    private void StopEffectInternal(GameObject effectObj)
    {
        ParticleSystem[] particleSystems = effectObj.GetComponentsInChildren<ParticleSystem>();
        foreach (var ps in particleSystems)
        {
            ps.Stop();
        }
        
#if UNITY_VFX_GRAPH
        UnityEngine.VFX.VisualEffect vfx = effectObj.GetComponentInChildren<UnityEngine.VFX.VisualEffect>();
        if (vfx != null)
        {
            vfx.Stop();
        }
#endif
    }
    
    /// <summary>
    /// Запустить все эффекты
    /// </summary>
    public void PlayAllEffects()
    {
        foreach (var effect in effects)
        {
            if (effect.effectObject != null)
            {
                PlayEffectInternal(effect.effectObject);
            }
        }
    }
}
