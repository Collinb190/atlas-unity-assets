using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PotionDatabase : ItemDatabase<Potion>
{
    private List<Potion> potionsList = new List<Potion>();

    public static void ShowWindow()
    {
        GetWindow<PotionDatabase>("Potion Database");
    }

    protected override void DrawItemList()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        GUILayout.Label("Potions List", EditorStyles.boldLabel);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Load potions from folder
        LoadPotions();

        foreach (var potion in potionsList)
        {
            // Check if the current potion is selected
            bool isSelected = (selectedItem == potion);

            // Display button with the potion's name, highlighting if selected
            if (GUILayout.Button(potion.itemName, isSelected ? GUI.skin.box : GUI.skin.button, GUILayout.ExpandWidth(true)))
            {
                selectedItem = potion;
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    protected override void DrawPropertiesSection()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label("Properties Section:", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (selectedItem != null && selectedItem is Potion)
        {
            Potion potion = selectedItem as Potion;

            EditorGUILayout.LabelField("Name:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(selectedItem.itemName);
            EditorGUILayout.Space();

            potion.description = EditorGUILayout.TextField("Description: ", potion.description);
            potion.baseValue = EditorGUILayout.FloatField("Base Value: ", potion.baseValue);
            potion.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity: ", potion.rarity);
            EditorGUILayout.Space();

            GUILayout.Label("Potion Properties:", EditorStyles.boldLabel);
            potion.potionEffect = (PotionEffect)EditorGUILayout.EnumPopup("Potion Effect: ", potion.potionEffect);
            potion.effectPower = EditorGUILayout.FloatField("Effect Power: ", potion.effectPower);
            potion.duration = EditorGUILayout.FloatField("Duration: ", potion.duration);
            potion.cooldown = EditorGUILayout.FloatField("Cooldown: ", potion.cooldown);
            potion.isStackable = EditorGUILayout.Toggle("Is Stackable: ", potion.isStackable);
        }
        else
        {
            EditorGUILayout.LabelField("No potion selected");
        }

        EditorGUILayout.EndVertical();
    }

    private void LoadPotions()
    {
        potionsList.Clear();

        string folderPath = "Assets/Items/Potions";
        string[] guids = AssetDatabase.FindAssets("t:Potion", new[] { folderPath });

        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Potion potion = AssetDatabase.LoadAssetAtPath<Potion>(path);
            if (potion != null)
            {
                potionsList.Add(potion);
            }
        }
    }

    protected override void ExportItemsToCSV()
    {
        // Implement exporting potions to CSV
        Debug.Log("Exporting potions to CSV...");
    }

    protected override void ImportItemsFromCSV()
    {
        // Implement importing potions from CSV
        Debug.Log("Importing potions from CSV...");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Potion Database", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        // Left panel (top and bottom squares)
        EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.5f));

        // Top square (options and create new potion)
        EditorGUILayout.BeginVertical(GUI.skin.box);
        DrawTopLeftOptions();
        if (GUILayout.Button("Create New Potion"))
        {
            PotionCreation.ShowWindow();
        }
        EditorGUILayout.EndVertical();

        // Bottom square (potions list)
        EditorGUILayout.BeginVertical(GUI.skin.box);
        DrawItemList();
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();

        // Right panel (tall rectangle for properties)
        EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(position.width * 0.5f));
        DrawPropertiesSection();
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
    }
}