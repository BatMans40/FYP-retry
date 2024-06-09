using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public List<MenuButton> menuButtons;
    public int index = 0;

    public GameObject mainMenuPanel;
    public GameObject newPanel; // Panel that appears when Start Game is pressed
    public GameObject optionsPanel; // Panel for Options
    public GameObject profilePanel; // Panel for Profile

    private void Start()
    {
        ShowPanel(mainMenuPanel);
    }

    void Update()
    {
        // Keyboard navigation
        if (Input.GetAxis("Vertical") < 0)
        {
            index++;
            if (index >= menuButtons.Count)
            {
                index = 0;
            }
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            index--;
            if (index < 0)
            {
                index = menuButtons.Count - 1;
            }
        }

        for (int i = 0; i < menuButtons.Count; i++)
        {
            if (i == index)
            {
                menuButtons[i].Select();
                if (Input.GetAxis("Submit") == 1)
                {
                    menuButtons[i].Press();
                }
            }
            else
            {
                menuButtons[i].Deselect();
            }
        }
    }

    // Show the specified panel and hide all others
    private void ShowPanel(GameObject panelToShow)
    {
        mainMenuPanel.SetActive(panelToShow == mainMenuPanel);
        newPanel.SetActive(panelToShow == newPanel);
        optionsPanel.SetActive(panelToShow == optionsPanel);
        profilePanel.SetActive(panelToShow == profilePanel);
    }

    public void StartGame()
    {
        ShowPanel(newPanel);
    }

    public void OpenOptions()
    {
        ShowPanel(optionsPanel);
    }

    public void OpenProfile()
    {
        ShowPanel(profilePanel);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();
    }

    // Mouse pointer interactions
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Find the button that the pointer entered and set it as selected
        for (int i = 0; i < menuButtons.Count; i++)
        {
            if (menuButtons[i].gameObject == eventData.pointerEnter)
            {
                index = i;
                menuButtons[i].Select();
                break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Find the button that the pointer exited and deselect it
        for (int i = 0; i < menuButtons.Count; i++)
        {
            if (menuButtons[i].gameObject == eventData.pointerEnter)
            {
                menuButtons[i].Deselect();
                break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Find the button that was clicked and press it
        for (int i = 0; i < menuButtons.Count; i++)
        {
            if (menuButtons[i].gameObject == eventData.pointerPress)
            {
                menuButtons[i].Press();
                break;
            }
        }
    }
}
