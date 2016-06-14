/**
 * Created by ab on 12/05/2016.
 */
describe('myService test', function () {
    describe('when I call getUserRecettes', function () {
        beforeEach(module('servicesModules'));
        it('returns 1', inject(function (NgCookingServices) {
            var recette1 = {};
            recette1.name = "Tajine";
            recette1.creatorId = 1;
            var recette2 = {};
            recette2.name = "Patte";
            recette2.creatorId = 2;
            var recette3 = {};
            recette3.name = "poulet";
            recette3.creatorId = 3;
            var recettes = [recette1, recette2, recette3];
            var recetteExpected = [recette3];
            expect(NgCookingServices.getUserRecette(recettes, 3)).toEqual(recetteExpected);
        }));

    });
    describe('when I call getRecette in my factories', function () {
        beforeEach(module('servicesModules'));
        /**
         var httpBasedService,
         httpBackend;

         beforeEach(function (){
    // load the module.
    module('myApp');

    // get your service, also get $httpBackend
    // $httpBackend will be a mock, thanks to angular-mocks.js
    inject(function($httpBackend, _httpBasedService_) {
      httpBasedService = _httpBasedService_;
      httpBackend = $httpBackend;
    });
  });

         afterEach(function() {
            $httpBackend.verifyNoOutstandingExpectation();
            $httpBackend.verifyNoOutstandingRequest();
        });*/
        it('returns the list recettes', inject(function (NgCookingFactories, $httpBackend) {

            var $scope = {};
            $httpBackend.when('GET', 'json/recettes.json')
                .respond(200, {foo: 'bar'});


            // var res =NgCookingFactories.getRecettes();
            NgCookingFactories.getRecettes().then(function (data) {
                //alert("ça marche dans recCtrnl!!"+data[0].name);
                $scope.recettes = data;
            });
            $httpBackend.flush();
            //alert("res:....gggdbbms"+res+"  "+";"+""+$scope.response);
            var resExpected = {foo: 'bar'};
            //expect($scope.recette).toEqual(resExpected);
            //expect(res).toEqual(recetteExpected);
        }));

    });
    /*describe("reddit api service", function () {
     var NgCookingFactories, httpBackend;

     beforeEach(module("serviceModule"));

     beforeEach(inject(function (NgCookingFactories, $httpBackend) {
     NgCookingFactories = NgCookingFactories;
     httpBackend = $httpBackend;
     }));

     it("should do something", inject(function (NgCookingFactories, $httpBackend) {
     $httpBackend.when('GET', 'json/recettes.json')
     .respond(200, { foo: 'bar' });
     NgCookingFactories.getRecettes().then(function (data) {
     //alert("ça marche dans recCtrnl!!"+data[0].name);
     $scope.recettes = data;
     //expect(subreddits).toEqual(["golang", "javascript"]);
     });
     httpBackend.flush();
     }));

     });*/

    describe('MyController', function () {
        var $httpBackend, $rootScope, createController, authRequestHandler;

        // Set up the module
        beforeEach(module('ngCooking'));

        beforeEach(inject(function ($injector) {
            // Set up the mock http service responses
            $httpBackend = $injector.get('$httpBackend');
            // backend definition common for all tests
            authRequestHandler = $httpBackend.when('GET', 'json/recettes.json')
                .respond({userId: 'userX'}, {'A-Token': 'xxx'});

            // Get hold of a scope (i.e. the root scope)
            $rootScope = $injector.get('$rootScope');
            // The $controller service is used to create instances of controllers
            var $controller = $injector.get('$controller');

            createController = function () {
                return $controller('recettesController', {'$scope': $rootScope});
            };
        }));


        afterEach(function () {
            $httpBackend.verifyNoOutstandingExpectation();
            $httpBackend.verifyNoOutstandingRequest();
        });


        it('should fetch authentication token', function () {
            $httpBackend.expectGET('json/recettes.json');
            var controller = createController();
            $httpBackend.flush();
        });


        it('should fail authentication', function () {

            // Notice how you can change the response even after it was set
            authRequestHandler.respond(401, '');

            $httpBackend.expectGET('json/recettes.json');
            var controller = createController();
            $httpBackend.flush();
            // expect($rootScope.status).toBe('Failed...');
        });
    });
});