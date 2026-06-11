using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DemoItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text tagsText;

    [Header("Data")]
    [SerializeField] private DemoData data; // <-- ссылка на DemoData (заполни в инспекторе)

    [Header("Sprites")]
    [SerializeField] private Image background; // можно перетащить Image самого айтема
    [SerializeField] private Sprite normalSprite; // спрайт для обычного состояния
    [SerializeField] private Sprite selectedSprite; // спрайт со свечением для выбранного состояния
    
    [Header("Optional colors (если не используешь спрайты)")]
    [SerializeField] private Color normalColor = new Color(1, 1, 1, 0.3f);      // полупрозрачный
    [SerializeField] private Color selectedColor = new Color(1, 1, 1, 1f); // полностью виден

    private Button button;
    private Action onClick;
    private Action onHover; // Callback при наведении
    private RectTransform backgroundRectTransform;
    private bool isHovered = false;
    private bool isSelected = false;

    public DemoData Data => data;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (background == null) background = GetComponent<Image>();
        if (background != null) backgroundRectTransform = background.GetComponent<RectTransform>();

        if (button != null)
        {
            button.onClick.RemoveListener(HandleClick);
            button.onClick.AddListener(HandleClick);
        }

        // если data назначена — сразу обновим тексты
        RefreshTexts();

        UpdateVisuals();
    }

    /// <summary>
    /// Если хочешь назначать DemoData кодом (не обязательно).
    /// </summary>
    public void Bind(DemoData demoData)
    {
        data = demoData;
        RefreshTexts();
    }

    private void RefreshTexts()
    {
        if (data == null) return;

        if (nameText != null)
            nameText.text = data.title; // предполагаю поле title

        if (tagsText != null)
            tagsText.text = BuildTagsString(data.tags); // предполагаю tags = List<string> или string[]
    }

    private string BuildTagsString(string[] tags)
{
    if (tags == null || tags.Length == 0) return "";
    return string.Join(" • ", tags);
}

    public void SetClickHandler(Action handler)
    {
        onClick = handler;
    }
    
    public void SetHoverHandler(Action handler)
    {
        onHover = handler;
    }

    private void HandleClick()
    {
        onClick?.Invoke();
    }
    
    // Обработка наведения курсора
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        UpdateVisuals();
        onHover?.Invoke(); // Вызываем callback при наведении
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        UpdateVisuals();
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        UpdateVisuals();
    }
    
    private void UpdateVisuals()
    {
        if (background == null) return;
        
        // Показываем спрайт со свечением при наведении ИЛИ если элемент выбран
        bool shouldHighlight = isHovered || isSelected;
        
        // Если назначены спрайты - меняем спрайт
        if (normalSprite != null && selectedSprite != null)
        {
            background.sprite = shouldHighlight ? selectedSprite : normalSprite;
            background.color = Color.white; // Полная непрозрачность
        }
        // Иначе меняем только цвет
        else
        {
            background.color = shouldHighlight ? selectedColor : normalColor;
        }
    }
}
