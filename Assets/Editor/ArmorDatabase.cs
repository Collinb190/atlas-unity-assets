using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArmorDatabase : ItemDatabase<Armor>
{
    private List<Armor> armorsList = new List<Armor>();

    public static void ShowWindow()
    {
        GetWindow<ArmorDatabase>("Armor Database");
    }

    protected override void DrawItemList()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        GUILayout.Label("Armors List", EditorStyles.boldLabel);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Load armors from folder
        LoadArmors();

        foreach (var armor in armorsList)
        {
            // Check if the current armor is selected
            bool isSelected = (selectedItem == armor);

            // Display button with the armor's name, highlighting if selected
            if (GUILayout.Button(armor.itemName, isSelected ? GUI.skin.box : GUI.skin.button, GUILayout.ExpandWidth(true)))
            {
                selectedItem = armor;
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

        if (selectedItem != null && selectedItem is Armor)
        {
            Armor armor = selectedItem as Armor;

            EditorGUILayout.LabelField("Name:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(selectedItem.itemName);
            EditorGUILayout.Space();

            armor.description = EditorGUILayout.TextField("Description: ", armor.description);
            armor.baseValue = EditorGUILayout.FloatField("Base Value: ", armor.baseValue);
            armor.requiredLevel = EditorGUILayout.IntField("Required Level: ", armor.requiredLevel);
            armor.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity: ", armor.rarity);
            EditorGUILayout.Space();

            GUILayout.Label("Armor Properties:", EditorStyles.boldLabel);
            armor.armorType = (ArmorType)EditorGUILayout.EnumPopup("Armor Type: ", armor.armorType);
            armor.defensePower = EditorGUILayout.FloatField("Defense Power: ", armor.defensePower);
            armor.resistance = EditorGUILayout.FloatField("Resistance: ", armor.resistance);
            armor.weight = EditorGUILayout.FloatField("Weight: ", armor.weight);
            armor.movementSpeedModifier = EditorGUILayout.FloatField("Movement Speed Modifier: ", armor.movementSpeedModifier);
            armor.equipSlot = (EquipSlot)EditorGUILayout.EnumPopup("Equip Slot: ", armor.equipSlot);
        }
        else
        {
            EditorGUILayout.LabelField("No armor selected");
        }

        EditorGUILayout.EndVertical();
    }

    private void LoadArmors()
    {
        armorsList.Clear();

        string folderPath = "Assets/Items/Armors";
        string[] guids = AssetDatabase.FindAssets("t:Armor", new[] { folderPath });

        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Armor armor = AssetDatabase.LoadAssetAtPath<Armor>(path);
            if (armor != null)
            {
                armorsList.Add(armor);
            }
        }
    }

    protected override void ExportItemsToCSV()
    {
        // Implement exporting armors to CSV
        Debug.Log("Exporting armors to CSV...");
    }

    protected override void ImportItemsFromCSV()
    {
        // Implement importing armors from CSV
        Debug.Log("Importing armors from CSV...");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Armor Database", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        // Left panel (top and bottom squares)
        EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.5f));

        // Top square (options and create new armor)
        EditorGUILayout.BeginVertical(GUI.skin.box);
        DrawTopLeftOptions();
        if (GUILayout.Button("Create New Armor"))
        {
            ArmorCreation.ShowWindow();
        }
        EditorGUILayout.EndVertical();

        // Bottom square (armors list)
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