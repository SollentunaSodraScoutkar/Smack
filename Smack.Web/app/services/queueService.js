(function () {
    'use strict';

    angular
        .module('myApp.queueService', [])
        .factory('QueueService', queueService);

    queueService.$inject = ['$http', 'RestService'];

    function queueService($http, restService) {
        return {
            add: add
        }

        function add(queueMember) {
            return $http.post(restService.getPath() + '/queuemembers', queueMember);
        };

    }
})();
