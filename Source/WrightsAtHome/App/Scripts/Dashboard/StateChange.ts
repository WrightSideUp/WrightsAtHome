module WrightsAtHome.Directives {

    export class StateChange {
        public static stateChangeDirective($q: ng.IQService): ng.IDirective {
            return {
                require: 'ngModel',
                link: function (scope: Model.IDeviceScope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes, ctrl: any) {
                    ctrl.$asyncValidators.stateChange = function (modelValue, viewValue) {
                        var def = $q.defer();

                        console.log("Validation Triggered for ", scope.device.id, " value: ", modelValue);

                        def.resolve();

                        return def.promise;
                    };
                }
            };
        }
    }
    StateChange.$inject = ['$q'];

    app.directive('stateChange', ['$q', StateChange.stateChangeDirective]);
}
