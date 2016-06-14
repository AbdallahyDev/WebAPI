/**
 * Created by ab on 27/04/2016.
 */
app.controller('communauteController', function ($scope, $http, $cookies, NgCookingFactories, NgCookingServices) {

    NgCookingFactories.getCommunaute().then(function (data) {
        $scope.communautes = data;
        $scope.setUsersRecettes();

    }).catch(function () {
        $scope.error = 'unable to get the recette list';
    });
    NgCookingFactories.getRecettes().then(function (data) {
        //alert("ça marche dans recCtrnl!!"+data[0].name);
        $scope.recettes = data;
    }).catch(function () {
        $scope.error = 'unable to get the data'; 
    });

    $scope.setUsersRecettes = function () {
        for (var i = 0; i < $scope.communautes.length; i++) {
            $scope.communautes[i].recettes = NgCookingServices.getUserRecette($scope.recettes, $scope.communautes[i].id);

        }
    };
    $scope.numberAllowed = 8;
    $scope.getLevel = function (level) {
        var res;
        switch (level) {
            case 1:
                res = "NOVICE";
                break;
            case 2:
                res = "CONFIRMÉ";
                break;
            case 3:
                res = "EXPERT";
                break;
            default:
                break;
        }
        return res;

    }
    $scope.getStars = function (level) {
        var res;
        switch (level) {
            case 1:
                res = ["NOVICE"];
                break;
            case 2:
                res = ["CONFIRMÉ", "CONFIRMÉ"];
                break;
            case 3:
                res = ["EXPERT", "EXPERT", "EXPERT"];
                break;
            default:
                break;
        }
        return res;

    }
    $scope.canon = '';
    $scope.getCanon = function () {
        return $scope.canon;
    };
    $scope.addToCookie = function (id) {
        $cookies.remove('currentUserId');
        $cookies.put('currentUserId', id);
    };

});