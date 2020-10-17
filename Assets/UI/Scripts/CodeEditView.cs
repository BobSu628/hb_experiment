using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CodeEditState: ViewState {

}

public class CodeEditView : MonoBehaviour, ViewElement<CodeEditState>
{
    public CodeEditState GetCombinedState(Model model, CodeEditState newState) {
        return newState;
    }

    CodeEditState state;

    public CodeEditState GetState() {
        return state;
    }

    public void SetState(CodeEditState newState) {
        state = newState;
    }

    public void UpdateState(CodeEditState newState) {

    }

    public CodeEditState UpdateChildrenState(CodeEditState newState) {
        return newState;
    }

}
