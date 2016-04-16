class GameController {
  constructor($http, $stateParams) {
    this._$http = $http;
    this.$stateParams = $stateParams;
    this.getData();
  }

  getData() {
    this.game = {
      name: "Nick",
      money: 600,
      moneypermin: 60,
      unemployed: 3,
      message: "You have 2 new people",
      messsagetype: "",
      tower: [],
      NextFloorCost: 1000,
      people: []
      }

      this.floor = {
        peoplelimit: 4,
        numpeople: 5,
        floortype: "",
        people: []
      }

      this.person = {
        home: "string",
        work: "string",
        name: "string"
      }



  }
}









export default GameController
