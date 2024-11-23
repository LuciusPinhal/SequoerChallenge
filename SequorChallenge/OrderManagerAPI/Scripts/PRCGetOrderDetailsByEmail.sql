-- Procedure para retornar ordens do email fornecido
CREATE PROCEDURE PRCGetOrderDetailsByEmail
    @Email VARCHAR(255) 
AS
BEGIN
    SELECT 
        OS.[Order] AS [O.S], 
        SUM(OS.Quantity) AS [quantity],
        PRT.ProductCode AS [productCode],  
        PRT.ProductDescription AS [productDescription],
        PRT.Image AS [image],
        PRT.CycleTime AS [cycleTime],
        PRD.MaterialCode AS [materialCode],  
        M.MaterialDescription AS [materialDescription]  
    FROM 
        [Order] OS
    INNER JOIN 
        Product PRT ON PRT.ProductCode = OS.ProductCode
    INNER JOIN
        ProductMaterial PM ON PM.ProductCode = PRT.ProductCode  
    INNER JOIN 
        Material M ON M.MaterialCode = PM.MaterialCode 
    INNER JOIN
        Production PRD ON PRD.[Order] = OS.[Order]
    WHERE
        PRD.Email = @Email
    GROUP BY 
        OS.[Order], PRT.ProductCode, PRT.ProductDescription, PRT.Image, PRT.CycleTime, PRD.MaterialCode, M.MaterialDescription;
END;
