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

    attendanceController.$inject = ['$window', '$location', 'DivisionService', 'AttendanceService'];

    function attendanceController($window, $location, divisionService, attendanceService) {

        var vm = this;
        vm.user = JSON.parse($window.sessionStorage.getItem('user'));

        vm.getDivisions = function () {
            divisionService.getAllActive()
                .then(function (response) {
                    vm.divisions = response.data;
                });
        }

        vm.getDivisions();
        vm.attendance = {};

        vm.getMembers = function () {
            divisionService.getMembers(vm.attendance.intDivisionId)
                .then(function (response) {
                    vm.members = response.data;
                });
            attendanceService.getAttendance(vm.attendance)
                .then(function (response) {
                    var s = response.data;
                    s.dtmAttendanceDate = new Date(s.dtmAttendanceDate);
                    vm.attendance = s;
                });

        }

        vm.attend = function (member) {
            member.blnAttend = true;
        }
        
        vm.unattend = function (member) {
            member.blnAttend = false;
        }

        vm.toggleAttend = function (member) {
            member.blnAttend = !member.blnAttend;
        }

        vm.setToday = function (event) {
            event.preventDefault();
            vm.attendance.dtmAttendanceDate = new Date();
        }

    }
})();