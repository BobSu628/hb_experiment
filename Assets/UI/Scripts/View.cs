using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ViewState
{

}

public interface ViewElement<S> where S : ViewState {
    GameObject gameObject {
        get;
    }
    void UpdateState(S newState);
    S UpdateChildrenState(S newState);
    S GetCombinedState(Model model, S newState);
    S GetState();
    void SetState(S newState);
}

public enum UITab {
    Shell,
    CodeEdit,
}

public struct BaseState : ViewState {
    public UITab activeTab;
    public MenuBarState menuBar;
    public ShellState shell;
    public CodeEditState codeEdit;
}

public class View : MonoBehaviour, ViewElement<BaseState>
{
    public BaseState GetCombinedState(Model model, BaseState newState) {
        newState.activeTab = model.activeTab;
        return newState;
    }

    BaseState state;

    public BaseState GetState() {
        return state;
    }

    public void SetState(BaseState newState) {
        state = newState;
    }

    public MenuBarView menuBar;
    public ShellView shell;
    public CodeEditView codeEdit;

    public void UpdateState(BaseState newState) {
        shell.gameObject.SetActive(newState.activeTab == UITab.Shell);
        codeEdit.gameObject.SetActive(newState.activeTab == UITab.CodeEdit);
    }

    public BaseState UpdateChildrenState(BaseState newState) {
        newState.menuBar = Utils.UpdateState(menuBar, menuBar.GetState());
        newState.shell = Utils.UpdateState(shell, shell.GetState());
        newState.codeEdit = Utils.UpdateState(codeEdit, codeEdit.GetState());
        return newState;
    }

    void Start()
    {
        Utils.UpdateState(this, state);
    }

    long updateCount = -1;
    // Update is called once per frame
    void Update()
    {
        if (Utils.store.needsUpdate > updateCount) {
            Debug.Log("updating");
            Utils.UpdateState(this, state);
            updateCount = Utils.store.needsUpdate;
        }
    }

}
