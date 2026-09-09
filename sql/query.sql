-- Returns unique customer names and countries for orders shipped by United Package.
SELECT DISTINCT
    c.CustomerName,
    c.Country
FROM Customers AS c
INNER JOIN Orders AS o
    ON o.CustomerID = c.CustomerID
INNER JOIN Shippers AS s
    ON s.ShipperID = o.ShipperID
WHERE s.ShipperName = 'United Package';
