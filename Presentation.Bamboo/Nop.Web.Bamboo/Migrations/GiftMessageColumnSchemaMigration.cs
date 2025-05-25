using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;
using Nop.Core.Domain.Orders;
using Nop.Data.Migrations;

namespace Nop.Web.Migrations
{
    [NopSchemaMigration("2025-05-24 22:40:00", "Order.GiftMessage new column schema")]
    public class GiftMessageColumnSchemaMigration : Migration
    {
        public override void Up()
        {
            // Add the GiftMessage column to the Order table
            Alter.Table(nameof(Order))
                .AddColumn(nameof(Order.GiftMessage)).AsString().Nullable();
        }
        public override void Down()
        {
            // Remove the GiftMessage column from the Order table
            Delete.Column(nameof(Order.GiftMessage)).FromTable(nameof(Order));
        }
    }

    //[NopSchemaMigration("2025-05-24 22:40:00", "ShoppingCart.GiftMessage new column schema")]
    //public class ShoppingCartGiftMessageColumnSchemaMigration : Migration
    //{
    //    public override void Up()
    //    {
    //        // Add the GiftMessage column to the Order table
    //        Alter.Table(nameof(ShoppingCartItem))
    //            .AddColumn(nameof(ShoppingCartItem.GiftMessage)).AsString().Nullable();
    //    }
    //    public override void Down()
    //    {
    //        // Remove the GiftMessage column from the Order table
    //        Delete.Column(nameof(ShoppingCartItem.GiftMessage)).FromTable(nameof(ShoppingCartItem));
    //    }
    //}
}
