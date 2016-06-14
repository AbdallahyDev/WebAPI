/**
 * Created by ab on 26/04/2016.
 */
var filter = angular.module('filters', []);
//the recette filter
filter.filter('findRecetteByName', function () {
    return function (input, params) {
        var listFound = {};
        listFound.recettes = [];
        var j = 0;
        if (params !== undefined) {
            if (params.name !== undefined && params.name !== "") {
                j = 0;
                for (i = 0; i < input.length; i++) {
                    if (((input[i].name).toUpperCase()).indexOf((params.name).toUpperCase()) > -1) {
                        listFound.recettes[j] = input[i];
                        j++;
                    }
                }
                return listFound.recettes;
            } else {

                return input;
            }
        } else {
            return input;
        }
    };
    var inArray = function (needle, haystack) {
        var length = haystack.length;
        for (var i = 0; i < length; i++) {
            if (haystack[i] == needle)
                return true;
        }
        return false;
    }
});
filter.filter('findRecetteByCalValue', function () {
    return function (input, params) {
        var listFound = {};
        listFound.recettes = [];
        var j = 0;
        if (params !== undefined) {
            if ((params.minCaloriesValue != null) && (params.maxCaloriesValue != null )) {
                for (i = 0; i < input.length; i++) {
                    if (input[i] != undefined) {
                        if ((input[i].calories >= params.minCaloriesValue && input[i].calories <= params.maxCaloriesValue) || (input[i].calories <= params.minCaloriesValue) && (input[i].calories >= params.maxCaloriesValue)) {
                            listFound.recettes[j] = input[i];
                            j++;
                        }
                    }
                }
                return listFound.recettes;
            } else {
                return ((params.name === undefined && params.ingredients === undefined)) ? undefined : input;
            }
        } else {
            return undefined;
        }
    };
});
filter.filter('findRecetteByIngs', function () {
    return function (input, params) {
        var listFound = {};
        listFound.recettes = [];
        var j = 0;
        if (params !== undefined) {
            if (params.ingredients !== undefined && params.ingredients !== "") {
                for (i = 0; i < input.length; i++) {
                    if (input[i].ingredients !== undefined) {
                        for (k = 0; k < input[i].ingredients.length; k++) {
                            if (params.ingredients.indexOf(';') > -1) {
                                var tab = params.ingredients.split(';');
                                for (index = 0; index < tab.length; index++) {
                                    if (tab[index] !== "" && ((input[i].ingredients[k].name.toUpperCase()) === (tab[index].toUpperCase()))) {
                                        if (((listFound.recettes).indexOf(input[i])) < 0) {
                                            listFound.recettes[j] = input[i];
                                            j++;
                                        }
                                    }
                                }
                            } else {
                                if ((input[i].ingredients[k].name.toUpperCase()).indexOf(params.ingredients.toUpperCase()) > -1) {
                                    if (((listFound.recettes).indexOf(input[i])) < 0) { 
                                        listFound.recettes[j] = input[i];
                                        j++;
                                    }
                                }
                            }
                        }
                    }
                }
                if (listFound.recettes.length > 0) {
                    return listFound.recettes;
                } else {
                    return input;
                }
            } else {
                return input;
            }
        } else {
            return input;
        }
    };
});
filter.filter('filterByCalValue', function () {
    return function (input, params) {
        var listFound = {};
        listFound.list = [];
        var j = 0;
        if (input !== undefined) {
            if (input.length > 3) {
                for (i = 0; i < input.length; i++) {
                    if ((Math.abs(input[i].calories - params.calories)) < 50) {
                        listFound.list[j] = input[i];
                        j++;
                    }
                }
                return listFound.list;
            } else {
                return input;
            }
        }
    };
});
filter.filter('findIngByName', function () {
    return function (input, params) {
        var listFound = {};
        listFound.ingredients = [];
        var j = 0;
        if (params !== undefined) {
            if (params.name !== undefined && params.name !== "") {
                j = 0;
                for (i = 0; i < input.length; i++) {
                    if (((input[i].name).toUpperCase()).indexOf((params.name).toUpperCase()) > -1) {
                        listFound.ingredients[j] = input[i];
                        listFound.isFound = true;
                        j++;
                    }
                }
                return listFound.ingredients;
            } else {

                return input;
            }
        } else {
            return input;
        }
    };
});
filter.filter('findIngsByCategory', function () {
    return function (input, params) {
        var listFound = {};
        listFound.ingredients = [];
        var j = 0;
        if (params !== undefined) {
            if (params.category !== undefined && params.category.name !== '') {
                if (input !== undefined) {
                    for (k = 0; k < input.length; k++) {
                        if ((input[k].category.toUpperCase()).indexOf(params.category.name.toUpperCase()) > -1) {
                            listFound.ingredients[j] = input[k];
                            listFound.isFound = true;
                            j++;
                        }
                    }
                    return listFound.ingredients;
                }

            } else {
                return input;
            }
        } else {
            return input;
        }
    };
});
filter.filter('findIngByCalValue', function () {
    return function (input, params) {
        var listFound = {};
        listFound.ingredients = [];
        var j = 0;
        if (params === undefined) {// || params.category==="" || params.name!=="" &&params.minCaloriesValue!==undefined&& params.maxCaloriesValue!==undefined) {
            return undefined;
        } else {
            if ((params.minCaloriesValue != null) && (params.maxCaloriesValue != null )) {
                for (i = 0; i < input.length; i++) {
                    if (input[i] != undefined) {
                        if ((input[i].calories >= params.minCaloriesValue) && (input[i].calories <= params.maxCaloriesValue) || (input[i].calories <= params.minCaloriesValue) && (input[i].calories >= params.maxCaloriesValue)) {//input[i].name === params.name ){
                            listFound.ingredients[j] = input[i];
                            listFound.isFound = true;
                            j++;
                        }
                    }
                }
                return listFound.ingredients;
            } else {
                return (params.name === undefined && params.category === undefined) ? undefined : input;
            }
        }
    }
});
filter.filter('filterSameIngByCategory', function () {
    return function (input, params) {
        var listFound = {};
        listFound.ingredients = [];
        var j = 0;
        if (params !== undefined) {
            if (input !== undefined) {
                for (k = 0; k < input.length; k++) {
                    if (input[k].category === params) {
                        listFound.ingredients[j] = input[k];
                        listFound.isFound = true;
                        j++;
                    }
                }
                return listFound.ingredients;
            } else {
                return undefined;
            }
        } else {
            return undefined;
        }
    };
});