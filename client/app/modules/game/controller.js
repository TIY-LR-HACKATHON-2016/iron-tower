class GameController {
  constructor($http, $stateParams, $interval) {
    this._$http = $http;
    this.$stateParams = $stateParams;
    this.getData();
    this.possibleFloors();
    $interval(this.getData.bind(this), 10000);
    $interval(this.possibleFloors.bind(this), 5000);

    this.playerName = "";
    // this.deletePlayer();
  }

  buyFloor() {
    this._$http.get("http://irontower2016.azurewebsites.net/Games/AddFloor").then((response) => {
      // console.log(response)
      this.game = response.data;
    })
  }

  possibleFloors() {
    this._$http.get("http://irontower2016.azurewebsites.net/Games/PossibleFloors").then((response) => {
      // console.log(response)
      this.possibleFloors = response.data;
    })
  }

  setPlayerName() {
    this._$http.post("http://irontower2016.azurewebsites.net/Games/PlayerName", {
      Name: this.playerName
    }).then((response) => {
      this.game.Name = this.playerName;
    });
  }

  deletePlayer() {
    this._$http.get("http://irontower2016.azurewebsites.net/Games/Delete")
      .then((response) => {
        // console.log(response);
      })
  }


  changeFloor(floor) {
    console.log(floor);
    this._$http.get(`http://irontower2016.azurewebsites.net/Games/ChangeFloor?floorid=${floor.FloorId}&floorTypeId=${floor.pickedType}`)
      .then((response) => {
        console.log(response);
      })
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
   let results = response.data;
   this.game = results;
 })


  }
}











export default GameController
