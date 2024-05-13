using Microsoft.Maui.Controls;
using PlanPlate.ViewModels;

namespace PlanPlate.View;

public partial class Plan : ContentPage
{
    private readonly PlanViewModel _viewModel;
    public Plan(PlanViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
        _viewModel.SubscribeToErrorEvents(OnShowError);
        AddCategoryViews();

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_viewModel.InitPerformed)
        {

            await _viewModel.GetAllRecipesFromPlanner();

            _viewModel.InitPerformed = true;
        }
    }

    private async void OnShowError(string errorMessage)
    {
        await DisplayAlert("Error", errorMessage, "OK");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();


        _viewModel.UnsubscribeFromErrorEvents(OnShowError);

    }


    private void AddCategoryViews()
    {

        var surfaceColor = (Color)Application.Current.Resources["Surface"];
        var backgroundColor = (Color)Application.Current.Resources["Background"];
        var buttonColor = (Color)Application.Current.Resources["Button"];
        var textColor = (Color)Application.Current.Resources["TextLight"];
        var secondaryColor = (Color)Application.Current.Resources["Secondary"];

        if (App.Current.RequestedTheme == AppTheme.Dark)
        {
            surfaceColor = (Color)Application.Current.Resources["SurfaceDark"];
            backgroundColor = (Color)Application.Current.Resources["BackgroundDark"];
            buttonColor = (Color)Application.Current.Resources["ButtonDark"];
            textColor = (Color)Application.Current.Resources["TextDark"];
            secondaryColor = (Color)Application.Current.Resources["SecondaryDark"];
        }

        foreach (PlannerCategory category in Enum.GetValues(typeof(PlannerCategory)))
        {

            var categoryLabel = new Label
            {
                Text = category.ToString(),
                Margin = new Thickness(0, 25, 0, 0),
                Style = (Style)Application.Current.Resources["SubHeadline"],
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start
            };

            var emptyLabel = new Label
            {
                Style = (Style)Application.Current.Resources["SubHeadline"],
                Text = "No planned recipe, add a recipe?",
                TextColor = buttonColor,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                IsVisible = false
            };

            #region frame
            var frame = new Frame
            {
                Style = (Style)Application.Current.Resources["RecipeeCard"],
                Padding = 0,
                WidthRequest = 250,
                Margin = new Thickness(0, 16, 0, 16),
                CornerRadius = 20
            };

            var grid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection { new ColumnDefinition(), new ColumnDefinition() },
                RowDefinitions = new RowDefinitionCollection { new RowDefinition { Height = new GridLength(250) }, new RowDefinition { Height = new GridLength(120) } }
            };
            

            frame.Content = grid;

            var innerframe = new Frame
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = surfaceColor

            };

            var recipeImage = new Image
            {
                Aspect = Aspect.AspectFill
            };

            recipeImage.SetBinding(Image.SourceProperty, "Image");

            innerframe.Content = recipeImage;

            var bottomFrame = new Frame
            {
                HeightRequest = 150,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = surfaceColor
            };

            var bottomText = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
            };

            var recipeNameLabel = new Label
            {
                VerticalOptions = LayoutOptions.Start,
                Margin = 0,
                LineBreakMode = LineBreakMode.WordWrap,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center
            };

            recipeNameLabel.SetBinding(Label.TextProperty, "Name");

            var recipeCategoryLabel = new Label
            {
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 10, 0, 0),
                LineBreakMode = LineBreakMode.WordWrap,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center
            };

            recipeCategoryLabel.SetBinding(Label.TextProperty, "Category", stringFormat: "Category: {0}");

            var recipeByLabel = new Label
            {
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(0),
                LineBreakMode = LineBreakMode.WordWrap,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center
            };

            recipeByLabel.SetBinding(Label.TextProperty, "RecipeBy", stringFormat: "Recipe by: {0}");


            bottomText.Children.Add(recipeNameLabel);
            bottomText.Children.Add(recipeCategoryLabel);
            bottomText.Children.Add(recipeByLabel);


            bottomFrame.Content = bottomText;


            var deleteButtonFrame = new Frame
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                IsVisible = true,
                HasShadow = true,
                Padding = new Thickness(0),
                Margin = new Thickness(0, 0, 20, 20),
                HeightRequest = 50,
                WidthRequest = 50,
                IsClippedToBounds = true,
                CornerRadius = 70,
                BackgroundColor = backgroundColor,
                BorderColor = buttonColor
            };

            var deleteButtonImage = new Image
            {
                Source = new FontImageSource
                {
                    FontFamily = "FAS",
                    Glyph = (string)Application.Current.Resources["trash"],
                    Color = buttonColor,
                    Size = 24
                }
            };

                
            var deleteTapGestureRecognizer = new TapGestureRecognizer();
            deleteTapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, new Binding
            {
                Source = _viewModel, 
                Path = "DeleteRecipeFromPlannerCommand"
            });
            deleteTapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, "Id");
            deleteButtonImage.GestureRecognizers.Add(deleteTapGestureRecognizer);
              
            deleteButtonFrame.Content = deleteButtonImage;

            var detailsTapGesture = new TapGestureRecognizer();
            detailsTapGesture.SetBinding(TapGestureRecognizer.CommandProperty, new Binding
            {
                Source = _viewModel,
                Path = "GoToRecipeDetailsCommand"
            });
            
            detailsTapGesture.SetBinding(TapGestureRecognizer.CommandParameterProperty, "Id");

            innerframe.GestureRecognizers.Add(detailsTapGesture);
            bottomFrame.GestureRecognizers.Add(detailsTapGesture);

            grid.SetRow(innerframe, 0);
            grid.SetColumnSpan(innerframe, 2);
            grid.Children.Add(innerframe);

            grid.SetRow(bottomFrame, 1);
            grid.SetColumnSpan(bottomFrame, 2);
            grid.Children.Add(bottomFrame);

            grid.SetRow(deleteButtonFrame, 1);
            grid.SetColumn(deleteButtonFrame, 1);
            grid.Children.Add(deleteButtonFrame);

            #endregion frame

            switch (category)
            {
                case PlannerCategory.Breakfast:
                    if (_viewModel.BreakfastRecipe == null)
                    {
                        frame.IsVisible = false;
                        emptyLabel.IsVisible = true;
                        break;
                    } 
                    
                    frame.SetBinding(ItemsView.ItemsSourceProperty, "BreakfastRecipe");
                    frame.IsVisible = true;
                    emptyLabel.IsVisible = false;
                    break;

                case PlannerCategory.Lunch:
                    if (_viewModel.LunchRecipe == null)
                    {
                        frame.IsVisible = false;
                        emptyLabel.IsVisible = true;
                        break;
                    }

                    frame.SetBinding(ItemsView.ItemsSourceProperty, "LunchRecipe");
                    frame.IsVisible = true;
                    emptyLabel.IsVisible = false;
                    break;

                case PlannerCategory.Dinner:
                    if (_viewModel.DinnerRecipe == null)
                    {
                        frame.IsVisible = false;
                        emptyLabel.IsVisible = true;
                        break;
                    }

                    frame.SetBinding(ItemsView.ItemsSourceProperty, "DinnerRecipe");
                    frame.IsVisible = true;
                    emptyLabel.IsVisible = false;
                    break;

                case PlannerCategory.Dessert:
                    if (_viewModel.DessertRecipe == null)
                    {
                        frame.IsVisible = false;
                        emptyLabel.IsVisible = true;
                        break;
                    }

                    frame.SetBinding(ItemsView.ItemsSourceProperty, "DessertRecipe");
                    frame.IsVisible = true;
                    emptyLabel.IsVisible = false;
                    break;

                case PlannerCategory.Other:

                    if (_viewModel.OtherRecipe == null)
                    {
                        frame.IsVisible = false;
                        emptyLabel.IsVisible = true;
                        break;
                    }

                    frame.SetBinding(ItemsView.ItemsSourceProperty, "OtherRecipe");
                    frame.IsVisible = true;
                    emptyLabel.IsVisible = false;
                    break;
                   
            }

            PlanStackLayout.Children.Add(categoryLabel);
            PlanStackLayout.Children.Add(emptyLabel);
            PlanStackLayout.Children.Add(frame);

        }
    }
    
}
