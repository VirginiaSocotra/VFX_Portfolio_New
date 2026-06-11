using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("About Panel")]
    [SerializeField] private GameObject aboutPanel;

    [Header("Filter Panel")]
    [SerializeField] private GameObject filterPanel;

    // ✅ Показать панель About
    public void ShowAbout()
    {
        Debug.Log("ShowAbout called!"); // Тест
        if (aboutPanel != null)
        {
            bool isActive = aboutPanel.activeSelf;
            aboutPanel.SetActive(!isActive);
            
            // Закрыть Filter если открыт
            if (filterPanel != null && filterPanel.activeSelf && !isActive)
                filterPanel.SetActive(false);
        }
    }

    // ✅ Показать панель Filter
    public void ShowFilter()
    {
        if (filterPanel != null)
        {
            bool isActive = filterPanel.activeSelf;
            filterPanel.SetActive(!isActive);
            
            // Закрыть About если открыт
            if (aboutPanel != null && aboutPanel.activeSelf && !isActive)
                aboutPanel.SetActive(false);
        }
    }

    // ✅ Выйти из приложения
    public void ExitApp()
    {
        Debug.Log("Exit Application");
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // ✅ Открыть LinkedIn профиль
    public void OpenLinkedIn()
    {
        Debug.Log("=== LINKEDIN BUTTON CLICKED ===");
        string url = "https://www.linkedin.com/in/anna-franchuk-246757a3/";
        Debug.Log($"Opening URL: {url}");
        Application.OpenURL(url);
    }
}
