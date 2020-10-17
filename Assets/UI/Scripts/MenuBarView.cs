using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MenuBarState: ViewState {
    public UITab activeTab;
}

public class MenuBarView : MonoBehaviour, ViewElement<MenuBarState>
{
    public MenuBarState GetCombinedState(Model model, MenuBarState newState) {
        newState.activeTab = model.activeTab;
        return newState;
    }

    MenuBarState state;

    public MenuBarState GetState() {
        return state;
    }

    public void SetState(MenuBarState newState) {
        state = newState;
    }

    [System.Serializable]
    public struct TabButtonPair {
        public UITab tab;
        public GameObject obj;
    }

    public TabButtonPair[] tabButtons;

    public void UpdateState(MenuBarState newState) {

    }

    public MenuBarState UpdateChildrenState(MenuBarState newState) {
        return newState;
    }

    public void OnTabClick(int tabNum) {
        UITab tab = (UITab)tabNum;
        Utils.Dispatch((Model model) => {
            model.activeTab = tab;
        });
    }

}
