using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveManagerWindow : EditorWindow
{
    private string key = "player";
    private Vector2 scrollPosition;
    private string loadedJson = "";

    private JsonSaveProvider jsonProvider;

    [MenuItem("Tools/Save Manager")]
    private static void OpenWindow()
    {
        GetWindow<SaveManagerWindow>("Save Manager");
    }

    private void OnEnable()
    {
        jsonProvider = new JsonSaveProvider();
    }

    private void OnGUI()
    {
        GUILayout.Label("Save Manager Tool", EditorStyles.boldLabel);

        key = EditorGUILayout.TextField("Save Key", key);

        EditorGUILayout.Space();

        if (GUILayout.Button("Load JSON"))
        {
            LoadFile();
        }

        if (GUILayout.Button("Delete Save"))
        {
            DeleteFile();
        }

        EditorGUILayout.Space();

        GUILayout.Label("Loaded JSON:", EditorStyles.boldLabel);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
        EditorGUILayout.TextArea(loadedJson, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        GUILayout.Label("Utilities", EditorStyles.boldLabel);

        if (GUILayout.Button("Open Save Folder"))
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
    }

    private void LoadFile()
    {
        string folder = Path.Combine(Application.persistentDataPath, "SaveData");
        string path = Path.Combine(folder, key + ".json");

        if (!File.Exists(path))
        {
            loadedJson = "File not found.";
            return;
        }

        loadedJson = File.ReadAllText(path);
    }

    private void DeleteFile()
    {
        string folder = Path.Combine(Application.persistentDataPath, "SaveData");
        string path = Path.Combine(folder, key + ".json");

        if (File.Exists(path))
        {
            File.Delete(path);
            loadedJson = "File deleted.";
        }
        else
        {
            loadedJson = "File not found.";
        }
    }
}