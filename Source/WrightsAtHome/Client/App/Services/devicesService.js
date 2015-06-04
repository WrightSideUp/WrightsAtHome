/// <reference path="../../Typings/AngularJS/angular.d.ts" />
/// <reference path="../../Typings/Restangular/restangular.d.ts" />
var WrightsAtHome;
(function (WrightsAtHome) {
    var Services;
    (function (Services) {
        var DevicesService = (function () {
            function DevicesService(restangular) {
                this.rng = restangular;
            }
            DevicesService.devicesServiceFactory = function (restangular) {
                return new DevicesService(restangular);
            };
            DevicesService.prototype.getDevices = function () {
                return this.rng.all('devices').getList().then(WrightsAtHome.Model.Device.apiResultTransformer);
            };
            DevicesService.prototype.requestDeviceChangeState = function (request) {
                var self = this;
                var stateRequests = this.rng.all('DeviceState');
                return stateRequests.post(request);
            };
            return DevicesService;
        })();
        Services.DevicesService = DevicesService;
        DevicesService.$inject = ['Restangular'];
        WrightsAtHome.app.factory('devicesService', ['Restangular', DevicesService.devicesServiceFactory]);
    })(Services = WrightsAtHome.Services || (WrightsAtHome.Services = {}));
})(WrightsAtHome || (WrightsAtHome = {}));
//# sourceMappingURL=devicesService.js.map