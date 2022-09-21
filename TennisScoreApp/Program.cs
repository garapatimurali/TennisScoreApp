// See https://aka.ms/new-console-template for more information
using TennisScoreApp;


Console.WriteLine("Welcome To Tennis Score App");

var scoringService = new GameScoringService("player1", "player2");
Console.WriteLine(scoringService.PointWonBy("player1"));
Console.WriteLine(scoringService.PointWonBy("player2"));
Console.WriteLine(scoringService.PointWonBy("player1"));
Console.WriteLine(scoringService.PointWonBy("player1"));
Console.WriteLine(scoringService.PointWonBy("player2"));
Console.WriteLine(scoringService.PointWonBy("player2"));
Console.WriteLine(scoringService.PointWonBy("player1"));
Console.WriteLine(scoringService.PointWonBy("player2"));
Console.WriteLine(scoringService.PointWonBy("player2"));
Console.WriteLine(scoringService.PointWonBy("player2"));