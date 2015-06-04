/// <reference path="../../Typings/AngularJS/angular.d.ts" />

module WrightsAtHome.Controllers {

    export class MainCtrl {
        private $scope: Model.IMainScope;
        private dashboardOpen: boolean;
        private devicesSvc: Services.DevicesService;
        private sensorsSvc: Services.SensorsService;

        private devices: Array<Model.Device>;
        private sensors: Array<Model.SensorReading>;

        constructor($scope: Model.IMainScope, devservice: Services.DevicesService, senservice: Services.SensorsService) {
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
    }
    MainCtrl.$inject = ['$scope', 'devicesService', 'sensorsService'];

    app.controller('mainCtrl', MainCtrl);
}
