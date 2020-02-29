var app = angular.module("basketAgenda", ["ngRoute"]);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/addGame', {
            templateUrl: 'views/addgame.html',
            controller: 'basketAgendaController'
        })
        .when('/showResults', {
            templateUrl: 'views/showresults.html',
            controller: 'basketAgendaController'
        }).otherwise({
            redirectTo: '/addGame'
        });
}]);

app.controller("basketAgendaController", function ($scope, $http) {

    $scope.Add = function (gameDate, points) {

        if(gameDate == null || points == null){
            alertify.alert('Preencha todos os campos').set({ title: "" }); 
        }else if(points < 0){
            alertify.alert('A pontuação deve ser igual ou maior que (0)zero').set({ title: "" }); 
        }

        var body = {
            GameDate: gameDate,
            Points: points
        };

        $http.post("http://localhost:5000/api/basket/add", body).then(function (ret) {

            if (ret.status != 201) {
                alertify.alert('Ocorreu um erro ao tentar incluir o jogo').set({ title: "" });
            } else {
                alertify.alert('Jogo adicionado com sucesso').set({ title: "" });
            }
        });
    };

    var loadResults = function () {
        $http.get("http://localhost:5000/api/basket/gameResult").then(function (data) {

            var ret = data.data;
            $scope.firstGameDate = ret.firstGameDate;
            $scope.lastGameDate = ret.lastGameDate;

            $scope.results = [
                { description: "Jogos Disputados", result: ret.gamesPlayed },
                { description: "Total de pontos marcados na temporada ", result: ret.totalPoints },
                { description: "Média de pontos por jogo", result: ret.averagePoints },
                { description: "Maior pontuação em um jogo", result: ret.highestScore },
                { description: "Menor pontuação em um jogo", result: ret.lowestScore },
                { description: "Quantidade de vezes que bateu o próprio recorde", result: ret.records },
            ];
        });
    };

    loadResults();
});