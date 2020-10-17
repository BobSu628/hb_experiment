using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShellState: ViewState {

}

public class ShellView : MonoBehaviour, ViewElement<ShellState>
{
    public ShellState GetCombinedState(Model model, ShellState newState) {
        return newState;
    }

    ShellState state;

    public ShellState GetState() {
        return state;
    }

    public void SetState(ShellState newState) {
        state = newState;
    }

    public void UpdateState(ShellState newState) {

    }

    public ShellState UpdateChildrenState(ShellState newState) {
        return newState;
    }

}
