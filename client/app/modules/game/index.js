import angular from 'angular';
import config from './config';
import controller from './controller';

let game = angular.module('tiy.game', []);

characters.config(config);
characters.controller('GameController', controller);

export default game;
