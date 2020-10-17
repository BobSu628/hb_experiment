using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Linq;

public static class Utils
{
    public static string Normalize(string s) {
        return s.Trim().ToLower();
    }
    public static bool MatchAnswer(string a, string b) {
        return Normalize(a) == Normalize(b);
    }

    public static bool MatchAnswer(string query, string[] answers) {
        query = Normalize(query);
        return answers.Select(Normalize).Any((s) => s == query);
    }

    public static bool NullableArrayEqual<T> (T[] a, T[] b) {
        if (a == null && b == null) {
            return true;
        }
        if (a == null || b == null) {
            return false;
        }
        return a.SequenceEqual(b);
    }
    public static T DeepClone<T>(this T obj)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (T)formatter.Deserialize(ms);
        }
    }

    public static bool SameState(ViewState a, ViewState b) {
        if (a == null && b == null) {
            return true;
        }
        if (a == null || b == null) {
            return false;
        }
        // Use reflection to find all UIState children
        Type aType = a.GetType();
        Type bType = b.GetType();
        Debug.Assert(aType == bType);

        foreach(FieldInfo fieldInfo in aType.GetFields()) {
            if (!fieldInfo.FieldType.GetInterfaces().Contains(typeof(ViewState))) {
                if (fieldInfo.FieldType.IsArray) {
                    var arrayFieldA = (Array)fieldInfo.GetValue(a);
                    var arrayFieldB = (Array)fieldInfo.GetValue(b);
                    if (arrayFieldA == null && arrayFieldB == null) {
                        continue;
                    }
                    if (arrayFieldA == null || arrayFieldB == null) {
                        return false;
                    }
                    if (arrayFieldA.Length != arrayFieldB.Length) {
                        return false;
                    }
                    for (int i = 0; i < arrayFieldA.Length; i++) {
                        if (!arrayFieldA.GetValue(i).Equals(arrayFieldB.GetValue(i))) {
                            return false;
                        }
                    }
                } else {
                    var fieldA = fieldInfo.GetValue(a);
                    var fieldB = fieldInfo.GetValue(b);
                    if (fieldA == null && fieldB == null) {
                        continue;
                    }
                    if (fieldA == null || fieldB == null) {
                        return false;
                    }
                    if (!fieldA.Equals(fieldB)) {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    public static Model store = new Model();
    public static S UpdateState<S>(ViewElement<S> element, S state) where S : ViewState {
        if (!element.gameObject.activeInHierarchy) {
            return state;
        }
        state = element.GetCombinedState(Utils.store, state);
        if (!Utils.SameState(element.GetState(), state)) {
            Debug.Log(element.GetType().Name + " Updating");
            element.UpdateState(state);
            element.SetState(state);
        }
        state = element.UpdateChildrenState(state);
        return state;
    }

    public static void Dispatch(System.Action<Model> modifier) {
        modifier(store);
        Utils.store.needsUpdate++;
    }

    public static void SaveStateToFile(string destination) {
        StreamWriter writer = new StreamWriter(destination, false);
        writer.WriteLine(JsonUtility.ToJson(store, true));
        writer.Close();
        Debug.Log(string.Format("state written to {0}", destination));
    }
    public static void LoadStateFromFile(string source) {
        StreamReader reader = new StreamReader(source, false);
        string s = reader.ReadToEnd();
        reader.Close();
        Utils.store = JsonUtility.FromJson<Model>(s);
        Utils.store.needsUpdate = 0;
        Debug.Log(string.Format("state read from {0}", source));
    }
}
