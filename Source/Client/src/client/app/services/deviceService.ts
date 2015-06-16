module app.services {
    'use strict';

    export interface IDeviceService {
        getAllDeviceInfo: () => ng.IPromise<Array<models.IDeviceInfo>>;
    }

    export class DeviceService implements IDeviceService {
        static $inject: Array<string> = ['Restangular', 'exception', 'logger'];
        constructor(
            private restangular: restangular.IService,
            private exception: blocks.exception.IException,
            private logger: blocks.logger.Logger) {
        }

        getAllDeviceInfo: () => ng.IPromise<Array<models.IDeviceInfo>> = () => {

            return this.restangular.all('devices').getList()
                .catch(this.fail);
        };

        private fail: (error: any) => {} = (error) => {
            var msg = "No data returned";
            if (error.hasOwnProperty('data')) {
                if (error.data.hasOwnProperty('message')) {
                    msg = error.data.message;
                }
            }
            var reason = 'Get Device query failed';
            this.exception.catcher(msg)(reason);
            throw new Error("Device Read Failed.");
            return {};
        }
    }

    angular
        .module('app.services')
        .service('deviceService', DeviceService);
}
