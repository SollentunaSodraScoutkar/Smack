(function () {
    'use strict';

    angular
        .module('myApp.authService', [])
        .factory('AuthService', authService);

    authService.$inject = ['$http', 'RestService'];

    function authService($http, restService) {
        return {
            login: login,
            //register: register,
            //requestPasswordReset: requestPasswordReset,
            //resetPassword: resetPassword,
            //verifyEmail: verifyEmail,
            //changePassword: changePassword
        }

        function login(user) {
            return $http.post(restService.getPath() + '/auth/login', user);
        };

        //function register(user) {
        //    return $http.post(restService.getPath() + '/auth/register?hosturl=' + restService.getHostUrl(), user);
        //};

        //function requestPasswordReset(user) {
        //    return $http.put(restService.getPath() + '/auth/password/reset/request?hosturl=' + restService.getHostUrl(), user);
        //}

        //function resetPassword(token, user) {
        //    return $http.put(restService.getPath() + '/auth/password/reset/' + token, user);
        //}

        //function verifyEmail(token) {
        //    return $http.put(restService.getPath() + '/auth/email/verify/' + token);
        //}

        //function changePassword(email, oldpassword, newpassword) {
        //    var passwordChangeData = { email: email, oldpassword: oldpassword, newpassword: newpassword }
        //    return $http.put(restService.getPath() + '/auth/password/change', passwordChangeData);
        //}

    }
})();
