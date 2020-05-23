using UnityEditor;


namespace FPSLogic.Editor
{
    public sealed class CustomMenus
    {
        [MenuItem("FPSLogic/Custom Widnow")]
        private static void MenuOption()
        {
            EditorWindow.GetWindow(typeof(CustomWindow), false, "Custom Window");
        }
    }
}