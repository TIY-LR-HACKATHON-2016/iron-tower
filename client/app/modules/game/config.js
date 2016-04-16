function config($stateProvider) {
  $stateProvider
    .state("game", {
      url: "/",
      controller: "GameController as gameCtrl",
      template: require("./view.html")
    });
}

export default config;
