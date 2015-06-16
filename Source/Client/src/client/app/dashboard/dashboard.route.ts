module app.dashboard {
    'use strict';

    function configureStates($stateProvider: ng.ui.IStateProvider) {

        var states = [
            {
                state: 'dashboard',
                config: {
                    url: '/',
                    templateUrl: 'app/dashboard/dashboard.html',
                    controller: 'DashboardController',
                    controllerAs: 'vm',
                    title: 'Dashboard',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Dashboard'
                    }
                }
            }];

        states.forEach(s => { $stateProvider.state(s.state, s.config); });
    }
    configureStates.$inject = ['$stateProvider'];

    angular
        .module('app.dashboard')
        .config(configureStates); /* @ngInject */

}
