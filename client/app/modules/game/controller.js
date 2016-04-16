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
      unemployed: 3,
      message: "You have two new people",
      messagetype: "something",
      NextFloorCost: 1000,
      People: [],
      Tower: []
    }

    this.floor = {
      PeopleLimit: 4,
      numPeople: 5,
      floorType: "",
      people: []

    }

    this.person = {
      home: "test",
      work: "test",
      name: "test"
    }

    // this._$http
    // .get(``)

  }
}











export default GameController
