namespace UnitTesting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Balance = c.Double(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Deposits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: false)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amout = c.Double(nullable: false),
                        FromId = c.Int(nullable: false),
                        ToId = c.Int(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.FromId, cascadeDelete: false)
                .ForeignKey("dbo.Accounts", t => t.ToId, cascadeDelete: false)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.FromId)
                .Index(t => t.ToId)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Withdraws",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Withdraws", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "ToId", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "FromId", "dbo.Accounts");
            DropForeignKey("dbo.Deposits", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "ClientId", "dbo.Clients");
            DropIndex("dbo.Withdraws", new[] { "AccountId" });
            DropIndex("dbo.Transactions", new[] { "Account_Id" });
            DropIndex("dbo.Transactions", new[] { "ToId" });
            DropIndex("dbo.Transactions", new[] { "FromId" });
            DropIndex("dbo.Deposits", new[] { "AccountId" });
            DropIndex("dbo.Accounts", new[] { "ClientId" });
            DropTable("dbo.Withdraws");
            DropTable("dbo.Transactions");
            DropTable("dbo.Deposits");
            DropTable("dbo.Clients");
            DropTable("dbo.Accounts");
        }
    }
}
