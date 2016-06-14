/**
 * Created by ab on 27/04/2016.
 */
app.controller('recettesNewController', function ($scope, $http, $cookies, NgCookingFactories,Upload, $timeout) {

    $scope.i;
    $scope.newRec = {};
    $scope.IngCategory = {};
    $scope.ing = {};
    $scope.ings = [];
    $scope.ingsNumber = 0;
    //$scope.newRec.picture;
    $scope.newRec.calories = 0;
    $scope.newRec.ingredients = [];
    $scope.itemsIng = [];
    $scope.isValid; 
    //$scope.picFile = [];
    NgCookingFactories.getCommunaute().then(function (data) { 
        $scope.communautes = data; 
    }).catch(function () {
        $scope.error = 'unable to get the recette list'; 
    });
    //getting the categories list 
    NgCookingFactories.getCategories().then(function (data) {
        $scope.categories = data;
    }).catch(function () {
        $scope.error = 'unable to get the recette list'; 
    });
    //get the ingredients list
    NgCookingFactories.getIngredients().then(function (data) {
        $scope.ingredients = data;
    }).catch(function () {
        $scope.error = 'unable to get the recette list';
    });
    $scope.addToIng = function () {
        if ($scope.ing.name !== undefined) {
            if ($scope.newRec.ingredients.length > 0) {
                $scope.newRec.ingredients[$scope.newRec.ingredients.length] = $scope.ing;
            } else {
                $scope.newRec.ingredients[0] = $scope.ing;
            }
            $scope.ingsNumber = $scope.newRec.ingredients.length;
            $scope.updateCaloriesValue();
        }
    };
    $scope.updateCaloriesValue = function () {
        $scope.newRec.calories = 0;
        for (i = 0; i < $scope.newRec.ingredients.length; i++) {
            $scope.newRec.calories += $scope.newRec.ingredients[i].calories; 
        }
    }
    $scope.removeFromIng = function (ing) {
        var index = $scope.newRec.ingredients.indexOf(ing);
        if (index > -1) {
            $scope.newRec.ingredients.splice(index, 1);
        }
        $scope.ingsNumber = $scope.newRec.ingredients.length; 
        $scope.updateCaloriesValue();   
    };
    $scope.addNewRec = function (file) {
       
        if ($scope.ingsNumber >= 3) {
            $scope.isValid = true;
            $scope.isInvalid = false;
            $scope.newRec.creatorId = parseInt(getUserId());
            $scope.newRec.category = $scope.newRec.category.name;
            $scope.newRec.picture = file[0];   
            $http({
                method: 'POST',
                url: 'http://localhost:59317/api/Recette/Uploads',
                data: $scope.newRec
            }).then(function successCallback(response) { 
                // this callback will be called asynchronously
                // when the response is available
                alert("recette is added" + response.data);
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                alert("Error!" + response.error); 
            });
        } else { 
            alert("Il faut choisir au moins trois ingrédients pour votre recette");
            $scope.isValid = false;  
            $scope.isInvalid = true;
        }
    }
    

    $scope.fillIng = function () {
        if ($scope.ingredients !== undefined) {
            var k = 0;
            $scope.itemsIng = [];
            for (j = 0; j < $scope.ingredients.length; j++) {
                if ($scope.IngCategory.name !== undefined) {
                    if ($scope.IngCategory.name === $scope.ingredients[j].category) {
                        $scope.itemsIng[k] = $scope.ingredients[j];  
                        k++;
                    }
                } else {
                    $scope.itemsIng[k] = $scope.ingredients[j]; 
                    k++;
                }
            }
        }
    };
    $scope.fillCategoryTab = function () {
        for (i = 0; i < $scope.ingredients.length; i++) {
            //$scope.itemsCategory[i]
        }
    };
    //method to upload file which contains the picture path  
    $scope.uploadFile = function (event) {
        var files = event.target.files;
        if (files[0] !== undefined) {
            $scope.newRec.picture = event.target.files[0].name;

        }
    };
    //add user to cookies the user in order to show his profil detail
    $scope.addToCookie = function () {
        
        var id=getUserId();
        if (id > 0) {
            $cookies.remove('currentUserId');
            $cookies.put('currentUserId', id);
        } 
        
    };
    var getUserId = function () {
        var login = $cookies.get('login');
        var id;
        for (var i = 0; i < $scope.communautes.length; i++) {   
            if($scope.communautes[i].username === login) 
                 id=$scope.communautes[i].id;
        }
        return id;
    }

    //function to upload image file 
    $scope.uploadPic = function (file) {
       
        //file.upload = Upload.upload({
        //    url: 'https://angular-file-upload-cors-srv.appspot.com/upload', 
        //    data: { username: "abd", file: file },
        //});
        //file.upload.then(function (response) {
        //    $timeout(function () {
        //        file.result = response.data;  
        //    });
        //}, function (response) {
        //    if (response.status > 0)
        //        $scope.errorMsg = response.status + ': ' + response.data;
        //}, function (evt) {
        //    file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total)); 
        //});
        return file.result;
    }
});