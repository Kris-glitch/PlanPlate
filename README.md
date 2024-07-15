# PlanPlate- .NET Maui App
PlanPlate is a recipe app that uses public API for recipe information and Firebase for user authentication, database, and Crashlytics. It is written in C# using .NET Maui. Uses MVVM architecture pattern and dependency injection. Upon launching the app, users are greeted with a splash screen, leading them to the login/signup interface. The homepage has a bottom navigation bar and a search bar. You can navigate between the Discover tab which relies on the public API, the Cookbook tab which depends on the Firebase database and represents the user's recipes, and the Plan tab which also saves the plan in Firebase. The user can also add or edit recipes in its cookbook or generate a shopping list of ingredients. 

Key Technologies:

C# & .NET Maui: PlanPlate delivers a cross-platform application with a rich and responsive user interface.

The MVVM: (Model-View-ViewModel) architecture pattern ensures a clear separation of concerns, enhancing the maintainability and testability of the codebase.

Dependency Injection: Utilizing dependency injection promotes a modular and scalable codebase, making the app easier to manage and test.

Firebase Auth and Firebase Database: Firebase provides robust authentication and real-time database capabilities, providing secure user authentication and efficient data management, while Firebase Crashlytics offers robust crash reporting and analytics.

Public API Integration: PlanPlate integrates with a public API to fetch detailed recipe information, providing users with a vast collection of recipes.

Shell Navigation: Using .NET MAUI Shell for navigation, PlanPlate provides a streamlined and structured navigation experience, making it easier for users to explore the app's features.

XAML for UI Development: XAML designs the user interface, enabling a declarative approach to building the app's visual elements. This ensures a clean and maintainable UI codebase.

Asynchronous Programming: Utilizing asynchronous programming techniques, such as async/await, ensures that PlanPlate remains responsive and performs efficiently, even when handling network operations and data processing.

Data Binding: Data binding is extensively used to connect the UI components with the underlying data models, facilitating real-time updates and a dynamic user experience.

Resource Management: Resource management practices, such as using resource dictionaries for styles and themes, ensure a consistent and customizable user interface throughout the app.

![splash](https://github.com/Kris-glitch/PlanPlate/assets/78586563/672160eb-14e3-4651-9852-4ca3fcc60bd0)
![signup](https://github.com/Kris-glitch/PlanPlate/assets/78586563/a1ee05a5-a3e4-4c38-b143-d7fa0e5de4b0)
![discover](https://github.com/Kris-glitch/PlanPlate/assets/78586563/97094d95-cc77-47f4-98d9-42b3464431d1)
![new](https://github.com/Kris-glitch/PlanPlate/assets/78586563/4dd5a96b-8319-4b0c-a151-bc52e03c021e)
![cookbook](https://github.com/Kris-glitch/PlanPlate/assets/78586563/df9e613a-7ddd-4e51-bd0c-75de37ff2b90)
![save](https://github.com/Kris-glitch/PlanPlate/assets/78586563/e87cb73b-bcb8-4ea5-b73e-0c9daec8bfc9)
![plan](https://github.com/Kris-glitch/PlanPlate/assets/78586563/69cf7aab-cffc-4ea9-9be7-e01044644231)
![details](https://github.com/Kris-glitch/PlanPlate/assets/78586563/8a806d27-3ff8-42c2-a077-50776caf8993)
![details2](https://github.com/Kris-glitch/PlanPlate/assets/78586563/0fe3f893-18af-493f-bcc5-64b96b41cc09)
![shopping](https://github.com/Kris-glitch/PlanPlate/assets/78586563/29c4ebab-0881-4c5d-85a8-1c5343d3bade)
