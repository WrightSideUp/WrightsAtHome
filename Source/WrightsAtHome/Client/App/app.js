/// <reference path="../Typings/AngularJS/angular.d.ts" />
/// <reference path="../Typings/AngularJS/angular-route.d.ts" />
/// <reference path="../Typings/Restangular/restangular.d.ts" />
/// <reference path="../Typings\Angular-UI-Bootstrap\angular-ui-bootstrap.d.ts" />
var WrightsAtHome;
(function (WrightsAtHome) {
    WrightsAtHome.app = angular.module("wrightsAtHome", ['restangular', 'ui.bootstrap', 'frapontillo.bootstrap-switch', 'ngSanitize']);
    var Config = (function () {
        function Config(restangular) {
            restangular.setBaseUrl("API");
        }
        return Config;
    })();
    WrightsAtHome.Config = Config;
    Config.$inject = ['RestangularProvider'];
    WrightsAtHome.app.config(Config);
})(WrightsAtHome || (WrightsAtHome = {}));
//# sourceMappingURL=app.js.map