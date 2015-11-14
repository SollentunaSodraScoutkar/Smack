(function () {
    'use strict';

    angular
        .module('myApp.attendance', ['ngRoute'])
        .config([
            '$routeProvider', function ($routeProvider) {
                $routeProvider.when('/attendance', {
                    templateUrl: 'attendance/attendance.html',
                    controller: 'AttendanceController',
                    controllerAs: 'vm'
                });
            }
        ]);

    angular
        .module('myApp.attendance')
        .controller('AttendanceController', attendanceController);

    attendanceController.$inject = ['$window', '$location', 'DivisionService'];

    function attendanceController($window, $location, divisionService) {

        var vm = this;
        vm.user = JSON.parse($window.sessionStorage.getItem('user'));

        vm.getDivisions = function () {
            divisionService.getAllActive()
                .then(function (response) {
                    vm.divisions = response.data;
                });
        }

        vm.getDivisions();
    }
})();