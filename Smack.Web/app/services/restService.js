(function () {
    'use strict';

    angular
        .module('myApp.restService', [])
        .factory('RestService', restService);

    restService.$inject = ['$location'];

    function restService($location) {


        return {
            getPath: getPath,
            getHostUrl: getHostUrl,
            getHostOrigin: getHostOrigin
        }


        function getHostOrigin() {
                return "UserGui";
        }

        function getHostUrl() {
            var absUrl = $location.absUrl();
            return absUrl.substring(0, absUrl.indexOf('#'));
        }


        function getPath() {
            var host = $location.host();
            if (host === 'localhost') {
                return 'http://localhost:51151/smack';
            }
            return 'unknown host for REST API';
        };

    }
})();