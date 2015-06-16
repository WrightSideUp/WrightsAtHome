module applayout {

    'use strict';

    interface IHtTopNavScope {
        navline: string
    }

    class HtTopNav implements ng.IDirective {
        static $inject: Array<string> = [''];
        constructor() { }

        static instance() : ng.IDirective {
            return new HtTopNav();
        }

        bindToController: boolean = true;
        link: (scope: ng.IScope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes) => void;
        restrict: string = 'EA';
        scope: IHtTopNavScope = {
            'navline': '='
        };
    }

    angular
        .module('app.layout')
        .directive('htTopNav', HtTopNav.instance);
}
