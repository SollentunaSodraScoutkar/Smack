(function () {
    'use strict';

    angular
        .module('myApp.memberService', [])
        .factory('MemberService', memberService);

    memberService.$inject = ['$http', 'RestService'];

    function memberService($http, restService) {
        return {
            getAllActive: getAllActive,
        }

        function getAllActive() {
            return $http.get(restService.getPath() + '/members');
        };

    }
})();
