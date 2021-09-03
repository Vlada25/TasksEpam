﻿CREATE TABLE [dbo].[Books] 
(
	[Id] INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	[Name] NVARCHAR(100) NOT NULL, 
	[Author] NVARCHAR(100) NOT NULL,
	[GenreId] INT NOT NULL, 
	[Condition] INT NOT NULL,
	FOREIGN KEY ([GenreId]) REFERENCES Genres([Id])
)
