module app.core {
    'use strict';

    var config = {
        appErrorPrefix: '[WrightsAtHome Error] ',
        appTitle: 'WrightsAtHome'
    };

    angular
        .module('app.core')
        .config(toastrConfig)
        .config(configure)
        .value('config', config);

    toastrConfig.$inject = ['toastr'];
    /* @ngInject */
    function toastrConfig(toastr: Toastr) {
        toastr.options.timeOut = 4000;
        toastr.options.positionClass = 'toast-bottom-right';
    }

    configure.$inject = ['$logProvider', 'exceptionHandlerProvider'];
    /* @ngInject */
    function configure($logProvider: ng.ILogProvider,
        exceptionHandlerProvider: blocks.exception.ExceptionHandlerProvider) {
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }
        exceptionHandlerProvider.configure(config.appErrorPrefix);
    }
}
