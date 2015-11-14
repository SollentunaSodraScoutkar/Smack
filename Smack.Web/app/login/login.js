(function () {
    'use strict';

    angular
        .module('myApp.login', ['ngRoute'])
        .config([
            '$routeProvider', function ($routeProvider) {
                $routeProvider.when('/login', {
                    templateUrl: 'login/login.html',
                    controller: 'LoginController',
                    controllerAs: 'vm'
                });
            }
        ]);

    angular
        .module('myApp.login')
        .controller('LoginController', loginController);

    loginController.$inject = ['$window', '$location', 'AuthService'];

    function loginController($window, $location, authService) {

        var vm = this;
        vm.login = login;

        (function initController() {
            // reset login status
            //TODO: do we need to clear anything on the server side?
            $window.sessionStorage.removeItem('token');
            $window.sessionStorage.removeItem('user');
        })();

        //Note: This method differs from the userapp version
        function login() {
            vm.dataLoading = true;
            return authService.login(vm.user)
                .then(function (response) {

                        $window.sessionStorage.setItem('token', response.data.token);
                        $window.sessionStorage.setItem('user', JSON.stringify(response.data.user));
                        $location.path('/attendance');
                })
                .catch(function (reponse) {
                    vm.error = "Fel användarnamn eller lösenord";
                    vm.dataLoading = false;
                });
        }
    }
})();