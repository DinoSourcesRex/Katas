CREATE PROCEDURE [dbo].[Customer_Upsert]
	@Id INT,
	@Name NVARCHAR(100),
	@LastActive DATETIME,
	@WebCustomer BIT,
	@PreviouslyOrdered BIT,
	@Colours dbo.ColourList READONLY
AS
	SET NOCOUNT OFF

	BEGIN TRANSACTION update_customer

	--Make sure the colours exist in the table, if not add them
	IF NOT EXISTS (SELECT c.Id FROM Colour c WHERE c.Colour IN (SELECT tCol.Colour FROM @Colours tCol))
	BEGIN
		INSERT INTO dbo.Colour SELECT tCol.Colour FROM @Colours tCol WHERE tCol.Colour NOT IN (SELECT c.Colour FROM Colour c)
	END

	--Check that the record exists
	IF EXISTS (SELECT c.Name FROM dbo.Customer c WHERE c.Id = @Id AND c.Name = @Name)
		BEGIN
			--If it exists, update the values
			UPDATE dbo.Customer SET Name = @Name, LastActive = @LastActive, WebCustomer = @WebCustomer, PreviouslyOrdered = @PreviouslyOrdered WHERE Id = @Id
			--Delete the colours that are no longer favourites
			DELETE CustomerColour FROM CustomerColour cc
				LEFT JOIN dbo.Colour c ON c.Id = cc.ColourId
				WHERE cc.CustomerId = @Id 
				AND c.Colour NOT IN 
					(SELECT tCol.Colour FROM @Colours tCol)
			--Insert the new favourites
			INSERT INTO dbo.CustomerColour SELECT @Id, Colour FROM @Colours WHERE Colour NOT IN (SELECT Colour FROM dbo.CustomerColour)
		END
	ELSE
		BEGIN
			--If the customer does not exist, add the customer
			INSERT INTO dbo.Customer VALUES(@Name, @LastActive, @WebCustomer, @PreviouslyOrdered)
			--Link the favourite colours too
			INSERT INTO dbo.CustomerColour SELECT SCOPE_IDENTITY() AS 'Id', c.Id FROM @Colours cols
				INNER JOIN dbo.Colour c ON c.Colour = cols.Colour
		END

	COMMIT TRANSACTION update_customer
RETURN