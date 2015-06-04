
module WrightsAtHome.Model {
    
    class Utils {
        public static copyProperties(source: any, target: any): void {
            for (var prop in source) {
                target[prop] = source[prop];
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

    export enum SensorType {
        Temperture = 0,
        Light = 1
    }

    export enum LightLevel {
        Daytime = 0,
        Night = 1
    }

    export class SensorReading {
        id: number;
        name: string;
        sensorType: SensorType;
        temperature: number;
        lightReading: LightLevel;
        smallImageUrl: string;
        largeImageUrl: string;

        readingDisplayText() {

            if (this.sensorType == SensorType.Temperture)
                return this.temperature + '&deg;';
            else
                if (this.lightReading = LightLevel.Daytime)
                    return 'Daytime';
                else
                    return 'Night-time';
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