using System.IO;
using System.Reflection;
using Type = System.Type;

namespace VPTK.Editor.Utility
{
    public static class LayoutUtility
    {

        private enum MethodType
        {
            Save,
            Load
        };

        private static MethodInfo GetMethod(MethodType methodType)
        {

            Type layout = Type.GetType("UnityEditor.WindowLayout,UnityEditor");

            MethodInfo save = null;
            MethodInfo load = null;

            if (layout != null)
            {
                load = layout.GetMethod("LoadWindowLayout",
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, null,
                    new Type[] {typeof(string), typeof(bool)}, null);
                save = layout.GetMethod("SaveWindowLayout",
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, null,
                    new Type[] {typeof(string)}, null);
            }

            return methodType == MethodType.Save ? save : load;
        }

        public static void SaveLayout(string path)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), path);
            GetMethod(MethodType.Save).Invoke(null, new object[] {path});
        }

        public static void LoadLayout(string path)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), path);
            GetMethod(MethodType.Load).Invoke(null, new object[] {path, false});
        }
    }
}