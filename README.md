# 🐀 Remy's Recipes

*"Anyone can cook!"* — A recipe management and shopping list web app inspired by Ratatouille.

## Overview

Remy's Recipes lets you store, organize, and search for recipes in one place — then generate a shopping list from the ingredients you need. No account required. All data is stored locally in your browser.

## Features

### Recipe Management
- **Add** new recipes with name, category, prep/cook time, servings, ingredients, and instructions
- **Edit** existing recipes inline
- **Delete** recipes you no longer need
- **Search** recipes by name or category via the navbar search bar
- **Filter** by category using the dropdown menu
- **Review** recipes with star ratings and comments

### Shopping List
- **Generate** a shopping list directly from recipe ingredients ("Add to Shopping List" button)
- **Manually add** items with quantity and unit of measurement
- **Merge** duplicate items automatically (quantities are summed)
- **Check off** items while shopping
- **Delete** checked items when done
- **Sort** by Qty, Item name, or Recipe Requirement (click column headers)
- **Edit** items inline

### Design
- Ratatouille-inspired theme (warm colors, Merriweather + Open Sans typography)
- Responsive layout with hamburger menu for mobile devices
- Consistent branding and navigation across all pages

## Tech Stack

- **.NET 8** — Blazor WebAssembly (client-side)
- **C#** — Application logic
- **HTML/CSS** — Razor components with scoped CSS
- **Browser localStorage** — Data persistence (no server/database required)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Run Locally

```bash
git clone https://github.com/LetMahomed/CSE325-team7.git
cd CSE325-team7
dotnet run --project CSE325_team7.csproj
```

Open `http://localhost:5062` (check terminal output for actual port).

### Build

```bash
dotnet build CSE325_team7.csproj
```

## Usage Guide

### Adding a Recipe

1. Go to the **Recipes** page
2. Click **"+ New Recipe"**
3. Fill in the form:
   - **Ingredients**: one per line as `amount unit name` (e.g., `2 cups Flour`) or `amount name` (e.g., `3 Eggs`)
   - **Instructions**: one step per line
4. Click **Save**

### Editing / Deleting a Recipe

- Click **✏️ Edit** on any recipe card to edit inline
- Click **🗑️ Delete** to remove a recipe

### Adding to Shopping List

1. On the Recipes page, click **"🛒 Add to Shopping List"** on any recipe
2. All ingredients are added to your shopping list
3. Duplicate items are merged automatically

### Using the Shopping List

1. Go to the **Shopping List** page
2. Check off items as you shop
3. Click **"Delete Checked Items"** to clear completed items
4. Use column headers to sort by Qty, Item, or Recipe Requirement

## Project Links

- **GitHub:** https://github.com/LetMahomed/CSE325-team7
- **Trello:** https://trello.com/b/zvS9euzf/cse325

## Team

- Adam Cottam
- Christina Lane
- Leticia De Sousa
