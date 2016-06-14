/**
 * Created by ab on 27/04/2016.
 */
app.controller('ingredientsController', function ($scope, $http, $cookies, NgCookingFactories) {

    $scope.ingredients = [];
    $scope.filtredIngs = [];
    $scope.categories = [];
    //getting the ingredients list
    NgCookingFactories.getIngredients().then(function (data) {
        $scope.ingredients = data;

    }).catch(function () {
        $scope.error = 'unable to get the recette list';
    });
    //getting the categories list 
    NgCookingFactories.getCategories().then(function (data) {
        $scope.categories = data;
    }).catch(function () {
        $scope.error = 'unable to get the recette list';
    });
    //get the width for adapting the battery percentage 
    $scope.getWidth = function (cal) {
        var maxCalories = $scope.getMaxCalories();
        return 100 * (cal / maxCalories);
    };
    $scope.getMaxCalories = function () {
        var maxCalories;
        if ($scope.ingredients !== undefined) {
            maxCalories = $scope.ingredients[0].calories;
            for (i = 0; i < $scope.ingredients.length; i++) {

                if ($scope.ingredients[i].calories > maxCalories) {
                    maxCalories = $scope.ingredients[i].calories;
                }
            }
        }
        return maxCalories;
    };
    $scope.limitNbr;
    $scope.ShowMoreIng = function () {
        $scope.limitNbr += 10;
    };
}); 