module WrightsAtHome.Controllers {

    export class ScheduleCtrl {
        private $scope: Model.IScheduleScope;
        private scheduleOpen: boolean;

        constructor($scope: any) {
            var self = this;
            self.$scope = $scope;
            self.scheduleOpen = false;
            self.$scope.scheduleOpen = self.scheduleOpen;
        }
    }
    ScheduleCtrl.$inject = ['$scope'];

    app.controller('scheduleCtrl', ScheduleCtrl);

}
