using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;
    Interpreter interpreter;
    public string savedText = ">> ";
    public string buffer = "";

    void Start()
    {
        interpreter = new Interpreter();
        inputField.text = savedText;
    }

    void Update() {
        foreach (char c in Input.inputString) {
            if (c == '\b') {
                if (buffer.Length != 0) {
                    buffer = buffer.Substring(0, buffer.Length-1);
                }
            } else if (c == '\n' || c == '\r') {
                string output = interpreter.Run(buffer);
                buffer = buffer + '\n' + output + "\n>> ";
                inputField.text = inputField.text + output + "\n>> ";
                savedText += buffer;
                buffer = "";
            } else {
                buffer += c;
            }
        }

        inputField.caretPosition = Math.Max(inputField.caretPosition, savedText.Length);
    }  

    public void OnEdit() {
        if (inputField.text.Length < savedText.Length) {
            inputField.text = savedText;
        }
    }
}
