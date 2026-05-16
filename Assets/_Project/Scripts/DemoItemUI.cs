using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DemoItemUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text tagsText;

    [Header("Data")]
    [SerializeField] private DemoData data; // <-- ссылка на DemoData (заполни в инспекторе)

    [Header("Optional visuals")]
    [SerializeField] private Image background; // можно перетащить Image самого айтема
    [SerializeField] private Color normalColor = new Color(1, 1, 1, 0);      // прозрачный
    [SerializeField] private Color selectedColor = new Color(1, 1, 1, 0.12f); // лёгкая подсветка

    private Button button;
    private Action onClick;

    public DemoData Data => data;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (background == null) background = GetComponent<Image>();

        if (button != null)
        {
            button.onClick.RemoveListener(HandleClick);
            button.onClick.AddListener(HandleClick);
        }

        // если data назначена — сразу обновим тексты
        RefreshTexts();

        SetSelected(false);
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

    private void HandleClick()
    {
        onClick?.Invoke();
    }

    public void SetSelected(bool isSelected)
    {
        if (background == null) return;
        background.color = isSelected ? selectedColor : normalColor;
    }
}
