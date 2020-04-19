using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IManager
{
    [SerializeField] private TaskMenu taskMenu = null;

    private Stack<Menu> menuStack;

    public void Initialize()
    {
        menuStack = new Stack<Menu>();
    }

    public void BindTask(TaskSlot slot)
    {
        OpenMenu(taskMenu);
        taskMenu.BindToTaskSlot(slot);
    }

    public void OpenMenu(Menu menuToOpen)
    {
        menuToOpen.Open();
        menuStack.Push(menuToOpen);
    }

    public void Quit()
    {
        Menu menuToClose = menuStack.Pop();
        menuToClose.Close();
    }
}
