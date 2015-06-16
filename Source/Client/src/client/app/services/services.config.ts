module app.services {
    'use strict';

    function restangularConfig(restangularProvider: restangular.IProvider) {

        restangularProvider.setBaseUrl('http://localhost:4868/api');
    }

    restangularConfig.$inject = ['RestangularProvider'];

    angular
        .module('app.services')
        .config(restangularConfig);

}