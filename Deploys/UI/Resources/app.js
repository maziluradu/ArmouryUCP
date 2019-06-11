var app = angular.module('armouryPanel', []);

app.controller('mainController', function ($scope) {
});

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

app.controller('secondaryNavController', ['$scope', function ($scope) {
    var secondaryItems = this;

    secondaryItems.items = [
        [ 'General', 'Properties', 'Businesses', 'Vehicles', 'Clan', 'Faction History', 'War Info', 'Stats' ],
        [ 'General', 'Properties', 'Businesses', 'Vehicles', 'Clan', 'Faction History', 'War Info', 'Stats' ]
    ];
}]);