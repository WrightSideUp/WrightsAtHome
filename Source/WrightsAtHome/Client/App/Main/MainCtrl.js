/// <reference path="../../Typings/AngularJS/angular.d.ts" />
var WrightsAtHome;
(function (WrightsAtHome) {
    var Controllers;
    (function (Controllers) {
        var MainCtrl = (function () {
            function MainCtrl($scope, devservice, senservice) {
                var self = this;
                self.devicesSvc = devservice;
                self.sensorsSvc = senservice;
                self.$scope = $scope;
                self.dashboardOpen = true;
                self.$scope.dashboardOpen = self.dashboardOpen;
                self.sensorsSvc.getSensorReadings().then(function (data) {
                    self.sensors = data;
                    self.$scope.sensors = self.sensors;
                });
                self.devicesSvc.getDevices().then(function (data) {
                    self.devices = data;
                    self.$scope.devices = self.devices;
                });
            }
            return MainCtrl;
        })();
        Controllers.MainCtrl = MainCtrl;
        MainCtrl.$inject = ['$scope', 'devicesService', 'sensorsService'];
        WrightsAtHome.app.controller('mainCtrl', MainCtrl);
    })(Controllers = WrightsAtHome.Controllers || (WrightsAtHome.Controllers = {}));
})(WrightsAtHome || (WrightsAtHome = {}));
//# sourceMappingURL=MainCtrl.js.map