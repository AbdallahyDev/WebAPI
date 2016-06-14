/**
 * Created by ab on 22/04/2016.
 */
//the module which build the application and  its dependencies
var app = angular.module('ngCooking', ['ngCookies', 'ngRoute', 'ngSanitize', 'filters', 'servicesModules', 'ngFileUpload']); 
//app configurations
app.config(function ($routeProvider) {   
    $routeProvider
    // route for the home page
        .when('/', {
            templateUrl: 'home.html',
            controller: 'homeController'
        })
        //route for ingredients page
        .when('/ingredients', {
            templateUrl: 'ingredients.html',
            controller: 'ingredientsController'
        })
        //route for inscription
        .when('/inscription', { 
            templateUrl: 'inscription.html', 
            controller: 'inscriptionController'
        })
        //route for communaute page
        .when('/communaute', {
            templateUrl: 'communaute.html',
            controller: 'communauteController'
        })
        //route for communaute_details page
        .when('/communaute_details', {
            templateUrl: 'communaute_details.html',
            controller: 'communauteController'
        })
        //route for recette_detail  page
        .when('/recettes_details', {
            templateUrl: 'recettes_details.html',
            controller: 'recettesDetailsController'
        })
        .when('/recettes_new', {
            templateUrl: 'recettes_new.html',
            controller: 'recettesNewController'
        })
        // route for the recette page recettes_new
        .when('/recettes', {
            templateUrl: 'recettes.html',
            controller: 'recettesController'
        });
});  

