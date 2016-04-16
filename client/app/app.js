import angular from 'angular';
import uiRouter from 'angular-ui-router';

let App = angular.module('app', [
  'ui.router'
]);

function config($urlRouterProvider) {
  $urlRouterProvider.otherwise("/");
}

App.config(config);
