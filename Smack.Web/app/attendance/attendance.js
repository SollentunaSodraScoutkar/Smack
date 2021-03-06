﻿(function () {
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
            if (vm.attendance.intDivisionId) {
                attendanceService.getAttendance(vm.attendance)
                    .then(function (response) {
                        var s = response.data;
                        s.dtmAttendanceDate = new Date(s.dtmAttendanceDate);
                        vm.attendance = s;
                    })
                    .then(function () {
                        attendanceService.getMemberAttendanceById(vm.attendance.intAttendanceId)
                            .then(function (response) {
                                vm.memberAttendances = response.data;
                            });
                    })
            }
        }

        vm.attend = function (member) {
            member.blnAttend = true;
            attendanceService.saveMemberAttendance(member);
        }

        vm.unattend = function (member) {
            member.blnAttend = false;
            attendanceService.saveMemberAttendance(member);
        }

        vm.toggleAttend = function (member) {
            member.blnAttend = !member.blnAttend;
            attendanceService.saveMemberAttendance(member);
        }

        vm.setToday = function (event) {
            event.preventDefault();
            vm.attendance.dtmAttendanceDate = new Date();
        }

        vm.confirm = function () {
            vm.attendance.blnConfirmed = true;
            attendanceService.updateAttendance(vm.attendance).then(function() {
                vm.message = "Listan är klar!";
            });
        }

        vm.attendAll = function () {
            vm.memberAttendances.forEach(function (member) {
                vm.attend(member);
            });
        }
        vm.unattendAll = function () {
            vm.memberAttendances.forEach(function (member) {
                vm.unattend(member);
            });
        }
    }
})();