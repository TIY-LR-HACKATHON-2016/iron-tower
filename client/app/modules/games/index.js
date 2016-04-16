import angular from 'angular';
import config from './config';
import controller from './controller';

let game = angular.module('tiy.game', []);

events.config(config);
events.controller('GameController', controller);

export default game;
