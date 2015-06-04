var WrightsAtHome;
(function (WrightsAtHome) {
    var Controllers;
    (function (Controllers) {
        var DashboardCtrl = (function () {
            function DashboardCtrl($scope, service) {
                var self = this;
                self.$scope = $scope;
                self.devicesSvc = service;
            }
            DashboardCtrl.prototype.changeDeviceState = function (id, isOn) {
                this.devicesSvc.requestDeviceChangeState(new WrightsAtHome.Model.DeviceStateRequest(id, isOn));
            };
            return DashboardCtrl;
        })();
        Controllers.DashboardCtrl = DashboardCtrl;
        DashboardCtrl.$inject = ['$scope', 'devicesService'];
        WrightsAtHome.app.controller('dashboardCtrl', DashboardCtrl);
    })(Controllers = WrightsAtHome.Controllers || (WrightsAtHome.Controllers = {}));
})(WrightsAtHome || (WrightsAtHome = {}));
//# sourceMappingURL=DashboardCtrl.js.map