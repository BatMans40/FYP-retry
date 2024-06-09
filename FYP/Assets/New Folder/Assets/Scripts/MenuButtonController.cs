using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour {

    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    public AudioSource audioSource;
    [SerializeField] MenuButton[] menuButtons;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        UpdateButtonSelection();
    }
    
    void Update () {
        if(Input.GetAxis ("Vertical") != 0){
            if(!keyDown){
                if (Input.GetAxis ("Vertical") < 0) {
                    if(index < maxIndex){
                        index++;
                    } else {
                        index = 0;
                    }
                } else if(Input.GetAxis ("Vertical") > 0){
                    if(index > 0){
                        index--; 
                    } else {
                        index = maxIndex;
                    }
                }
                keyDown = true;
                UpdateButtonSelection();
            }
        } else {
            keyDown = false;
        }

        if (Input.GetButtonDown("Submit")) {
            menuButtons[index].Press();
        }
    }

    void UpdateButtonSelection() {
        for (int i = 0; i < menuButtons.Length; i++) {
            if (i == index) {
                menuButtons[i].Select();
            } else {
                menuButtons[i].Deselect();
            }
        }
    }
}