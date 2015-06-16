module app.layout {
    'use strict';

    export interface IStateExtra extends ng.ui.IState {
        settings: any;
    }

    export class TopNavController {
        static $inject: Array<string> = ['$state', 'config'];
        constructor(private $state: ng.ui.IStateService,
                    private config: {appTitle: string }) {
            this.getNavRoutes();
        }

        navRoutes: IStateExtra[];
        states: IStateExtra[] = <IStateExtra[]>this.$state.get();

        navline = {
            title: this.config.appTitle,
            text: 'Created by Bruce Wright',
            link: 'http://wrightsideup.com'
        };
        
        isCurrent(route: { title: string }) {
            var currentState: any = this.$state.current;
            if (!route.title || !currentState || !currentState.title) {
                return '';
            }
            var menuName: string = route.title;
            return currentState.title.substr(0, menuName.length) === menuName ? 'active' : '';
        }

        private getNavRoutes() {
            this.navRoutes = this.states
                .filter((state) => state.settings && state.settings.nav)
                .sort((state1, state2) => state1.settings.nav - state2.settings.nav);
        }
    }

    angular
        .module('app.layout')
        .controller('TopNavController', TopNavController);
}
