(function () {
    'use strict';

    angular
        .module('myApp.dashboard', ['ngRoute'])
        .config([
            '$routeProvider', function ($routeProvider) {
                $routeProvider.when('/dashboard', {
                    templateUrl: 'dashboard/dashboard.html',
                    controller: 'DashboardController',
                    controllerAs: 'vm'
                });
            }
        ]);

    angular
    .module('myApp.dashboard')
    .controller('DashboardController', dashboardController);

    dashboardController.$inject = ['$window', '$scope', '$location'];

    function dashboardController($window, $scope, $location) {
        var vm = this;
    }
})();