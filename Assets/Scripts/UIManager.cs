using System.Collections;
using System.Collections.Generic;
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

    public Animator endAnimator;
    public Button endRetryButton;
    public Button endQuitButton;

    public Text entValueText;
    public Text confValueText;
    public Text bhValueText;
    public Text allItemText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
            endQuitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });

            endRetryButton.onClick.AddListener(() =>
            {
                GameManager.Instance.StartGame();
                endAnimator.SetBool("hasEnded", false);
            });

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
        if (time >= 0)
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
    }
    public void SetSteps(int steps)
    {
        stepText.text = steps.ToString("0");
    }
    public void SetEnd(bool won, List<PickUpObject> pickUpObjects)
    {
        allItemText.text = "You picked up:\n";
        int ent = 0;
        int conf = 0;
        int bh = 0;
        if (pickUpObjects.Count == 0)
            allItemText.text += "NOTHING?!";
        foreach(PickUpObject puo in pickUpObjects)
        {
            allItemText.text += puo.name + "\n";
            ent += puo.entertainmentValue;
            conf += puo.confortValue;
            bh += puo.hygieneValue;
        }

        entValueText.text = ent.ToString();
        confValueText.text = conf.ToString();
        bhValueText.text = bh.ToString();

        Text text = timeUpTranform.GetComponentInChildren<Text>();
        if (won)
        {
            text.text = "Shitastrophy averted!";
            StartCoroutine(WaitForEnd(2));
        }
        else
        {
            text.text = "You have soiled your panties!";
            StartCoroutine(WaitForEnd(4));
        }
        timeUpTranform.gameObject.SetActive(true);
    }

    private IEnumerator WaitForEnd(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        endAnimator.SetBool("hasEnded", true);
        timeUpTranform.gameObject.SetActive(false);
    }
}
