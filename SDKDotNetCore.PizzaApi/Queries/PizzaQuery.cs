﻿namespace SDKDotNetCore.PizzaApi.Queries
{
    public class PizzaQuery
    {
        public static string PizzaOrderQuery { get; } = 
            @"select po.*, p.Pizza from [dbo].[Tbl_PizzaOrder] po
            inner join [dbo].[Tbl_Pizza] p on p.PizzaId = po.PizzaId
            where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";

        public static string PizzaOrderDetailQuery { get; } =
            @"select pod.*, pe.PizzaExtraName, pe.Price from [dbo].[Tbl_PizzaOrderDetail] pod
            inner join [dbo].[Tbl_PizzaExtra] pe on pe.PizzaExtraId = pod.PizzaExtraId
            where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";
    }
}
