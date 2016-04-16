import angular from 'angular';
import uiRouter from 'angular-ui-router';

import game from './modules/game';

let App = angular.module('app', [
  'ui.router',
  'tiy.game'
]);

function config($urlRouterProvider) {
  $urlRouterProvider.otherwise("/");
}

App.config(config);
