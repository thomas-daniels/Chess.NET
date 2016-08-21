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
 - Support for claiming draw for the 50 move rule.

Planned features:

 - Draw on threefold repetition
 - Draw on insufficient mating material
