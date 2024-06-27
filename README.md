# Custom Item Database System for Unity Editor

## Getting Started

### Prerequisites

- Use Unity version 2021.3.x or later.

### Installation

1. **Clone the Repository**: Clone the repository to your local machine.
    ```bash
    git clone https://github.com/your/repository.git
    ```
2. **Open in Unity**: Open the cloned project in Unity by selecting **File -> Open Project** and navigating to the project folder.

## Overview

- **Weapon Database Window**: A comprehensive tool for managing weapons with features like pagination, real-time search, filtering by type, and detailed property editing.
  
- **Potion and Armor Databases**: Dedicated windows (Potion Database and Armor Database) for managing potions and armors, respectively, with similar functionalities tailored to their respective attributes.
  
- **Statistics Page**: Provides insights into item counts, average attributes, data grouping, and graphical representations of weapon, potion, and armor data.

## Using the Weapon Database

### Opening the Weapon Database Window

- In Unity's Editor, navigate to **Window -> Item Manager -> Databases Manager**.
- Click on **Weapon Database** to open the Weapon Database window.

### Deleting a Weapon

- Select the weapon you want to delete in the weapons list.
- Click on the **Delete Selected Item** button.

### Creating a New Weapon

- Click on **Create New Weapon** in the top left corner of the Weapon Database window.
- Fill in the details for the new weapon in the properties section on the right panel.

### Editing an Existing Weapon

- Select a weapon from the weapons list in the bottom left panel.
- Modify the properties in the properties section on the right panel.

### Searching and Filtering

- Use the search bar at the top to search for weapons by name.
- Use the dropdowns for **Weapon Type** to filter the displayed weapons.
- Click **Clear Filters** to reset the search bar and clear filters.

### Exporting and Importing

- **CSV Import and Export**: Currently under development. Importing and exporting weapon data to CSV files is planned for a future release.

### Feedback and No Match Display

- If no weapons match the search criteria, a message stating "**No items match your search criteria**" will be displayed.
- This feedback helps in understanding when no results are found based on the applied filters or search query.

## Contributing

Contributions to this project are welcome. Please fork the repository, make your changes, and submit a pull request.
