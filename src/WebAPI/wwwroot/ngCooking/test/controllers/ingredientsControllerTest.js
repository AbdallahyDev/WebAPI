/**
 * Created by ab on 12/05/2016.
 */
describe('communauteController', function () {
    beforeEach(module('ngCooking'));

    var $controller;

    beforeEach(inject(function (_$controller_) {
        // The injector unwraps the underscores (_) from around the parameter names when matching
        $controller = _$controller_;
    }));

    describe('$scope.getCanon', function () {
        it('for getting the canon in oreder to sort the communaute ', function () {
            var $scope = {};
            var $http = {};
            var $cookies, NgCookingFactories, NgCookingServices = {};
            var controller = $controller('communauteController', {
                $scope: $scope,
                $http: $http,
                $cookies: $cookies,
                NgCookingFactories: NgCookingFactories,
                NgCookingServices: NgCookingServices
            });
            $scope.canon = 'byname';
            var res = $scope.getCanon();
            expect(res).toEqual('itsworks');
        });
    });
    describe('$scope.getLevel', function () {
        it('for getting the level of an user', function () {
            var $scope = {};
            var $http = {};
            var $cookies, NgCookingFactories, NgCookingServices = {};
            var controller = $controller('communauteController', {
                $scope: $scope,
                $http: $http,
                $cookies: $cookies,
                NgCookingFactories: NgCookingFactories,
                NgCookingServices: NgCookingServices
            });
            //$scope.canon = 'byname';
            var resNovice = $scope.getLevel(1);
            expect(resNovice).toEqual('NOVICE');
            var resConfirme = $scope.getLevel(2);
            expect(resConfirme).toEqual('CONFIRMÉ');
            var resExpert = $scope.getLevel(3);
            expect(resExpert).toEqual('EXPERT');
        });
    });
    describe('$scope.getStars', function () {
        it('for getting the starts number of an user', function () {
            var $scope = {};
            var $http = {};
            var $cookies, NgCookingFactories, NgCookingServices = {};
            var controller = $controller('communauteController', {
                $scope: $scope,
                $http: $http,
                $cookies: $cookies,
                NgCookingFactories: NgCookingFactories,
                NgCookingServices: NgCookingServices
            });
            var resExpected = ["EXPERT", "EXPERT", "EXPERT"];
            var resEffective = $scope.getStars(3);
            expect(resEffective).toEqual(resExpected);
        });
    });
    describe('$scope.addToCookie()', function () {
        it('for adding a currrent user id in the cookies', function () {
            var $scope = {};
            var $http = {};
            var $cookies, NgCookingFactories, NgCookingServices = {};
            var controller = $controller('communauteController', {
                $scope: $scope,
                $http: $http,
                $cookies: $cookies,
                NgCookingFactories: NgCookingFactories,
                NgCookingServices: NgCookingServices
            });
            var id = 5;
            var resExpected = id;
            //$scope.addToCookie(id);
            inject(function ($cookies) {
                // expect(cookies.get('currentUserId')).toBe(5);
            });
            // $scope.addToCookie(id);
            //var resEffective= $cookies.get('currentUserId');
            // expect(resEffective).toEqual(resExpected);
        });
    });
});