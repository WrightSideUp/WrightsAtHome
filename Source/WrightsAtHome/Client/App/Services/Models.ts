
module WrightsAtHome.Model {
    
    class Utils {
        public static copyProperties(source: any, target: any): void {
            for (var prop in source) {
                if (source.hasOwnProperty(prop)) {
                    target[prop] = source[prop];
                }
            }
        }
    }

    export class Device {
        id: number;
        name: string;
        isOn: boolean;
        smallImageUrl: string;
        largeImageUrl: string;
        nextEvent: string;

        constructor(object) {
            Utils.copyProperties(object, this);
        }

        public static apiResultTransformer(data: any): Array<Device> {
            var result = Array<Device>();

            if (angular.isArray(data)) {
                for (var i = 0; i < data.length; i++) {
                    result.push(new Device(data[i]));
                }
            }
            else {
                result.push(new Device(data));
            }

            return result;
        }
    }

    export class SensorReading {
        id: number;
        name: string;
        sensorType: string;
        reading: number;
        smallImageUrl: string;
        largeImageUrl: string;

        readingDisplayText() {

            if (this.sensorType === "Temperture")
                return this.reading + "&deg;";
            else
                if (this.reading >= 1)
                    return "Daytime";
                else
                    return "Night-time";
        }

        constructor(object) {
            Utils.copyProperties(object, this);
        }

        public static apiResultTransformer(data: any): Array<SensorReading> {
            var result = Array<SensorReading>();

            if (angular.isArray(data)) {
                for (var i = 0; i < data.length; i++) {
                    result.push(new SensorReading(data[i]));
                }
            }
            else {
                result.push(new SensorReading(data));
            }

            return result;
        }
    }

    export class DeviceStateRequest {
        id: number;
        desiredState: boolean;

        constructor(id: number, desiredState: boolean) {
            this.id = id;
            this.desiredState = desiredState;
        }
    }

    export interface IMainScope extends ng.IScope {
        dashboardOpen: boolean;
        devices: Array<Device>;
        sensors: Array<SensorReading>;
    }

    export interface IScheduleScope extends ng.IScope {
        scheduleOpen: boolean;
    }

    export interface IDeviceScope extends ng.IScope {
        device: Device;
    }
}