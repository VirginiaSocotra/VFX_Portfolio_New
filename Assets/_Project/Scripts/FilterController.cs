using UnityEngine;
using UnityEngine.UI;

public class FilterController : MonoBehaviour
{
    [Header("Demo List Reference")]
    [SerializeField] private DemoListController demoListController;

    [Header("Filter Categories")]
    private string currentFilter = "All";

    /// <summary>
    /// Called when a filter toggle is selected
    /// </summary>
    public void OnFilterChanged(string category)
    {
        currentFilter = category;
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        if (demoListController == null) return;

        // Get all demo items
        var demoItems = demoListController.GetComponentsInChildren<DemoItemUI>(true);

        foreach (var item in demoItems)
        {
            if (currentFilter == "All")
            {
                // Show all items
                item.gameObject.SetActive(true);
            }
            else
            {
                // Show only items matching the current filter category
                bool matches = item.Data != null && 
                              item.Data.category != null && 
                              item.Data.category.Equals(currentFilter, System.StringComparison.OrdinalIgnoreCase);
                
                item.gameObject.SetActive(matches);
            }
        }

        Debug.Log($"Filter applied: {currentFilter}");
    }

    /// <summary>
    /// Reset filter to show all effects
    /// </summary>
    public void ResetFilter()
    {
        OnFilterChanged("All");
    }
}
