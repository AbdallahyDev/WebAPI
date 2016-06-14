/**
 * Created by ab on 12/05/2016.
 */
describe('findRecetteByName', function () {

    var $filter;
    beforeEach(module('filters'));

    var $controller;

    beforeEach(inject(function (_$controller_) {
        // The injector unwraps the underscores (_) from around the parameter names when matching
        $controller = _$controller_;
    }));

    beforeEach(inject(function (_$filter_) {
        $filter = _$filter_;
    }));

    it('returns 0 when given null', function () {
        var findRecetteByName = $filter('findRecetteByName');

        var recette1 = {};
        recette1.name = "Tajine";
        var recette2 = {};
        recette2.name = "Patte";
        var recette3 = {};
        recette3.name = "poulet";
        var recettes = [recette1, recette2, recette3];
        var recetteExpected = [recette1];
        expect(findRecetteByName(recettes, recette1)).toEqual(recetteExpected);
    });
});
