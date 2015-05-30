/// <reference path="../Typings/AngularJS/angular.d.ts" />
/// <reference path="../Typings/Restangular/restangular.d.ts" />

module WrightsAtHome.Services {

    export class DevicesService {
        private rng: restangular.IService;

        constructor(restangular: restangular.IService) {
            this.rng = restangular;
        }

        public static devicesServiceFactory(restangular: restangular.IService) {
            return new DevicesService(restangular);
        }

        getDevices(): ng.IPromise<any> {
            return this.rng.all('devices').getList().then(Model.Device.apiResultTransformer);
        }

        requestDeviceChangeState(request: Model.DeviceStateRequest): ng.IPromise<any> {
            var self = this;

            var stateRequests = this.rng.all('DeviceState');

            return stateRequests.post(request);
        }
    }
    DevicesService.$inject = ['Restangular'];

    app.factory('devicesService', ['Restangular', DevicesService.devicesServiceFactory]);
}



