(function () {
    'use strict';

    angular
        .module('myApp.addQueueMember', ['ngRoute'])
        .config([
            '$routeProvider', function ($routeProvider) {
                $routeProvider.when('/addqueuemember', {
                    templateUrl: 'addqueuemember/addQueueMember.html',
                    controller: 'AddQueueMemberController',
                    controllerAs: 'vm'
                });
            }
        ]);

    angular
        .module('myApp.attendance')
        .controller('AddQueueMemberController', addQueueMemberController);

    addQueueMemberController.$inject = ['$window', '$location', 'QueueService'];

    function addQueueMemberController($window, $location, queueService) {

        var vm = this;

        vm.add = function (queueMember) {
            queueService.add(queueMember);
        }

    }
})();