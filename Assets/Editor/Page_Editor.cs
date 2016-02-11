using UnityEngine;
using UnityEditor; //additional using statement to get access to the Unity Editor functionality

//CustomerEditor allows us to create a custom view for the Page class
[CustomEditor(typeof(Page))]
public class Page_Editor : Editor
{
    //overrides the OnInspectorGUI method that all Editors have
    public override void OnInspectorGUI()
    {
        //casts the target class as a page to a local page variable so we can edit it
        Page page = target as Page;

        //show a text field with the heading Name: and store what is entered into the Name variable of our local page
        page.Name = EditorGUILayout.TextField("Name:",page.name);

        //create a label saying Message
        EditorGUILayout.LabelField("Message:");

        //show a text field and store what is entered into the Message variable of our local page
        page.Message = EditorGUILayout.TextArea(page.Message, GUILayout.Height(85));

        //create a label saying Example
        EditorGUILayout.LabelField("Example:");

        //show a text field and store what is entered into the Example variable of our local page
        page.Example = EditorGUILayout.TextArea(page.Example, GUILayout.Height(85));
    }
}