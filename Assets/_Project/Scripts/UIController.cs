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
        Application.OpenURL("https://www.linkedin.com/in/anna-franchuk-246757a3/");
    }
}
