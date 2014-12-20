var ngApp = angular.module('ngApp', ['ngRoute']);

ngApp.config(function($routeProvider, $locationProvider){

	$routeProvider

	.when('/', {
		templateUrl : '/Content/views/home.html'
	})

	.when('/login',{
	    templateUrl: '/Content/views/login.html',
		controller : 'loginController'
	})

	.when('/chat',{
	    templateUrl: '/Content/views/chat.html',
		controller : 'chatController'
	})

	$locationProvider.html5Mode(true);

});


ngApp.controller('mainController', function($scope){
	function setLoginLabels(){
	    $scope.lUser = ($scope.user.userName == 'anonymous') ? "Login" : $scope.user.userName;
		
	}

	$scope.$on('login', function(event, args){
		setLoginLabels();
	});

	$scope.user = {userName : "anonymous"};
	$scope.lLoginButton = "Change Name";
	setLoginLabels();
});

ngApp.controller('loginController', function($scope, $http, $window){
	


	$scope.login = function(){
		$scope.user.userName = $scope.user.username; //sets scope user to textbox value; badly named atm
		
		
		// $http
		// 	.post('/login', user)
		// 	.success(function(data, status, headers, config){
		// 		$window.sessionStorage.token = data.token;
		// 		$scope.$emit('login', true);
		// 	})
		// 	.error(function(){
		// 		delete $window.sessionStorage.token;
		// 	});
		$scope.$emit('login', true);
		
	};

});

ngApp.controller('chatController', function($scope, $http){

	$scope.roomName = "Public";
	$scope.viewRoomEdit = false;
	$scope.joinButtonText = "Edit Room";

	$scope.editRoom = function(){ 
		$scope.viewRoomEdit = true;
		$scope.joinButtonText = "Change Room";		
	};
	
    $http.get('/api/chat/' + $scope.roomName)
		.success(function(data){
			$scope.messages = data;
	});

	var socket = io("/socket");

	$scope.joinRoom = function(){
		
		if (! $scope.viewRoomEdit){
			$scope.editRoom();
			return;
		}

		socket.emit('joinRoom', $scope.formData.roomName);
		$scope.roomName = $scope.formData.roomName;
		$scope.formData.roomName = "";

		$http.get('/api/chat/' + $scope.roomName)
			.success(function(data){
				$scope.messages = data;
		});

		$scope.viewRoomEdit = false;
		$scope.joinButtonText = "Edit Room";	
		return false;
	};

	$scope.sendMessage = function(){
		var message = {
			reciever : $scope.roomName,
			sender : $scope.user.userName,
			message : $scope.formData.chatMessage
		};

		socket.emit('message', message);

		$scope.formData.chatMessage = "";

		
		$http.post('/api/chat', message)
			.success(function(data){
				$scope.messages.push(data);		
			});
	};

//	socket.on('message', function(message){
//		console.log("Message rcd: ", message);
//		$scope.messages.push(message);		
//		$scope.$apply();
//	});

    $scope.formatDate = function(date) {
        return moment(date).format('MM/DD/YY h:mm a');
    }

});


//function isLoggedIn(){
//	return (window.sessionStorage.token != null);
//}

//figure out a custom directive for an enter event in the future
// ngApp.directive('ng+enter', function(){
// 	return function(scope, element, attrs) {
//         element.bind("keydown keypress", function(event) {
//             if(event.which === 13) {
//                 scope.$apply(function(){
//                     scope.$eval(attrs.ngEnter, {'event': event});
//                 });

//                 event.preventDefault();
//             }
//         });
//     };
// });