/// <reference path="../Typings/AngularJS/angular.d.ts" />
/// <reference path="../Typings/AngularJS/angular-route.d.ts" />
/// <reference path="../Typings/Restangular/restangular.d.ts" />
/// <reference path="../Typings\Angular-UI-Bootstrap\angular-ui-bootstrap.d.ts" />

module WrightsAtHome {

    export var app = angular.module("wrightsAtHome", ['restangular', 'ui.bootstrap', 'frapontillo.bootstrap-switch', 'ngSanitize']);

    export class Config {
        constructor(restangular: restangular.IProvider) {
            restangular.setBaseUrl("API");
        }
    }
    Config.$inject = ['RestangularProvider'];
    
    app.config(Config);
    
}
