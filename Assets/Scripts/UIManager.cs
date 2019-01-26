using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Canvas WorldCanvas;
    public Canvas UICanvas;

    public RectTransform interactionPrefab;
    public RectTransform interactionUIObject;
    private RectTransform currentInteractionPrefab;

    public Text timeText;
    public RectTransform timeUpTranform;
    public int fontSizeLimit;

	public Text stepText;

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

    public void SetInteraction(PickUpObject puo)
    {
        if (puo == null)
        {
            Destroy(currentInteractionPrefab.gameObject);
            //Set Text empty.
        }
        else
        {
            if (currentInteractionPrefab == null)
                currentInteractionPrefab = Instantiate(interactionPrefab, WorldCanvas.transform);
            currentInteractionPrefab.position = puo.transform.position + Vector3.up;
            //Set Text to value.
        }
    }
    public void Interact(PickUpObject puo)
    {
        interactionUIObject.gameObject.SetActive(true);
        interactionUIObject.GetComponentInChildren<Text>().text = string.Format("You picked up {0}!", puo.name);
        StopCoroutine("WaitForFade");
        StartCoroutine(WaitForFade(interactionUIObject.gameObject));
    }
    private IEnumerator WaitForFade(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }

    public void SetTime(float time)
    {
        if(time >= 0)
        {
            int previousTime = (int)(time + 0.9f);
            timeText.text = (time + 0.4f).ToString("0");

            if (time < 11 && (int)time == previousTime)
                timeText.fontSize = fontSizeLimit;

            if (time < 11 && time > 0 && timeText.fontSize > 50)
            {
                timeText.fontSize -= 1;
            }
        }
        else
            timeUpTranform.gameObject.SetActive(true);
    }

	public void SetSteps(int steps)
	{
		stepText.text = steps.ToString("0");
	}
}
