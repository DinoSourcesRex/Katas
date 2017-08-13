CREATE PROCEDURE [dbo].[Customer_GetLatest]
AS
	SELECT TOP 20 
		cOuter.Id
		,cOuter.Name
		,cOuter.PreviouslyOrdered
		,cOuter.WebCustomer
		,cOuter.LastActive
		,FavouriteColours = 
		STUFF((
			SELECT ',' + colours.Colour
			FROM Customer c
			INNER JOIN CustomerColour cc ON cc.CustomerId = c.Id
			INNER JOIN Colour colours ON colours.Id = cc.ColourId
			WHERE c.Id = cOuter.Id
			FOR XML PATH ('')
		), 1, 1, '')
	FROM Customer cOuter
	ORDER BY cOuter.LastActive DESC

RETURN