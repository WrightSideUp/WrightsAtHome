var WrightsAtHome;
(function (WrightsAtHome) {
    var Directives;
    (function (Directives) {
        var StateChange = (function () {
            function StateChange() {
            }
            StateChange.stateChangeDirective = function ($q) {
                return {
                    require: 'ngModel',
                    link: function (scope, element, attrs, ctrl) {
                        ctrl.$asyncValidators.stateChange = function (modelValue, viewValue) {
                            var def = $q.defer();
                            console.log("Validation Triggered for ", scope.device.id, " value: ", modelValue);
                            def.resolve();
                            return def.promise;
                        };
                    }
                };
            };
            return StateChange;
        })();
        Directives.StateChange = StateChange;
        StateChange.$inject = ['$q'];
        WrightsAtHome.app.directive('stateChange', ['$q', StateChange.stateChangeDirective]);
    })(Directives = WrightsAtHome.Directives || (WrightsAtHome.Directives = {}));
})(WrightsAtHome || (WrightsAtHome = {}));
//# sourceMappingURL=StateChange.js.map