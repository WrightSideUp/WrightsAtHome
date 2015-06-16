module app.services {
    'use strict';

    export interface ISensorService {
        getCurrentSensorReadings: () => ng.IPromise<Array<models.ISensorReading>>;
    }

    export class SensorService implements ISensorService {
        static $inject: Array<string> = ['Restangular', 'exception', 'logger'];
        constructor(
            private restangular: restangular.IService,
            private exception: blocks.exception.IException,
            private logger: blocks.logger.Logger) {
        }

        getCurrentSensorReadings: () => ng.IPromise<Array<models.ISensorReading>> = () => {

            this.restangular.extendModel('sensorreadings', s => models.SensorReadingMixer().mixInto(s));

            return this.restangular.all('sensorreadings').getList()
                .catch(this.fail);
        };

        private fail: (error: any) => {} = (error) => {
            var msg = "No data returned";
            if (error.hasOwnProperty('data')) {
                if (error.data.hasOwnProperty('message')) {
                    msg = error.data.message;
                }
            }
            var reason = 'Sensor Reading query failed';
            this.exception.catcher(msg)(reason);
            throw new Error("Sensor Read Failed.");
            return {};
        }
    }

    angular
        .module('app.services')
        .service('sensorService', SensorService);
}
