import angular from 'angular';
import config from './config';
import controller from './controller';

let game = angular.module('tiy.game', []);

game.config(config);
game.controller('GameController', controller);

export default game;
