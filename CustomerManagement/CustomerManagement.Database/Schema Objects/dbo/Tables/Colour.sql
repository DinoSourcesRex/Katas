﻿CREATE TABLE [dbo].[Colour]
(
	[Id] INT IDENTITY NOT NULL,
	[Colour] NVARCHAR(30) NOT NULL,
	CONSTRAINT pk_colourId PRIMARY KEY (Id)
)