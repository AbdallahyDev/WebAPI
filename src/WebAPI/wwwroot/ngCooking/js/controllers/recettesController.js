/**
 * Created by ab on 27/04/2016.
 */
app.controller('recettesController', function ($scope, $http, NgCookingFactories, $cookies) {
    $scope.recetteTofound = {};
    $scope.nbrPermitted;
    $scope.recetteFound = [];
    $scope.recettesFiltred = [];
    $scope.filtredRecettes = [];
    $scope.recettes = [];
    $scope.isFound = false;
    isConnected = true;
    $scope.recsArray = [];
    $scope.sortedRecettes = [];
    NgCookingFactories.getRecettes().then(function (data) {
        $scope.recettes = data;
        $scope.sortRecettes();
    }).catch(function () {
        $scope.error = 'unable to get the poneys';
    });
    /*/à supprimer
     $scope.show = function () {
     //if ($scope.recetteTofound.name!==undefined && $scope.recetteTofound.ingredients!==undefined && $scope.recetteTofound.minCaloriesValue!==undefined  ){
     if (isEmpty($scope.recetteTofound)) {
     return true;
     } else {
     return false;
     }
     }*/
    var isEmpty = function (obj) {
        // null and undefined are "empty"
        if (obj == null) return true;
        // Assume if it has a length property with a non-zero value
        // that that property is correct.
        if (obj.length > 0)    return false;
        if (obj.length === 0)  return true;
        // Otherwise, does it have any properties of its own?
        // Note that this doesn't handle
        for (var key in obj) {
            if (hasOwnProperty.call(obj, key)) return false;
        }
        return true;
    }
    $scope.findList = function (input, params) {
        var listFound = [];
        var j = 0;
        var result = [];
        if (input != "undefined") {
            if (params != "undefined") {
                for (i = 0; i < input.length; i++) {
                    if (input[i].name === params.name) {
                        listFound[j] = input[i];
                        j++;
                    } else {
                        if ((params.ingrediets) !== undefined) {
                            result = (params.ingrediets).split(';');
                        }
                    }
                }
            }
        }
        if (listFound.length != 0) {
            $scope.isFound = true;
        } else {
            $scope.isFound = false;
        }
        return listFound;
    };
    $scope.sortRecettes = function () {
        var notesSum = 0;
        var i, j, k;
        var recette = {};
        for (i = 0; i < $scope.recettes.length; i++) {
            recette = {};
            notesSum = 0;
            var stars = [];
            recette.name = $scope.recettes[i].name;
            recette.picture = $scope.recettes[i].picture;
            recette.date = new Date().toUTCString();
            recette.calories = $scope.recettes[i].calories;
            recette.creatorId = $scope.recettes[i].creatorId;
            recette.id = $scope.recettes[i].id;
            recette.ingredients = $scope.recettes[i].ingredients;
            if (($scope.recettes[i].comments != null)) {
                for (j = 0; j < $scope.recettes[i].comments.length; j++) {
                    notesSum += $scope.recettes[i].comments[j].mark;
                }
                recette.notes = notesSum / ( $scope.recettes[i].comments.length);
                recette.nbrHearts = Math.round(recette.notes);
                for (k = 0; k < recette.nbrHearts; k++) {
                    stars[k] = "star";
                }
            } else {
                recette.notes = 0;
                recette.nbrHearts = 0;
            }
            recette.stars = stars;
            $scope.sortedRecettes[i] = recette;
        }
    };
    $scope.addRecetteToCookie = function (id) {
        $cookies.remove('currentRecetteId');
        $cookies.put('currentRecetteId', id);
    };
}); 