var WrightsAtHome;
(function (WrightsAtHome) {
    var Controllers;
    (function (Controllers) {
        var ScheduleCtrl = (function () {
            function ScheduleCtrl($scope) {
                var self = this;
                self.$scope = $scope;
                self.scheduleOpen = false;
                self.$scope.scheduleOpen = self.scheduleOpen;
            }
            return ScheduleCtrl;
        })();
        Controllers.ScheduleCtrl = ScheduleCtrl;
        ScheduleCtrl.$inject = ['$scope'];
        WrightsAtHome.app.controller('scheduleCtrl', ScheduleCtrl);
    })(Controllers = WrightsAtHome.Controllers || (WrightsAtHome.Controllers = {}));
})(WrightsAtHome || (WrightsAtHome = {}));
//# sourceMappingURL=ScheduleCtrl.js.map