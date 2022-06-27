using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class QuickTool : EditorWindow {
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/QuickTool _%#T")]
    public static void ShowExample() {
        QuickTool wnd = GetWindow<QuickTool>();
        wnd.titleContent = new GUIContent("QuickTool Test");
        wnd.minSize = new Vector2(280, 50);
    }

    // public void CreateGUI()
    // {
    //     // Each editor window contains a root VisualElement object
    //     VisualElement root = rootVisualElement;

    //     var btn = new Button{text = "My Button"};
    //     btn.style.width = 160;
    //     btn.style.height = 30;
    //     root.Add(btn);

    //     // VisualElements objects can contain other VisualElement following a tree hierarchy.
    //     VisualElement label = new Label("Hello World! From C#");
    //     root.Add(label);
    //     //root.Add(label);

    //     // Instantiate UXML
    //     VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
    //     root.Add(labelFromUXML);
    // }

    void CreateGUI() {
        var root = rootVisualElement;
        var sheet = Resources.Load<StyleSheet>("QuickTool_Style");
        root.styleSheets.Add(sheet);
        var quickToolVisualTree = Resources.Load<VisualTreeAsset>("QuickTool_Main");
        quickToolVisualTree.CloneTree(root);

        var toolButtons = root.Query(className: "quicktool-button");
        toolButtons.ForEach(SetupButton);
    }

    void SetupButton(VisualElement btn) {
        var btnIcon = btn.Q(className: "quicktool-button-icon");
        var iconPath = "Icons/" + btn.parent.name + "_icon";
        //var iconPath = "Icons/Plane_icon";
        Debug.Log("dafuq " + iconPath);
        var iconAsset = Resources.Load<Texture2D>(iconPath);
        btnIcon.style.backgroundImage = iconAsset;
        btnIcon.name = "Plane";
        btn.RegisterCallback<PointerUpEvent, string>(CreateObject, btn.parent.name);
        btn.tooltip = btn.parent.name;
    }

    void CreateObject(PointerUpEvent _, string primitiveType) {
        var pt = (PrimitiveType)Enum.Parse(
            typeof(PrimitiveType), primitiveType, true
        );
        var go = ObjectFactory.CreatePrimitive(pt);
        go.transform.position = Vector3.zero;
    }
}
