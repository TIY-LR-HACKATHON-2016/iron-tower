class GameController {
  constructor($http, $stateParams) {
    this._$http = $http;
    this.$stateParams = $stateParams;
    this.getData();
  }

  getData() {
    this.game = {
      name: "Nick",
      money: 600
    }

    // this._$http
    // .get(``)

  }
}









export default GameController
