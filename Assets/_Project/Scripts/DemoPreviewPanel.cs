using TMPro;
using UnityEngine;

public class DemoPreviewPanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    [Header("Tags")]
    [SerializeField] private Transform tagsWrap;
    [SerializeField] private TMP_Text tagPrefab; // это твой TMP_Tag prefab

    public void Show(DemoData data)
    {
        if (data == null) return;

        if (titleText) titleText.text = data.title;
        if (descriptionText) descriptionText.text = data.description;

        RebuildTags(data.tags);
    }

    private void RebuildTags(string[] tags)
    {
        if (!tagsWrap || !tagPrefab) return;

        // очистка старых тегов
        for (int i = tagsWrap.childCount - 1; i >= 0; i--)
            Destroy(tagsWrap.GetChild(i).gameObject);

        if (tags == null) return;

        // создание новых
        foreach (var t in tags)
        {
            if (string.IsNullOrWhiteSpace(t)) continue;

            TMP_Text tag = Instantiate(tagPrefab, tagsWrap);
            tag.text = t.Trim();
        }
    }
}
