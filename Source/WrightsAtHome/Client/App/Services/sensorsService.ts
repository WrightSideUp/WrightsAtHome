/// <reference path="../../Typings/AngularJS/angular.d.ts" />
/// <reference path="../../Typings/Restangular/restangular.d.ts" />

module WrightsAtHome.Services {

    export class SensorsService {
        private rng: restangular.IService;

        constructor(restangular: restangular.IService) {
            this.rng = restangular;
        }
        
        public static sensorsServiceFactory(restangular: restangular.IService) {
            return new SensorsService(restangular);
        }

        getSensorReadings(): ng.IPromise<any> {
            return this.rng.all('sensors').getList().then(Model.SensorReading.apiResultTransformer);
        }

        getSensorReading(id: number): ng.IPromise<any> {
            return this.rng.one('sensors', id).get().then(Model.SensorReading.apiResultTransformer);
        }
    }
    SensorsService.$inject = ['Restangular'];

    app.factory('sensorsService', ['Restangular', SensorsService.sensorsServiceFactory]);
}



