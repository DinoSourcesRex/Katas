CREATE TABLE [dbo].[CustomerColor]
(
	[Id] INT NOT NULL,
	[Colour] NVARCHAR(30) NOT NULL,
	CONSTRAINT pk_customerIdColor PRIMARY KEY (Id, Colour),
	CONSTRAINT fk_customerColorId FOREIGN KEY(Id) REFERENCES Customer(Id)
)
