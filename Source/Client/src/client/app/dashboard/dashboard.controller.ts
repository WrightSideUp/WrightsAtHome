module app.dashboard {
    'use strict';

    export interface IDashboardVm {
        title: string;

        sensors: Array<models.ISensorReading>;
        sensorReadError: boolean;

        devices: Array<models.IDeviceInfo>;
        deviceReadError: boolean;

        changeDeviceState(deviceId: number, newStateId: number);
    }
    
    export class DashboardController implements IDashboardVm {
        static $inject: Array<string> = ['$q', 'sensorService', 'deviceService', 'logger'];

        title: string = 'Dashboard';
        sensors: Array<models.ISensorReading>;
        sensorReadError = false;
        devices: Array<models.IDeviceInfo>;
        deviceReadError = false;

        changeDeviceState(deviceId: number, newStateId: number) {
            
        }

        constructor(private $q: ng.IQService,
                    private sensorService: services.ISensorService,
                    private deviceService: services.IDeviceService,
                    private logger: blocks.logger.Logger) {

                var promises = [this.getSensors(), this.getDevices()];
                this.$q.all(promises).then(() => {
                    logger.info('Activated Dashboard View');
                });
        }

        getSensors()  {
            return this.sensorService.getCurrentSensorReadings()
                .then((data) => { this.sensors = data; })
                .catch(() => { this.sensorReadError = true; });
        }

        getDevices() {
            return this.deviceService.getAllDeviceInfo()
                .then((data) => { this.devices = data; })
                .catch(() => { this.deviceReadError = true; });
        }
    }

    angular
        .module('app.dashboard')
        .controller('DashboardController', DashboardController);
}
