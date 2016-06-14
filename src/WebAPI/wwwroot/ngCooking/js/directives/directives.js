/**
 * Created by ab on 27/04/2016.
 */
//directive for page header
app.directive('headerSection', function () {
    return {
        restrict: 'E',
        templateUrl: 'header_section.html'
    };
});
//directive for page footer
app.directive('footerSection', function () {
    return {
        restrict: 'E',
        templateUrl: 'footer_section.html'
    };
});

//directive to load the file that contains the pic of the new recette
app.directive('customOnChange', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var onChangeHandler = scope.$eval(attrs.customOnChange);
            element.bind('change', onChangeHandler);
        }
    };
});

//directive for getting the best recettes
app.directive('bestRecettes', function () {
    return {
        restrict: 'E',
        templateUrl: 'best_recettes.html'
    };
});

//directive for getting the new recettes
app.directive('newRecettes', function () {
    return {
        restrict: 'E',
        templateUrl: 'new_recettes.html'
    };
});