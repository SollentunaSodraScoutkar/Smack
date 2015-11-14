(function () {
    'use strict';

    angular
        .module('myApp.divisionService', [])
        .factory('DivisionService', divisionService);

    divisionService.$inject = ['$http', 'RestService'];

    function divisionService($http, restService) {
        return {
            getAllActive: getAllActive,
        }

        function getAllActive() {
            return $http.get(restService.getPath() + '/divisions');
        };

    }
})();
