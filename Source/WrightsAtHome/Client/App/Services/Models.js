var WrightsAtHome;
(function (WrightsAtHome) {
    var Model;
    (function (Model) {
        var Utils = (function () {
            function Utils() {
            }
            Utils.copyProperties = function (source, target) {
                for (var prop in source) {
                    target[prop] = source[prop];
                }
            };
            return Utils;
        })();
        var Device = (function () {
            function Device(object) {
                Utils.copyProperties(object, this);
            }
            Device.apiResultTransformer = function (data) {
                var result = Array();
                if (angular.isArray(data)) {
                    for (var i = 0; i < data.length; i++) {
                        result.push(new Device(data[i]));
                    }
                }
                else {
                    result.push(new Device(data));
                }
                return result;
            };
            return Device;
        })();
        Model.Device = Device;
        (function (SensorType) {
            SensorType[SensorType["Temperture"] = 0] = "Temperture";
            SensorType[SensorType["Light"] = 1] = "Light";
        })(Model.SensorType || (Model.SensorType = {}));
        var SensorType = Model.SensorType;
        (function (LightLevel) {
            LightLevel[LightLevel["Daytime"] = 0] = "Daytime";
            LightLevel[LightLevel["Night"] = 1] = "Night";
        })(Model.LightLevel || (Model.LightLevel = {}));
        var LightLevel = Model.LightLevel;
        var SensorReading = (function () {
            function SensorReading(object) {
                Utils.copyProperties(object, this);
            }
            SensorReading.prototype.readingDisplayText = function () {
                if (this.sensorType == 0 /* Temperture */)
                    return this.temperature + '&deg;';
                else if (this.lightReading = 0 /* Daytime */)
                    return 'Daytime';
                else
                    return 'Night-time';
            };
            SensorReading.apiResultTransformer = function (data) {
                var result = Array();
                if (angular.isArray(data)) {
                    for (var i = 0; i < data.length; i++) {
                        result.push(new SensorReading(data[i]));
                    }
                }
                else {
                    result.push(new SensorReading(data));
                }
                return result;
            };
            return SensorReading;
        })();
        Model.SensorReading = SensorReading;
        var DeviceStateRequest = (function () {
            function DeviceStateRequest(id, desiredState) {
                this.id = id;
                this.desiredState = desiredState;
            }
            return DeviceStateRequest;
        })();
        Model.DeviceStateRequest = DeviceStateRequest;
    })(Model = WrightsAtHome.Model || (WrightsAtHome.Model = {}));
})(WrightsAtHome || (WrightsAtHome = {}));
//# sourceMappingURL=Models.js.map