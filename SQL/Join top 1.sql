-- Join with top 1 row of other table
SELECT   Orders.OrderNumber, LineItems.Quantity, LineItems.Description
FROM     Orders
JOIN     LineItems
ON       LineItems.LineItemGUID =
         (
         SELECT  TOP 1 LineItemGUID 
         FROM    LineItems
         WHERE   OrderID = Orders.OrderID
         )
         
-- SQL Server 2005 and above
SELECT  Orders.OrderNumber, LineItems2.Quantity, LineItems2.Description
FROM    Orders
CROSS APPLY
        (
        SELECT  TOP 1 LineItems.Quantity, LineItems.Description
        FROM    LineItems
        WHERE   LineItems.OrderID = Orders.OrderID
        ) LineItems2
