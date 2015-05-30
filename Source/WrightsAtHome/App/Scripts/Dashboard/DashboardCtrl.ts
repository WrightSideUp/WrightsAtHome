module WrightsAtHome.Controllers {

    export class DashboardCtrl {
        private $scope: ng.IScope;
        private devicesSvc: Services.DevicesService;

        constructor($scope: ng.IScope, service: Services.DevicesService) {
            var self = this;
            self.$scope = $scope;
            self.devicesSvc = service;
        }

        changeDeviceState(id: number, isOn: boolean) {
            this.devicesSvc.requestDeviceChangeState(new Model.DeviceStateRequest(id, isOn));
        }
    }
    DashboardCtrl.$inject = ['$scope', 'devicesService'];

    app.controller('dashboardCtrl', DashboardCtrl);

}
