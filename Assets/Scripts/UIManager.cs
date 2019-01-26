using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    public Canvas WorldCanvas;
    public Canvas UICanvas;

    public RectTransform interactionPrefab;
    public Text interactionText;

    private RectTransform currentInteractionPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void SetInteraction(PickUpObject child)
    {
        if (child == null)
        {
            Destroy(currentInteractionPrefab.gameObject);
            //Set Text empty.
        }
        else
        {
            if (currentInteractionPrefab == null)
                currentInteractionPrefab = Instantiate(interactionPrefab, WorldCanvas.transform);
            currentInteractionPrefab.position = child.transform.position + Vector3.up;
            //Set Text to value.
        }
    }
}
