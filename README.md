# CSE325-Project-Team-9

## Project: Recipe Manager.
## Team: Four participants.

Overview: Help busy families and individuals organize their day better by planning out all of their meals ahead of time and keeping recipes, meals, and meal schedules all in one place. The recipe manager will also help users create a shopping list based on the recipes they have chosen for the week.  
Our app adds value by automating the recipe building, meal scheduling, and grocery list generating process. As soon as recipes are added to a user’s profile, they are available to be added to a meal schedule which will generate a shopping list.  
## Scope:  
-	 User Authentication – Users will be able to sign up, log in, and log out securely. 
-	 Recipe Management (CRUD) – Users can create, edit, view, and delete recipes with fields such as title, ingredients, and ## steps. 
-	 Ingredient Management – Each recipe will allow multiple ingredients to be added, each with a name, quantity, and unit. 
-	 Shopping List Generator – The system will automatically create a shopping list that includes ingredients from selected recipes. 
-	 Responsive Design – The web app will be accessible and easy to use on both desktop and mobile devices. 

## App Features:  
-	 Users will be able to sign up, log in, and log out securely. 
-	 Users can create, edit, view, and delete recipes with fields such as title, ingredients, and steps. 
-	 Users can add multiple ingredients, each with a name, quantity, and unit. 
-	 Users can automatically create a shopping list that includes ingredients from selected recipes and edit or remove items from the shopping list manually. 
-	 Users can search ingredients in recipes 

 
## Technical Considerations: 
-	 Data Storage: Recipes, ingredients, users, shopping list 
-	 User Accounts: Required to save Recipes, ingredients, and create shopping list. 
-	 External Services: We will rely on our own API for pulling recipes saved in the database. Depending on how that goes, we may employ external APIs for additional recipes. 
-	 Device Compatibility: Mobile and Desktop 
-	 Basic Security: Data validation and encryption for storing passwords and sensitive data 

## Project Links: 
-	 Trello: https://trello.com/b/fQXplb2z/project  


# 📌 Development Phases (Blazor CRUD Plan)

## Phase 1 – Setup & Routing
- Create Blazor Web App project (dotnet new blazorserver -n RecipeManager).
- Configure routing and layouts (App.razor, MainLayout.razor, NavMenu.razor).
- Use Blazor routing & layouts module.

## Phase 2 – Data & EF Core
- Add ApplicationDbContext.cs with DbSets for Recipe, Ingredient, MealSchedule, ShoppingListItem, ApplicationUser.
- Configure SQLite (App.db) in Program.cs.
- Use Interact with data in Blazor apps.

## Phase 3 – Authentication 
- Integrate ASP.NET Core Identity with ApplicationUser.
- Build Login.razor, Register.razor, LoginDisplay.razor.

## Phase 4 – CRUD Operations
- Recipes: RecipeList.razor, RecipeCreate.razor, RecipeEdit.razor, RecipeDetails.razor.
- Ingredients: Inline editing with IngredientRow.razor.
- Shopping List: Auto-generate from recipes, editable in ShoppingList.razor.
- Use Blazor forms & validation module.

## Phase 5 – Components & Interactivity
- Build reusable components: RecipeCard.razor, ShoppingListItemRow.razor.
- Add interactivity (checkboxes, dynamic ingredient rows).
- Use Blazor interactive components and reusable components.

## Phase 6 – Testing & Deployment
- Unit tests in Tests/RecipeServiceTests.cs and ShoppingListTests.cs.
- Deploy to Azure or local IIS.

# 🗂️ Trello Assignments (4 Participants)

## Participant A – Authentication   Hector
Identity setup, Login.razor, Register.razor, LoginDisplay.razor.

## Participant B – Data & Services  Dylan
ApplicationDbContext.cs, migrations, RecipeService.cs, ScheduleService.cs, ShoppingListService.cs.

## Participant C – Recipes & Ingredients  Jonathan
CRUD pages (RecipeList, RecipeCreate, RecipeEdit, RecipeDetails), IngredientRow.razor.

## Participant D – Shopping List & Schedule
ShoppingList.razor, aggregation logic, ShoppingListItemRow.razor, ScheduleService.cs.

# 📁 Project Folder Structure & Initial Files


How to put the following structure in a README file at GitHub
```
RecipeManager/
│   Program.cs
│   appsettings.json
│   App.razor
│   Imports.razor
│   App.db
│
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── Migrations/
│   └── SeedData.cs
│
├── Models/
│   ├── Recipe.cs
│   ├── Ingredient.cs
│   ├── ShoppingListItem.cs
│   ├── MealSchedule.cs
│   └── ApplicationUser.cs
│
├── Services/
│   ├── IRecipeService.cs
│   ├── RecipeService.cs
│   ├── IScheduleService.cs
│   ├── ScheduleService.cs
│   ├── IShoppingListService.cs
│   ├── IAuthService.cs
│   └── AuthService.cs
│
├── Pages/
│   ├── Index.razor
│   ├── Login.razor
│   ├── Register.razor
│   ├── Recipes/
│   │     ├── RecipeList.razor
│   │     ├── RecipeCreate.razor
│   │     ├── RecipeEdit.razor
│   │     └── RecipeDetails.razor
│   │
│   └── Shopping/
│       └── ShoppingList.razor
│      
├── Components/
│   ├── RecipeCard.razor
│   ├── IngredientRow.razor
│   └── ShoppingListItemRow.razor
│
├── Shared/
│   ├── NavMenu.razor
│   ├── MainLayout.razor
│   └── LoginDisplay.razor
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── icons/
│
└── Tests/
    ├── RecipeServiceTests.cs
    └── ShoppingListTests.cs
```
## Technology Stack
- **Frontend**: Blazor WebAssembly
- **Backend**: ASP.NET Core
- **Database**: SQLite with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Testing**: xUnit or MSTest

## Getting Started

1. Clone the repository
2. Restore NuGet packages
3. Run database migrations
4. Build and run the application

For detailed setup instructions, see the [Installation Guide](docs/installation.md).

## Team Members
- Dylan Stephenson
- Hector Olivares
- Malcolm Nigel Nkomo
- Jonathan Uribe
