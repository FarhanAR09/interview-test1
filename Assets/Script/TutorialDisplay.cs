using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(9999)]
public class TutorialDisplay : MonoBehaviour
{
    [SerializeField]
    private List<Tutorial> tutorials = new();

    [SerializeField]
    private TMP_Text titleDisplay, descriptionDisplay;
    [SerializeField]
    private Button previousButton, nextButton, closeButton;

    private int _index = 0;
    private int Index
    {
        get => Mathf.Clamp(_index, 0, tutorials.Count - 1);
        set => _index = Mathf.Clamp(value, 0, tutorials.Count - 1);
    }

    private void OnEnable()
    {
        if (previousButton != null)
        {
            previousButton.onClick.AddListener(Previous);
        }
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(Next);
        }
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(Close);
        }

        if (PlayerEventManager.Instance != null)
        {
            PlayerEventManager.Instance.SetInteruption("TutorialDisplay", new PlayerInteruption { enableMovement = false });
        }
    }

    private void OnDisable()
    {
        if (previousButton != null)
        {
            previousButton.onClick.RemoveListener(Previous);
        }
        if (nextButton != null)
        {
            nextButton.onClick.RemoveListener(Next);
        }
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(Close);
        }

        if (PlayerEventManager.Instance != null)
        {
            PlayerEventManager.Instance.SetInteruption("TutorialDisplay", new PlayerInteruption { enableMovement = true });
        }
    }

    private void Start()
    {
        MoveToIndex(Index);
    }

    private void Next()
    {
        MoveToIndex(++Index);
    }

    private void Previous()
    {
        MoveToIndex(--Index);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    private void MoveToIndex(int index)
    {
        int count = tutorials.Count;
        if (count > 0 && index < count && index >= 0)
        {
            Tutorial tutorial = tutorials[index];
            titleDisplay.SetText(tutorial.title);
            descriptionDisplay.SetText(tutorial.description);
        }
        else
        {
            titleDisplay.SetText("Tutorial");
            descriptionDisplay.SetText("-");
        }

        if (nextButton!= null && previousButton != null)
        {
            nextButton.gameObject.SetActive(true);
            nextButton.interactable = true;
            previousButton.gameObject.SetActive(true);
            previousButton.interactable = true;
            closeButton.gameObject.SetActive(false);
            if (Index <= 0)
            {
                previousButton.interactable = false;
            }
            else if (Index >= count - 1)
            {
                nextButton.interactable = false;
                nextButton.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(true);
            }
        }
    }
}
