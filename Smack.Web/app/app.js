(function () {

    'use strict';

    // Declare app level module which depends on views, and components
    angular
        .module('myApp', [
            'ui.bootstrap',
            'ngRoute',
            'myApp.login',
            'myApp.dashboard',
            'myApp.attendance',
            'myApp.restService',
            'myApp.authService',
            'myApp.divisionService'
        ])
        .config(config)
        .run(run);

    config.$inject = ['$httpProvider', '$routeProvider'];
    function config($httpProvider, $routeProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
        $httpProvider.interceptors.push('AuthInterceptorService');
        $routeProvider.otherwise({ redirectTo: '/attendance' });
    }


    run.$inject = ['$rootScope', '$location', '$window'];
    function run($rootScope, $location, $window) {
        //// keep user logged in after page refresh
        //$rootScope.globals = $cookieStore.get('globals') || {};
        //if ($rootScope.globals.currentUser) {
        //    $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata; // jshint ignore:line
        //}

        $rootScope.showMenu = 0;

        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            // redirect to login page if not logged in and trying to access a restricted page
         //   var restrictedPage = !_.contains(['/login', '/registerUser', '/requestPasswordReset', '/passwordReset', '/companySelect'], $location.path());
            var restrictedPage = !_.contains(['/login'], $location.path());
            var loggedIn = $window.sessionStorage.getItem('token');
            if (restrictedPage) {
                $rootScope.showMenu = 1;
            } else {
                $rootScope.showMenu = 0;
            }
            if (restrictedPage && !loggedIn) {
                $location.path('/login');
            }
            //else if (restrictedPage && loggedIn) {
            //    var user = JSON.parse($window.sessionStorage.getItem('user'));
            //    if (!user.isAdmin) {
            //        $location.path('/login');
            //    }
            //}
        });
    }
})();

