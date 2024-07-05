# PlanPlate- .NET Maui App
PlanPlate is a recipe app that uses public API for the recipe information and Firebase for user authentication, database and crashlytics. It is written in C# using .NET Maui. Uses MVVM architecture pattern and depeendency injection. Upon launching the app, users are greeted with a splash screen, leading them into the login/signup interface. The homepage has a bottom navigation bar and a search bar. You can navigate between the discover tab that relies on the public Api, the cookbook tab that relies on the Firebase database and represents users recipes and the Plan tab that also saves the plan in Firebase. The user has also options to add or edit recipe in its cookbook or generate a shopping list of ingredients. 

Key Technologies:

C# & .NET Maui: PlanPlate delivers a cross-platform application with a rich and responsive user interface.

The MVVM: (Model-View-ViewModel) architecture pattern ensures a clear separation of concerns, enhancing the maintainability and testability of the codebase.

Dependency Injection: Utilizing dependency injection promotes a modular and scalable codebase, making the app easier to manage and test.

Firebase Auth and Firebase Database: Firebase provides robust authentication and real-time database capabilities, empowering WhispersInk with secure user authentication and efficient data management, while Firebase Crashlytics offers robust crash reporting and analytics.

Public API Integration:PlanPlate integrates with a public API to fetch detailed recipe information, providing users with a vast collection of recipes.

Shell Navigation: Using .NET MAUI Shell for navigation, PlanPlate provides a streamlined and structured navigation experience, making it easier for users to explore the app's features.

XAML for UI Development: XAML is used for designing the user interface, enabling a declarative approach to building the app's visual elements. This ensures a clean and maintainable UI codebase.

Asynchronous Programming: Utilizing asynchronous programming techniques, such as async/await, ensures that PlanPlate remains responsive and performs efficiently, even when handling network operations and data processing.

Data Binding: Data binding is extensively used to connect the UI components with the underlying data models, facilitating real-time updates and a dynamic user experience.

Resource Management: Resource management practices, such as using resource dictionaries for styles and themes, ensure a consistent and customizable user interface throughout the app.
![splash](https://github.com/Kris-glitch/PlanPlate/assets/78586563/30202406-4d77-44d7-bb1a-71d61d2704b4)
![signup](https://github.com/Kris-glitch/PlanPlate/assets/78586563/833bbdcc-5c7e-4c82-b7c7-d247332f010d)
![discover](https://github.com/Kris-glitch/PlanPlate/assets/78586563/b3f08c37-5c5c-45e1-8d3c-3afb285008e7)
![details](https://github.com/Kris-glitch/PlanPlate/assets/78586563/0fa27e8d-6481-43f0-a71a-1e97a78a64a2)
![details2](https://github.com/Kris-glitch/PlanPlate/assets/78586563/164d35f1-4179-4074-a314-86d6c58e1f36)
![cookbook](https://github.com/Kris-glitch/PlanPlate/assets/78586563/b8df0c93-9737-4865-bbe1-ded488f2ca21)
![save](https://github.com/Kris-glitch/PlanPlate/assets/78586563/587346d2-607c-4aaa-9ff4-dd17784dce4f)
![new](https://github.com/Kris-glitch/PlanPlate/assets/78586563/291e5600-d60e-47c2-a5d4-6ed0bd18e7c3)
![plan](https://github.com/Kris-glitch/PlanPlate/assets/78586563/bb0cf999-0ff9-49f3-aa1e-27720413722e)
![shopping](https://github.com/Kris-glitch/PlanPlate/assets/78586563/cef04dba-11e5-4835-ad90-1e925631cb42)
