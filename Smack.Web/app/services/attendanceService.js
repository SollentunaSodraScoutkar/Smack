(function () {
    'use strict';

    angular
        .module('myApp.attendanceService', [])
        .factory('AttendanceService', attendanceService);

    attendanceService.$inject = ['$http', 'RestService'];

    function attendanceService($http, restService) {
        return {
            getAttendance: getAttendance,
            getMemberAttendanceByDivisionId: getMemberAttendanceByDivisionId
        }

        function getAttendance(attendance) {
            return $http.post(restService.getPath() + '/attendance', attendance);
        };

        function getMemberAttendanceByDivisionId(divisionId) {
            return $http.get(restService.getPath() + '/attendance/' + divisionId + '/members');
        };

    }
})();
