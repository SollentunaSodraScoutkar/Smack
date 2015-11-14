(function () {
    'use strict';

    angular
        .module('myApp.divisionService', [])
        .factory('DivisionService', divisionService);

    divisionService.$inject = ['$http', 'RestService'];

    function divisionService($http, restService) {
        return {
            getAllActive: getAllActive,
            getMembers: getMembers
        }

        function getAllActive() {
            return $http.get(restService.getPath() + '/divisions');
        }
        function getMembers(divisionId) {
            return $http.get(restService.getPath() + '/divisions/' + divisionId + '/members');
        }
    }
})();
