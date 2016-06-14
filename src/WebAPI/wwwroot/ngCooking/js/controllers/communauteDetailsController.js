/**
 * Created by ab on 27/04/2016.
 */
app.controller('communauteDetailsController', function ($scope, $http, $cookies, NgCookingFactories, NgCookingServices) {

    $scope.getReceete = function () {
        var id = $cookies.get('currentRecetteId');
        for (i = 0; i < $scope.sortedRecettes.length; i++) {
            if (($scope.sortedRecettes[i].id) === id) {
                $scope.recDetail = $scope.sortedRecettes[i];
            }
        }
    };

    NgCookingFactories.getRecettes().then(function (data) {
        $scope.recettes = data;
        $scope.recettes = NgCookingServices.sortRecettes($scope.recettes);
    }).catch(function () {
        $scope.error = 'unable to get the data';
    });
    NgCookingFactories.getCommunaute().then(function (data) {
        $scope.communautes = data;
        $scope.getUser();
        $scope.getUserRecette();
    }).catch(function () {
        $scope.error = 'unable to get the recette list';
    });


    $scope.getUserById = function () {
        var id = $cookies.get('currentUserId');
    };

    $scope.getUser = function () {
        var id = $cookies.get('currentUserId');
        for (i = 0; i < $scope.communautes.length; i++) {
            if (($scope.communautes[i].id) == id) {
                $scope.userDetail = $scope.communautes[i];
            }
        }
    };
    $scope.getUserRecette = function () {

        var id = $cookies.get('currentUserId');
        $scope.userDetail.recettes = [];
        var k = 0;
        for (i = 0; i < $scope.recettes.length; i++) {
            if (($scope.recettes[i].creatorId) == id) {
                $scope.userDetail.recettes[k] = $scope.recettes[i];
                k++;
            }
        }
    }
    $scope.getAge = function (birth_year) {
        var today_date = new Date();
        var today_year = today_date.getFullYear();
        var age = today_year - birth_year;
        return age;
    };
    $scope.getIsConnected = function () {
        return $cookies.get('isConnected');
    }

});