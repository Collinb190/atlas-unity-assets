using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArmorDatabase : ItemDatabase<Armor>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void ShowWindow()
    {
        GetWindow<ArmorDatabase>("Armor Database");
    }

    protected override void DrawItemList()
    {
        Debug.Log("Drawing the item list");
    }

    protected override void DrawPropertiesSection()
    {
        Debug.Log("Drawing the properties section");
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
}
