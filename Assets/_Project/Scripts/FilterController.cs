using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        
        // Force layout rebuild in next frame
        StartCoroutine(RebuildLayoutNextFrame());

        Debug.Log($"Filter applied: {currentFilter}");
    }
    
    private IEnumerator RebuildLayoutNextFrame()
    {
        // Wait for end of frame to ensure all SetActive calls are processed
        yield return new WaitForEndOfFrame();
        
        if (demoListController == null) yield break;
        
        // Get Content RectTransform
        var demoItems = demoListController.GetComponentsInChildren<DemoItemUI>(true);
        if (demoItems.Length == 0) yield break;
        
        Transform contentTransform = demoItems[0].transform.parent;
        if (contentTransform == null) yield break;
        
        // Force rebuild all parent layouts
        RectTransform current = contentTransform.GetComponent<RectTransform>();
        while (current != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(current);
            current = current.parent as RectTransform;
            
            // Stop at Canvas
            if (current != null && current.GetComponent<Canvas>() != null)
                break;
        }
    }

    /// <summary>
    /// Reset filter to show all effects
    /// </summary>
    public void ResetFilter()
    {
        OnFilterChanged("All");
    }
}
