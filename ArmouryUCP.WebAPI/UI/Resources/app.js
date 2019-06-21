var app = angular.module('armouryPanel', ['ngRoute', 'ngSanitize']);

app.config(function ($routeProvider) {

    $routeProvider.
        when('/', {
            templateUrl: 'main.html',
            controller: 'frontPageController'
        })
        .when('/player/:id', {
            templateUrl: 'profile.html',
            controller: 'playerController'
        })
        .when('/houses', {
            templateUrl: 'houses.html',
            controller: 'housesController'
        })
        .otherwise({
            templateUrl: '404.html'
        });

});

app.controller('mainController', ['$scope', function ($scope) {

}]);

app.controller('housesController', ['$scope', '$location', function ($scope, $location) {
    $scope.mainLocation = $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + '/#!/';

    var houseRequest = new XMLHttpRequest();
    houseRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (houseRequest.readyState == 4 && houseRequest.status == 200) {
                $scope.houseInfos = JSON.parse(houseRequest.responseText);
            }
        });
    }

    houseRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/house");
    houseRequest.send();
}]);

app.controller('frontPageController', ['$scope', '$location', function ($scope, $location) {
    $scope.mainLocation = $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + '/#!/';

    var serverInfoRequest = new XMLHttpRequest();
    serverInfoRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (serverInfoRequest.readyState == 4 && serverInfoRequest.status == 200) {
                $scope.serverInfo = JSON.parse(serverInfoRequest.responseText);
            }
        });
    }

    var serverNewsRequest = new XMLHttpRequest();
    serverNewsRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (serverNewsRequest.readyState == 4 && serverNewsRequest.status == 200) {
                $scope.serverNews = JSON.parse(serverNewsRequest.responseText);
            }
        });
    }

    $scope.onlinePlayersNumber = 0;

    var onlinePlayersRequest = new XMLHttpRequest();
    onlinePlayersRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (onlinePlayersRequest.readyState == 4 && onlinePlayersRequest.status == 200) {
                $scope.onlinePlayers = JSON.parse(onlinePlayersRequest.responseText);
                if ($scope.onlinePlayers.length > 0) {
                    $scope.onlinePlayersNumber = $scope.onlinePlayers[0]['TotalPlayers'];

                    $scope.onlinePlayers.forEach(function (element) {
                        element['profileUrl'] = $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/#!/player/" + element['ID'];
                    });
                }
            }
        });
    }

    onlinePlayersRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/player/briefonline");
    onlinePlayersRequest.send();

    serverInfoRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/server");
    serverInfoRequest.send();

    serverNewsRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/server/news");
    serverNewsRequest.send();
}]);

app.controller('mainNavController', ['$scope', function ($scope) {
    var mainItems = this;

    mainItems.items = [
        { name: 'Profile', faItem: 'user', link: 'test' },
        { name: 'Staff', faItem: 'crown', link: 'test' },
        { name: 'Economy', faItem: 'comment-dollar', link: 'test' },
        { name: 'Complaints', faItem: 'exclamation-triangle', link: 'test' },
        { name: 'Unban Requests', faItem: 'user-lock', link: 'test' }
    ];
}]);

app.controller('secondaryNavController', ['$scope', '$location', function ($scope, $location) {
    var secondaryItems = this;

    secondaryItems.items = [
        [
            ['General', ''],
            ['Houses', '/#!/houses'],
            ['Businesses', 'businesses'],
            ['Vehicles', ''],
            ['Clan', ''],
            ['Faction History', ''],
            ['War Info', ''],
            ['Stats', ''],
        ]
    ];
}]);

app.controller('playerController', ['$scope', '$location', function ($scope, $location) {
    var factionHistoryRequest = new XMLHttpRequest();
    factionHistoryRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (factionHistoryRequest.readyState == 4 && factionHistoryRequest.status == 200) {
                $scope.factionHistory = JSON.parse(factionHistoryRequest.responseText);
            }
        });
    }

    var houseRequest = new XMLHttpRequest();
    houseRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (houseRequest.readyState == 4 && houseRequest.status == 200) {
                $scope.houseInfos = JSON.parse(houseRequest.responseText);

                $scope.housesValue = 0;
                $scope.housesCount = $scope.houseInfos.length;
                $scope.houseInfos.forEach(function (element) {
                    $scope.housesValue += element['Value'];
                });
            }
        });
    }

    var vehicleRequest = new XMLHttpRequest();
    vehicleRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (vehicleRequest.readyState == 4 && vehicleRequest.status == 200) {
                $scope.vehicleInfos = JSON.parse(vehicleRequest.responseText);

                $scope.vehiclesValue = 0;
                $scope.vehiclesCount = $scope.vehicleInfos.length;
                $scope.vehicleInfos.forEach(function (element) {
                    $scope.vehiclesValue += element['Value'];
                });
            }
        });
    }

    var businessRequest = new XMLHttpRequest();
    businessRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (businessRequest.readyState == 4 && businessRequest.status == 200) {
                $scope.businessInfos = JSON.parse(businessRequest.responseText);

                $scope.businessesValue = 0;
                $scope.businessesCount = $scope.businessInfos.length;
                $scope.businessInfos.forEach(function (element) {
                    $scope.businessesValue += element['Value'];
                });
            }
        });
    }

    var playerRequest = new XMLHttpRequest();
    playerRequest.onreadystatechange = function () {
        $scope.$apply(function () {
            if (playerRequest.readyState == 4 && playerRequest.status == 200) {
                $scope.playerInfos = JSON.parse(playerRequest.responseText);
                $scope.modelPadded = ("00" + $scope.playerInfos.Model).slice(-3);
                $scope.adminLevelName = getAdminLevel($scope.playerInfos['AdminLevel']);

                houseRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/house/" + $scope.playerInfos['Name']);
                houseRequest.send();

                vehicleRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/vehicle/" + $scope.playerInfos['Name']);
                vehicleRequest.send();

                businessRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/business/" + $scope.playerInfos['Name']);
                businessRequest.send();

                factionHistoryRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/player/" + $location.path().split('/').pop() + "/factionhistory");
                factionHistoryRequest.send();
            }
        });
    }

    function getAdminLevel($adminLevel) {
        switch ($adminLevel) {
            case 0:
                return 'None';
                break;
            case 6:
                return 'Coordonator';
                break;
            case 7:
                return 'Scripter si Fondator'
                break;
            case 8:
                return 'Fondator';
                break;
            default:
                return 'Admin ' + $adminLevel;
                break;
        }
    }

    playerRequest.open("GET", $location.protocol() + '://' + $location.host() + ':' + ($location.port() !== 80 ? $location.port() : '') + "/api/player/" + $location.path().split('/').pop());
    playerRequest.send();
}]);