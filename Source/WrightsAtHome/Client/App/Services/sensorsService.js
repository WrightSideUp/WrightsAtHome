/// <reference path="../../Typings/AngularJS/angular.d.ts" />
/// <reference path="../../Typings/Restangular/restangular.d.ts" />
var WrightsAtHome;
(function (WrightsAtHome) {
    var Services;
    (function (Services) {
        var SensorsService = (function () {
            function SensorsService(restangular) {
                this.rng = restangular;
            }
            SensorsService.sensorsServiceFactory = function (restangular) {
                return new SensorsService(restangular);
            };
            SensorsService.prototype.getSensorReadings = function () {
                return this.rng.all('sensors').getList().then(WrightsAtHome.Model.SensorReading.apiResultTransformer);
            };
            SensorsService.prototype.getSensorReading = function (id) {
                return this.rng.one('sensors', id).get().then(WrightsAtHome.Model.SensorReading.apiResultTransformer);
            };
            return SensorsService;
        })();
        Services.SensorsService = SensorsService;
        SensorsService.$inject = ['Restangular'];
        WrightsAtHome.app.factory('sensorsService', ['Restangular', SensorsService.sensorsServiceFactory]);
    })(Services = WrightsAtHome.Services || (WrightsAtHome.Services = {}));
})(WrightsAtHome || (WrightsAtHome = {}));
//# sourceMappingURL=sensorsService.js.map