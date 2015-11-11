
(function () {
    'use strict';

    angular
        .module('myApp')
        .factory('AuthInterceptorService', authInterceptorService);

    authInterceptorService.$inject = ['$window', '$q', '$location'];

    function authInterceptorService($window, $q, $location) {
        return {
            request: request,
            response: response
        }

        function request(config) {
            config.headers = config.headers || {};
            if ($window.sessionStorage.getItem('token')) {
                config.headers.Authorization = 'Token ' + $window.sessionStorage.getItem('token');
            }
            return config || $q.when(config);
        };

        function response(res) {
            if (res.status === 401) {
                $location.path('/login');
            }
            return res || $q.when(res);
        }
    }
})();