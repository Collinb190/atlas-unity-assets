using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponDatabase : ItemDatabase<Weapon>
{
    private List<Weapon> weaponsList = new List<Weapon>();
    private string searchQuery = "";
    private WeaponType? selectedWeaponType = null;

    public static void ShowWindow()
    {
        GetWindow<WeaponDatabase>("Weapon Database");
    }

    protected override void DrawItemList()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        GUILayout.Label("Weapons List", EditorStyles.boldLabel);

        // Search Bar
        searchQuery = EditorGUILayout.TextField("Search", searchQuery);

        // Filters
        selectedWeaponType = (WeaponType?)EditorGUILayout.EnumPopup("Weapon Type", selectedWeaponType ?? WeaponType.None);

        if (GUILayout.Button("Clear Filters"))
        {
            searchQuery = "";
            selectedWeaponType = null;
            GUI.FocusControl(null); // Clear focus to ensure search text field updates visually
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Load weapons from folder
        LoadWeapons();

        bool foundWeapons = false; // Flag to track if any weapons match the criteria

        foreach (var weapon in weaponsList)
        {
            // Apply search and filters
            if (IsWeaponMatch(weapon))
            {
                foundWeapons = true;

                // Check if the current weapon is selected
                bool isSelected = (selectedItem == weapon);

                // Display button with the weapon's name, highlighting if selected
                if (GUILayout.Button(weapon.itemName, isSelected ? GUI.skin.box : GUI.skin.button, GUILayout.ExpandWidth(true)))
                {
                    selectedItem = weapon;
                }
            }
        }

        if (!foundWeapons)
        {
            EditorGUILayout.HelpBox("No weapons meet your criteria.", MessageType.Info);
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private bool IsWeaponMatch(Weapon weapon)
    {
        if (!string.IsNullOrEmpty(searchQuery) && !weapon.itemName.ToLower().Contains(searchQuery.ToLower()))
        {
            return false;
        }

        if (selectedWeaponType.HasValue && weapon.weaponType != selectedWeaponType.Value)
        {
            return false;
        }

        return true;
    }

    protected override void DrawPropertiesSection()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label("Properties Section:", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (selectedItem != null && selectedItem is Weapon)
        {
            Weapon weapon = selectedItem as Weapon;

            EditorGUILayout.LabelField("Name:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(selectedItem.itemName);
            EditorGUILayout.Space();

            weapon.description = EditorGUILayout.TextField("Description: ", weapon.description);
            weapon.baseValue = EditorGUILayout.FloatField("Base Value: ", weapon.baseValue);
            weapon.requiredLevel = EditorGUILayout.IntField("Required Level: ", weapon.requiredLevel);
            weapon.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity: ", weapon.rarity);
            EditorGUILayout.Space();

            GUILayout.Label("Weapon Properties:", EditorStyles.boldLabel);
            weapon.weaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type: ", weapon.weaponType);
            weapon.attackPower = EditorGUILayout.IntField("Attack Power: ", (int)weapon.attackPower);
            weapon.attackSpeed = EditorGUILayout.FloatField("Attack Speed: ", weapon.attackSpeed);
            weapon.durability = EditorGUILayout.FloatField("Durability: ", weapon.durability);
            weapon.range = EditorGUILayout.FloatField("Range: ", weapon.range);
            weapon.criticalHitChance = EditorGUILayout.FloatField("Critical Hit Chance: ", weapon.criticalHitChance);
            weapon.equipSlot = (EquipSlot)EditorGUILayout.EnumPopup("Equip Slot: ", weapon.equipSlot);
        }
        else
        {
            EditorGUILayout.LabelField("No weapon selected");
        }

        EditorGUILayout.EndVertical();
    }

    private void LoadWeapons()
    {
        weaponsList.Clear();

        string folderPath = "Assets/Items/Weapons";
        string[] guids = AssetDatabase.FindAssets("t:Weapon", new[] { folderPath });

        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Weapon weapon = AssetDatabase.LoadAssetAtPath<Weapon>(path);
            if (weapon != null)
            {
                weaponsList.Add(weapon);
            }
        }
    }

    protected override void ExportItemsToCSV()
    {
        // Implement exporting weapons to CSV
        Debug.Log("Exporting weapons to CSV...");
    }

    protected override void ImportItemsFromCSV()
    {
        // Implement importing weapons from CSV
        Debug.Log("Importing weapons from CSV...");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Weapon Database", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        // Left panel (top and bottom squares)
        EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.5f));

        // Top square (options and create new weapon)
        EditorGUILayout.BeginVertical(GUI.skin.box);
        DrawTopLeftOptions();
        if (GUILayout.Button("Create New Weapon"))
        {
            WeaponCreation.ShowWindow();
        }
        EditorGUILayout.EndVertical();

        // Bottom square (weapons list)
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
