class GameController {
  constructor($http, $stateParams) {
    this._$http = $http;
    this.$stateParams = $stateParams;
    this.getData();
  }

  getData() {
  //   this.game = {
  //     Name: "Nick",
  //     Money: 600,
  //     MoneyPerMin: 60,
  //     Unemployed: 3,
  //     Message: "You have 2 new people",
  //     MesssageType: "",
  //     Tower: [],
  //     NextFloorCost: 1000,
  //     People: []
  //     }
  //
  //     this.floor = {
  //       PeopleLimit: 4,
  //       NumPeople: 5,
  //       FloorType: "",
  //       People: []
  //     }
  //
  //     this.person = {
  //       Home: "string",
  //       Work: "string",
  //       Name: "string"
  //     }

 this._$http
 .get("http://irontower2016.azurewebsites.net/")
 .then((response) => {
   console.log(response);
   let results = response.data[0];
   this.game = results;
   
 })


  }
}











export default GameController
