app.controller('inscriptionController', function ($scope, $http, $window, $cookies, NgCookingFactories) {
    $scope.newUser = {};
    $scope.addUser = function () {
        $scope.newUser.picture = "pic";
        $scope.newUser.level = 3;
        $http({
            method: 'POST',
            url: 'http://localhost:59317/api/Communaute/Register', 
            data: $scope.newUser
        }).then(function successCallback(response) {
            // this callback will be called asynchronously
            // when the response is available
            alert(response.data);
            $window.location.href = '/ngCooking/index.html#/';
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
            alert("Error!" + response.error); 
        });
        
    }


});