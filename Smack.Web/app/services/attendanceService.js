(function () {
    'use strict';

    angular
        .module('myApp.attendanceService', [])
        .factory('AttendanceService', attendanceService);

    attendanceService.$inject = ['$http', 'RestService'];

    function attendanceService($http, restService) {
        return {
            getAttendance: getAttendance,
            getMemberAttendanceById: getMemberAttendanceById,
            saveMemberAttendance: saveMemberAttendance
        }

        function getAttendance(attendance) {
            return $http.post(restService.getPath() + '/attendance', attendance);
        };

        function getMemberAttendanceById(attendanceId) {
            return $http.get(restService.getPath() + '/attendance/' + attendanceId + '/members');
        };

        function saveMemberAttendance(memberAttendance) {
            $http.put(restService.getPath() + '/attendance/members', memberAttendance);
        };

    }
})();
