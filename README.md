[![CircleCI build status](https://circleci.com/gh/ProgramFOX/Chess.NET.svg?style=shield)](https://circleci.com/gh/ProgramFOX/Chess.NET)

Chess.NET
=
Chess.NET is a chess library for .NET, written in C#.

It contains the following features:

 - Move validation (including castling and en passant).
 - Check validation.
 - Checkmate and stalemate validation.
 - FEN string parsing and game-to-FEN conversion.
 - Support of the Atomic chess variant - http://lichess.org/variant/atomic
 - Support of the King of the Hill chess variant - https://en.lichess.org/variant/kingOfTheHill
 - Support of the Three-check chess variant - https://en.lichess.org/variant/threeCheck
 - Support of the Antichess variant - https://en.lichess.org/variant/antichess
 - Support of the Horde variant - https://en.lichess.org/variant/horde
 - Support of the Racing Kings variant - https://en.lichess.org/variant/racingKings
 - Support for claiming draw for the 50 move rule.

Planned features:

 - Draw on threefold repetition
 - Draw on insufficient mating material

## .NET Core support

Chess.NET supports .NET Core. The global.json and project.json files are provided so you can build it using the .NET Core CLI:

    cd ChessDotNet
    dotnet build -c Release
    cd ..\ChessDotNet.Variants
    dotnet build -c Release


