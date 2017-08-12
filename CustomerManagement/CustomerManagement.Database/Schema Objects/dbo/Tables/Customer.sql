CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[PreviouslyOrdered] BIT NOT NULL,
	[WebCusomter] BIT NOT NULL,
	[LastActive]  DATETIME NOT NULL,
	CONSTRAINT pk_customerId PRIMARY KEY (Id)
)