using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] MenuController menuController;
    [SerializeField] int thisIndex;

    public void Select()
    {
        animator.SetBool("selected", true);
    }

    public void Deselect()
    {
        animator.SetBool("selected", false);
    }

    public void Press()
    {
        animator.SetBool("pressed", true);
        animatorFunctions.disableOnce = true;

        switch (thisIndex)
        {
            case 0:
                menuController.StartGame();
                break;
            case 1:
                menuController.OpenOptions();
                break;
            case 2:
                menuController.OpenProfile();
                break;
            case 3:
                menuController.QuitGame();
                break;
        }
    }
}
