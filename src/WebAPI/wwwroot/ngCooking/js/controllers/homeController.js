/**
 * Created by ab on 27/04/2016.
 */

app.controller('homeController', function ($scope, $http, $location, $route, $cookies, NgCookingFactories, NgCookingServices) {
    
    //$http.get('http://localhost:59317/api/Comment/1').then(function (response) {
        //alert("comment size:"+response.data.length);  
    //});
    $scope.isIdentify = false;
    $scope.sortedRecettes = []; 
    $scope.user = {};
    NgCookingFactories.getCommunaute().then(function (data) {  
        $scope.communautes = data;  
    }).catch(function () {
        $scope.error = 'unable to get the recette list'; 
    });
    $scope.init = function () {
        $scope.isActive($location.path()); 
        $scope.select($location.path());
    };
    NgCookingServices.getState(); 
    $scope.signIn = function () {
        $scope.errorMessage = "";
        $http({
            method: 'POST',
            url: 'http://localhost:59317/api/Communaute/Login',  
            data: $scope.user
        }).then(function successCallback(response) { 
            // this callback will be called asynchronously
            // when the response is available
            alert(response.data);
            $scope.isIdentify = true;
            $cookies.put('login', $scope.user.id); 
            $cookies.put('isConnected', true);
            if ($scope.isIdentify === false) {
                $scope.errorMessage = "L'identifiant ou le mot de passe est incorrect";
            }
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status. 
            alert("Error!" + response.error);
        });
      
        
    };
    $scope.signOut = function () {
        $http({
            method: 'POST',
            url: 'http://localhost:59317/api/Communaute/Logout'
        }).then(function successCallback(response) {
            // this callback will be called asynchronously
            // when the response is available
            alert(response.data);
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status. 
            alert("Error!" + response.error);
        });
        $cookies.remove('login');
        $cookies.remove('psw');
        $cookies.remove('isConnected');
    };
    $scope.getIsConnected = function () {
        return $cookies.get('isConnected');
    }

    //**************The active item
    $scope.isActive = function (item) {
        var result = $scope.selected === item;
        return result;
    };
    //The select item
    $scope.select = function (item) {
        $scope.selected = item;

    };
    NgCookingFactories.getRecettes().then(function (data) {
        $scope.recettes = data;   
        $scope.sortRecettes(); 
    }).catch(function () {
        $scope.error = 'unable to get the data';
    });

    $scope.addToCookie = function () {
        $cookies.remove('currentUserId');
        var id;
        for (i = 0; i < $scope.communautes.length; i++) {
            if ($cookies.get('login') === $scope.communautes[i].email) {
                id = $scope.communautes[i].id; 
            }
        }
        $cookies.put('currentUserId', id);
    }
    // for sorting the data in order to give theme the notes
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
            if (($scope.recettes[i].comments.length>0)) {
                for (j = 0; j < $scope.recettes[i].comments.length; j++) {  
                    notesSum += $scope.recettes[i].comments[j].mark;
                }
                recette.notes = notesSum / ($scope.recettes[i].comments.length); 
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
    }
    $scope.getNote = function (comments) {
        var notesSum = 0;
        if (comments == null) {
            return 0;
        }
        else {
            for (i = 0; i < comments.length; i++) {
                notesSum += comments[i].mark;
            }
            alert("notes:" + notesSum / comments.length);
            var i;
            for (i = 0; i < Math.round(notesSum / (comments.length)); i++) {
                $scope.stars[i] = "star";
            }
            alert("elemt" + stars[0])
            return notesSum / (comments.length);
        }
    }
    $scope.addRecetteToCookie = function (id) {
        $cookies.remove('currentRecetteId');
        $cookies.put('currentRecetteId', id);
    }
});